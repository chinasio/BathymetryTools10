//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BathymetryTools10 {
    using ESRI.ArcGIS.Framework;
    using ESRI.ArcGIS.ArcMapUI;
    using System;
    using System.Collections.Generic;
    using ESRI.ArcGIS.Desktop.AddIns;
    
    
    /// <summary>
    /// A class for looking up declarative information in the associated configuration xml file (.esriaddinx).
    /// </summary>
    internal class ThisAddIn {
        
        internal static string Name {
            get {
                return "BathymetryTools";
            }
        }
        
        internal static string AddInID {
            get {
                return "{22ecbe30-864a-40e4-9016-149e8c24e68b}";
            }
        }
        
        internal static string Company {
            get {
                return "Starlight GIS";
            }
        }
        
        internal static string Version {
            get {
                return "1.0";
            }
        }
        
        internal static string Description {
            get {
                return "BathymetryTools for ArcGIS 10";
            }
        }
        
        internal static string Author {
            get {
                return "Eric O\'Neal";
            }
        }
        
        internal static string Date {
            get {
                return "2/24/2012";
            }
        }
        
        /// <summary>
        /// A class for looking up Add-in id strings declared in the associated configuration xml file (.esriaddinx).
        /// </summary>
        internal class IDs {
            
            /// <summary>
            /// Returns 'Starlight_GIS_BathymetryTools10_extBathymetryTools_btnAreaVolume', the id declared for Add-in Button class 'btnAreaVolume'
            /// </summary>
            internal static string btnAreaVolume {
                get {
                    return "Starlight_GIS_BathymetryTools10_extBathymetryTools_btnAreaVolume";
                }
            }
            
            /// <summary>
            /// Returns 'Starlight_GIS_BathymetryTools10_extBathymetryTools_btnPingSampler', the id declared for Add-in Button class 'btnPingSampler'
            /// </summary>
            internal static string btnPingSampler {
                get {
                    return "Starlight_GIS_BathymetryTools10_extBathymetryTools_btnPingSampler";
                }
            }
            
            /// <summary>
            /// Returns 'Starlight_GIS_BathymetryTools10_extBathymetryTools_btnShallowInterpolation', the id declared for Add-in Button class 'btnShallowInterpolation'
            /// </summary>
            internal static string btnShallowInterpolation {
                get {
                    return "Starlight_GIS_BathymetryTools10_extBathymetryTools_btnShallowInterpolation";
                }
            }
            
            /// <summary>
            /// Returns 'Starlight_GIS_BathymetryTools10_extBathymetryTools_btnDeepInterpolation', the id declared for Add-in Button class 'btnDeepInterpolation'
            /// </summary>
            internal static string btnDeepInterpolation {
                get {
                    return "Starlight_GIS_BathymetryTools10_extBathymetryTools_btnDeepInterpolation";
                }
            }
            
            /// <summary>
            /// Returns 'Starlight_GIS_BathymetryTools10_extBathymetryTools_btnAutoInterpolate', the id declared for Add-in Button class 'btnAutoInterpolate'
            /// </summary>
            internal static string btnAutoInterpolate {
                get {
                    return "Starlight_GIS_BathymetryTools10_extBathymetryTools_btnAutoInterpolate";
                }
            }
            
            /// <summary>
            /// Returns 'Starlight_GIS_BathymetryTools10_extBathymetryTools_btnRasterInterpolate', the id declared for Add-in Button class 'btnRasterInterpolate'
            /// </summary>
            internal static string btnRasterInterpolate {
                get {
                    return "Starlight_GIS_BathymetryTools10_extBathymetryTools_btnRasterInterpolate";
                }
            }
            
            /// <summary>
            /// Returns 'Starlight_GIS_BathymetryTools10_extBathymetryTools', the id declared for Add-in Extension class 'extBathymetryTools'
            /// </summary>
            internal static string extBathymetryTools {
                get {
                    return "Starlight_GIS_BathymetryTools10_extBathymetryTools";
                }
            }
        }
    }
    
internal static class ArcMap
{
  private static IApplication s_app = null;
  private static IDocumentEvents_Event s_docEvent;

  public static IApplication Application
  {
    get
    {
      if (s_app == null)
        s_app = Internal.AddInStartupObject.GetHook<IMxApplication>() as IApplication;

      return s_app;
    }
  }

  public static IMxDocument Document
  {
    get
    {
      if (Application != null)
        return Application.Document as IMxDocument;

      return null;
    }
  }
  public static IMxApplication ThisApplication
  {
    get { return Application as IMxApplication; }
  }
  public static IDockableWindowManager DockableWindowManager
  {
    get { return Application as IDockableWindowManager; }
  }
  public static IDocumentEvents_Event Events
  {
    get
    {
      s_docEvent = Document as IDocumentEvents_Event;
      return s_docEvent;
    }
  }
}

namespace Internal
{
  [StartupObjectAttribute()]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public sealed partial class AddInStartupObject : AddInEntryPoint
  {
    private static AddInStartupObject _sAddInHostManager;
    private List<object> m_addinHooks = null;

    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    public AddInStartupObject()
    {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool Initialize(object hook)
    {
      bool createSingleton = _sAddInHostManager == null;
      if (createSingleton)
      {
        _sAddInHostManager = this;
        m_addinHooks = new List<object>();
        m_addinHooks.Add(hook);
      }
      else if (!_sAddInHostManager.m_addinHooks.Contains(hook))
        _sAddInHostManager.m_addinHooks.Add(hook);

      return createSingleton;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void Shutdown()
    {
      _sAddInHostManager = null;
      m_addinHooks = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal static T GetHook<T>() where T : class
    {
      if (_sAddInHostManager != null)
      {
        foreach (object o in _sAddInHostManager.m_addinHooks)
        {
          if (o is T)
            return o as T;
        }
      }

      return null;
    }

    // Expose this instance of Add-in class externally
    public static AddInStartupObject GetThis()
    {
      return _sAddInHostManager;
    }
  }
}
}
