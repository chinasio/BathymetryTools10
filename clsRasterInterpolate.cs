using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.GeoDatabaseUI;
using System.Windows.Forms;


namespace BathymetryTools10
{
    class clsRasterInterpolate
    {


        IStepProgressor pStepProgressor;
        string strSaveFile;

        public void ExtractPointData(string sLineLayer, string sRasterLayer, double dblInterval,string strFileName)
        {
            try
            {

                strSaveFile = strFileName;
             
                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;

             
                IFeatureLayer pLineLayer = FindLayer(sLineLayer) as IFeatureLayer;
                IRasterLayer pRasterLayer = FindLayer(sRasterLayer) as IRasterLayer;
                //IFeatureLayer pPointLayer = FindLayer(sPointLayer) as IFeatureLayer;
                IFeatureLayer pPointLayer = new FeatureLayerClass();
                pPointLayer.FeatureClass = MakePointFC();
                //pPointLayer.Name = "Points";
               // ArcMap.Document.ActiveView.FocusMap.AddLayer(pPointLayer);

          
                //get the Workspace from the IDataset interface on the feature class
                IDataset dataset = (IDataset)pLineLayer.FeatureClass;
                IWorkspace workspace = dataset.Workspace;
                //Cast for an IWorkspaceEdit
                IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)workspace;

                //Start an edit session and operation
                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();

                IProgressDialog2 pProgressDialog = ShowProgressIndicator("Calculating...", pLineLayer.FeatureClass.FeatureCount(null), 1);
                pProgressDialog.ShowDialog();


                //Set up the Outpoints cursor
                IFeatureCursor pFCurOutPoints = pPointLayer.Search(null, false);
                pFCurOutPoints = pPointLayer.FeatureClass.Insert(true);

                //Set up the LineLayer Cursor
                IFeatureCursor pFCur = pLineLayer.Search(null, false);
                IFeature pLineFeature = pFCur.NextFeature();

                IFeatureBuffer pFBuffer = pPointLayer.FeatureClass.CreateFeatureBuffer();



                ICurve pCurve;

  
                double dlbProcessedLength = 0;
                double dblFCTotalLength = 0;
                int p = 0;

                while (pLineFeature != null)
                {

                    pProgressDialog.Description = "Calculating line segment " + p.ToString() + " of: " + pLineLayer.FeatureClass.FeatureCount(null).ToString();

                    //create startpoint
                    pCurve = pLineFeature.Shape as ICurve;
                    pFBuffer.Shape = pCurve.FromPoint;
                  

                    pFBuffer.set_Value(pFBuffer.Fields.FindField("Distance"), 0);
                    pFBuffer.set_Value(pFBuffer.Fields.FindField("LineOID"), pLineFeature.OID);
                    pFBuffer.set_Value(pFBuffer.Fields.FindField("X"), pCurve.FromPoint.X);
                    pFBuffer.set_Value(pFBuffer.Fields.FindField("Y"), pCurve.FromPoint.Y);

                    pFCurOutPoints.InsertFeature(pFBuffer);

                    double dblTotalDistance = pCurve.Length;
                    dlbProcessedLength = dblInterval;


                    IConstructPoint contructionPoint;
                    IPoint ppoint;
                    while (dlbProcessedLength <= dblTotalDistance)
                    {




                        contructionPoint = new PointClass();
                        contructionPoint.ConstructAlong(pCurve, esriSegmentExtension.esriNoExtension, dlbProcessedLength, false);
                        ppoint = new PointClass();
                        ppoint = contructionPoint as IPoint;

                        pFBuffer.Shape = ppoint;

                  

                        //dblFCTotalLength += dblInterval;
                     
                        pFBuffer.set_Value(pFBuffer.Fields.FindField("Distance"), Math.Round(dlbProcessedLength, 4));
                        pFBuffer.set_Value(pFBuffer.Fields.FindField("LineOID"), pLineFeature.OID);
                        pFBuffer.set_Value(pFBuffer.Fields.FindField("X"), ppoint.X);
                        pFBuffer.set_Value(pFBuffer.Fields.FindField("Y"), ppoint.Y);

                        pFCurOutPoints.InsertFeature(pFBuffer);
                        dlbProcessedLength += dblInterval;

                        pStepProgressor.Step();

                    }

                    dblFCTotalLength += dblInterval;
                    p++;
                    pLineFeature = pFCur.NextFeature();

                }

                //cleanup
                pFCurOutPoints.Flush();
                pFCur.Flush();

                //Stop editing
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);

                Extract(pRasterLayer, pPointLayer);

