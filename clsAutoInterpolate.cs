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
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Catalog;
using System.Windows.Forms;

namespace BathymetryTools10
{
    class clsAutoInterpolate
    {

      
        IStepProgressor pStepProgressor;

        public void Calculate(string sShorelineLayer, string sDepthPointsLayer,  double dblInterval, string strShoreDepthField, string strDepthPointDepthField)
        {

            try
            {
                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;

                IFeatureLayer pDepthSoundings = FindLayer(pmap, sDepthPointsLayer) as IFeatureLayer;
                IFeatureLayer pShorelineLayer = FindLayer(pmap, sShorelineLayer) as IFeatureLayer;

                //IFeatureLayer pOutpoints = FindLayer(pmap, sOutpoints) as IFeatureLayer;
                IFeatureLayer pOutpoints = new FeatureLayerClass();
                pOutpoints.FeatureClass = MakePointFC();
                pOutpoints.Name = "Output Points";
                ArcMap.Document.ActiveView.FocusMap.AddLayer(pOutpoints);

                IFeatureLayer pConnectingLines = FindLayer(pmap, "ConnectingLines") as IFeatureLayer;
                IFeatureLayer pShorePoints = FindLayer(pmap, "ShorePoints") as IFeatureLayer;



                ////Add fields if necessary
                //AddField(pOutpoints, "Distance");
                //AddField(pOutpoints, "Elevation");


                //Set up the Outpoints cursor
                IFeatureCursor pFCurOutPoints = pOutpoints.Search(null, false);
                pFCurOutPoints = pOutpoints.FeatureClass.Insert(true);
                IFeatureBuffer pFOutPointsBuffer = pOutpoints.FeatureClass.CreateFeatureBuffer();


                //Set up the LineLayer Cursor (now using selected lines)
                IFeatureSelection pShoreLineSelection = pShorelineLayer as IFeatureSelection;

                if (pShoreLineSelection.SelectionSet.Count  == 0)
                {
                    MessageBox.Show("You must have at least one shoreline feature selected");
                    return;
                }
                ICursor pShorelineCursor;
                IFeatureCursor pSelShoreFeatCur;
                pShoreLineSelection.SelectionSet.Search(null, false, out pShorelineCursor);
                pSelShoreFeatCur = pShorelineCursor as IFeatureCursor;
                IFeature pShoreLineFeature = pSelShoreFeatCur.NextFeature();
                //IFeatureCursor pFCur = pShorelineLayer.Search(null, false);
                //IFeature pShoreLineFeature = pFCur.NextFeature();

                //Set up the pConnectingLines cursor
                IFeatureCursor pFCurOutLines = pConnectingLines.Search(null, false);
                pFCurOutLines = pConnectingLines.FeatureClass.Insert(true);
                IFeatureBuffer pFBuffer = pConnectingLines.FeatureClass.CreateFeatureBuffer();

                //Set up the pShorePoints cursor
                IFeatureCursor pShorePointsCursor = pShorePoints.Search(null, false);
                IFeature pShorePointFeature = pShorePointsCursor.NextFeature();



                IFeature pNewLineFeature = null;
                IPointCollection pNewLinePointColl = null;


                double dblDistance = 0;
                double dblTotalDistanceCalculated = 0;
                int iNumIntervals = 0;
                double dblElevationDiff = 0;
                double dblElevationInterval = 0;
                double dblElevation = 0;
                double dlbStartElevation = 0;
                double dblEndElevation = 0;

                int iLineProgressCount = 0;
                IProgressDialog2 pProgressDialog = ShowProgressIndicator("Calculating points for shoreline vertex: ", 0, 1);
                pProgressDialog.ShowDialog();



                IEnvelope pCombinedEnvelope = CombineExtents(pDepthSoundings.FeatureClass, pShorelineLayer.FeatureClass);
                while (pShoreLineFeature != null)
                {
                    //IPointCollection4 pPointColl = pShoreLineFeature.Shape as IPointCollection4;

                    IPoint ppoint = new PointClass();



                    //for (int i = 0; i <= pPointColl.PointCount - 1; i++)
                    while (pShorePointFeature != null)
                    {
                        ppoint = pShorePointFeature.ShapeCopy as IPoint;
                        System.Diagnostics.Debug.WriteLine("ppoint: " + ppoint.X.ToString());
                        System.Diagnostics.Debug.WriteLine("shorepoint: " + pShorePointFeature.OID.ToString());

                        ////try walking line at set intervals instead of just at vertices
                        //IPoint pLineVertex = new PointClass();
                        //pLineVertex = pPointColl.get_Point(i);

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

                        IPoint pEndPoint = new PointClass();
                        pEndPoint = pCloseFeature.Shape as IPoint;


                        //Make the line here
                        pNewLineFeature = pConnectingLines.FeatureClass.CreateFeature();
                        pNewLinePointColl = new PolylineClass();

                        object missing = Type.Missing;
                        pNewLinePointColl.AddPoint(pEndPoint, ref missing, ref missing);
                        pNewLinePointColl.AddPoint(ppoint, ref missing, ref missing);
                        pNewLineFeature.Shape = pNewLinePointColl as PolylineClass;

                        //Check to see if new line crosses land, if not, process it.
                        //bool bLineIntersectsShore = FeatureIntersects(pShorelineLayer, pConnectingLines, pNewLineFeature);
                        bool bLineIntersectsShore = false;
                        if (bLineIntersectsShore == false)
                        {
                            pNewLineFeature.Store();



                            ICurve pCurve = pNewLineFeature.Shape as ICurve;
                            pCurve.Project(pmap.SpatialReference);

                            //Get the Starting Elevation from the closest depth point
                            dlbStartElevation = GetStartElevation(pDepthSoundings, pConnectingLines, pNewLineFeature, strDepthPointDepthField);
                            //The Elevation for the first run IS the start elevation
                            dblElevation = dlbStartElevation;
                            //////Get the ending elevation from the shoreline
                            dblEndElevation = GetEndElevation(pShorelineLayer, pConnectingLines, pNewLineFeature, strShoreDepthField);

                            //number of line segments based on the user's interval
                            iNumIntervals = Convert.ToInt32(pCurve.Length / dblInterval);
                            dblElevationDiff = Math.Abs(dblEndElevation - dlbStartElevation);
                            //The calculated elevation interval to step up each time
                            dblElevationInterval = dblElevationDiff / iNumIntervals;


                            ppoint = new PointClass();

                            while (dblTotalDistanceCalculated <= pCurve.Length)
                            {


                                pFOutPointsBuffer.set_Value(pFOutPointsBuffer.Fields.FindField("LineOID"), pNewLineFeature.OID);
                                pFOutPointsBuffer.set_Value(pFOutPointsBuffer.Fields.FindField("Distance"), dblDistance);
                                pFOutPointsBuffer.set_Value(pFOutPointsBuffer.Fields.FindField("Elevation"), dblElevation);
                             

                                //this code set the point on the line at a distance
                                pCurve.QueryPoint(0, dblDistance, false, ppoint);

                                pFOutPointsBuffer.set_Value(pFOutPointsBuffer.Fields.FindField("X"), ppoint.X);
                                pFOutPointsBuffer.set_Value(pFOutPointsBuffer.Fields.FindField("Y"), ppoint.Y);


                                //reCalc the new distance and elevation values for the next iteration
                                dblDistance = dblDistance + dblInterval;

                                //add or subtract elevation depending on whether dblElevationDiff is positve or negative
                                if (dblElevationDiff > 0)
                                {
                                    dblElevation = dblElevation - dblElevationInterval;
                                }
                                else
                                {
                                    dblElevation = dblElevation + dblElevationInterval;
                                }
                                dblTotalDistanceCalculated = dblTotalDistanceCalculated + dblInterval;



                                //Insert the feature into the featureclass
                                pFOutPointsBuffer.Shape = ppoint;
                                pFCurOutPoints.InsertFeature(pFOutPointsBuffer);

                            }



                            //Reset the distance values back to 0 for the next feature
                            dblDistance = 0;
                            dblTotalDistanceCalculated = 0;
                            pFCurOutPoints.Flush();


                            pStepProgressor.Step();
                            pProgressDialog.Description = "Calculating points for shoreline vertex: " + pShorePointFeature.OID.ToString();
                            iLineProgressCount++;
                        }

                        pShorePointFeature = pShorePointsCursor.NextFeature();

                    }

                    pShoreLineFeature = pSelShoreFeatCur.NextFeature();


                }
                //cleanup
                pFCurOutLines.Flush();
                pSelShoreFeatCur.Flush();
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
            string strFCName;
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


                strFCName = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName());
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


        private double GetStartElevation(IFeatureLayer pDepthSoundings, IFeatureLayer pLineLayer, IFeature pLineFeature, string strDepthField)
        {
            try
            {
                IEnvelope pCombinedEnvelope = CombineExtents(pDepthSoundings.FeatureClass, pLineLayer.FeatureClass);

                IPointCollection4 pPointColl = pLineFeature.Shape as IPointCollection4;
                IPoint ppoint = new PointClass();

                //pPointColl.QueryPoint(0, ppoint);

                ppoint = pPointColl.get_Point(0);

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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                return 0;
            }

        }

        private double GetEndElevation(IFeatureLayer pDepthSoundings, IFeatureLayer pLineLayer, IFeature pLineFeature, string strDepthField)
        {
            try
            {
                IEnvelope pCombinedEnvelope = CombineExtents(pDepthSoundings.FeatureClass, pLineLayer.FeatureClass);

                IPointCollection4 pPointColl = pLineFeature.Shape as IPointCollection4;
                IPoint ppoint = new PointClass();


                //Get the last point on the line
                ppoint = pPointColl.get_Point(-1);

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
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                return 0;
            }

        }


        public void PlotShorePoints(string sShorelineLayer, double dblInterval)
        {
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;


            IFeatureLayer pShorelineLayer = FindLayer(pmap, sShorelineLayer) as IFeatureLayer;
            IFeatureLayer pShorePoints = FindLayer(pmap, "ShorePoints") as IFeatureLayer;

           

            //Set up the Outpoints cursor
            IFeatureCursor pFCurOutPoints = pShorePoints.Search(null, false);
            pFCurOutPoints = pShorePoints.FeatureClass.Insert(true);


            ////Set up the LineLayer Cursor
            //IFeatureCursor pFCur = pShorelineLayer.Search(null, false);
            //IFeature pLineFeature = pFCur.NextFeature();
            IFeatureSelection pShoreLineSelection = pShorelineLayer as IFeatureSelection;
            if (pShoreLineSelection.SelectionSet.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("You must first select at least one feature in the shoreline layer to proceed.");
                pmap.DeleteLayer(pShorelineLayer);
                pmap.DeleteLayer(pShorePoints);
                return;
            }
            ICursor pShorelineCursor;
            IFeatureCursor pSelShoreFeatCur;
            pShoreLineSelection.SelectionSet.Search(null, false, out pShorelineCursor);
            pSelShoreFeatCur = pShorelineCursor as IFeatureCursor;
            IFeature pShoreLineFeature = pSelShoreFeatCur.NextFeature();



            IFeatureBuffer pFBuffer = pShorePoints.FeatureClass.CreateFeatureBuffer();
            ICurve pCurve;
            IPoint ppoint;

            double dblTotalDistanceCalculated = 0;
            double dblDistance = 0;

            while (pShoreLineFeature != null)
            {
                pCurve = pShoreLineFeature.Shape as ICurve;
                pCurve.Project(pmap.SpatialReference);

                ppoint = new PointClass();

                while (dblTotalDistanceCalculated <= pCurve.Length)
                {

                    //this code set the point on the line at a distance
                    pCurve.QueryPoint(0, dblDistance, false, ppoint);

                    dblDistance = dblDistance + dblInterval;
                    dblTotalDistanceCalculated = dblTotalDistanceCalculated + dblInterval;

                    //Insert the feature into the featureclass
                    pFBuffer.Shape = ppoint;
                    pFCurOutPoints.InsertFeature(pFBuffer);
                }
                dblDistance = 0;
                dblTotalDistanceCalculated = 0;
                pShoreLineFeature = pSelShoreFeatCur.NextFeature();

            }
            pFCurOutPoints.Flush();
            pSelShoreFeatCur.Flush();
            pmxdoc.ActiveView.Refresh();

        }

        public void MakeScratchLines()
        {

            try
            {
                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;

                ILayer pConnectingLines = FindLayer(pmap, "ConnectingLines");

                if (pConnectingLines != null)
                {
                    pmap.DeleteLayer(pConnectingLines);
                }

                IScratchWorkspaceFactory workspaceFactory = new FileGDBScratchWorkspaceFactoryClass();
                IScratchWorkspaceFactory2 workspaceFactory2 = workspaceFactory as IScratchWorkspaceFactory2;
                IWorkspace scratchWorkspace = workspaceFactory2.CreateNewScratchWorkspace();

                IFeatureWorkspace pFeatWorkspace = scratchWorkspace as IFeatureWorkspace;


                // This function creates a new feature class in a supplied feature dataset by building all of the
                // fields from scratch. IFeatureClassDescription (or IObjectClassDescription if the table is 
                // created at the workspace level) can be used to get the required fields and are used to 
                // get the InstanceClassID and ExtensionClassID.
                // Create new fields collection with the number of fields you plan to add. Must add at least two fields
                // for a feature class: Object ID and Shape field.
                IFields fields = new FieldsClass();
                IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
                fieldsEdit.FieldCount_2 = 3;

                // Create Object ID field.
                IField fieldUserDefined = new Field();

                IFieldEdit fieldEdit = (IFieldEdit)fieldUserDefined;
                fieldEdit.Name_2 = "OBJECTID";
                fieldEdit.AliasName_2 = "OBJECT ID";
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                fieldsEdit.set_Field(0, fieldUserDefined);

                // Create Shape field.
                fieldUserDefined = new Field();
                fieldEdit = (IFieldEdit)fieldUserDefined;

                // Set up geometry definition for the Shape field.
                // You do not have to set the spatial reference, as it is inherited from the feature dataset.
                IGeometryDef geometryDef = new GeometryDefClass();
                IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                geometryDefEdit.GeometryType_2 = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline;
                geometryDefEdit.SpatialReference_2 = pmap.SpatialReference;
                // By setting the grid size to 0, you are allowing ArcGIS to determine the appropriate grid sizes for the feature class. 
                // If in a personal geodatabase, the grid size is 1,000. If in a file or ArcSDE geodatabase, the grid size
                // is based on the initial loading or inserting of features.
                geometryDefEdit.GridCount_2 = 1;
                geometryDefEdit.set_GridSize(0, 0);
                geometryDefEdit.HasM_2 = false;
                geometryDefEdit.HasZ_2 = false;

                // Set standard field properties.
                fieldEdit.Name_2 = "SHAPE";
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                fieldEdit.GeometryDef_2 = geometryDef;
                fieldEdit.IsNullable_2 = true;
                fieldEdit.Required_2 = true;
                fieldsEdit.set_Field(1, fieldUserDefined);

                // Create a field of type double to hold some information for the features.
                fieldUserDefined = new Field();

                fieldEdit = (IFieldEdit)fieldUserDefined;
                fieldEdit.Name_2 = "ID";
                fieldEdit.AliasName_2 = "ID";
                fieldEdit.Editable_2 = true;
                fieldEdit.IsNullable_2 = false;
                fieldEdit.Precision_2 = 2;
                fieldEdit.Scale_2 = 5;
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                fieldsEdit.set_Field(2, fieldUserDefined);

                // Create a feature class description object to use for specifying the CLSID and EXTCLSID.
                IFeatureClassDescription fcDesc = new FeatureClassDescriptionClass();
                IObjectClassDescription ocDesc = (IObjectClassDescription)fcDesc;

                IFeatureClass pLineFClass = pFeatWorkspace.CreateFeatureClass("ConnectingLines", fields, ocDesc.InstanceCLSID, ocDesc.ClassExtensionCLSID, esriFeatureType.esriFTSimple, "SHAPE", "");


                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pLineFClass;

                pFeatureLayer.Name = "ConnectingLines";
                pmxdoc.AddLayer(pFeatureLayer);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }

        }


        public void MakeScratchPoints()
        {

            try
            {
                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;

                ILayer pShorePoints = FindLayer(pmap, "ShorePoints");

                if (pShorePoints != null)
                {
                    pmap.DeleteLayer(pShorePoints);
                }


                IScratchWorkspaceFactory workspaceFactory = new FileGDBScratchWorkspaceFactoryClass();
                IScratchWorkspaceFactory2 workspaceFactory2 = workspaceFactory as IScratchWorkspaceFactory2;
                IWorkspace scratchWorkspace = workspaceFactory2.CreateNewScratchWorkspace();

                IFeatureWorkspace pFeatWorkspace = scratchWorkspace as IFeatureWorkspace;


                // This function creates a new feature class in a supplied feature dataset by building all of the
                // fields from scratch. IFeatureClassDescription (or IObjectClassDescription if the table is 
                // created at the workspace level) can be used to get the required fields and are used to 
                // get the InstanceClassID and ExtensionClassID.
                // Create new fields collection with the number of fields you plan to add. Must add at least two fields
                // for a feature class: Object ID and Shape field.
                IFields fields = new FieldsClass();
                IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
                fieldsEdit.FieldCount_2 = 3;

                // Create Object ID field.
                IField fieldUserDefined = new Field();

                IFieldEdit fieldEdit = (IFieldEdit)fieldUserDefined;
                fieldEdit.Name_2 = "OBJECTID";
                fieldEdit.AliasName_2 = "OBJECT ID";
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                fieldsEdit.set_Field(0, fieldUserDefined);

                // Create Shape field.
                fieldUserDefined = new Field();
                fieldEdit = (IFieldEdit)fieldUserDefined;

                // Set up geometry definition for the Shape field.
                // You do not have to set the spatial reference, as it is inherited from the feature dataset.
                IGeometryDef geometryDef = new GeometryDefClass();
                IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                geometryDefEdit.GeometryType_2 = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint;
                geometryDefEdit.SpatialReference_2 = pmap.SpatialReference;
                // By setting the grid size to 0, you are allowing ArcGIS to determine the appropriate grid sizes for the feature class. 
                // If in a personal geodatabase, the grid size is 1,000. If in a file or ArcSDE geodatabase, the grid size
                // is based on the initial loading or inserting of features.
                geometryDefEdit.GridCount_2 = 1;
                geometryDefEdit.set_GridSize(0, 0);
                geometryDefEdit.HasM_2 = false;
                geometryDefEdit.HasZ_2 = false;

                // Set standard field properties.
                fieldEdit.Name_2 = "SHAPE";
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                fieldEdit.GeometryDef_2 = geometryDef;
                fieldEdit.IsNullable_2 = true;
                fieldEdit.Required_2 = true;
                fieldsEdit.set_Field(1, fieldUserDefined);

                // Create a field of type double to hold some information for the features.
                fieldUserDefined = new Field();

                fieldEdit = (IFieldEdit)fieldUserDefined;
                fieldEdit.Name_2 = "ID";
                fieldEdit.AliasName_2 = "ID";
                fieldEdit.Editable_2 = true;
                fieldEdit.IsNullable_2 = false;
                fieldEdit.Precision_2 = 2;
                fieldEdit.Scale_2 = 5;
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                fieldsEdit.set_Field(2, fieldUserDefined);

                // Create a feature class description object to use for specifying the CLSID and EXTCLSID.
                IFeatureClassDescription fcDesc = new FeatureClassDescriptionClass();
                IObjectClassDescription ocDesc = (IObjectClassDescription)fcDesc;

                IFeatureClass pLineFClass = pFeatWorkspace.CreateFeatureClass("ShorePoints", fields, ocDesc.InstanceCLSID, ocDesc.ClassExtensionCLSID, esriFeatureType.esriFTSimple, "SHAPE", "");


                IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                pFeatureLayer.FeatureClass = pLineFClass;

                pFeatureLayer.Name = "ShorePoints";
                pmxdoc.AddLayer(pFeatureLayer);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }

        }


        private IFeature WalkLine(IFeatureLayer pDepthSoundings, IFeatureLayer pLineLayer, IFeature pLineFeature)
        {
            try
            {
                IEnvelope pCombinedEnvelope = CombineExtents(pDepthSoundings.FeatureClass, pLineLayer.FeatureClass);

                IPointCollection4 pPointColl = pLineFeature.Shape as IPointCollection4;
                IPoint ppoint = new PointClass();



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
                return pCloseFeature;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                return null;
            }

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


        private bool FeatureIntersects(IFeatureLayer pShorelineLayer, IFeatureLayer pLineHolderLayer, IFeature pFeature)
        {
            bool bHasIntersection = false;

            // Create the spatial filter and set its spatial constraints.  
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = pFeature.ShapeCopy;
            spatialFilter.GeometryField = pLineHolderLayer.FeatureClass.ShapeFieldName;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses ;

            //Apply the filter t0 the feature layer
            IFeatureSelection featureSelection = pShorelineLayer as IFeatureSelection;
            featureSelection.SelectFeatures(spatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            if (featureSelection.SelectionSet.Count > 0)
            {
                bHasIntersection = true;
            }
        
            featureSelection.Clear();
            return bHasIntersection;
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
