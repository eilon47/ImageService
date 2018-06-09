using Communication.Client;
using Communication.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
        public int PhotosCounter { get { return PhotoCountFunc(); } }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Studnets")]
        public ObservableCollection<Student> Students { get; set; }

        public string OutputDir { get; set; }
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
                SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, null, null));

            } catch (Exception e)
            {
                Status = "No connection";
            }
        }
        public void GetMessageFromClient(object sender, string crea)
        {
            CommandRecievedEventArgs command = CommandRecievedEventArgs.FromJson(crea);
            if (command.CommandID == (int)CommandEnum.GetConfigCommand )
            {
                string m = command.Args[0];
                JObject json = JObject.Parse(m);
                OutputDir = (string)json["OutputDir"];
                string[] studs = ((string)json["Students"]).Split(';');
                Students = new ObservableCollection<Student>();
                foreach (string s in studs) {
                    Students.Add(Student.fromString(s));
                }
                Status = client.Connection ? "Running" : "Not Running";
                NotifyRefresh?.Invoke();
            }
            
        }
        private int PhotoCountFunc()
        {
            string outputDir = OutputDir;
            if (outputDir == null || outputDir == string.Empty || outputDir == " ")
                return 0;
            try
            {
                int count = Directory.GetFiles(outputDir, "*", SearchOption.AllDirectories).Length;
                return count / 2;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void SendCommandToService(CommandRecievedEventArgs command)
        {
            client.Write(command.ToJson());
        }
        private void DefaultValues()
        {
            Status = "No";
            Students = new ObservableCollection<Student>();
        }
    }
    public class Student
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "ID")]
        public string ID { get; set; }
        public static Student fromString(string s)
        {
            string[] ss = s.Split(',');
            return new Student { FullName = ss[0], ID = ss[1] };
        }
    }
}