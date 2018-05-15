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
        private TcpClient client;
        private IPEndPoint ep;
        private int portNumber;
        //Propeties
        public bool Connection { get { return client.Connected; } }
       
        //Singleton!
        private static ISClient clientService;
        
        //instance
        public static ISClient ClientServiceIns
        {
            get
            {
                if (clientService == null)
                {
                    try
                    {
                        clientService = new ISClient(ComSettings.Default.IP, ComSettings.Default.Port);
                    } catch(Exception e)
                    {
                        throw e;
                    }
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
            try
            { 
                client.Connect(this.ep);//connect to the server
            } catch (Exception e)
            {
                throw e;
            }
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
                writerMutex.WaitOne();
                writer.Write(command);
                writer.Flush();
                writerMutex.ReleaseMutex();
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
            if (client != null)
            {
                client.GetStream().Close();
                client.Close();
                client = null;
            }
        }
    }
}
