using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;
        private ILoggingService loggingService;


        public ImageController(IImageServiceModal modal, ILoggingService loggingService)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            this.loggingService = loggingService;
            commands = new Dictionary<int, ICommand>()
            {
                // For Now will contain NEW_FILE_COMMAND
                {(int) CommandEnum.NewFileCommand,new NewFileCommand(m_modal)},
                //{(int) CommandEnum.DeleteCommand,new DeleteCommand(m_modal)},

            };
        }
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            if(isCommand(commandID))
            {
               Task<Tuple<string,bool>> task = new Task<Tuple<string, bool>>(() =>
               {
                    string path = commands[commandID].Execute(args,out bool res);
                    return Tuple.Create(path,res);
               });
               task.Start();
               resultSuccesful = task.Result.Item2;
               if (resultSuccesful)
               {
                    return task.Result.Item1;
               }
            }
            resultSuccesful = false;
            loggingService.Log("ErrorMesssage", Logging.Modal.MessageTypeEnum.FAIL);
            return "ErrorMesssage";
        }

        public bool isCommand(int commandID)
        {
            return this.commands.ContainsKey(commandID);
        }
    }
}
