
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        private EventLog eventLog;
        public LoggingService(EventLog eventLog)
        {
            this.eventLog = eventLog;
        }
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        public void Log(string message, MessageTypeEnum type)
        {
            eventLog.WriteEntry("mcds");
        }
    }
}
