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
using ESRI.ArcGIS.Geometry;

namespace BathymetryTools10
{
    public partial class frmPingSampler : Form
    {

    
        public frmPingSampler()
        {
            InitializeComponent();
           

           
        }

        private void frmPingSampler_Load(object sender, EventArgs e)
        {
            fillcombos();

            if (cboEchosounderPoints.Items.Count == 0)
            {
                MessageBox.Show("You need to have at least one layer with Point geometry present in the current project.");
                this.Close();
            }
        }

        private void fillcombos()
        {
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;
            IFeatureLayer  player;
            for (int i = 0; i <= pmap.LayerCount - 1; i++)
            {
                player = pmap.get_Layer(i) as IFeatureLayer;
              
             
                if (player is IFeatureLayer)
                {
                      
                      if (player.FeatureClass.ShapeType  == esriGeometryType.esriGeometryPoint)
                      {
                          cboEchosounderPoints.Items.Add(player.Name);
                          cboSedimentCores.Items.Add(player.Name);
                      }
                }

            }
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



        private void btnRangeSelect_Click(object sender, EventArgs e)
        {

            if (cboEchosounderPoints.Text == "" || cboSedimentCores.Text == "" || txtRange.Text == "" || cbounits.Text == "")
            {
                MessageBox.Show("All data fields must be completed and valid before processing");
                return;
            }


            clsPingFunctions PingFunctions = new clsPingFunctions();
            PingFunctions.CalculatePingsWithDistance(cboEchosounderPoints.Text, cboSedimentCores.Text,cboDepthField.Text , Convert.ToDouble(txtRange.Text), cbounits.Text);
            this.Close();
        }

        private void btnQuantitySelect_Click(object sender, EventArgs e)
        {
            if (cboEchosounderPoints.Text == "" || cboSedimentCores.Text == "" || txtNumPoints.Text == "")
            {
                MessageBox.Show("All data fields must be completed and valid before processing");
                return;
            }
            clsPingFunctions PingFunctions = new clsPingFunctions();
            PingFunctions.CalculatePingsNPoints(cboEchosounderPoints.Text, cboSedimentCores.Text,cboDepthField.Text , Convert.ToInt32(txtNumPoints.Text), Convert.ToDouble (this.txtIteration.Text));
            this.Close();
        }

        private void txtNumPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }

        }

        private void txtRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }

        }

        private void txtIteration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void cboEchosounderPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDepthField();
        }

        private void FillDepthField()
        {
            cboDepthField.Items.Clear();
            clsPingFunctions PingFunctions = new clsPingFunctions();
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;
            IFeatureLayer player = PingFunctions.FindLayer(pmap, cboEchosounderPoints.Text) as IFeatureLayer;
            for (int i = 0; i <= player.FeatureClass.Fields.FieldCount - 1; i++)
            {
                cboDepthField.Items.Add(player.FeatureClass.Fields.get_Field(i).Name);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
