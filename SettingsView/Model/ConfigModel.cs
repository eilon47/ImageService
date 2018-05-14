﻿using Communication.Client;
using Communication.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SettingsView.Model
{
    class ConfigModel : IConfigModel
    {

        private IISClient client;
        public event PropertyChangedEventHandler PropertyChanged;
        public ConfigModel()
        {
            try
            {
                this.client = ISClient.ClientServiceIns;
                this.client.MessageRecieved += GetMessageFromClient;
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null));
            } catch (Exception e)
            {
                NotConnectedValues();
            }
        }
        public void NotConnectedValues()
        {
            OutputDir = "Not connected to server!";
            SourceName = null;
            ThumbnailSize = 0;
            LogName = null;
            Handlers = null;
        }
        public void GetMessageFromClient(object sender, string message)
        {
            if (message.Contains("Config "))
            {
                Console.WriteLine("Working on config...");
                int i = message.IndexOf(" ") + 1;
                message = message.Substring(i);
                JObject json = JObject.Parse(message);
                OutputDir = (string)json["OutputDir"];
                SourceName = (string)json["SourceName"];
                ThumbnailSize = int.Parse((string)json["ThumbnailSize"]);
                LogName = (string)json["LogName"];
                string[] handlersArray = ((string)json["Handler"]).Split(';');
                Handlers = new ObservableCollection<string>(handlersArray);
                
                Console.WriteLine("Done!");
            }
            else if (message.Contains("Close "))
            {
                string[] newHandlers = message.Split(';');
                Handlers = new ObservableCollection<string>(newHandlers);
            } else
            {
                Console.WriteLine("Config model ignored message = " + message);
            }
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            client.Write(command.ToJson());
        }




        private string outputDir;
        public string OutputDir
        {
            get { return this.outputDir; }
            set
            {
                this.outputDir = value;
                NotifyPropertyChanged("OutputDir");
            }
        }
        private string sourceName;
        public string SourceName
        {
            get { return this.sourceName; }
            set
            {
                this.sourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }
        private string logName;
        public string LogName
        {
            get { return this.logName; }
            set
            {
                this.logName = value;
                NotifyPropertyChanged("LogName");
            }
        }
        private int thumbnailSize;
        public int ThumbnailSize
        {
            get { return this.thumbnailSize; }
            set
            {

                this.thumbnailSize = value;
                NotifyPropertyChanged("ThumbnailSize");
            }
        }
        private ObservableCollection<string> handlers;
        public ObservableCollection<string> Handlers
        {
            get { return this.handlers; }
            set
            {
                this.handlers = value;
                NotifyPropertyChanged("Handlers");
            }
        }


        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
