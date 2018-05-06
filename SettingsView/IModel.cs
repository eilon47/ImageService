using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsView
{
    interface IModel : INotifyPropertyChanged
    {
        void SendCommandToService();
        void GetMessageFromClient(object sender, string message);
    }
}
