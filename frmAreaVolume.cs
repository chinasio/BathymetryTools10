using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Analyst3D;
using System.IO;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;

namespace BathymetryTools10
{
    public partial class frmAreaVolume : Form
    {
       
        IStepProgressor pStepProgressor;


        public frmAreaVolume()
        {
            InitializeComponent();
         

        }


        private void frmAreaVolume_Load(object sender, EventArgs e)
        {

            FillComboWithRasters();

            if (cboRasterLayers.Items.Count == 0)
            {
                MessageBox.Show("You must have at least one TIN or Raster in the current project.");
                this.Close();
            }
        }


        private void btnProcess_Click(object sender, EventArgs e)
        {
            try {
            if(cboRasterLayers.Text == "" || txtEndingElevation.Text == "" || txtGradInterval.Text == "" || txtStartingElevation.Text == "")
            {
                MessageBox.Show ("All data fields must be completed and valid before processing");
                return;
            }

            double dStartingElev = Convert.ToDouble(txtStartingElevation.Text);
            double dEndingElev = Convert.ToDouble(txtEndingElevation.Text);
            double dGraduationValue = Convert.ToDouble(txtGradInterval.Text);

            MakeAreaVolumeTable(dStartingElev, dEndingElev, dGraduationValue);
            this.Close();
            }

            catch(Exception ex)
            {
                if (ex.Message == "Input string was not in a correct format.")
                {
                    MessageBox.Show("An input value appears to be invalid. Use only integers or decimals.");
                    
                }
            }
        }





        private void FillComboWithRasters()
        {
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;

            ILayer player;

            for (int i = 0; i <= pmap.LayerCount - 1; i++)
            {
                player = pmap.get_Layer(i);

                if (player is IRasterLayer || player is ITinLayer)
                {
                    cboRasterLayers.Items.Add(player.Name);
                }

            }


        }


