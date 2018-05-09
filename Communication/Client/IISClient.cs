using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Client
{
    public interface IISClient
    {
        event EventHandler<string> MessageRecieved;
        void Write(string command);
        void Read(); // blocking call
        void Disconnect();
    }
}
