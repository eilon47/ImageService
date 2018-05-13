using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Infrastructure
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageTypeEnum Status { get; set; }
        public string Message { get; set; }

        public MessageRecievedEventArgs(int MessageType, string message)
        {
            Status = (MessageTypeEnum)MessageType;
            Message = message;
        }

        public string ToJson()
        {
            //One string with no new lines.
            return JsonConvert.SerializeObject(this).Replace(Environment.NewLine, " ");
        }
        public static MessageRecievedEventArgs FromJson(string jStr)
        {
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(jStr);
                int messageType = (int)jObject["Status"];
                string message = (string)jObject["Message"];
                return new MessageRecievedEventArgs(messageType, message);
            }catch (Exception e)
            {
                throw e;
            }
        }
    }
}
