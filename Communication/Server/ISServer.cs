using Communication.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Server
{
    public class ISServer : IISServer
    {
        #region Properties , Events and Members
        private int port;
        private TcpListener listener;
        private IISClientHandler ch;
        private string ip;
        public IISClientHandler ClientHandler { get { return ch; } set { this.ch = value; } }
        public string IP { get { return ip; } set { this.ip = value; } }
        public int Port { get { return port; } set { this.port = value; } }
        public int MobilePort { get; set; }
        public TcpListener Listener { get { return this.listener; } set { this.listener = value; } }
        #endregion
        #region Methods
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ch"></param>
        public ISServer(IISClientHandler ch)
        {
            this.ch = ch;
            ServerConfig();
        }
        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), Port);
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
                        ch.HandleClient(client, 0); //handle the player through the client handler
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            task.Start();
            IPEndPoint epMobile = new IPEndPoint(IPAddress.Parse(IP), MobilePort);
            listener = new TcpListener(epMobile);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task taskMobile = new Task(() =>//creating a listening thread that keeps running.
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient(); //recieve new client
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client, 1); //handle the player through the client handler
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            taskMobile.Start();
        }
        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
        /// <summary>
        /// Configuration of server
        /// </summary>
        private void ServerConfig()
        {
            IP = SettingsHolder.IP;
            Port = SettingsHolder.Port;
            MobilePort = SettingsHolder.MobilePort;
        }
        #endregion
    }
}
