﻿using Communication.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    /// <summary>
    /// Interface for our service Logger.
    /// </summary>
    public interface ILoggingService
    {
        #region properties,events
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        List<MessageRecievedEventArgs> LogList { get; }

        #endregion
        #region methods
        /// <summary>
        /// Log : 
        /// write entrys for the service's eventsLog.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="type">type</param>
        void Log(string message, MessageTypeEnum type);           // Logging the Message
        #endregion
    }
}
