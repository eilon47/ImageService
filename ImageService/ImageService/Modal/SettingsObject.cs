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
    class SettingsObject
    {
        public string OutPutDir
        {
            get { return ConfigurationManager.AppSettings["OutPutDir"]; }
            set
            {
                ConfigurationManager.AppSettings["OutPutDir"] = value;
            }
        }
        public string SourceName
        {
            get { return ConfigurationManager.AppSettings["SourceName"]; }
            set
            {
                ConfigurationManager.AppSettings["SourceName"] = value;
            }
        }
        public string LogName
        {
            get { return ConfigurationManager.AppSettings["LogName"]; }
            set
            {
                ConfigurationManager.AppSettings["LogName"] = value;
            }
        }
        public int ThumbnailSize
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]); }
            set
            {
                ConfigurationManager.AppSettings["ThumbnailSize"] = value.ToString();
            }
        }
        //private List<string> handlers;
        public string[] Handlers
        {
            get { return ConfigurationManager.AppSettings["Handlers"].Split(';'); }
            set
            {
                List<string> handlers = new List<string>();
                foreach (string handle in value)
                {
                    handlers.Add(handle);
                }
                string handlersAsString = string.Join(";", handlers.ToArray());
                ConfigurationManager.AppSettings["Handlers"] = handlersAsString;
            }
        }
        
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static SettingsObject FromJson(string jStr)
        {
            SettingsObject sO = new SettingsObject();
            JObject jObject = (JObject)JsonConvert.DeserializeObject(jStr);

            sO.OutPutDir = (string)jObject["OutPutDir"];
            sO.SourceName = (string)jObject["SourceName"];
            sO.LogName = (string)jObject["LogName"];
            sO.ThumbnailSize = (int)jObject["ThumbnailSize"];
            var h = jObject["Handlers"];
            sO.Handlers  = h.ToObject<string[]>();
            return sO;
        }


    }
}
