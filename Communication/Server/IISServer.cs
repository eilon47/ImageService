using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Server
{
    interface IISServer
    {
        void Start();
        void Stop();
        IISClientHandler ClientHandler { get; set; }
        string IP { get; set; }
        int Port { get; set; }
        TcpListener Listener { get; set; }
    }
}
