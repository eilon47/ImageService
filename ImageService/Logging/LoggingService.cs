
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
        private Queue<Tuple<MessageTypeEnum, string>> logQueue;
        public Queue<Tuple<MessageTypeEnum, string>> LogQueue
        {
            get { return logQueue; }
        }
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        public LoggingService()
        {
            logQueue = new Queue<Tuple<MessageTypeEnum, string>>();
        }
        public void Log(string message, MessageTypeEnum type)
        {
            LogQueue.Enqueue(new Tuple<MessageTypeEnum, string>(type, message));
            MessageRecievedEventArgs eventArgs = new MessageRecievedEventArgs((int)type,message);
            MessageRecieved?.Invoke(this, eventArgs);
        }
    }
}
