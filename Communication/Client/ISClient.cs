using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Client
{
    class ISClient : IISClient 
    {
        public event EventHandler<string> MessageRecieved;
        //Propeties
        private bool connection;
        public bool Connection { get { return connection; } set { connection = value; } }
        private NetworkStream stream;
        //public NetworkStream Stream { get { return stream; } set { stream = value; } }
        private StreamReader streamReader;
        //public StreamReader ReaderProp { get { return streamReader; } set { streamReader = value; } }
        private StreamWriter streamWriter;
        //public StreamWriter WriterProp { get { return streamWriter; } set { streamWriter = value; } }
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
                    clientService = new ISClient("127.0.0.1", 8000);
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
            this.stream = client.GetStream();
            streamReader = new StreamReader(this.stream);
            streamWriter = new StreamWriter(this.stream);
            streamWriter.AutoFlush = true;
            Console.WriteLine("you are connected ");
            connection = true;
        }


        public void Write(string command)
        {
            if (!Connection)
            {
                CreateANewConnection();
            }
            streamWriter.WriteLine(command);
            streamWriter.Flush();
            //Get result from server.
            Read();
        }

        public void Read()
        {
            if (!Connection)
            {
                CreateANewConnection();
            }
            string result = streamReader.ReadLine();
            if (result == null)
            {
                File.AppendAllText(@"C:\Users\eilon\Desktop\אילון\gui.txt", "Result = " + result);
            }
            //לשלוח לכל מי שנרשם לאיוונט
            MessageRecieved?.Invoke(this, result);
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}
