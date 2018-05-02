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

        public void Connect(string ip, int port)
        {
            this.tcpClient = new TcpClient();
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            try
            {
                tcpClient.Connect(ipep);
                Console.WriteLine($"You Are Connected To Service On IP: {ip}, Port: {port}");
            } catch(SocketException e)
            {
                throw new Exception(e.ToString());
                //Console.WriteLine("Unable To Connect To Server.");
                //Console.WriteLine(e.ToString());
            }
        }

        public void Write(string command)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            NetworkStream stream = tcpClient.GetStream();
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", command);
        }
        public string Read()
        {
            NetworkStream stream = tcpClient.GetStream();
            Byte[] data = new Byte[256];
            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //Console.WriteLine("Received: {0}", responseData);
            return responseData;
        }

        public void Disconnect()
        {
            tcpClient.Close();
        }
    }
}