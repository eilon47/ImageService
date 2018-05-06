using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Server
{
    interface IISClientHandler
    {
        void HandleClient(TcpClient client);
    }
}
