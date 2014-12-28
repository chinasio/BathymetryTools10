using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Catalog;
using System.Windows.Forms;

namespace BathymetryTools10
{
    class clsInterpFunctions
    {
       
        IStepProgressor pStepProgressor;

   

        public void CalculatePoints(string sLineLayer,  string sShorelineLayer, string sDepthLayer, double dblInterval, string ShoreDepthField, string DepthPointDepthField)
        {


            try
            {
                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;


                IFeatureLayer pDepthSoundings = FindLayer(pmap, sDepthLayer) as IFeatureLayer;
                IFeatureLayer pShorelineLayer = FindLayer(pmap, sShorelineLayer) as IFeatureLayer;
                IFeatureLayer pLineLayer = FindLayer(pmap, sLineLayer) as IFeatureLayer;
                //IFeatureLayer pOutpoints = FindLayer(pmap, sOutpoints) as IFeatureLayer;
                IFeatureLayer pConnectingLines = FindLayer(pmap, "ConnectingLines") as IFeatureLayer;
                IFeatureLayer pShorePoints = FindLayer(pmap, "ShorePoints") as IFeatureLayer;
                IFeatureLayer pOutpoints = new FeatureLayerClass();
                pOutpoints.FeatureClass = MakePointFC();
                pOutpoints.Name = "Output Points";
                ArcMap.Document.ActiveView.FocusMap.AddLayer(pOutpoints);


                //AddField(pOutpoints, "Distance");
                //AddField(pOutpoints, "Elevation");

                IProgressDialog2 pProgressDialog = ShowProgressIndicator("Calculating...", pLineLayer.FeatureClass.FeatureCount(null), 1);
                pProgressDialog.ShowDialog();


                //Set up the Outpoints cursor
                IFeatureCursor pFCurOutPoints = pOutpoints.Search(null, false);
                pFCurOutPoints = pOutpoints.FeatureClass.Insert(true);

                //Set up the LineLayer Cursor
                IFeatureCursor pFCur = pLineLayer.Search(null, false);
                IFeature pLineFeature = pFCur.NextFeature();

                IFeatureBuffer pFBuffer = pOutpoints.FeatureClass.CreateFeatureBuffer();

                ICurve pCurve;
                IPoint ppoint;

                int iLineProgressCount = 1;
                double dblDistance = 0;
                double dblTotalDistanceCalculated = 0;
                int iNumIntervals = 0;
                double dblElevationDiff = 0;
                double dblElevationInterval = 0;
                double dblElevation = 0;
                double dlbStartElevation = 0;
                double dblEndElevation = 0;

                pProgressDialog.Description = "Generating points for Line: " + iLineProgressCount + " of " + pLineLayer.FeatureClass.FeatureCount(null).ToString();
                while (pLineFeature != null)
                {

                    pCurve = pLineFeature.Shape as ICurve;
                    pCurve.Project(pmap.SpatialReference);

                    //Get the Starting Elevation from the closest depth point
                    dlbStartElevation = GetStartElevation(pDepthSoundings, pLineLayer, pLineFeature, DepthPointDepthField);
                    //The Elevation for the first run IS the start elevation
                    dblElevation = dlbStartElevation;

                    //Get the ending elevation from the shoreline
                    dblEndElevation = GetFinalElevation(pShorelineLayer, pLineLayer, pLineFeature, ShoreDepthField);

                    //number of line segments based on the user's interval
                    iNumIntervals = Convert.ToInt32(pCurve.Length / dblInterval);
                    dblElevationDiff = Math.Abs(dblEndElevation - dlbStartElevation);
                    //The calculated elevation interval to step up each time
                    dblElevationInterval = dblElevationDiff / iNumIntervals;


                    ppoint = new PointClass();
                    //loop until the distance calculated hits the line length
                    while (dblTotalDistanceCalculated <= pCurve.Length)
                    {

                        pFBuffer.set_Value(pFBuffer.Fields.FindField("LineOID"), pLineFeature.OID);
                        pFBuffer.set_Value(pFBuffer.Fields.FindField("Distance"), dblDistance);
                        pFBuffer.set_Value(pFBuffer.Fields.FindField("Elevation"), dblElevation);
 

                        //this code set the point on the line at a distance
                        pCurve.QueryPoint(0, dblDistance, false, ppoint);

                        pFBuffer.set_Value(pFBuffer.Fields.FindField("X"), ppoint.X);
                        pFBuffer.set_Value(pFBuffer.Fields.FindField("Y"), ppoint.Y);

                        //reCalc the new distance and elevation values for the next iteration
                        dblDistance = dblDistance + dblInterval;
                        dblElevation = dblElevation + dblElevationInterval;
                        dblTotalDistanceCalculated = dblTotalDistanceCalculated + dblInterval;

                        //Insert the feature into the featureclass
                        pFBuffer.Shape = ppoint;
                        pFCurOutPoints.InsertFeature(pFBuffer);

                    }

                    //Reset the distance values back to 0 for the next feature
                    dblDistance = 0;
                    dblTotalDistanceCalculated = 0;
                    pLineFeature = pFCur.NextFeature();
                    pStepProgressor.Step();
                    pProgressDialog.Description = "Generating points for Line: " + iLineProgressCount + " of " + pLineLayer.FeatureClass.FeatureCount(null).ToString();
                    iLineProgressCount++;
                }
                //cleanup
                pFCurOutPoints.Flush();
                pFCur.Flush();
                pProgressDialog.HideDialog();
                pmxdoc.ActiveView.Refresh();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }




        }


