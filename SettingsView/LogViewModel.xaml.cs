using SettingsView.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SettingsView
{
    /// <summary>
    /// Interaction logic for LogViewModel.xaml
    /// </summary>
    public partial class LogViewModel : UserControl, INotifyPropertyChanged
    {
        private IModel logModel;

        public LogViewModel()
        {
            this.logModel = new LogModel();
            InitializeComponent();
            this.DataContext = logModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
