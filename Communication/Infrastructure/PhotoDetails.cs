using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Infrastructure
{
    public class PhotoDetails
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
        public DateTime Date { get; set; }
        public PhotoDetails(string path, string name, string ext, string tpath, DateTime d)
        {
            Name = name;
            Extension = ext;
            Path = path;
            ThumbnailPath = tpath;
            Date = d;
        }
        public string ToJson()
        {
            JObject jStr = new JObject();
            jStr["Path"] = Path;
            jStr["ThumbnailPath"] = ThumbnailPath;
            jStr["DateTime"] = Date.ToString();
            jStr["Name"] = Name;
            jStr["Extension"] = Extension;
            return jStr.ToString().Replace(Environment.NewLine, " ");
        }
        public static PhotoDetails FromJson(string json)
        {
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(json);
                string path = (string)jObject["Path"];
                string name = (string)jObject["Name"];
                string ext = (string)jObject["Extension"];
                string tpath = (string)jObject["ThumbnailPath"];
                DateTime d;
                string date = (string)jObject["DateTime"];
                DateTime.TryParse(date, out d);
                return new PhotoDetails(path,name, ext, tpath, d);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
