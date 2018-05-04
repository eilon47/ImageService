﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging.Modal
{
    /// <summary>
    /// Evevt Args for Message Recived event.
    /// </summary>
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageTypeEnum Status { get; set; }
        public string Message { get; set; }

        public MessageRecievedEventArgs(int MessageType, string message)
        {
            Status = (MessageTypeEnum) MessageType;
            Message = message;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static MessageRecievedEventArgs fromJson(string jStr)
        {
            JObject jObject = (JObject)JsonConvert.DeserializeObject(jStr);
            int messageType = (int)jObject["Status"];
            string message = (string)jObject["Message"];
            return new MessageRecievedEventArgs(messageType,message);
        }

    }

    
}
