using Communication.Client;
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
        private IISClient client;
        private bool connection;
        public bool IsConnected
        {
            get { return connection; }
            private set { connection = value; }
        }
        public MainWindowVM()
        {
            try
            {
                client = ISClient.ClientServiceIns;
                connection = client.Connection;
            } catch(Exception e) {
                connection = false;
            }
        }
    }

}
