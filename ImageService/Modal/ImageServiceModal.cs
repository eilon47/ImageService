using ImageService.Infrastructure;
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
using ImageService.Logging;


namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {

        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        private ILoggingService ls;
        #endregion

        string AddFile(string path, out bool result)
        {
            //Check/Create if the output dir exists.
            //add file to the direcorty.
            //make thumbnail.
            //done
            //result = true/false 
            string stringResult;
            if (result) {
                stringResult = "Success adding file";
            } else
            {
                stringResult = "Failed adding file";
            }
            ls.Log(stringResult, MessageTypeEnum.INFO);
            //and return.

        }
    }
}
