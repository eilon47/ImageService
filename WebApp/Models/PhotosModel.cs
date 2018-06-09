using Communication.Client;
using Communication.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PhotosModel
    {
        #region Properties
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Photos")]
        public List<PhotoDetails> Photos { get; set; }
        #endregion
        #region Members & Events
        private static IISClient client;
        public delegate void Refresh();
        public event Refresh NotifyRefresh;
        #endregion
        public PhotosModel()
        {
            try
            {
                client = ISClient.ClientServiceIns;
                client.MessageRecieved += GetMessageFromClient;
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetPhotoDetailsList, null, null));
            } catch(Exception e)
            {
                Photos = null;
            }
        }
        public void GetMessageFromClient(object sender, string message)
        {
            //If message if log - handle and notify, else ignore.
            CommandRecievedEventArgs command = CommandRecievedEventArgs.FromJson(message);
            if (command.CommandID == (int)CommandEnum.GetPhotoDetailsList)
            {
                Photos = new List<PhotoDetails>();
                string[] photos = command.Args[0].Split(';');
                foreach (string s in photos)
                {
                    try
                    {
                        PhotoDetails p = PhotoDetails.FromJson(s);
                        Photos.Add(p);
                    } catch(Exception e)
                    {
                        continue;
                    }
                }
            }
            else if (command.CommandID == (int)CommandEnum.NewFileCommand)
            {
                try
                {
                    PhotoDetails p = PhotoDetails.FromJson(command.Args[0]);
                    List<PhotoDetails> tempList = new List<PhotoDetails>(Photos);
                    tempList.Add(p);
                    this.Photos = tempList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            } else if (command.CommandID == (int)CommandEnum.RemovePhoto)
            {
                try
                {
                    PhotoDetails p = PhotoDetails.FromJson(command.Args[0]);
                    List<PhotoDetails> list = new List<PhotoDetails>(Photos);
                    list.Remove(p);
                    Photos = list;
                } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            NotifyRefresh?.Invoke();
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            client.Write(command.ToJson());
        }
    }
}