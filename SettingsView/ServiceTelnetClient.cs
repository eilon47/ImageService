using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SettingsView
{
    class ServiceTelnetClient : ITelnetClient
    {
        private TcpClient tcpClient;
        private IPEndPoint endPoint;


        public void Connect(string ip, int port)
        {
            this.tcpClient = new TcpClient();
            this.endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            try
            {
                tcpClient.Connect(endPoint);
                Console.WriteLine($"You Are Connected To Service On IP: {ip}, Port: {port}");
                //get the app config for the first time...
                
            } catch(SocketException e)
            {
                
            }
        }

        public void Write(string command)
        {
            try
            {
                //tcpClient.Connect(endPoint);
                using (NetworkStream stream = tcpClient.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Send data to server
                    Console.WriteLine($"Send {command} to Server");
                    writer.Write(command);
                    //stream.Dispose();
                }
            } catch (ObjectDisposedException ode) { od}
           
        }

        public string Read()
        {
            //tcpClient.Connect(endPoint);
            string result = string.Empty;
            using (NetworkStream stream = tcpClient.GetStream()) 
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Get result from server

                result = reader.ReadString();
                Console.WriteLine($"Recieve {result} from Server");
            }
            //Console.WriteLine("Received: {0}", result);
            //tcpClient.Close();
            return result;
        }

        public void Disconnect()
        {
            tcpClient.Close();
        }
    }
}