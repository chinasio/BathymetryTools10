using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Geodatabase;

namespace BathymetryTools10
{
    public partial class frmRasterInterpolate : Form
    {
        public frmRasterInterpolate()
        {
            InitializeComponent();
        }

        private void frmRasterInterpolate_Load(object sender, EventArgs e)
        {
            FillCombos();

            if (cboRasterLayers.Items.Count == 0)
            {
                MessageBox.Show("You must have at least one TIN or Raster in the current project.");
                this.Close();
            }
        }



        private void FillCombos()
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
                if (player is IFeatureLayer)
                {
                    cboLineLayer.Items.Add(player.Name);
                }

            }


        }

        private void btnProcess_Click(object sender, EventArgs e)
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "dbf files (*.dbf)|*.dbf|All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                clsRasterInterpolate RI = new clsRasterInterpolate();
                RI.ExtractPointData(cboLineLayer.Text, cboRasterLayers.Text, Convert.ToDouble(txtIntervalDistance.Text),dlg.FileName);
            }


            this.Close();
        }

 

    }
}
