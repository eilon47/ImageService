using ImageService.Commands;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class LogCommand : ICommand
    {
        //Members
        private IImageServiceModal m_modal;

        /// <summary>
        /// Constructors.
        /// </summary>
        /// <param name="modal">Service Modal</param>
        public LogCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }
        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and will return the error message

            return this.m_modal.GetLog(args[0], out result);

        }
    }

}
