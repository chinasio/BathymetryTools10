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
using ESRI.ArcGIS.Geoprocessing;
using System.Windows.Forms;


namespace BathymetryTools10
{
    class clsDeepInterpolation
    {
        
        IStepProgressor pStepProgressor;


        public void GeneratePoints2(string sLineLayer, string sDepthPointsLayer, double dblInterval, string strDepthField)
        {

            try
            {
                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;

                IFeatureLayer pDepthSoundings = FindLayer(pmap, sDepthPointsLayer) as IFeatureLayer;
                IFeatureLayer pLineLayer = FindLayer(pmap, sLineLayer) as IFeatureLayer;
                IFeatureLayer pOutpoints = new FeatureLayerClass();
                pOutpoints.FeatureClass = MakePointFC();
                pOutpoints.Name = "Output Points";
                ArcMap.Document.ActiveView.FocusMap.AddLayer(pOutpoints);

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
                double iNumIntervals = 0;
                double dblElevationDiff = 0;
                double dblElevationInterval = 0;
                double dblElevation = 0;
                double dlbStartElevation = 0;
                double dblEndElevation = 0;

                pProgressDialog.Description = "Generating points for Line: " + iLineProgressCount + " of " + pLineLayer.FeatureClass.FeatureCount(null).ToString();

                while (pLineFeature != null)
                {




                    int iStartVertex = 0;
                    int iEndVertex = 1;
                    //Get the vertices of the line feature
                    IPointCollection4 pPointColl = pLineFeature.Shape as IPointCollection4;


                    //loop thru the vertices
                    for (int i = 0; i <= pPointColl.PointCount - 1; i++)
                    {

                        IPoint pStartPoint = pPointColl.get_Point(iStartVertex);
                        IPoint pEndPoint = pPointColl.get_Point(iEndVertex);
                        //Make an intermediate line segment between each set of vertices
                        IPath pPath = MakePath(pStartPoint, pEndPoint);

                        //Get the starting elevation from the depth point layer
                        dlbStartElevation = GetPointElevation(pDepthSoundings, pLineLayer, pStartPoint, strDepthField);
                        dblElevation = dlbStartElevation;
                        IPointCollection pPointForPath = null;
                        //Get the ending elevation from the next vertex
                        pEndPoint = pPointColl.get_Point(iEndVertex);
                        dblEndElevation = GetPointElevation(pDepthSoundings, pLineLayer, pEndPoint, strDepthField);
                        //If the returned elevation is 0, then there is no coincident depth point, move to the next vertex for your endpoint
                        if (dblEndElevation == 0)
                        {
                            //IPointCollection reshapePath = new PathClass();
                            object missing = Type.Missing;
                            pPointForPath = new PathClass();
                            pPointForPath.AddPoint(pStartPoint, ref missing, ref missing);
                            while (dblEndElevation == 0)
                            {
                                pEndPoint = pPointColl.get_Point(iEndVertex);
                                dblEndElevation = GetPointElevation(pDepthSoundings, pLineLayer, pEndPoint, strDepthField);


                                pPointForPath.AddPoint(pEndPoint, ref missing, ref missing);
                                //pLineSegment.Reshape(reshapePath as IPath);

                                if (dblEndElevation != 0)
                                {
                                    break;
                                }
                                iEndVertex++;
                            }

                        }

                        if (pPointForPath != null)
                        {
                            pPath = pPointForPath as IPath;
                        }




                        //number of line segments based on the user's interval
                        iNumIntervals = Convert.ToDouble(pPath.Length / dblInterval);
                        dblElevationDiff = dblEndElevation - dlbStartElevation;

                        //The calculated elevation interval to step up each time
                        dblElevationInterval = dblElevationDiff / iNumIntervals;


                        ppoint = new PointClass();

                        while (dblTotalDistanceCalculated <= pPath.Length)
                        {


                            pFBuffer.set_Value(pFBuffer.Fields.FindField("LineOID"), pLineFeature.OID);
                            pFBuffer.set_Value(pFBuffer.Fields.FindField("Distance"), Math.Round(dblDistance, 4));
                            pFBuffer.set_Value(pFBuffer.Fields.FindField("Elevation"), Math.Round(dblElevation, 4));
                 

                            //this code set the point on the line at a distance
                            pPath.QueryPoint(0, dblDistance, false, ppoint);

                            

                            pFBuffer.set_Value(pFBuffer.Fields.FindField("X"), ppoint.X);
                            pFBuffer.set_Value(pFBuffer.Fields.FindField("Y"), ppoint.Y);

                            //reCalc the new distance and elevation values for the next iteration
                            dblDistance = dblDistance + dblInterval;
                            dblTotalDistanceCalculated = dblTotalDistanceCalculated + dblInterval;

                            //Insert the feature into the featureclass
                            pFBuffer.Shape = ppoint;

                            if (!IsPointCoincident(ppoint, pOutpoints))
                            {
                                pFCurOutPoints.InsertFeature(pFBuffer);
                            }
                          

                            if (dblTotalDistanceCalculated >= pPath.Length)
                            {

                                break;
                            }

                            dblElevation = dblElevation + dblElevationInterval;

                           




                        }



                        //start the next line segment at the end of last one
                        iStartVertex = iEndVertex;
                        //look to the next vertex for the new ending point
                        iEndVertex++;

                 
                            if (iEndVertex == pPointColl.PointCount)
                            {

                                ////if its the last vertex of the last line, add a point
                                //pFBuffer.Shape = pPath.ToPoint;
                                //pFBuffer.set_Value(pFBuffer.Fields.FindField("LineOID"), pLineFeature.OID);
                                //pFBuffer.set_Value(pFBuffer.Fields.FindField("Distance"), Math.Round(dblDistance, 4));
                                //pFBuffer.set_Value(pFBuffer.Fields.FindField("Elevation"), Math.Round(dblElevation, 4));
                                //pFBuffer.set_Value(pFBuffer.Fields.FindField("X"), pPath.ToPoint.X);
                                //pFBuffer.set_Value(pFBuffer.Fields.FindField("Y"), pPath.ToPoint.Y);
                                //pFCurOutPoints.InsertFeature(pFBuffer);

                                //Reset the distance values back to 0 for the next feature
                                dblDistance = 0;
                                dblTotalDistanceCalculated = 0;
                                pStepProgressor.Step();
                                iLineProgressCount++;
                                pPath.SetEmpty();
                                break;
                            }
               

                        //Reset the distance values back to 0 for the next feature
                        dblDistance = 0;
                        dblTotalDistanceCalculated = 0;
                        //pLineFeature = pFCur.NextFeature();
                        pStepProgressor.Step();
                        //pProgressDialog.Description = "Generating points for Line: " + iLineProgressCount + " of " + pLineLayer.FeatureClass.FeatureCount(null).ToString();
                        iLineProgressCount++;
                        pPath.SetEmpty();

                    }
                    pLineFeature = pFCur.NextFeature();

                }



                //cleanup
                pFCurOutPoints.Flush();
                pFCur.Flush();
                pProgressDialog.HideDialog();
                pmxdoc.ActiveView.Refresh();



            }
            catch (Exception ex)
            {

            }



        }




