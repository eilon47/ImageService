using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class SettingsObject
    {
        private static SettingsObject instance = null;
        private SettingsObject()
        {
            OutPutDir = ConfigurationManager.AppSettings["OutPutDir"];
            SourceName = ConfigurationManager.AppSettings["SourceName"];
            LogName = ConfigurationManager.AppSettings["LogName"];
            ThumbnailSize = int.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);
            Handlers = ConfigurationManager.AppSettings["Handler"];
            
        }
        public static SettingsObject GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingsObject();
                }
                return instance;
            }
        }
        private string outputDir;
        public string OutPutDir
        {
            get { return outputDir; }
            set { outputDir = value; }
        }
        private string sourceName;
        public string SourceName
        {
            get { return sourceName; }
            set { sourceName = value; }
        }
        private string logName;
        public string LogName
        {
            get { return logName; }
            set { logName = value; }
        }
        private int thumbnailSize;
        public int ThumbnailSize
        {
            get { return thumbnailSize; }
            set { thumbnailSize = value; }
        }
        private string handlers;
        public string Handlers
        {
            get { return handlers; }
            set
            {
                handlers = value;
            }
        }
        
        public string ToJson()
        {
            JObject j = new JObject();
            j["OutputDir"] = OutPutDir;
            j["SourceName"] = SourceName;
            j["ThumbnailSize"] = ThumbnailSize.ToString();
            j["LogName"] = LogName;
            j["Handler"] = Handlers;
            return j.ToString();
        }
        public bool RemoveHandler(string path)
        {
            if (Handlers.Contains(path))
            {
                Handlers.Replace(path, "");
                Handlers.Replace(";;", ";");
                return true;
                // Remove From app config
            }
            return false;
        }


    }
}
