﻿using Communication.Infrastructure;
using ImageService.Commands;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        //Members.
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;
        private ILoggingService loggingService;


        public ImageController(IImageServiceModal modal, ILoggingService loggingService)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            this.loggingService = loggingService;
            //Dictionary for commands.
            commands = new Dictionary<int, ICommand>()
            {
                 {(int) CommandEnum.NewFileCommand,new NewFileCommand(m_modal)},
                {(int) CommandEnum.GetConfigCommand,new GetConfigCommand(m_modal)},
                {(int) CommandEnum.LogCommand,new LogCommand(m_modal)},
                {(int) CommandEnum.CloseCommand,new CloseCommand(m_modal)}
            };
        }
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            //Checks if the id is of a existing command.
            if (IsCommand(commandID))
            {
                this.loggingService.Log("Controller got commnad " + ((CommandEnum)commandID).ToString() + " " + args[0], MessageTypeEnum.INFO);
                //Make new task to do it in another thread.
                Task<Tuple<string, bool>> task = new Task<Tuple<string, bool>>(() =>
                 {
                   string path = commands[commandID].Execute(args, out bool res);
                     this.loggingService.Log(ImageServiceSettings.Default.Handler, MessageTypeEnum.INFO);
                   return Tuple.Create(path, res);
                 });
                //start the task.
                task.Start();
                resultSuccesful = task.Result.Item2;
                if(resultSuccesful)
                {
                    this.loggingService.Log("command " + (CommandEnum)commandID + " was successful ", MessageTypeEnum.INFO);
                } else
                {

                    this.loggingService.Log("command " + (CommandEnum)commandID + "was unsuccessful", MessageTypeEnum.FAIL);
                }
                return task.Result.Item1;
                
            }
            else
            {
                resultSuccesful = false;
                loggingService.Log("ErrorMesssage", MessageTypeEnum.FAIL);
                return "ErrorMesssage";
            }
        }

        public bool IsCommand(int commandID)
        {
            return this.commands.ContainsKey(commandID);
        }
    }
}
