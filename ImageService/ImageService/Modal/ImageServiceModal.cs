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
                    using (StreamWriter sw = File.AppendText(pth)) 
                    {
                    sw.WriteLine("got so far1");
                    DateTime creation = File.GetCreationTime(path);
                    string thumbnailPath = this.OutputFolder + "\\Thumbnails";
                    string year = creation.Year.ToString();
                    string month = creation.Month.ToString();
                    string name = Path.GetFileName(path);

                    //creates the outputDir and ThumbnailsDir if not exist.
                    Directory.CreateDirectory(this.OutputFolder);

                    Directory.CreateDirectory(thumbnailPath);
                    
                    sw.WriteLine("got so far2");

                    
                    //Create the directory for the year
                    Directory.CreateDirectory(this.OutputFolder + "\\" + year);
                    Directory.CreateDirectory(thumbnailPath + "\\" + year);
                    sw.WriteLine("got so far3");

                    //Create the directory for the month
                    string loc = this.OutputFolder + "\\" + year + "\\" + month ;
                    DirectoryInfo locationToCopy = Directory.CreateDirectory(loc);
                     //Create the thumbnails directory for the month
                    string thumLoc = thumbnailPath + "\\" + year + "\\" + month;
                    DirectoryInfo locationToCopyThumbnail =Directory.CreateDirectory(thumLoc);
                    sw.WriteLine(path);
                    //copy the file to new direcory.
                    string dstFile = System.IO.Path.Combine(loc, name);
                    File.Move(path, dstFile);
                    
                    sw.WriteLine("got so far4");

                    //Save the thumbnail image.
                    string dstThum = System.IO.Path.Combine(thumLoc, name);
                    Image thumbImage = Image.FromFile(dstFile);
                    thumbImage = thumbImage.GetThumbnailImage(this.m_thumbnailSize, this.m_thumbnailSize, () => false, IntPtr.Zero);
                    thumbImage.Save(dstThum);
                    sw.WriteLine("got so far5");
                    result = true;
                    return locationToCopy.ToString() + "\\" + name;
                        }
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
