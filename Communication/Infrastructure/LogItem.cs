using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Infrastructure
{
    public class LogItem
    {
        private LogInfoEnum info;
        private string message;
        public string Message
        {
            get { return message; }
            set { this.message = value; }
        }
        public LogInfoEnum Info
        {
            get { return info; }
            set { info = value; }
        }
        public LogItem(LogInfoEnum info, string message)
        {
            this.info = info;
            this.message = message;
        }
        public override string ToString()
        {
            return info + "-" + message;
        }
        public static LogItem FromString(string s)
        {
            string[] ss = s.Split('-');
            LogInfoEnum inf = (LogInfoEnum)int.Parse(ss[0]);
            return new LogItem(inf, ss[1]);
        }
    }
}
