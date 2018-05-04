using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SettingsView
{
    class LogModel : IModel
    {
        private ITelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;

        public LogModel(ITelnetClient client)
        {
            this.telnetClient = client;
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        //connect to service.
        public void Connect(string ip, int port)
        {
            try
            {
                this.telnetClient.Connect(ip, port);
            }
            catch (Exception e) { }
            Start();
        }
        //start comuunicate with service
        public void Start()
        {
            new Thread(delegate () {
                //while (!this.stop)
               
               
                
            }).Start();
        }
        //disconnect from service
        public void Disconnect()
        {
            this.telnetClient.Disconnect();
        }
    }
}