        private IFeatureClass MakePointFC()
        {

            try
            {

                IGxCatalogDefaultDatabase Defaultgdb = ArcMap.Application as IGxCatalogDefaultDatabase;
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IWorkspace pWorkspace = workspaceFactory.OpenFromFile(Defaultgdb.DefaultDatabaseName.PathName, 0);




                IFeatureWorkspace workspace = pWorkspace as IFeatureWorkspace;
                UID CLSID = new UID();
                CLSID.Value = "esriGeodatabase.Feature";

                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
                pFieldsEdit.FieldCount_2 = 6;


                IGeoDataset geoDataset = ArcMap.Document.ActiveView.FocusMap.get_Layer(0) as IGeoDataset;


                IGeometryDef pGeomDef = new GeometryDef();
                IGeometryDefEdit pGeomDefEdit = pGeomDef as IGeometryDefEdit;
                pGeomDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                pGeomDefEdit.SpatialReference_2 = geoDataset.SpatialReference;



                IField pField;
                IFieldEdit pFieldEdit;

                //pField = new FieldClass();
                //pFieldEdit = pField as IFieldEdit;
                //pFieldEdit.AliasName_2 = "ObjectID";
                //pFieldEdit.Name_2 = "ObjectID";
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                //pFieldsEdit.set_Field(0, pFieldEdit);

                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "SHAPE";
                pFieldEdit.Name_2 = "SHAPE";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                pFieldEdit.GeometryDef_2 = pGeomDef;
                pFieldsEdit.set_Field(0, pFieldEdit);


                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "LineOID";
                pFieldEdit.Name_2 = "LineOID";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldsEdit.set_Field(1, pFieldEdit);

                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "Distance";
                pFieldEdit.Name_2 = "Distance";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.set_Field(2, pFieldEdit);

                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "Elevation";
                pFieldEdit.Name_2 = "Elevation";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.set_Field(3, pFieldEdit);

                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "X";
                pFieldEdit.Name_2 = "X";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.set_Field(4, pFieldEdit);

                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "Y";
                pFieldEdit.Name_2 = "Y";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.set_Field(5, pFieldEdit);



                string strFCName = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName());
                char[] chars = strFCName.ToCharArray();
                if (Char.IsDigit(chars[0]))
                {
                    strFCName = strFCName.Remove(0, 1);
                }
                KillExistingFeatureclass(strFCName);


                IFeatureClass pFeatureClass = workspace.CreateFeatureClass(strFCName, pFieldsEdit, CLSID, null, esriFeatureType.esriFTSimple, "SHAPE", "");
                return pFeatureClass;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                return null;
            }

        }

        private void KillExistingFeatureclass(string strFilename)
        {
            try
            {

                IGxCatalogDefaultDatabase Defaultgdb = ArcMap.Application as IGxCatalogDefaultDatabase;
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                IWorkspace pWorkspace = workspaceFactory.OpenFromFile(Defaultgdb.DefaultDatabaseName.PathName, 0);

                IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(strFilename);
                IDataset pDataset = pFeatureLayer.FeatureClass as IDataset;
                if (pDataset.CanDelete())
                {
                    pDataset.Delete();
                }
            }

            catch { }
        }

        private double GetFinalElevation(IFeatureLayer pShoreLineLayer, IFeatureLayer pLineLayer, IFeature pLineFeature, string ShoreDepthField)
        {

            IEnvelope pCombinedEnvelope = CombineExtents(pShoreLineLayer.FeatureClass, pLineLayer.FeatureClass);


            IFeatureCursor pDepthCursor = pShoreLineLayer.Search(null, false);
            IFeatureIndex2 pFtrInd = new FeatureIndexClass();
            pFtrInd.FeatureClass = pShoreLineLayer.FeatureClass;
            pFtrInd.FeatureCursor = pDepthCursor;
            pFtrInd.Index(null, pCombinedEnvelope);
            IIndexQuery2 pIndQry = pFtrInd as IIndexQuery2;

            int FtdID = 0;
            double dDist2Ftr = 0;
            pIndQry.NearestFeature(pLineFeature.Shape, out FtdID, out dDist2Ftr);

            IFeature pCloseFeature = pShoreLineLayer.FeatureClass.GetFeature(FtdID);
            return Convert.ToDouble(pCloseFeature.get_Value(pCloseFeature.Fields.FindField(ShoreDepthField)));

        }