        //public void GeneratePoints2(string sLineLayer, string sDepthPointsLayer,  double dblInterval, string strDepthField)
        //{

        //    try{


                  

        //            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
        //            IMap pmap = pmxdoc.FocusMap;

        //            IFeatureLayer pDepthSoundings = FindLayer(pmap, sDepthPointsLayer) as IFeatureLayer;
        //            IFeatureLayer pLineLayer = FindLayer(pmap, sLineLayer) as IFeatureLayer;
        //            IFeatureLayer pOutpoints = new FeatureLayerClass();
        //            pOutpoints.FeatureClass = MakePointFC();
        //            pOutpoints.Name = "Output Points";
        //            ArcMap.Document.ActiveView.FocusMap.AddLayer(pOutpoints);

        //            //IFeatureLayer pOutpoints = FindLayer(pmap, sOutpoints) as IFeatureLayer;
        //            //AddField(pOutpoints, "Distance");
        //            //AddField(pOutpoints, "Elevation");

        //            //get the Workspace from the IDataset interface on the feature class
        //            IDataset dataset = (IDataset)pLineLayer.FeatureClass;
        //            IWorkspace workspace = dataset.Workspace;
        //            //Cast for an IWorkspaceEdit
        //            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;

        //            //Start an edit session and operation
        //            workspaceEdit.StartEditing(true);
        //            workspaceEdit.StartEditOperation();

        //            IProgressDialog2 pProgressDialog = ShowProgressIndicator("Calculating...", pLineLayer.FeatureClass.FeatureCount(null), 1);
        //            pProgressDialog.ShowDialog();


