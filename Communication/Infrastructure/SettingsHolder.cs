using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Infrastructure
{
    class SettingsHolder
    {
        public static int Port { get { return 8500; } }
        public static string IP { get { return "127.0.0.1"; } }
    }
}
