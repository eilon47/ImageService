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
    class LogModel : ILogModel
    {
        private IISClient client;
        public event PropertyChangedEventHandler PropertyChanged;
         
        private ObservableCollection<MessageRecievedEventArgs> logs;
        public ObservableCollection<MessageRecievedEventArgs> Logs
        {
            set
            {
                logs = value;
                NotifyPropertyChanged("Logs");
            }
            get { return logs; }
        }
        public LogModel()
        {
            try
            {
                this.client = ISClient.ClientServiceIns;
                this.client.MessageRecieved += GetMessageFromClient;
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null));
            } catch(Exception e)
            {
                this.Logs = null;
            }
        }

        public void GetMessageFromClient(object sender, string message)
        {
            //If message if log - handle and notify, else ignore.
            if (message.Contains("GetLog "))
            {
                Console.WriteLine("Working on Log..");
                ObservableCollection<MessageRecievedEventArgs> list = new ObservableCollection<MessageRecievedEventArgs>();
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
                            list.Add(m);
                        } catch (Exception e)
                        {
                            continue;
                        }
                        
                    }
                }
                Logs = list;
                Console.WriteLine("Done working on log!");
            }
            //else if (message.Contains("AddLogEntry "))
            //{
            //    int i = message.IndexOf(" ") + 1;
            //    message = message.Substring(i);
            //    MessageRecievedEventArgs m = MessageRecievedEventArgs.FromJson(message);
            //    Logs.Add(m);
            //    NotifyPropertyChanged("Logs");
            //}
            else
            {
                Console.WriteLine("Log model ignored message = " + message);
            }
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            client.Write(command.ToJson());
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
