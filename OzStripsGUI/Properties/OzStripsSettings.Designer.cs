﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaxRumsey.OzStripsPlugin.Gui.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
    internal sealed partial class OzStripsSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static OzStripsSettings defaultInstance = ((OzStripsSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new OzStripsSettings())));
        
        public static OzStripsSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseVatSysPopup {
            get {
                return ((bool)(this["UseVatSysPopup"]));
            }
            set {
                this["UseVatSysPopup"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public float StripScale {
            get {
                return ((float)(this["StripScale"]));
            }
            set {
                this["StripScale"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int SmartResize {
            get {
                return ((int)(this["SmartResize"]));
            }
            set {
                this["SmartResize"] = value;
            }
        }
    }
}
