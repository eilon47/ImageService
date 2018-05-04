using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    /// <summary>
    /// Event args for the Commands.
    /// </summary>
    public class CommandRecievedEventArgs : EventArgs
    {
        public int CommandID { get; set; }      // The Command ID
        public string[] Args { get; set; }
        public string RequestDirPath { get; set; }  // The Request Directory

        public CommandRecievedEventArgs(int id, string[] args, string path)
        {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static CommandRecievedEventArgs FromJson(string jStr)
        {
            JObject jObject = (JObject)JsonConvert.DeserializeObject(jStr);
            int id = (int)jObject["CommandID"];
            var args = jObject["Args"];
            string[] argsArr = args.ToObject<string[]>();
            string path = (string)jObject["RequestDirPath"];
            return new CommandRecievedEventArgs(id, argsArr, path);
        }

    } 
    
}
