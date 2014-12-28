using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;

namespace BathymetryTools10
{
    class clsPingFunctions
    {

        IStepProgressor pStepProgressor;


        public void CalculatePingsNPoints(string sSounderLayer, string sSedCoreLayer, string strDepthValField, long numpoints, double dblIterationRange)
        {
            try
            {
                IMxApplication pMxApp = ArcMap.Application  as IMxApplication;
                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;
                IActiveView pActiveView = pmap as IActiveView;


                //clear any previous selections so they wont interfere
                pmap.ClearSelection();


                IFeatureLayer pSounderLayer = FindLayer(pmap, sSounderLayer) as IFeatureLayer;
                IFeatureSelection pSounderLayerSel = pSounderLayer as IFeatureSelection;



                IFeatureLayer pSedCoreLayer = FindLayer(pmap, sSedCoreLayer) as IFeatureLayer;
                IFeatureSelection pSedCoreLayerSel = pSedCoreLayer as IFeatureSelection;

                //Add fields to hold the data if they do not exist already.
                string strAvgfld = "Avg_" + strDepthValField;

                if (strAvgfld.Length > 9)
                {
                    strAvgfld = strAvgfld.Substring(0, 8);
                    strAvgfld = AddField(pSedCoreLayer, strAvgfld);
                }
                else
                {
                    strAvgfld = AddField(pSedCoreLayer, strAvgfld);
                }



                string strStdFld = "Std_" + strDepthValField;

                if (strStdFld.Length > 9)
                {
                    strStdFld = strStdFld.Substring(0, 8);
                    strStdFld = AddField(pSedCoreLayer, strStdFld);
                }
                else
                {
                    strStdFld = AddField(pSedCoreLayer, strStdFld);
                }


                string strSampFld = AddField(pSedCoreLayer, "Samp_Size");


                //Get a cursor from the sample layer
                IFeatureCursor pFCur = pSedCoreLayer.Search(null, false);
                IFeature pFeature = pFCur.NextFeature();

                int iPointProgress = 1;
                IProgressDialog2 pProgressDialog = ShowProgressIndicator("Calculating...", pSedCoreLayer.FeatureClass.FeatureCount(null), 1);
                pProgressDialog.Description = "Processing Point: " + iPointProgress + " of " + pSedCoreLayer.FeatureClass.FeatureCount(null).ToString();
                pProgressDialog.ShowDialog();

                //Each point will be buffered at "dblRange" until the desired amount of points are collected.
                double dblRange = dblIterationRange;
                long lngFeatureCount = 0;

                while (pFeature != null)
                {


                    //loop thru the featureclass, selecting each feature at a time.
                    SelectFeatures(pSedCoreLayer, pSedCoreLayer.FeatureClass.OIDFieldName + " = " + pFeature.OID);

                    //Select Depth features within the range of the previously selected Sample point features
                    lngFeatureCount = SpatialSelect(sSounderLayer, sSedCoreLayer, dblRange, pmap.MapUnits);

                    //If we reach the user-entered point threshold, stop and calculate.
                    if (lngFeatureCount >= numpoints)
                    {

                        IStatisticsResults pStats = GetDataStatistics(pSounderLayer, strDepthValField);
                        pFeature.set_Value(pFeature.Fields.FindField(strAvgfld), pStats.Mean);
                        pFeature.set_Value(pFeature.Fields.FindField(strStdFld), pStats.StandardDeviation);
                        pFeature.set_Value(pFeature.Fields.FindField(strSampFld), lngFeatureCount);
                        pSounderLayerSel.Clear();
                        pSedCoreLayerSel.Clear();
                        pFeature.Store();

                        dblRange = .1;
                        pFeature = pFCur.NextFeature();
                        pStepProgressor.Step();
                        iPointProgress++;
                        pProgressDialog.Description = "Processing Point: " + iPointProgress + " of " + pSedCoreLayer.FeatureClass.FeatureCount(null).ToString();
                    }
                    else
                    {

                        dblRange = dblRange + dblIterationRange;

                    }


                }


                pProgressDialog.HideDialog();

                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
              
            }



        }


