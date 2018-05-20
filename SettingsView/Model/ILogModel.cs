using Communication.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsView.Model
{
    public interface ILogModel
    {
        #region methods

        event PropertyChangedEventHandler PropertyChanged;
        void SendCommandToService(CommandRecievedEventArgs command);
        void GetMessageFromClient(object sender, string message);
        void NotifyPropertyChanged(string propName);
        ObservableCollection<MessageRecievedEventArgs> Logs { get; set; }
        #endregion
    }
}
