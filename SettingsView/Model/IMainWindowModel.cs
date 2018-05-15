using Communication.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsView.Model
{
    public interface IMainWindowModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        void SendCloseCommandToService();
        void NotifyPropertyChanged(string propName);
        bool IsConnected { get; }
    }
}
