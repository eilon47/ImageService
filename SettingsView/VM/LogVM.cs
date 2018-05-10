using Communication.Infrastructure;
using SettingsView.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsView.VM
{
    public class LogVM : INotifyPropertyChanged
    {
        private LogModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public LogVM()
        {
            model = new LogModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventArgs p = new PropertyChangedEventArgs(propName);
            PropertyChanged?.Invoke(this, p);
        }
        public ObservableCollection<MessageRecievedEventArgs> LogsList 
        {
            get { return model.Logs; }
        }
    }
}
