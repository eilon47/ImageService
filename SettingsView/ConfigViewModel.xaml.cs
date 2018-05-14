﻿using SettingsView.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ConfigViewModel.xaml
    /// </summary>
    public partial class ConfigViewModel : UserControl,INotifyPropertyChanged
    {
        private IConfigModel configModel;
        private ObservableCollection<string> handlers = new ObservableCollection<string>();
        public ConfigViewModel()
        {
            this.configModel = new ConfigModel();
            this.DataContext = configModel;
            
            InitializeComponent();
            //adds the NotifyPropertyChanged to the model.
            configModel.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs args)
                {
                    NotifyPropertyChanged(args.PropertyName);
                };
           

            //pointing the data context to the model as source.
            handlerView.ItemsSource = handlers;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        

        private void RmvBtn_Click(object sender, RoutedEventArgs e)
        {
            if (handlerView.SelectedItem != null)
                handlers.Remove(handlerView.SelectedItem as string);
        }
    }
}
