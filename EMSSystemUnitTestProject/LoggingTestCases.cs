/*
 *  FILENAME        : LoggingTestCases.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016-12-08
 *  DESCRIPTION     : This file hold all test cases for the Logging class.
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMSSystem.Logger;
using EMSSystem;
using System.IO;

namespace EMSSystemUnitTestProject
{
    [TestClass]
    public class LoggingTestCases
    {
        // [TestMethod] should come before all methods

        /// \test <b>LOG.1.1</b><br />&emsp;
        /// Log to a file, happy path
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> INPUTS <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - true <br />&emsp;
        /// Actual - ACTUAL OUTPUT <br />
        [TestMethod()]
        public void LogT1()
        {
           // Logging logger = new Logging();
            string testMsg = "Testing...";
            DateTime today = DateTime.Now;
            string logFileName = "ems." + today.ToString("yyy-MM-dd") + ".log";
            if (File.Exists(logFileName)) // in case a log file was created by another test get rid of it
            {
                File.Delete(logFileName);
            }
           // Logger.Log(testMsg);


            bool expected = true;
            bool actual = false;

            using (StreamReader r = new StreamReader(logFileName))
            {
                if (r.ReadLine() == (today.ToString("yyyy-MM-dd hh:mm:ss") + " " + testMsg))
                {
                    actual = true;
                }
            }
            File.Delete(logFileName);
            Assert.AreEqual(expected, actual);
        }
    }
}
