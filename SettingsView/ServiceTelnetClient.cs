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
    class ServiceTelnetClient 
    {
        public event EventHandler<string> MessageRecieved;
        //להוסיף איוונט ששולח לכל מי שנרשם אליו את מה שהוא קרא
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
        private static ServiceTelnetClient clientService;
        //instance
        public static ServiceTelnetClient ClientServiceIns
        {
            get
            {
                if (clientService == null)
                {
                    clientService = new ServiceTelnetClient("127.0.0.1", 8000);
                }
                return clientService;
            }
        }
        private ServiceTelnetClient(string ip, int port) {
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
            streamReader = new StreamReader(this.stream, ASCIIEncoding.UTF8);
            streamWriter = new StreamWriter(this.stream, ASCIIEncoding.UTF8);
            streamWriter.AutoFlush = true;
            Console.WriteLine("you are connected ");
            connection = true;
        }
        

        public void Write(string command)
        {
            if(!Connection)
            {
                CreateANewConnection();
            }
            File.AppendAllText(@"C:\Users\eilon\Desktop\אילון\file2.txt", "in client connection = " + Connection.ToString() + Environment.NewLine);

            File.AppendAllText(@"C:\Users\eilon\Desktop\אילון\file2.txt", "in client write sending command: " + command);
            streamWriter.WriteLine(command);
            streamWriter.Flush();
            //Get result from server.
            Read();
        }

        private void Read()
        {
            if (!Connection)
            {
                CreateANewConnection();
            }
            string result = streamReader.ReadToEnd(); ;
            if (result == null)
            {
                File.AppendAllText(@"C:\Users\eilon\Desktop\אילון\gui.txt", "Result = " + result );
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