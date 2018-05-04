
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
            MessageRecievedEventArgs eventArgs = new MessageRecievedEventArgs((int)type,message);
            MessageRecieved?.Invoke(this, eventArgs);
        }
    }
}
