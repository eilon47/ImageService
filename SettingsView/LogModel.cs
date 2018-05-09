using Communication.Client;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
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
        private IISClient client;
        public event PropertyChangedEventHandler PropertyChanged;
        public LogModel()
        {
            this.client = ISClient.ClientServiceIns;
            this.client.MessageRecieved += GetMessageFromClient;
        }

        public void GetMessageFromClient(object sender, string message)
        {
            //If message if log - handle and notify, else ignore.
            if (message.Contains("Log"))
            {
            }
        }
        public void SendCommandToService() {
            CommandRecievedEventArgs command = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null);
            string jCommand = command.ToJson();
            client.Write(jCommand);
        }
       
        
      
    }
}
