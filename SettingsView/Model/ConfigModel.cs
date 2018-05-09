using Communication.Client;
using Communication.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SettingsView.Model
{
    class ConfigModel : IModel
    {

        private IISClient client;
        public event PropertyChangedEventHandler PropertyChanged;
        public ConfigModel()
        {
            this.client = ISClient.ClientServiceIns;
            this.client.MessageRecieved += GetMessageFromClient;
            SendCommandToService(new CommandMessage(CommandEnum.GetConfigCommand, null));
        }
        public void GetChangeFromVM(object sender, PropertyChangedEventArgs e)
        {
            
        }
        public void GetMessageFromClient(object sender, string message)
        {
            if (message.Contains("Config "))
            {
                message = message.Replace("Config ", "");
                JObject json = JObject.Parse(message);
                OutputDir = (string)json["OutputDir"];
                SourceName = (string)json["SourceName"];
                ThumbnailSize = int.Parse((string)json["ThumbnailSize"]);
                LogName = (string)json["LogName"];
                string[] handlersArray = ((string)json["Handler"]).Split(';');
                Handlers = handlersArray.ToList<string>();
            }
        }
        public void SendCommandToService(CommandMessage command)
        {
           
            client.Write(command.ToString());
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
        private List<string> handlers;
        public List<string> Handlers
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