        public void CalculatePingsWithDistance(string sSounderLayer, string sSedCoreLayer, string strDepthField, double dblRange, string strUnits)
        {
            try{
            IMxApplication pMxApp = ArcMap.Application  as IMxApplication;
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;
            IActiveView pActiveView = pmap as IActiveView;


            pmap.ClearSelection();

            IFeatureLayer pSounderLayer = FindLayer(pmap, sSounderLayer) as IFeatureLayer;
            IFeatureSelection pSounderLayerSel = pSounderLayer as IFeatureSelection;



            IFeatureLayer pSedCoreLayer = FindLayer(pmap, sSedCoreLayer) as IFeatureLayer;
            IFeatureSelection pSedCoreLayerSel = pSedCoreLayer as IFeatureSelection;
            //Add fields to hold the data if they do not exist already.
            string strAvgfld = "Avg_" + strDepthField;

            if (strAvgfld.Length > 9)
            {
                strAvgfld = strAvgfld.Substring(0, 8);
                strAvgfld = AddField(pSedCoreLayer, strAvgfld);
            }
            else
            {
                strAvgfld = AddField(pSedCoreLayer, strAvgfld);
            }



            string strStdFld = "Std_" + strDepthField;

            if (strStdFld.Length > 9)
            {
                strStdFld = strStdFld.Substring(0, 8);
                strStdFld = AddField(pSedCoreLayer, strStdFld);
            }
            else
            {
                strStdFld = AddField(pSedCoreLayer, strStdFld);
            }



            string strSampFld = AddField(pSedCoreLayer, "Samp_Size");

            esriUnits eEsriUnitsContant = 0;

            //set the units
            switch (strUnits)
            {
                case "Feet":
                    eEsriUnitsContant = esriUnits.esriFeet;
                    break;
                case "Meters":
                    eEsriUnitsContant = esriUnits.esriMeters;
                    break;
                case "Kilometers":
                    eEsriUnitsContant = esriUnits.esriKilometers;
                    break;

            }


            IFeatureCursor pFCur = pSedCoreLayer.Search(null, false);
            IFeature pFeature = pFCur.NextFeature();
            pSedCoreLayerSel.Clear();

            int iPointProgress = 1;
            IProgressDialog2 pProgressDialog = ShowProgressIndicator("Calculating...", pSedCoreLayer.FeatureClass.FeatureCount(null), 1);
            pProgressDialog.Description = "Processing Point: " + iPointProgress + " of " + pSedCoreLayer.FeatureClass.FeatureCount(null).ToString();
            pProgressDialog.ShowDialog();

            long lFeatureCount = 0;
            while (pFeature != null)
            {
                pStepProgressor.Step();
                pProgressDialog.Description = "Processing Point: " + iPointProgress + " of " + pSedCoreLayer.FeatureClass.FeatureCount(null).ToString();
                //loop thru the featureclass, selecting each feature at a time.
                SelectFeatures(pSedCoreLayer, pSedCoreLayer.FeatureClass.OIDFieldName + " = " + pFeature.OID);

                //Select Depth features within the range of the previously selected Sample point features
                lFeatureCount = SpatialSelect(sSounderLayer, sSedCoreLayer, dblRange, eEsriUnitsContant);
                 
                //Calculate
                IStatisticsResults pStats = GetDataStatistics(pSounderLayer, strDepthField);
                pFeature.set_Value(pFeature.Fields.FindField(strAvgfld), pStats.Mean);
                pFeature.set_Value(pFeature.Fields.FindField(strStdFld), pStats.StandardDeviation);
                pFeature.set_Value(pFeature.Fields.FindField(strSampFld), lFeatureCount);
                pSounderLayerSel.Clear();
                pSedCoreLayerSel.Clear();
                pFeature.Store();
                pFeature = pFCur.NextFeature();
                iPointProgress++;



            }


            pProgressDialog.HideDialog();
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);

            }


        }





        private void SelectFeatures(IFeatureLayer pFeatLayer, string whereClause)
        {

            IFeatureClass pFc;
            ISelectionSet pSelSet;
            IFeatureSelection pFSel;


            pFc = pFeatLayer.FeatureClass;

            //'Create the query filter
            IQueryFilter pQF = new QueryFilterClass();
            pQF.WhereClause = whereClause;


            //'Get the features that meet the where clause
            pSelSet = pFc.Select(pQF, esriSelectionType.esriSelectionTypeIDSet, esriSelectionOption.esriSelectionOptionNormal, null);

            //'Apply the selection
            pFSel = pFeatLayer as IFeatureSelection;
            pFSel.SelectionSet = pSelSet;
        }




        private long SpatialSelect(string sSedCoreLayer, string sSounderLayer, double dblRange, esriUnits eEsriUnitsContant)
        {
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;
            IFeatureSelection pFSel = FindLayer(pmap, sSedCoreLayer) as IFeatureSelection;
            //pFSel.Clear();

            IQueryByLayer pQBLayer = new QueryByLayerClass();
            pQBLayer.ByLayer = FindLayer(pmap, sSounderLayer) as IFeatureLayer;
            pQBLayer.FromLayer = pFSel as IFeatureLayer;
            pQBLayer.BufferDistance = dblRange;
            pQBLayer.BufferUnits = eEsriUnitsContant;
            pQBLayer.ResultType = esriSelectionResultEnum.esriSelectionResultAdd;
            pQBLayer.LayerSelectionMethod = esriLayerSelectionMethod.esriLayerSelectIntersect;
            pQBLayer.UseSelectedFeatures = true;
            pFSel.SelectionSet = pQBLayer.Select();
            return pFSel.SelectionSet.Count;
        }


