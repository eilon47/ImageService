﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    /// <summary>
    /// Interface for Image service modal.
    /// </summary>
    public interface IImageServiceModal
    {
        /// <summary>
        /// The Function Addes A file to the system
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        string AddFile(string path, out bool result);
        string GetLog(string path, out bool result);
        string CloseHandler(string path, out bool result);
        string GetConfig(string path, out bool result);
    }
}
