/*
*  Filenmae        : FileIO.cs
*  Project         : EMS System
*  Programmers     : The Donkey Apocalypse
*  Date Created    : 2016-11-05
*  Description     : This file holds the FileIO class that can read, write, and parse contents of a
*                    CSV file.
*/
using System;
using System.IO;
using System.Collections.Generic;

namespace EMSSystem.FileHandling
{
    /// <summary>
    /// \class FileIO
    /// \brief <b>Description</b>
    /// \details The FileIO class allows for employee data to be read from an existing file, as well
    /// \details as allowing the system to save employees to a file. Its constructor requires
    /// \details a reference to the container, so employee information can be added/saved. It has two
    /// \dteails methods. OpenDB and SaveDB for reading and writing data.
    /// </summary>
    public static class FileIO
    {

        /// <summary>
        /// \brief <b>Description</b>
        /// \details This method opens an existing database file and tries to add each employee line
        /// \details to the collection
        /// </summary>
        public static bool LoadDB(out List<string> records, string dbFileName)
        {
            bool loaded = true;
            List<string> lines = new List<string>();
            string logMsg = "";
            try
            {
                using (StreamReader r = new StreamReader(dbFileName))
                {
                    for (int i = 0; !r.EndOfStream; i++)
                    {
                        lines.Add(r.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                logMsg = "[FileIO.SaveDB]" + ex.Message;
                Logger.Logging.Log(logMsg);
                loaded = false;
            }
            records = lines;
            return loaded;
        }

        /// <summary>
        /// \brief <b>Description</b>
        /// \details This method takes all employees in the collection and saves their information to a file.
        /// </summary>
        public static bool SaveDB(List<string> records, string dbFileName)
        {
            // write everything from allEmployees to a file
            int valid = 0;
            bool saved = true;
            string logMsg = "";
            try
            {
                using (StreamWriter w = new StreamWriter(dbFileName))
                {
                    w.WriteLine("; Date Created : " + DateTime.Now);
                    for (int i = 1; i < records.Count; i++)
                    {
                        w.WriteLine(records[i]);
                        valid++;
                    }
                }
            }
            catch (Exception ex)
            {
                logMsg = "[FileIO.SaveDB]" + ex.Message;
                Logger.Logging.Log(logMsg);
                saved = false;
            }
            finally
            {
                logMsg = String.Format("[FileIO.SaveDB] Total records: {0}", valid);
                Logger.Logging.Log(logMsg);
            }
            return saved;
        }


        /// <summary>
        /// \brief <b>Description</b>
        /// \details This method Logs the event when a user attempts to load a database from file into the Container.
        /// </summary>
        /// <param name="recordSize"></param>
        /// <param name="succeeded"></param>
        /// <param name="failed"></param>
        public static void logLoadStats(int recordSize,int succeeded, int failed)
        {
            string logMsg = String.Format("[FileIO.LoadDB] Total Valid: {0}", succeeded);
            Logger.Logging.Log(logMsg);
             logMsg = String.Format("[FileIO.LoadDB] Total Invalid: {0}", failed);
            Logger.Logging.Log(logMsg);
             logMsg = String.Format("[FileIO.LoadDB] Total Records in DB: {0}", recordSize);
            Logger.Logging.Log(logMsg);
        }
    }
}
