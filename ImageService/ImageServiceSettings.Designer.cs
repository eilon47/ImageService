﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageService {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.6.0.0")]
    internal sealed partial class ImageServiceSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static ImageServiceSettings defaultInstance = ((ImageServiceSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ImageServiceSettings())));
        
        public static ImageServiceSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\green\\Desktop\\handle2;C:\\Users\\green\\Desktop\\handle1")]
        public string Handler {
            get {
                return ((string)(this["Handler"]));
            }
            set {
                this["Handler"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\green\\Desktop\\outputDir")]
        public string OutputDir {
            get {
                return ((string)(this["OutputDir"]));
            }
            set {
                this["OutputDir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ImageServiceLog")]
        public string LogName {
            get {
                return ((string)(this["LogName"]));
            }
            set {
                this["LogName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ImageServiceSource")]
        public string SourceName {
            get {
                return ((string)(this["SourceName"]));
            }
            set {
                this["SourceName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public int ThumbnailsSize {
            get {
                return ((int)(this["ThumbnailsSize"]));
            }
            set {
                this["ThumbnailsSize"] = value;
            }
        }
    }
}