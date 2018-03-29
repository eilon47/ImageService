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
            this.m_controller = imageController;
            this.m_logging = loggingService;
            string[] dirPaths = ConfigurationSettings.AppSettings["Handler"].Split(';');
            foreach (string path in dirPaths)
            {
                //**need to check that dirPath is valid??***
                //**************************************
                this.CreateHandler(path);
            }
        }

        public void CreateHandler(string dirPath)
        {
            IDirectoryHandler dirHandler = new DirectoyHandler(dirPath);
            CommandRecieved += dirHandler.OnCommandRecieved;
            dirHandler.DirectoryClose += this.onClose;
            this.m_logging.Log("Created handler for: " + dirPath, Logging.Modal.MessageTypeEnum.INFO);
        }

        public void invokeCommand(CommandRecievedEventArgs commandArgs)
        {
            CommandRecieved?.Invoke(this,commandArgs);
        }

        public void onClose(object o, DirectoryCloseEventArgs dirArgs)
        {
            IDirectoryHandler dirHandler = (IDirectoryHandler)o;
            CommandRecieved -= dirHandler.OnCommandRecieved;
            string closingMessage = "the dir: " + dirArgs.DirectoryPath + "was closed";
            m_logging.Log(closingMessage, Logging.Modal.MessageTypeEnum.INFO);
        }
    }

}
