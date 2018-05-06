using ImageService.Commands;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class GetConfigCommand : ICommand
    {
        //Members
        private IImageServiceModal m_modal;

        /// <summary>
        /// Constructors.
        /// </summary>
        /// <param name="modal">Service Modal</param>
        public GetConfigCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }
        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and will return the error message
            File.AppendAllText(@"C:\Users\eilon\Desktop\אילון\handle.txt", "in Execute of getconfig command: " + Environment.NewLine);
            return this.m_modal.GetConfig(args[0], out result);

        }
    }
}
