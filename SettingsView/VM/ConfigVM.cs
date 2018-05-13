using SettingsView.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsView.VM
{
    class ConfigVM : INotifyPropertyChanged
    {
        private ConfigModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public ConfigVM()
        {
            model = new ConfigModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };


        }
        
        public void NotifyPropertyChanged(string propName) {
            if (propName.Equals("VM_OutputDir"))
            {
                VM_OutputDir = model.OutputDir;
            }
            if (propName.Equals("VM_ThumbnailSize"))
            {
                VM_ThumbnailSize = model.ThumbnailSize;
            }
            if (propName.Equals("VM_SourceName"))
            {
                VM_SourceName = model.SourceName;
            }
            if (propName.Equals("VM_LogName"))
            {
                VM_LogName = model.LogName;
            }
            if (propName.Equals("VM_Handlers"))
            {
                VM_Handlers = model.Handlers;
            }
        }
        private string outputDir;
        public string  VM_OutputDir
        {
            get { return model.OutputDir; }
            set
            {
                outputDir = value;
                NotifyPropertyChanged("OutputDir");
            }
           
        }
        private string sourceName;
        public string VM_SourceName
        {
            get { return model.SourceName; }
            set
            {
                this.sourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }
        private string logName;
        public string VM_LogName
        {
            get { return model.LogName; }
            set
            {
                this.logName = value;
                NotifyPropertyChanged("LogName");
            }
        }
        private int thumbnailSize;
        public int VM_ThumbnailSize
        {
            get { return model.ThumbnailSize; }
            set
            {
                this.thumbnailSize = value;
                NotifyPropertyChanged("ThumbnailSize");
            }
        }
        private ObservableCollection<string> handlers;
        public ObservableCollection<string> VM_Handlers
        {
            get { return model.Handlers; }
            set
            {
                this.handlers = value;
                NotifyPropertyChanged("Handlers");
            }
        }
    }
}
