﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BathymetryTools10
{
    class btnPingSampler : ESRI.ArcGIS.Desktop.AddIns.Button
    {

        protected override void OnClick()
        {
            frmPingSampler frm = new frmPingSampler();
            frm.ShowDialog();
            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }



    }
}
