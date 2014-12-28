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
    public partial class frmDeepInterpolation : Form
    {

       

        public frmDeepInterpolation()
        {
            InitializeComponent();
           

        
        }

        private void frmDeepInterpolation_Load(object sender, EventArgs e)
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
                 
                 
                }

            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (cboDepthLayer.Text == ""  || txtDistInterval.Text == "")
            {
                MessageBox.Show("All data controls must be populated to continue.");
                return;
            }

            clsDeepInterpolation InterpFunctions = new clsDeepInterpolation();
            InterpFunctions.GeneratePoints2(this.cboShorelineLayer.Text, cboDepthLayer.Text, Convert.ToDouble(txtDistInterval.Text), cboDepthlayerDepthfield.Text);
            //InterpFunctions.GeneratePoints2(this.cboShorelineLayer.Text, cboDepthLayer.Text, cboOutpoints.Text,Convert.ToDouble (txtDistInterval.Text),cboDepthlayerDepthfield.Text);
            this.Close();
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

        private void cboDepthLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDepthPointsDepthField();
        }

        private void FillDepthPointsDepthField()
        {
            this.cboDepthlayerDepthfield.Items.Clear();
            clsDeepInterpolation InterpFunctions = new clsDeepInterpolation();
            IMxDocument pmxdoc = ArcMap.Document as IMxDocument;
            IMap pmap = pmxdoc.FocusMap;
            IFeatureLayer player = InterpFunctions.FindLayer(pmap, cboDepthLayer.Text) as IFeatureLayer;
            for (int i = 0; i <= player.FeatureClass.Fields.FieldCount - 1; i++)
            {
                cboDepthlayerDepthfield.Items.Add(player.FeatureClass.Fields.get_Field(i).Name);
            }
        }

        private void txtDistInterval_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
