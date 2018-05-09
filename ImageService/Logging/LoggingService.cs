
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Communication.Infrastructure;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        private List<LogItem> logList;
        public List<LogItem> LogList
        {
            get { return logList; }
        }
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        public LoggingService()
        {
            logList = new List<LogItem>();
        }
        public void Log(string message, MessageTypeEnum type)
        {
            logList.Add(new LogItem((LogInfoEnum)((int) type), message));
            MessageRecievedEventArgs eventArgs = new MessageRecievedEventArgs((int)type,message);
            MessageRecieved?.Invoke(this, eventArgs);
        }
    }
}
