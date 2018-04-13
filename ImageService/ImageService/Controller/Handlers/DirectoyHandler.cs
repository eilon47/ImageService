﻿using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;
using ImageService.Commands;
using ImageService.Server;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller.
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir.
        private string m_path;                              // The Path of directory.
        private string[] extensionsToListen = { ".bmp", ".jpg", ".png", ".gif" };   // List for valid extensions.

        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dirPath">directory path</param>
        /// <param name="loggingService">logging service</param>
        /// <param name="imageController">image controller</param>
        public DirectoyHandler(string dirPath, ILoggingService loggingService, IImageController imageController)
        {
            this.m_dirWatcher = new FileSystemWatcher(dirPath);
            this.m_controller = imageController;
            this.m_logging = loggingService;
            this.m_path = dirPath;
        }


        public void StartHandleDirectory(string dirPath)
        {
            string startMessage = "Handeling directory: " + dirPath;
            this.m_logging.Log(startMessage, MessageTypeEnum.INFO);
            InitializeWatcher(dirPath);
        }


        public void InitializeWatcher(string dirPath)
        {
                m_dirWatcher.Path = dirPath;
                m_dirWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                m_dirWatcher.Filter = "*.*";
                //m_dirWatcher.Created += new FileSystemEventHandler(OnChanged);
                m_dirWatcher.Changed += new FileSystemEventHandler(OnChanged);
                //m_dirWatcher.Deleted += new FileSystemEventHandler(OnChanged);
                //m_dirWatcher.Renamed += new RenamedEventHandler(OnRenamed);
                m_dirWatcher.EnableRaisingEvents = true;
                //this.m_dirWatcher.Created += onChanged;
        }

        private void OnChanged(object source, FileSystemEventArgs comArgs)
        {
            string filEx = Path.GetExtension(comArgs.FullPath);
            //Checks the extension is relevant.
            if (extensionsToListen.Contains(filEx))
            {
                string[] args = { comArgs.FullPath };
                CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs(
                    (int)CommandEnum.NewFileCommand, args, m_path);
                OnCommandRecieved(this, commandArgs);
            }
        }


        public void OnCommandRecieved(object o, CommandRecievedEventArgs e)
        {

            bool result;
            //If it is it's directory it listens to:
            if (e.RequestDirPath.Equals(this.m_path))
            {
                string message = this.m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
                if (result)
                {
                    string logMessage = "Added file: " + message;
                    this.m_logging.Log(logMessage, MessageTypeEnum.INFO);
                } else
                {
                    string logMessage = "Couldn't add file: " + message;
                    this.m_logging.Log(message, MessageTypeEnum.FAIL);
                }
            }
        }
        public void CloseHandler(object sender, DirectoryCloseEventArgs dirArgs)
        {
            try
            {
                //Make the watcher enable to raise events.
                this.m_dirWatcher.EnableRaisingEvents = false;
                //Remove this handler from getting commands.
                ((ImageServer)sender).CommandRecieved -= this.OnCommandRecieved;
                this.m_logging.Log("Closed Directory handler for directory: " + m_path + " ", MessageTypeEnum.INFO);
            }
            catch (Exception e)
            {
                this.m_logging.Log("Could not close Directory handler for directory: " + m_path + " Reason: " + e.Message.ToString(), MessageTypeEnum.FAIL);
            }
        }
    }
}