using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class ImageFromPhoneCommand : ICommand
    {
        public string Execute(string[] args, out bool result)
        {
            SettingsObject settings = SettingsObject.GetInstance;
            string handler = (settings.Handlers.Split(';'))[0];
            //If there is no handler
            if (handler == null)
            {
                result = false;
                return null;
            }
            if (args.Length != 2)
            {
                result = false;
                return null;
            }
            byte[] photoBytes = Convert.FromBase64String(args[0]);
            Image img = ByteArrayToImage(photoBytes);
            string name = args[1];
            string path = Path.Combine(handler, name);
            try
            {
                img.Save(path, ImageFormat.Jpeg);
                result = true;
                return "Added new file from phone with name " + name;
            }
            catch(Exception e)
            {
                result = false;
                return null;

            }
           
        }
        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}
