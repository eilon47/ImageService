using Communication.Client;
using Communication.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class LogsModel
    {
        #region Properties
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Logs")]
        public ObservableCollection<MessageRecievedEventArgs> Logs { get; set; }
        #endregion
        #region Members & Events
        private static IISClient client;
        public delegate void Refresh();
        public event Refresh NotifyRefresh;
        #endregion
        public LogsModel()
        {
            try
            {
                client = ISClient.ClientServiceIns;
                client.MessageRecieved += GetMessageFromClient;
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, null));
            } catch(Exception e)
            {
                Logs = null;
            }
        }
        public void GetMessageFromClient(object sender, string message)
        {
            //If message if log - handle and notify, else ignore.
            CommandRecievedEventArgs command = CommandRecievedEventArgs.FromJson(message);
            if (command.CommandID == (int)CommandEnum.LogCommand)
            {
                ObservableCollection<MessageRecievedEventArgs> list = new ObservableCollection<MessageRecievedEventArgs>();
                string[] logsStrings = command.Args[0].Split(';');
                foreach (string s in logsStrings)
                {
                    if (s.Contains("Status") && s.Contains("Message"))
                    {
                        try
                        {
                            MessageRecievedEventArgs m = MessageRecievedEventArgs.FromJson(s);
                            list.Add(m);
                        }
                        catch (Exception e)
                        {
                            continue;
                        }

                    }
                }
                Logs = list;
                NotifyRefresh?.Invoke();
            }
            else if (command.CommandID == (int)CommandEnum.NewLogEntryCommand)
            {
                try
                {
                    MessageRecievedEventArgs m = MessageRecievedEventArgs.FromJson(command.Args[0]);
                    ObservableCollection<MessageRecievedEventArgs> tempList = new ObservableCollection<MessageRecievedEventArgs>(Logs);
                    tempList.Add(m);
                    this.Logs = tempList;
                    NotifyRefresh?.Invoke();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
           
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            client.Write(command.ToJson());
        }
    }
}