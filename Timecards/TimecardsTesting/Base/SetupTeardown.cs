using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using tc = TimecardsCore;

namespace TimecardsTesting.Base
{
    [TestClass]
    public class SetupTeardown
    {
        [AssemblyInitialize]
        public static void Setup(TestContext context)
        {
            // testing configuration here - do NOT call Load()
            tc.Configuration.CodeMask = "#####";
            tc.Configuration.DefaultCodes.Clear();
            tc.Configuration.MidnightTint = Color.CornflowerBlue;
            tc.Configuration.RoundCurrentTimeToMinutes = 5;
            tc.Configuration.TimeMask = "00:00";
            tc.Configuration.TimeSeparator = ':';
            tc.Configuration.Use24HourTime = true;

            // for control over date/time during testing
            tc.Configuration.TestMode = true;

            // OK to delete database at the beginning because first use of EF will create it
            DeleteTestDatabase();
        }

        [AssemblyCleanup]
        public static void Teardown()
        {
            DeleteTestDatabase();

            tc.Configuration.TestMode = false;
        }

        private static void DeleteTestDatabase()
        {
            // force garbage collector to run finalizers to clean up SqlLite's open file handles
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // delete the test database if it exists
            var connStringName = new TestAppConstants().ConnectionStringName;
            var connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            var pieces = connString.Split(';');
            foreach (var piece in pieces)
            {
                if (piece.ToLower().Contains("data source="))
                {
                    var fileName = piece.Substring(piece.IndexOf("=") + 1).Trim()
                        .Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

                    File.Delete(fileName);
                    break;
                }
            }
        }
    }
}
