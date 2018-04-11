using ImageService.Modal;
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


        public DirectoyHandler(string dirPath, ILoggingService loggingService, IImageController imageController)
        {
            string func = "DirectoyHandler constructor\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
        }	

            this.m_dirWatcher = new FileSystemWatcher(dirPath);
            this.m_controller = imageController;
            this.m_logging = loggingService;
            this.m_path = dirPath;
        }


        public void StartHandleDirectory(string dirPath)
        {
            string func = "StartHandleDirectory\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
        }	
            string startMessage = "Handeling directory: " + dirPath;
            this.m_logging.Log(startMessage, MessageTypeEnum.INFO);
            initializeWatcher(dirPath);
        }



        public void OnCommandRecieved(object o, CommandRecievedEventArgs e)
        {
            string func = "OnCommandRecieved\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
        }	
            bool result;
            if (e.RequestDirPath.Equals(this.m_path))
            {
                string message = this.m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
                if(result)
                {
                    this.m_logging.Log(message, MessageTypeEnum.INFO);
                } else
                {
                    this.m_logging.Log(message, MessageTypeEnum.FAIL);
                }
            }
        }



        private void onChanged(object o, FileSystemEventArgs comArgs)
        {
            string func = "onChanged\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
        }	
            //this method should check the extension of the file and if it fits than uses OnCommandRecieved
            string fileExtension = Path.GetExtension(comArgs.FullPath);

            if (extensionsToListen.Contains(fileExtension))
            {
                string[] args = { comArgs.FullPath };
                CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int) CommandEnum.NewFileCommand,
                    args, m_path);
                OnCommandRecieved(this, commandArgs);
            }
        }

        public void initializeWatcher(string dirPath)
        {
            string func = "initializeWatcher\n";
            string path = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(path)) 
        {
            sw.WriteLine(func);
        }	
            m_dirWatcher.Path = dirPath;
            m_dirWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            m_dirWatcher.Filter = "*.*";
            //m_dirWatcher.Created += new FileSystemEventHandler(onChanged);
            m_dirWatcher.Changed += new FileSystemEventHandler(onChanged);
            m_dirWatcher.EnableRaisingEvents = true;
            //this.m_dirWatcher.Created += onChanged;
        }
    }
}