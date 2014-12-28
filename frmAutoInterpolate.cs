using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;


namespace BathymetryTools10
{
    public partial class frmAutoInterpolate : Form
    {
      

        public frmAutoInterpolate()
        {
            InitializeComponent();
          

        }

        private void frmAutoInterpolate_Load(object sender, EventArgs e)
        {

            fillcombos();

            if (cboDepthLayer.Items.Count == 0)
            {
                MessageBox.Show("You must add layers to the project in order to continue");
                this.Close();
            }
        }


        private void fillcombos()
        {
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;
            IFeatureLayer player;
            for (int i = 0; i <= pmap.LayerCount - 1; i++)
            {
                player = pmap.get_Layer(i) as IFeatureLayer;
            
                if (player is IFeatureLayer)
                {
                    cboDepthLayer.Items.Add(player.Name);
                    cboShorelineLayer.Items.Add(player.Name);
                    //cboOutpoints.Items.Add(player.Name);
                    //cboOutlines.Items.Add(player.Name);
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

        private void txtDistInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

    
        private void FillShoreDepthField()
        {
            cboShoreDepthField.Items.Clear();
            clsAutoInterpolate Functions = new clsAutoInterpolate();
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;
            IFeatureLayer player = Functions.FindLayer(pmap, cboShorelineLayer.Text) as IFeatureLayer;
            for (int i = 0; i <= player.FeatureClass.Fields.FieldCount - 1; i++)
            {
                this.cboShoreDepthField.Items.Add(player.FeatureClass.Fields.get_Field(i).Name);
            }
        }

       

        private void FillDepthPointsDepthField()
        {
            this.cboDepthlayerDepthfield.Items.Clear();
            clsAutoInterpolate Functions = new clsAutoInterpolate();
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;
            IFeatureLayer player = Functions.FindLayer(pmap, cboDepthLayer.Text) as IFeatureLayer;
            for (int i = 0; i <= player.FeatureClass.Fields.FieldCount - 1; i++)
            {
                cboDepthlayerDepthfield.Items.Add(player.FeatureClass.Fields.get_Field(i).Name);
            }
        }

        private void cboShorelineLayer_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillShoreDepthField();
        }

        private void cboDepthLayer_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillDepthPointsDepthField();
        }

        private void txtDistInterval_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnProcess_Click_1(object sender, EventArgs e)
        {
            if((txtDistInterval.Text.Length ==0) || (txtShorelinePointInterval.Text.Length ==0)||(cboDepthLayer.Text.Length == 0)||(cboShoreDepthField.Text.Length ==0)||(cboShorelineLayer.Text.Length ==0))
            {
                MessageBox.Show ("All fields must be populated to continue.");
                return;
            }
            clsAutoInterpolate Functions = new clsAutoInterpolate();
            //Make an empty line featureclass
            Functions.MakeScratchLines();
            Functions.MakeScratchPoints();
            Functions.PlotShorePoints(cboShorelineLayer.Text, Convert.ToDouble (txtShorelinePointInterval.Text));
            Functions.Calculate(cboShorelineLayer.Text, cboDepthLayer.Text,  Convert.ToDouble(txtDistInterval.Text), cboShoreDepthField.Text, cboDepthlayerDepthfield.Text);
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
