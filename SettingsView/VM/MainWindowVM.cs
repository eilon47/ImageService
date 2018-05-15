using Communication.Client;
using SettingsView.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsView.VM
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IMainWindowModel model;
        public bool IsConnected
        {
            get { return model.IsConnected; }
        }
        public MainWindowVM()
        {
            this.model = new MainWindowModel();
        }
    }

}
