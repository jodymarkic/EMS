/*
 *  FILENAME        : FileIOTestCases.cs
 *  PROJECT         : EMSSystem
 *  PROGRAMMER      : The Donkey Apocalypse
 *  FIRST VERSION   : 2016-12-08
 *  DESCRIPTION     : This file hold all test cases for the FILEIO class.
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EMSSystem.TheCompany;
using EMSSystem.FileHandling;
using System.IO;
using System.Collections.Generic;

namespace EMSSystemUnitTestProject
{
    /// <summary>
    /// \class FileIOTestCases
    /// \brief <b>Description</b>
    /// \details>Description</b>
    /// \details This class acts as a way for to Unit Test a Methods of the FileIO Class
    /// that need potentially normal, exception, or boundary testing.
    /// \author <i>The Donkey Apocalypse</i>
    /// </summary>
    [TestClass]
    public class FileIOTestCases
    {

        // [TestMethod] should come before all methods

        /// \test <b>FIO.1.1</b><br />&emsp;
        /// Reading valid employees from a database file
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> File contents:  FT|Bray|Nathan|333333334|1993-09-15|2012-04-12|2015-08-13|54749.00| <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - One line is read <br />&emsp;
        /// Actual - ACTUAL OUTPUT <br />
        [TestMethod]
        public void DBReadT1()
        {
            List<string> lines = new List<string>();
            FileIO.LoadDB(out lines, "test1.csv");
            int numRecords = 0;
            numRecords = lines.Count;
            Assert.AreEqual(1, numRecords);
        }

        /// \test <b>FIO.1.2</b><br />&emsp;
        /// Reading invalid employees from a database file
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> File contents: SN|Clarke|Sean|333333333|1950-05-05|WINTER|0.15| <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - 2 lines are read <br />&emsp;
        /// Actual - ACTUAL OUTPUT <br />
        [TestMethod]
        public void DBReadT2()
        {
            List<string> lines = new List<string>();
            FileIO.LoadDB(out lines, "test2.csv");
            int numRecords = 0;
            numRecords = lines.Count;
            Assert.AreEqual(2, numRecords);
        }

        /// \test <b>FIO.2.1</b><br />&emsp;
        /// Writing to a database file.
        /// <br /><b>Method of Execution: </b> Automatic <br />
        /// <b>Inputs:</b> File contents: FT|Bray|Nathan|333333334|1993-09-15|2012-04-12|2015-08-13|54749 <br />
        ///                               PT|Cocca|Matthew|123456782|1993-09-15|2012-04-12|2015-08-13|54749 <br />
        /// <b>Output: </b><br />&emsp;
        /// Expected - Database file has 2 records <br />&emsp;
        /// Actual - ACTUAL OUTPUT <br />
        [TestMethod]
        public void DBWriteT1()
        {
            List<string> outlines = new List<string>();
            outlines.Add("FT|Bray|Nathan|333333334|1993-09-15|2012-04-12|2015-08-13|54749");
            outlines.Add("PT|Cocca|Matthew|123456782|1993-09-15|2012-04-12|2015-08-13|54749");
            int inlines = 0;
            FileIO.SaveDB(outlines, "test3-out.csv");
            using (StreamReader r = new StreamReader("test3-out.csv"))
            {
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    inlines++;
                }
            }
            File.Delete("test3-out.csv");
            Assert.AreEqual(2, inlines);
        }
    }
}
