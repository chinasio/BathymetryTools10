using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BathymetryTools10
{
    class btnRasterInterpolate : ESRI.ArcGIS.Desktop.AddIns.Button
    {

        protected override void OnClick()
        {
            frmRasterInterpolate frm = new frmRasterInterpolate();
            frm.ShowDialog();
            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }


    }
}