        private double GetStartElevation(IFeatureLayer pDepthSoundings, IFeatureLayer pLineLayer, IFeature pLineFeature, string DepthPointDepthField)
        {

            IEnvelope pCombinedEnvelope = CombineExtents(pDepthSoundings.FeatureClass, pLineLayer.FeatureClass);


            IFeatureCursor pDepthCursor = pDepthSoundings.Search(null, false);
            IFeatureIndex2 pFtrInd = new FeatureIndexClass();
            pFtrInd.FeatureClass = pDepthSoundings.FeatureClass;
            pFtrInd.FeatureCursor = pDepthCursor;
            pFtrInd.Index(null, pCombinedEnvelope);
            IIndexQuery2 pIndQry = pFtrInd as IIndexQuery2;

            int FtdID = 0;
            double dDist2Ftr = 0;
            pIndQry.NearestFeature(pLineFeature.Shape, out FtdID, out dDist2Ftr);

            IFeature pCloseFeature = pDepthSoundings.FeatureClass.GetFeature(FtdID);
            return Convert.ToDouble(pCloseFeature.get_Value(pCloseFeature.Fields.FindField(DepthPointDepthField)));

        }




        private IEnvelope CombineExtents(IFeatureClass pfclass1, IFeatureClass pfeatclass2)
        {

            try
            {

                IEnvelope pEnv = new EnvelopeClass();
                IGeoDataset pGeoDS = pfclass1 as IGeoDataset;
                pGeoDS.Extent.QueryEnvelope(pEnv);

                pGeoDS = pfeatclass2 as IGeoDataset;
                pEnv.Union(pGeoDS.Extent);

                return pEnv;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Be sure that both layers have matching Spatial References.");
                IEnvelope pEnv = new EnvelopeClass();
                pEnv.SetEmpty();
                return pEnv;
            }

        }

        private IProgressDialog2 ShowProgressIndicator(string strTitle, int iMax, int iStepValue)
        {

            IProgressDialogFactory pProgressDlgFact;
            IProgressDialog2 pProgressDialog;

            ITrackCancel pTrackCancel;


            //'Show a progress dialog while we cycle through the features 
            pTrackCancel = new CancelTrackerClass();
            pProgressDlgFact = new ProgressDialogFactoryClass();
            pProgressDialog = (IProgressDialog2)pProgressDlgFact.Create(pTrackCancel, 0);
            pProgressDialog.CancelEnabled = false;
            pProgressDialog.Title = strTitle;
            pProgressDialog.Animation = esriProgressAnimationTypes.esriProgressGlobe;


            //'Set the properties of the Step Progressor 
            pStepProgressor = (IStepProgressor)pProgressDialog;
            pStepProgressor.MinRange = 0;
            pStepProgressor.MaxRange = iMax;
            pStepProgressor.StepValue = iStepValue;

            return pProgressDialog;
        }


        //private void AddField(IFeatureLayer pFeaturelayer, string strFieldName)
        //{
        //    IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
        //    IMap pmap = pmxdoc.FocusMap;

        //    IFeatureClass pFeatureClass = pFeaturelayer.FeatureClass;

        //    IFieldEdit pNewField = new FieldClass();
        //    pNewField.Name_2 = strFieldName;

        //    if (pFeatureClass.FindField(strFieldName) != -1)
        //    {
        //        //MessageBox.Show("Field: " + strFieldName + " already exists.");
        //        return;
        //    }

        //    pNewField.Type_2 = esriFieldType.esriFieldTypeDouble;
        //    pNewField.Length_2 = 50;
        //    pNewField.Editable_2 = true;
        //    pNewField.IsNullable_2 = true;
        //    pNewField.DefaultValue_2 = null;
        //    pFeatureClass.AddField(pNewField);
        //    pNewField = null;
        //    GC.Collect();

        //}


        public ILayer FindLayer(IMap pmap, string layer)
        {
            for (int i = 0; i <= pmap.LayerCount - 1; i++)
            {
                if (pmap.get_Layer(i).Name == layer)
                    return pmap.get_Layer(i);
            }
            return null;


        }

    }
}
