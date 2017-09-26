/*
*  Filenmae        : Logger.cs
*  Project         : EMS System
*  Programmers     : The Donkey Apocalypse
*  Date Created    : 2016-11-05
*  Description     : This file holds the Logger class that logs events to a log file.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EMSSystem.Logger
{
    /// <summary>
    /// \class Logging
    /// \brief <b>Description</b>
    /// \details This class allows for important messages about the system to be logged to a file.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// \brief <b>Description</b>
        /// \details Log() will attach a timestamp to the message sent, and then will write the message
        ///  to the log file for the day. If there is no log file for the current day, one will
        ///  be created.
        /// \param string <i>message</i> - information about an event to be logged
        /// </summary>
        public static void Log(string message)
        {
            DateTime today = DateTime.Now;
            string logFileName = "ems." + today.ToString("yyy-MM-dd") + ".log";
            using (StreamWriter w = File.AppendText(logFileName))
            {
                string logEntry = today.ToString("yyyy-MM-dd hh:mm:ss") + " " + message;
                w.WriteLine(logEntry);
            }
        }
    }
}