        private string AddField(IFeatureLayer pFeaturelayer, string strFieldName)
        {
            try
            {
                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;

                IFeatureClass pFeatureClass = pFeaturelayer.FeatureClass;

                IFieldEdit pNewField = new FieldClass();
                pNewField.Name_2 = strFieldName;

                int s = pFeatureClass.FindField(strFieldName);

                if (pFeatureClass.FindField(strFieldName) == -1)
                {

                    pNewField.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pNewField.Length_2 = 50;
                    pNewField.Editable_2 = true;
                    pNewField.IsNullable_2 = true;
                    pNewField.DefaultValue_2 = null;
                    pFeatureClass.AddField(pNewField);
                    string strFldName = pNewField.Name;
                    pNewField = null;
                    GC.Collect();
                    return strFldName;

                }
                else
                {
                    return strFieldName;
                }

            }

            catch (Exception ex)
            {
                return null;
            }
        }


        private IStatisticsResults GetDataStatistics(IFeatureLayer pFeaturelayer, string strFieldname)
        {

            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;



            IFeatureSelection pFSel = pFeaturelayer as IFeatureSelection;
            ISelectionSet pSelSet = pFSel.SelectionSet;

            ICursor pCur;

            pSelSet.Search(null, false, out pCur);

            IFeatureClass pFeatureClass = pFeaturelayer.FeatureClass;
            IDataStatistics pData = new DataStatisticsClass();
            IStatisticsResults pStatResults;

            pData.Cursor = pCur;
            pData.Field = strFieldname;
            pStatResults = pData.Statistics;

            return pStatResults;

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



        ////private void MakeRing(IPoint ppoint, double distance)
        ////{

        ////    //IDistanceConverter 
        ////    IMxDocument pmxdoc = pmxApp.Document as IMxDocument;
        ////    IGraphicsContainer pGrCont = pmxdoc.FocusMap as IGraphicsContainer;

        ////    ITopologicalOperator pTopoOp;
        ////    IColor pColor = new RgbColorClass();

        ////    pColor.RGB = 25500;
        ////    IMarkerSymbol pSmpMSy = new SimpleMarkerSymbol();
        ////    pSmpMSy.Size = 3;
        ////    pSmpMSy.Color = pColor;

        ////    IElementProperties2 pelementprop;

        ////    IMarkerElement pMEl = new MarkerElement() as IMarkerElement;
        ////    pMEl.Symbol = pSmpMSy;
        ////    IElement pEl = pMEl as IElement;
        ////    pEl.Geometry = ppoint;

        ////    //All graphics will get tagged as "RING" so we can remove only these on the next run of the tool.
        ////    pelementprop = pEl as IElementProperties2;
        ////    pelementprop.Name = "RING";

        ////    pGrCont.AddElement(pEl, 0);
        ////    pTopoOp = pEl.Geometry as ITopologicalOperator;


        ////    IGeometry pPolygon = pTopoOp.Buffer(distance);
        ////    IElement pElement = new PolygonElement();
        ////    pElement.Geometry = pPolygon;

        ////    pelementprop = pElement as IElementProperties2;
        ////    pelementprop.Name = "RING";

        ////    pGrCont.AddElement(pElement, 0);

        ////    //Color the ring and set some properties
        ////    ModifyElement(pElement, 0, 255, 0);


        ////    pmxdoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pElement, null);


        ////    //pmxdoc.ActiveView.Refresh();

        ////}




        ////private void ModifyElement(IElement pelement, int r, int g, int b)
        ////{



        ////    IFillShapeElement pFillShapeElement;
        ////    ISimpleFillSymbol pFillSym = new SimpleFillSymbol();
        ////    ISimpleLineSymbol pLineSym = new SimpleLineSymbol();
        ////    IRgbColor pLineColor = new RgbColorClass();


        ////    pLineColor.Red = r;
        ////    pLineColor.Green = g;
        ////    pLineColor.Blue = b;

        ////    pLineSym.Color = pLineColor;
        ////    pLineSym.Style = esriSimpleLineStyle.esriSLSSolid;
        ////    pLineSym.Width = 2;


        ////    pFillSym.Outline = pLineSym;
        ////    pFillSym.Style = esriSimpleFillStyle.esriSFSHollow;


        ////    pFillShapeElement = pelement as IFillShapeElement;
        ////    pFillShapeElement.Symbol = pFillSym;
        ////}




        //Find a layer by its name.
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