                pProgressDialog.HideDialog();
                pmxdoc.ActiveView.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }

        }

        private void ExportTable2DBF(ITable ptable, string strPath, string strFilename)
        {
            IDataset pDataset = ptable as IDataset;
            IDatasetName pDatasetName = pDataset.FullName as IDatasetName;


            
            IWorkspaceFactory pWF = new ShapefileWorkspaceFactory();
            IWorkspace pWorkspace = pWF.OpenFromFile(strPath, 0);
            IDataset pDatasetOUT = pWorkspace as IDataset;
            IWorkspaceName pWorkspaceName = pDatasetOUT.FullName as IWorkspaceName;
            IDatasetName pOUTDatasetName = new TableNameClass ();
            pOUTDatasetName.Name = strFilename;
            pOUTDatasetName.WorkspaceName = pWorkspaceName;

            IExportOperation pExpOp = new ExportOperationClass();
            pExpOp.ExportTable(pDatasetName, null, null, pOUTDatasetName, 0);

        }

        private void AddTable(string strPath, string strFileName)
        {
            try
            {

                IWorkspaceFactory pFact = new ShapefileWorkspaceFactory();
                IWorkspace pWorkspace = pFact.OpenFromFile(strPath, 0);
                IFeatureWorkspace pFeatws = pWorkspace as IFeatureWorkspace;
                ITable ptable = pFeatws.OpenTable(strFileName);

                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;

                IStandaloneTable pStTab = new StandaloneTableClass();
                pStTab.Table = ptable;
                IStandaloneTableCollection pStTabColl = pmap as IStandaloneTableCollection;
                pStTabColl.AddStandaloneTable(pStTab);
                pmxdoc.UpdateContents();

                ITableWindow2 ptableWindow = new TableWindowClass();
                ptableWindow.Application = ArcMap.Application;
                ptableWindow.StandaloneTable = pStTab;

                ptableWindow.Show(true);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFact);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pWorkspace);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatws);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ptable);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pStTab);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pStTabColl);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ptableWindow);

                GC.Collect();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void Extract(IRasterLayer pRasterLayer,IFeatureLayer pFeatureLayer)
        {
            try
            {
              
          
                IRaster pRaster = pRasterLayer.Raster;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;

                ToggleSA(true);

                IExtractionOp2 pExtractionOp = new RasterExtractionOpClass();

                IGeoDataset pOutGeoDS = pExtractionOp.ExtractValuesToPoints(pFeatureClass as IGeoDataset, pRaster as IGeoDataset, false);

                IFeatureLayer player = new FeatureLayerClass();
                player.FeatureClass = pOutGeoDS as IFeatureClass;
                player.Name = "Raster2Points";

                string g =  System.IO.Path.GetDirectoryName (strSaveFile);
                if (System.IO.File.Exists(strSaveFile))
                {
                    System.IO.File.Delete(strSaveFile);
                }
                ITable ptable = player.FeatureClass as ITable;
                ExportTable2DBF(ptable, System.IO.Path.GetDirectoryName (strSaveFile),System.IO.Path.GetFileName (strSaveFile));
                AddTable(System.IO.Path.GetDirectoryName(strSaveFile), System.IO.Path.GetFileName(strSaveFile));
                ArcMap.Document.ActiveView.FocusMap.AddLayer(player);
                ToggleSA(false);
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
                pFieldsEdit.FieldCount_2 = 5;


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
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger ;
                pFieldsEdit.set_Field(1, pFieldEdit);

                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "Distance";
                pFieldEdit.Name_2 = "Distance";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.set_Field(2, pFieldEdit);

                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "X";
                pFieldEdit.Name_2 = "X";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.set_Field(3, pFieldEdit);

                pField = new FieldClass();
                pFieldEdit = pField as IFieldEdit;
                pFieldEdit.AliasName_2 = "Y";
                pFieldEdit.Name_2 = "Y";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldsEdit.set_Field(4, pFieldEdit);



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

        private void ToggleSA(bool toggle)
        {
            UID puid = new UID();
            puid.Value = "esriGeoAnalyst.SAExtension.1";

            object v = 0;
            IExtensionManagerAdmin pLicAdmin = new ExtensionManagerClass();
            pLicAdmin.AddExtension(puid, ref v);

            IExtensionManager pLicManager = pLicAdmin as IExtensionManager;
            IExtensionConfig pExtensionConfig = pLicManager.FindExtension(puid) as IExtensionConfig;
            if (toggle)
            {
                pExtensionConfig.State = esriExtensionState.esriESEnabled;
            }
            else
            {
                pExtensionConfig.State = esriExtensionState.esriESDisabled;
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

        public ILayer FindLayer(string layer)
        {
            for (int i = 0; i <= ArcMap.Document.ActiveView.FocusMap.LayerCount - 1; i++)
            {
                if (ArcMap.Document.ActiveView.FocusMap.get_Layer(i).Name == layer)
                    return ArcMap.Document.ActiveView.FocusMap.get_Layer(i);
            }
            return null;


        }


    }
}
