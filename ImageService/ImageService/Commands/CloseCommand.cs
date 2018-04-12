using ImageService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService
{
    class CloseCommand : ICommand
    {
        private IImageServiceModal m_modal;
    }
}
