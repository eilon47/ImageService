using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using StreamJsonRpc;
using System.Threading.Tasks;
using Communication.Server;
using Communication.Infrastructure;
using System.Threading;

namespace ImageService.Server
{
    /// <summary>
    /// ImageServer . Server for the Service - Creating Directory handlers for each directory.
    /// Connecting between the service and the handlers.
    /// </summary>
    public class ImageServer : IISClientHandler
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        private Dictionary<TcpClient, bool> clientsReadyForNewLogs;
        private List<string> dirPaths;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        public event EventHandler<DirectoryCloseEventArgs> CloseEvent;
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggingService">Log</param>
        /// <param name="imageController">Controller</param>
        public ImageServer(ILoggingService loggingService, IImageController imageController)
        {
            this.m_controller = imageController;
            this.m_logging = loggingService;
            this.clientsReadyForNewLogs = new Dictionary<TcpClient, bool>();
            this.m_logging.MessageRecieved += this.NewLogEntry;
            string[] arr = ConfigurationManager.AppSettings["Handler"].Split(';');
            dirPaths = new List<string>(arr);
            //Creates the direcory handlers for each directory path recieved.
            m_logging.Log("Image server was created, making handlers now", MessageTypeEnum.INFO);
            foreach (string path in dirPaths)
            {
                if (Directory.Exists(path))
                {
                    m_logging.Log("Creating handler for :" + path, MessageTypeEnum.INFO);
                    this.CreateHandler(path);
                }
            }
        }
        /// <summary>
        /// CreateHandler :
        /// creates the handler for a given directory's path.
        /// </summary>
        /// <param name="dirPath"></param>
        public void CreateHandler(string dirPath)
        {
            m_logging.Log("In create handler", MessageTypeEnum.INFO);
            IDirectoryHandler dirHandler = new DirectoyHandler(dirPath, m_logging, m_controller);
            CommandRecieved += dirHandler.OnCommandRecieved;
            CloseEvent += dirHandler.CloseHandler;
            dirHandler.StartHandleDirectory(dirPath);
            this.m_logging.Log("Created handler for: " + dirPath, MessageTypeEnum.INFO);
        }
        /// <summary>
        /// InvokeCommand 
        /// </summary>
        /// <param name="commandArgs"></param>
        public void InvokeCommand(CommandRecievedEventArgs commandArgs)
        {
            CommandRecieved?.Invoke(this, commandArgs);
        }
        /// <summary>
        /// OnClose the server need to close all the handlers.
        /// </summary>
        public void OnClose()
        {
            try
            {
                foreach(string path in dirPaths)
                {
                    CloseDirectoryHandler(path);
                }
            }
            catch (Exception e)
            {
                e.ToString();
                m_logging.Log("Error in closing the handlers", MessageTypeEnum.FAIL);
            }
        }
        private static Mutex writeMutex = new Mutex();
        private static Mutex removeMutex = new Mutex();
        public void HandleClient(TcpClient client)
        {
            this.clientsReadyForNewLogs.Add(client, false);
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);
                while (client.Connected)
                {
                    string commandLine = reader.ReadString();
                    if (commandLine == null)
                        continue;
                    CommandRecievedEventArgs crea = CommandRecievedEventArgs.FromJson(commandLine);
                    bool result;
                    string res = this.m_controller.ExecuteCommand(crea.CommandID, crea.Args, out result);
                    MutexedWriter(client, res);
                    res = string.Empty;
                    if (crea.CommandID == (int)CommandEnum.GetConfigCommand)
                    {
                        //Ready to get new logs entries
                        clientsReadyForNewLogs[client] = true;
                    }
                    if(crea.CommandID == (int)CommandEnum.CloseCommand)
                    {
                        CloseDirectoryHandler(crea.Args[0]);
                    }
                }
            }).Start();
        }
        private void MutexedWriter(TcpClient client, string message)
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writeMutex.WaitOne();
            writer.Write(message);
            writeMutex.ReleaseMutex();
        }
        public void NewLogEntry(object sender, MessageRecievedEventArgs m)
        {
            if (clientsReadyForNewLogs.Keys.Count == 0)
                return;
            string[] args = new string[1];
            args[0] = m.ToJson();
            CommandRecievedEventArgs c = new CommandRecievedEventArgs((int)CommandEnum.NewLogEntryCommand, args, null);
            new Task(() =>
            {
                foreach (TcpClient client in clientsReadyForNewLogs.Keys)
                {

                    if (client.Connected)
                    {
                        if (clientsReadyForNewLogs[client])
                        {
                            MutexedWriter(client, c.ToJson());
                        }
                    }
                    else
                    {
                        clientsReadyForNewLogs.Remove(client);
                    }
                    
                }
            }).Start();
        }
        private void CloseDirectoryHandler(string path) {
            m_logging.Log("Server closing the handler: " + path, MessageTypeEnum.INFO);
            CloseEvent?.Invoke(this, new DirectoryCloseEventArgs(path, null));
            this.dirPaths.Remove(path);
        }
    }


}