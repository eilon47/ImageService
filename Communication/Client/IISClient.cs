using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Client
{
    interface IISClient
    {
        void Write(string command);
        void Read(); // blocking call
        void Disconnect();
    }
}
