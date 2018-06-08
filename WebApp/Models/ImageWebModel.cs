using Communication.Client;
using Communication.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class ImageWebModel
    {
        #region Properties
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhotosCounter")]
        public int PhotosCounter { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Studnets")]
        public ObservableCollection<string> Students { get; set; }
        #endregion
        #region Members & Events
        private static IISClient client;
        public delegate void Refresh();
        public event Refresh NotifyRefresh;
        #endregion
        public ImageWebModel()
        {
            DefaultValues();
            try
            {
                client = ISClient.ClientServiceIns;
                client.MessageRecieved += GetMessageFromClient;
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetServiceDetails, null, null));

            } catch (Exception e)
            {
               
            }
        }
        public void GetMessageFromClient(object sender, string crea)
        {
            CommandRecievedEventArgs command = CommandRecievedEventArgs.FromJson(crea);
            if (command.CommandID == (int)CommandEnum.GetServiceDetails )
            {
                string m = command.Args[0];
                JObject json = JObject.Parse(m);
                PhotosCounter = int.Parse((string)json["PhotoCounter"]);
                string s = (string)json["Students"];
                Students = new ObservableCollection<string>(s.Split(';'));
                Status = client.Connection.ToString();
                
            }
            NotifyRefresh?.Invoke();
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            client.Write(command.ToJson());
        }
        private void DefaultValues()
        {
            Status = "No";
            PhotosCounter = 0;
            Students = null;
        }
    }
}