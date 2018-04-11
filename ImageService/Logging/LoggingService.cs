﻿
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        public void Log(string message, MessageTypeEnum type)
        {
           string func = "Log\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
                sw.WriteLine(message);
        }


            MessageRecievedEventArgs eventArgs = new MessageRecievedEventArgs();
            eventArgs.Message = message;
            eventArgs.Status = type;
            MessageRecieved?.Invoke(this, eventArgs);
        }
    }
}
