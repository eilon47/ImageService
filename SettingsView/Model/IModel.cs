using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Infrastructure;
namespace SettingsView.Model
{
    interface IModel : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
        void SendCommandToService(CommandRecievedEventArgs command);
        void GetMessageFromClient(object sender, string message);
    }
}