        private void Toggle3DExtension(string strState)
        {

            try
            {
                IExtensionManagerAdmin pExtAdmin2 = new ExtensionManagerClass();
                IExtensionManager pExtManager2 = pExtAdmin2 as IExtensionManager;
                UID pUID = new UID();
                pUID.Value = "{94305472-592E-11D4-80EE-00C04FA0ADF8}";
                IExtensionConfig pExtConfig2 = pExtManager2.FindExtension(pUID) as IExtensionConfig;

                switch (strState)
                {
                    case "ON":
                        pExtConfig2.State = esriExtensionState.esriESEnabled;
                        break;
                    case "OFF":
                        pExtConfig2.State = esriExtensionState.esriESDisabled;
                        break;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void MakeAreaVolumeTable(double dStartingElev, double dEndingElev, double dGraduationValue)
        {
          
            SaveFileDialog dlgSaveFile = new SaveFileDialog();
            dlgSaveFile.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                Toggle3DExtension("ON");
                StreamWriter sw = new StreamWriter(dlgSaveFile.FileName);
                sw.WriteLine("Elevation,Area_acres,Volume");


                IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
                IMap pmap = pmxdoc.FocusMap;



                ILayer player = FindLayer(pmap, cboRasterLayers.Text);
              
                //bool bFeetCorrect = chkFeetCorrect.Checked;
                esriPlaneReferenceType eRef;

                double dlbRange = Math.Abs (dStartingElev - dEndingElev );
                IProgressDialog2 pProgressDialog = ShowProgressIndicator("Calculating...", Convert.ToInt32 (dlbRange / dGraduationValue), 1);
                pProgressDialog.ShowDialog();



                eRef = esriPlaneReferenceType.esriPlaneReferenceBelow ;
                ////if (radAbove.Checked)
                ////{
                ////     eRef = esriPlaneReferenceType.esriPlaneReferenceAbove;
                   
                ////}
                ////else
                ////{
                ////    eRef = esriPlaneReferenceType.esriPlaneReferenceBelow;
                ////}

                ISurface pSurface = GetSurfaceFromLayer(player);
                
                while (dEndingElev > dStartingElev)
                {
                    pStepProgressor.Step();

                    //Check difference between these two calculations
                    double dArea = pSurface.GetProjectedArea (dStartingElev, eRef);
                    //double dArea = pSurface.GetSurfaceArea(dStartingElev, eRef);

                    //Convert square meters to acres
                    dArea = dArea * 0.000247;

                    double dVolume = pSurface.GetVolume(dStartingElev, eRef);

                    //Convert cubic meter to acre-foot
                    dVolume = dVolume * 0.000811;


                    //Meters to feet.  Probably not needed
                    if (chkVertUnitCorrect.Checked)
                    {
                        dVolume = dVolume / 3.28084;
                    }
                  

                    sw.WriteLine(dStartingElev + "," + dArea + "," + dVolume);
                    dStartingElev = dStartingElev + dGraduationValue;


                }


                sw.Close();
                pProgressDialog.HideDialog();

            }
            
            AddTable(Path.GetDirectoryName(dlgSaveFile.FileName), Path.GetFileName(dlgSaveFile.FileName));
            Toggle3DExtension("OFF");
        }

        private ISurface GetSurfaceFromLayer(ILayer player)
        {
            try
            {
                IRasterLayer pRLayer;
                I3DProperties p3DProp = null;
                ILayerExtensions pLE;
                IRasterSurface pSurface = null; ;
                ITinLayer pTLayer;

                

                if (player is IRasterLayer)
                {
                    pRLayer = player as IRasterLayer;
                    pLE = player as ILayerExtensions;

                    for (int i = 0; i <= pLE.ExtensionCount - 1; i++)
                    {
                        if (pLE.get_Extension(i) is I3DProperties)
                        {
                            p3DProp = pLE.get_Extension(i) as I3DProperties;
                            break;
                        }
                    }
                     

                   

                    if (p3DProp == null)
                    {
                        IRasterBandCollection pBands;
                        if (pRLayer.Raster != null)
                        {
                            pSurface = new RasterSurfaceClass();
                            pBands = pRLayer.Raster as IRasterBandCollection;
                            pSurface.RasterBand = pBands.Item(0);
                        }
                        return pSurface as ISurface;
                    }
                    else
                    {
                        pSurface = p3DProp.BaseSurface as IRasterSurface;
                    }
                    return pSurface as ISurface;

                }

                else
                {
                    if (player is ITinLayer)
                    {
                        pTLayer = player as ITinLayer;
                        return pTLayer.Dataset as ISurface;
                        
                    }

                }
                return null;

            }

            catch (Exception ex)
            {
                string g = ex.Message;
                return null;
            }
        }


        public ILayer FindLayer(IMap pmap, string aLayerName)
        {

            int i = 0;

            while (i <= pmap.LayerCount - 1)
            {
                if ((pmap.get_Layer(i).Name.ToUpper()) == (aLayerName.ToUpper()))

                    break;
                i++;
            }
            return pmap.get_Layer(i);
        }


        private void AddTable(string strPath,string strFileName)
        {
            try
            {

                IWorkspaceFactory pFact = new TextFileWorkspaceFactory();
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
                ptableWindow.Application = ArcMap.Application ;
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



        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);

                // System.Drawing.Drawing2D.LinearGradientBrush baseBackground = (New (Point(0, 0)) New (Point(ClientSize.Width, 0)), Color.Gray, Color.RoyalBlue);
                using (Brush b = new
                           System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Point(0, 0), new
                           System.Drawing.Point(this.ClientSize.Width, this.ClientSize.Height),
                           Color.LightSkyBlue, Color.Silver))
                    e.Graphics.FillRectangle(b, ClientRectangle);
            }
            catch { }

        }

   
        private void cboRasterLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////try
            ////{
                
            ////    IMxDocument pmxdoc = pmxApp.Document as IMxDocument;
            ////    IMap pmap = pmxdoc.FocusMap;

            ////    ILayer player = FindLayer(pmap, cboRasterLayers.Text);
            ////    if (player is IRasterLayer)
            ////    {
            ////        IRasterLayer pRLayer = player as IRasterLayer;

            ////        IRasterBandCollection pBandCol = pRLayer.Raster as IRasterBandCollection;
            ////        IRasterBand pRasBand = pBandCol.Item(0);
            ////        IRasterStatistics pStats = pRasBand.Statistics;
            ////        txtStartingElevation.Text = Math.Round(pStats.Minimum, 3).ToString();
            ////        txtEndingElevation.Text = Math.Round(pStats.Maximum, 3).ToString();
            ////    }
            ////    if (player is ITinLayer)
            ////    {
            ////        ITinLayer pTLayer = player as ITinLayer;
            ////        ITin pTin = pTLayer.Dataset;
            ////        pTin.
            ////    }

            ////}

            ////catch (Exception ex)
            ////{

            ////    string g = ex.Message;
            ////}

        }

        private void txtStartingElevation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }

            
        }

 
        private void txtEndingElevation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtGradInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
