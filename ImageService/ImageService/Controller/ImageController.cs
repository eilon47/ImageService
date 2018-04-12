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
            // Write Code Here
            string path = commands[commandID].Execute(args,out resultSuccesful);

            if (resultSuccesful)
            {
                return path;
            }
            loggingService.Log(path, Logging.Modal.MessageTypeEnum.FAIL);
            return "ErrorMesssage";
        }
    }
}
