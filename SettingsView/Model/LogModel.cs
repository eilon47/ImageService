using Communication.Client;
using Communication.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SettingsView.Model
{
    class LogModel : IModel
    {
        private IISClient client;
        public event PropertyChangedEventHandler PropertyChanged;
        List<LogItem> logs;
        public List<LogItem> Logs
        {
            get { return logs; }
        }
        public LogModel()
        {
            this.client = ISClient.ClientServiceIns;
            this.client.MessageRecieved += GetMessageFromClient;
            SendCommandToService(new CommandMessage(CommandEnum.LogCommand, null));
        }

        public void GetMessageFromClient(object sender, string message)
        {
            //If message if log - handle and notify, else ignore.
            if (message.Contains("Log "))
            {
                message = message.Replace("Log", "");
                string[] logsStrings = message.Split(';');
                foreach(string s in logsStrings)
                {
                    logs.Add(LogItem.FromString(s));
                }
            }
        }
        public void SendCommandToService(CommandMessage command)
        {
            client.Write(command.ToString());
        }



    }
}
