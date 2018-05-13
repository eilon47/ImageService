﻿using Communication.Client;
using Communication.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
         
        private ObservableCollection<MessageRecievedEventArgs> logs;
        public ObservableCollection<MessageRecievedEventArgs> Logs
        {
            get { return logs; }
        }
        public LogModel()
        {
            this.client = ISClient.ClientServiceIns;
            this.client.MessageRecieved += GetMessageFromClient;
            SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null));
        }

        public void GetMessageFromClient(object sender, string message)
        {
            //If message if log - handle and notify, else ignore.
            if (message.Contains("Log "))
            {
                logs = new ObservableCollection<MessageRecievedEventArgs>();
                int i = message.IndexOf(" ") + 1;
                message = message.Substring(i);
                string[] logsStrings = message.Split(';');
                foreach(string s in logsStrings)
                {
                    if (s.Contains("Status") && s.Contains("Message"))
                    {
                        try
                        {
                            MessageRecievedEventArgs m = MessageRecievedEventArgs.FromJson(s);
                            logs.Add(m);
                        } catch (Exception e)
                        {
                            continue;
                        }
                        
                    }
                }
            } else
            {
                Console.WriteLine("Log model ignored message = " + message);
            }
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            client.Write(command.ToJson());
        }



    }
}
