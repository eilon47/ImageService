using Communication.Server;
using ImageService.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    class GuiServer
    {
        private int port;
        private TcpListener listener;
        private IISClientHandler imageServer;
        private string ip;
        public IISClientHandler ClientHandler { get { return imageServer; } set { this.imageServer = value; } }
        public string IP { get { return ip; } set { this.ip = value; } }
        public int Port { get { return port; } set { this.port = value; } }
        public TcpListener Listener { get { return this.listener; } set { this.listener = value; } }
        public GuiServer(int port, IISClientHandler imageServer)
        {
            this.ip = "127.0.0.1";
            this.port = port;
            this.imageServer = imageServer;
        }
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() =>//creating a listening thread that keeps running.
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient(); //recieve new client
                        Console.WriteLine("Got new connection");
                        File.AppendAllText(@"C:\Users\eilon\Desktop\אילון\handle.txt", "in server got new connection " + Environment.NewLine);
                        imageServer.HandleClient(client); //handle the player through the client handler
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
        public void Stop()
        {
            listener.Stop();
        }

    }
}
