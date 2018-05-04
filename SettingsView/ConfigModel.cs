using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SettingsView
{
    class ConfigModel : IModel
    {
        ITelnetClient telnetClient;
        //volatile Boolean stop;
        public event PropertyChangedEventHandler PropertyChanged;

        public ConfigModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            //this.stop = false;
            OutPutDir = "hey hey hey";
            Connect("127.0.0.0.1", 8000);
        }

        public void Connect(string ip, int port)
        {
            try
            {
                this.telnetClient.Connect(ip, port);
                OutPutDir = "connected";
            } catch (Exception e) { OutPutDir = e.ToString(); }

            Start();
        }

        public void Start()
        {
            new Thread(delegate () {
                //while (!this.stop)
                {
                    telnetClient.Write("getConfig");
                    Thread.Sleep(5000);
                    OutPutDir = "sent command";
                    Thread.Sleep(5000);
                    OutPutDir = telnetClient.Read();
                    Thread.Sleep(5000);
                    OutPutDir = " recieved outputdir";
                    SourceName = telnetClient.Read();
                    LogName = telnetClient.Read();
                    ThumbnailSize = int.Parse(telnetClient.Read());
                    Handlers =  new List<string>(telnetClient.Read().Split(';'));
                }
            }).Start();
        }

        public void Disconnect()
        {
            //this.stop = true;
            this.telnetClient.Disconnect();
        }

        private string outputDir;
        public string OutPutDir
        {
            get { return this.outputDir; }
            set {
                this.outputDir = value;
                NotifyPropertyChanged("OutPutDir");
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