        //            //Set up the Outpoints cursor
        //            IFeatureCursor pFCurOutPoints = pOutpoints.Search(null, false);
        //            pFCurOutPoints = pOutpoints.FeatureClass.Insert(true);

        //            //Set up the LineLayer Cursor
        //            IFeatureCursor pFCur = pLineLayer.Search(null, false);
        //            IFeature pLineFeature = pFCur.NextFeature();

        //            IFeatureBuffer pFBuffer = pOutpoints.FeatureClass.CreateFeatureBuffer();



        //            ICurve pCurve;

        //            double dlbStartElevation;
        //            double dblPointElevation;
        //            double dlbProcessedLength = 0;
        //            double dblFCTotalLength = 0;
        //            int p = 0;

        //            while (pLineFeature != null)
        //            {

        //                pProgressDialog.Description = "Calculating line segment " + p.ToString() + " of: " + pLineLayer.FeatureClass.FeatureCount(null).ToString();

        //                //create startpoint
        //                pCurve = pLineFeature.Shape as ICurve;
        //                pFBuffer.Shape = pCurve.FromPoint;
        //                dlbStartElevation = GetPointElevation(pDepthSoundings, pLineLayer, pCurve.FromPoint, strDepthField);

        //                pFBuffer.set_Value(pFBuffer.Fields.FindField("LineOID"), pLineFeature.OID);
        //                pFBuffer.set_Value(pFBuffer.Fields.FindField("Distance"), 0);
        //                pFBuffer.set_Value(pFBuffer.Fields.FindField("Elevation"), Math.Round(dlbStartElevation, 4));
        //                pFCurOutPoints.InsertFeature(pFBuffer);

        //                double dblTotalDistance = pCurve.Length;
        //                dlbProcessedLength = dblInterval;
             

        //                IConstructPoint contructionPoint;
        //                IPoint ppoint;
        //                while (dlbProcessedLength <= dblTotalDistance)
        //                {

                          
                         

        //                    contructionPoint = new PointClass();
        //                    contructionPoint.ConstructAlong(pCurve, esriSegmentExtension.esriNoExtension, dlbProcessedLength, false);
        //                    ppoint = new PointClass();
        //                    ppoint = contructionPoint as IPoint;

        //                    pFBuffer.Shape = ppoint;

        //                    dblPointElevation = GetPointElevation(pDepthSoundings, pLineLayer, ppoint, strDepthField);

        //                    //dblFCTotalLength += dblInterval;
        //                    pFBuffer.set_Value(pFBuffer.Fields.FindField("LineOID"), pLineFeature.OID);
        //                    pFBuffer.set_Value(pFBuffer.Fields.FindField("Distance"), Math.Round(dlbProcessedLength, 4));
        //                    pFBuffer.set_Value(pFBuffer.Fields.FindField("Elevation"), Math.Round(dblPointElevation, 4));

        //                    pFCurOutPoints.InsertFeature(pFBuffer);
        //                    dlbProcessedLength += dblInterval;

        //                    pStepProgressor.Step();
                          
        //                }

        //                dblFCTotalLength += dblInterval;
        //                p++;
        //                pLineFeature = pFCur.NextFeature();

        //            }

        //            //cleanup
        //            pFCurOutPoints.Flush();
        //            pFCur.Flush();

        //            //Stop editing
        //            workspaceEdit.StopEditOperation();
        //            workspaceEdit.StopEditing(true);


        //            pProgressDialog.HideDialog();
        //            pmxdoc.ActiveView.Refresh();

        //       }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        MessageBox.Show(ex.StackTrace);
        //    }

         
        //}

     
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


