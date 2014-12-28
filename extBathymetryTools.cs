using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

namespace BathymetryTools10
{
    public class extBathymetryTools : ESRI.ArcGIS.Desktop.AddIns.Extension
    {
        private string c_mainMenuID = "{1E739F59-E45F-11D1-9496-080009EEBECB}";
        esriExtensionState m_extensionState = esriExtensionState.esriESDisabled;

        public extBathymetryTools()
        {
        }

        protected override bool OnSetState(ESRI.ArcGIS.Desktop.AddIns.ExtensionState state)
        {

            this.State = state;

            if (state == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled)
            {
                m_extensionState = esriExtensionState.esriESEnabled;
                AddMenu();
            }
            else
            {
                m_extensionState = esriExtensionState.esriESDisabled ;
                RemoveMenu();
            }

            return base.OnSetState(state);
        }
        private void AddMenu()
        {

            ICommandBar mainMenuBar = GetMainBar();
            if (mainMenuBar == null)
            {
                return;
            }

            string menuID = "BathymetryTools";
            ICommandItem cmdItem = mainMenuBar.Find(menuID, false);

            if (cmdItem != null)
            {
                return;
            }

            UID uid = new UID();
            uid.Value = menuID;
            Object index = mainMenuBar.Count - 1;
            ICommandBar menuBathymetry = mainMenuBar.Add(uid, index) as ICommandBar;
            ICommandItem main = mainMenuBar as ICommandItem;
            main.Refresh();


        }


        private void RemoveMenu()
        {
            ICommandBar mainMenuBar = GetMainBar();
            if (mainMenuBar == null)
            {
                return;
            }

            object inddex = Type.Missing;

            for (int i = mainMenuBar.Count - 1; i >= 0; i += -1) 
            {
                if (mainMenuBar[i].Caption  == "Bathymetry Tools")
                {
                    mainMenuBar[i].Delete();
                }

            }

        }

        private ICommandBar GetMainBar()
        {
            try
            {
                IApplication papp = ArcMap.Application;
                UID uid = new UID();
                uid.Value = c_mainMenuID;
                MxDocument mx = papp.Document as MxDocument;
                ICommandBars cmdBars = mx.CommandBars;
                ICommandItem cmd = cmdBars.Find(uid, false, false);
                return cmd as ICommandBar;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected override void OnStartup()
        {
            WireDocumentEvents();
        }

        private void WireDocumentEvents()
        {
      
            ArcMap.Events.OpenDocument  += delegate()
            {
                if (m_extensionState == esriExtensionState.esriESEnabled)
                {
                    AddMenu();
                }
            };


            ArcMap.Events.NewDocument  += delegate()
            {
                if (m_extensionState == esriExtensionState.esriESEnabled)
                {
                    AddMenu();
                }
            };

        }


    }

}
