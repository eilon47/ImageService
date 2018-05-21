using Communication.Client;
using Communication.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsView.Model
{
    public class MainWindowModel : IMainWindowModel
    {
        #region constructor
        public MainWindowModel()
        {
            try
            {
                client = ISClient.ClientServiceIns;
                connection = client.Connection;
            }
            catch (Exception e)
            {
                connection = false;
            }
        }
        #endregion

        #region members, properties, 
        public event PropertyChangedEventHandler PropertyChanged;
        private IISClient client;

        private bool connection;
        public bool IsConnected {
            get
            {
                return connection;
            }
            private set
            {
                connection = value;
                NotifyPropertyChanged("IsConnected");
            }
        }
        #endregion
        #region methods
        public void SendCloseCommandToService()
        {
            //Need to restart the log list
            CommandRecievedEventArgs command = new CommandRecievedEventArgs((int)CommandEnum.WindowClosedCommand, null , null);
            client.Write(command.ToJson());
        }
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