        private IFeatureLayer DissolveLineLayer(IFeatureLayer InFeatureLayer)
        {
            try{
            ITable dissolveTable = (ITable)InFeatureLayer;
            IDataset dataset = (IDataset)InFeatureLayer;

            ESRI.ArcGIS.Geoprocessor.Geoprocessor geoprocessor = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
                            ESRI.ArcGIS.DataManagementTools.Dissolve d = new ESRI.ArcGIS.DataManagementTools.Dissolve();
            d.in_features = dissolveTable;
            d.out_feature_class = System.IO.Path.Combine(dataset.Workspace.PathName, "doodie");
            d.dissolve_field = "SHAPE";
            //d.statistics_fields = "";
            d.multi_part = "MULTI_PART";
            geoprocessor.Execute(d, null);

            return d.out_feature_class as IFeatureLayer ;


            //ESRI.ArcGIS.Geoprocessor.Geoprocessor geoprocessor = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            //ESRI.ArcGIS.DataManagementTools.Dissolve d = new ESRI.ArcGIS.DataManagementTools.Dissolve();
            //ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult Result;
            //IFeatureLayer pOutFeatureLayer = new FeatureLayer();



            //try
            //{

            //    geoprocessor.AddOutputsToMap = false;
            //    geoprocessor.OverwriteOutput = true;

            //    d.in_features = InFeatureLayer.Name ;
            //    d.dissolve_field = InFeatureLayer.FeatureClass.ShapeFieldName;
            //    d.out_feature_class = "Dissolve";

            //    Result = geoprocessor.Execute(d, null) as IGeoProcessorResult;

            //    pOutFeatureLayer.FeatureClass = geoprocessor.Open("Dissolve") as IFeatureClass;
            //    return pOutFeatureLayer;




            //IFeatureLayer OutFeatureLayer = new FeatureLayer();
            //IGxCatalogDefaultDatabase Defaultgdb = ArcMap.Application as IGxCatalogDefaultDatabase;


            //string g = Defaultgdb.DefaultDatabaseName.PathName;

            //ITable dissolveTable = (ITable)InFeatureLayer;
            //IDataset dataset = (IDataset)InFeatureLayer;

            //ESRI.ArcGIS.Geoprocessor.Geoprocessor geoprocessor = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            //ESRI.ArcGIS.DataManagementTools.Dissolve d = new ESRI.ArcGIS.DataManagementTools.Dissolve();
            //d.in_features = dissolveTable;
            //d.out_feature_class = Defaultgdb.DefaultDatabaseName.PathName + "\\diss";
            //d.dissolve_field = InFeatureLayer.FeatureClass.ShapeFieldName;
            ////d.statistics_fields = "";
            ////d.multi_part = "MULTI_PART";
            //geoprocessor.Execute(d, null);

            //OutFeatureLayer.FeatureClass = d.out_feature_class as IFeatureClass;
            //return OutFeatureLayer;
            ////return d.out_feature_class as IFeatureClass;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                return null;
            }

            
        }

        private IPath MakePath( IPoint ppointStart, IPoint ppointEnd)
        {
            IPath ppath = new PathClass();

            ppath.FromPoint = ppointStart;
            ppath.ToPoint = ppointEnd;


            return ppath;
        }


        private double GetPointElevation(IFeatureLayer pDepthSoundings, IFeatureLayer pLineLayer, IPoint ppoint, string strDepthField)
        {

            IEnvelope pCombinedEnvelope = CombineExtents(pDepthSoundings.FeatureClass, pLineLayer.FeatureClass);
            
                //if (IsPointCoincident(ppoint ,pDepthSoundings))
                //{


                IFeatureCursor pDepthCursor = pDepthSoundings.Search(null, false);
                IFeatureIndex2 pFtrInd = new FeatureIndexClass();
                pFtrInd.FeatureClass = pDepthSoundings.FeatureClass;
                pFtrInd.FeatureCursor = pDepthCursor;
                pFtrInd.Index(null, pCombinedEnvelope);
                IIndexQuery2 pIndQry = pFtrInd as IIndexQuery2;

                int FtdID = 0;
                double dDist2Ftr = 0;
                pIndQry.NearestFeature(ppoint, out FtdID, out dDist2Ftr);

                IFeature pCloseFeature = pDepthSoundings.FeatureClass.GetFeature(FtdID);
                return Convert.ToDouble(pCloseFeature.get_Value(pCloseFeature.Fields.FindField(strDepthField)));
                //}
                //else{

                //    return 0;
                //}
        


  
        }


       


        public bool IsPointCoincident(IPoint ppoint, IFeatureLayer pDepthPointsLayer)
        {
            IFeatureIndex2 pFI2 = new FeatureIndexClass();
            pFI2.FeatureClass = pDepthPointsLayer.FeatureClass;
            IGeoDataset pGDS = pDepthPointsLayer.FeatureClass as IGeoDataset;
            pFI2.Index(null, pGDS.Extent);

            IIndexQuery2 pIQ2 = pFI2 as IIndexQuery2;
             object pSAIds;

             pIQ2.IntersectedFeatures(ppoint, out pSAIds);

             if (pSAIds != null)
             {
                 return true;
             }
             return false;

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
