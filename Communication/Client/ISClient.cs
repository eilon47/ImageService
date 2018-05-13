using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Communication.Client
{
    public class ISClient : IISClient 
    {
        private static Mutex readerMutex = new Mutex();
        private static Mutex writerMutex = new Mutex();
        public event EventHandler<string> MessageRecieved;
        //Propeties
        public bool Connection { get { return client.Connected; } }
        private TcpClient client;
        //public TcpClient TcpClient { get { return client; } set { client = value; } }
        private IPEndPoint ep;
        //public IPEndPoint IpEndPoint { get { return ep; } set { ep = value; } }
        private int portNumber;
        //Singleton!
        private static ISClient clientService;
        
        //instance
        public static ISClient ClientServiceIns
        {
            get
            {
                if (clientService == null)
                {
                    clientService = new ISClient(ComSettings.Default.IP, ComSettings.Default.Port);
                }
                return clientService;
            }
        }
        private ISClient(string ip, int port)
        {
            portNumber = port;
            client = new TcpClient();
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            CreateANewConnection();
        }
        private void CreateANewConnection()
        {
            this.client = new TcpClient();
            client.Connect(this.ep);//connect to the server
            Console.WriteLine("you are connected ");
            Read();
        }


        public void Write(string command)
        {
           new Task(() =>
            { 
                if (!Connection)
                {
                    CreateANewConnection();
                }
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                Console.WriteLine("got command " + command);
                writerMutex.WaitOne();
                writer.Write(command);
                writer.Flush();
                writerMutex.ReleaseMutex();
                //Get result from server.
            }).Start();
        }

        public void Read()
        {
            new Task(() =>
            {
                try
                {
                    while (Connection)
                    {
                        NetworkStream stream = client.GetStream();
                        BinaryReader reader = new BinaryReader(stream);
                        string result = reader.ReadString();
                        Console.WriteLine(result);
                        if (result == null)
                        {
                            return;
                        }
                        MessageRecieved?.Invoke(this, result);
                    }
                } catch(Exception e)
                {

                }
            }).Start();
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}
