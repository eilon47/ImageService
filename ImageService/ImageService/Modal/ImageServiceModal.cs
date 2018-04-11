//using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        public string OutputFolder
        {
            get { return m_OutputFolder; }
            set { m_OutputFolder = value; }
        }
        public int ThumbnailSize
        {
            get { return m_thumbnailSize; }
            set { m_thumbnailSize = value; }
        }
        #endregion
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size

        public string AddFile(string path, out bool result)
        {
            string func = "AddFile\n";
            string pth = @"C:\Users\green\Desktop\hello.txt"; 
            using (StreamWriter sw = File.AppendText(pth)) 
        {
            sw.WriteLine(func);
        }	
            try
            {
                if (File.Exists(path))
                {
                    string name = Path.GetFileName(path);
                    //Creates(if it is not already exists) the hidden directory.
                    Directory.CreateDirectory(this.OutputFolder);
                    //Creates the thumbnail directory in the hidden directory.
                    string thumbnailPath = this.OutputFolder + "\\Thumbnails";
                    Directory.CreateDirectory(thumbnailPath);
                   
                    DateTime creation = File.GetCreationTime(path);
                    string year = creation.Year.ToString();
                    string month = creation.Month.ToString();
                    //Create the directory for the year
                    Directory.CreateDirectory(this.OutputFolder + "\\" + year);
                    Directory.CreateDirectory(thumbnailPath + "\\" + year);
                    //Create the directory for the month
                    DirectoryInfo locationToCopy = Directory.CreateDirectory(this.OutputFolder + "\\" + year + "\\" + month);
                    DirectoryInfo locationToCopyThumbnail =Directory.CreateDirectory(thumbnailPath + "\\" + year + "\\" + month);
                    File.Copy(path, locationToCopy.ToString());
                    //Save the thumbnail image.
                    Image thumbImage = Image.FromFile(path);
                    thumbImage = thumbImage.GetThumbnailImage(this.m_thumbnailSize, this.m_thumbnailSize, () => false, IntPtr.Zero);
                    thumbImage.Save(locationToCopyThumbnail.ToString() + "\\" + name);

                    result = true;
                    return locationToCopy.ToString() + "\\" + name;
                } else
                {
                    result = false;
                    return "Image does not exist!";
                }
            }
            catch(Exception e)
            {
                result = false;
                return e.ToString();
            }

        }

    }
  
}
