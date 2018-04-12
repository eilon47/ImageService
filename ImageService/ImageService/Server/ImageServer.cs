using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion

        public ImageServer(ILoggingService loggingService, IImageController imageController)
        {
            string func = "ImageServer\n";
            string pth = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(pth)) 
        {
            sw.WriteLine(func);
        }	
            this.m_controller = imageController;
            this.m_logging = loggingService;
            string[] dirPaths = ConfigurationManager.AppSettings["Handler"].Split(';');
            foreach (string path in dirPaths)
            {
                //**need to check that dirPath is valid??***
                //**************************************
                this.CreateHandler(path);
                using (StreamWriter sw = File.AppendText(pth)) 
        {
            sw.WriteLine("created handel for directory:");
                                sw.WriteLine(path);
                                sw.WriteLine("\n");


        }	
            }
        }

        public void CreateHandler(string dirPath)
        {
            string func = "Createhandler\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
        }
	        m_logging.Log(func, Logging.Modal.MessageTypeEnum.INFO);
            IDirectoryHandler dirHandler = new DirectoyHandler(dirPath,m_logging,m_controller);
            CommandRecieved += dirHandler.OnCommandRecieved;
            dirHandler.DirectoryClose += this.onClose;
            dirHandler.StartHandleDirectory(dirPath);
            this.m_logging.Log("Created handler for: " + dirPath, Logging.Modal.MessageTypeEnum.INFO);
        }

        public void invokeCommand(CommandRecievedEventArgs commandArgs)
        {
             string func = "invokeCommand\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
        }

            CommandRecieved?.Invoke(this,commandArgs);
        }

        public void onClose(object o, DirectoryCloseEventArgs dirArgs)
        {
            string func = "onClose\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
        }	
            IDirectoryHandler dirHandler = (IDirectoryHandler)o;
            CommandRecieved -= dirHandler.OnCommandRecieved;
            string closingMessage = "the dir: " + dirArgs.DirectoryPath + "was closed";
            m_logging.Log(closingMessage, Logging.Modal.MessageTypeEnum.INFO);
        }
         
    }
}