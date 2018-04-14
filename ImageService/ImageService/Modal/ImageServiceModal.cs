//using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            try
            {
    
                if (File.Exists(path))
                {
                    DateTime creation = this.GetDateTime(path);
                    string thumbnailPath = this.OutputFolder + "\\Thumbnails";
                    string year = creation.Year.ToString();
                    string month = getMonthName(creation.Month.ToString());
                    string name = Path.GetFileName(path);

                    //creates the outputDir and ThumbnailsDir if not exist.
                    DirectoryInfo dir =  Directory.CreateDirectory(this.OutputFolder) ;
                    dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    Directory.CreateDirectory(thumbnailPath);
                    
                    //Create the directory for the year
                    Directory.CreateDirectory(this.OutputFolder + "\\" + year);
                    Directory.CreateDirectory(thumbnailPath + "\\" + year);
                    
                    //Create the directory for the month
                    string loc = this.OutputFolder + "\\" + year + "\\" + month ;
                    DirectoryInfo locationToCopy = Directory.CreateDirectory(loc);
                     //Create the thumbnails directory for the month
                    string thumLoc = thumbnailPath + "\\" + year + "\\" + month;
                    DirectoryInfo locationToCopyThumbnail =Directory.CreateDirectory(thumLoc);
                    
                    //move the file to new direcory.
                    string dstFile = System.IO.Path.Combine(loc, name);
                    if(File.Exists(dstFile)) 
                    {
                        File.Delete(path);
                        result = true;
                        return "The file already exist";
                    }
                    //File.Create(dstFile);
                    File.Move(path, dstFile);
                    //Save the thumbnail image.
                    string dstThum = System.IO.Path.Combine(thumLoc, name);
                    Image thumbImage = Image.FromFile(dstFile);
                    thumbImage = thumbImage.GetThumbnailImage(this.m_thumbnailSize,
                        this.m_thumbnailSize, () => false, IntPtr.Zero);
                    thumbImage.Save(dstThum);
                    result = true;
                    return dstFile;               
                }
                else
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
        /// <summary>
        /// GetDateTime
        /// trying to get the date when the pic was taken, otherwise returns the creation date.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DateTime GetDateTime(string path)
        {
            Regex r = new Regex(":");
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    DateTime dt = DateTime.Parse(dateTaken);
                    return dt;
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return File.GetCreationTime(path);

            }
        }

        public string getMonthName(string MonthNum)
        {
           if(MonthNum.Equals("1")) { return "January"; }
           if(MonthNum.Equals("2")) { return "February"; }
           if(MonthNum.Equals("3")) { return "March"; }
           if(MonthNum.Equals("4")) { return "April"; }
           if(MonthNum.Equals("5")) { return "May"; }
           if(MonthNum.Equals("6")) { return "June"; }
           if(MonthNum.Equals("7")) { return "July"; }
           if(MonthNum.Equals("8")) { return "August"; }
           if(MonthNum.Equals("9")) { return "September"; }
           if(MonthNum.Equals("10")) { return "October"; }
           if(MonthNum.Equals("11")) { return "November"; }
           if(MonthNum.Equals("12")) { return "December"; }
           return MonthNum;
        }
    }
  
}
