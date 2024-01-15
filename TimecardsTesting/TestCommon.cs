using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using TC = TimecardsCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Configuration;


namespace TimecardsTesting
{
    public static class TestCommon
    {
        public static void InitializeTestConfiguration()
        {
            // testing configuration initialized - do NOT call Load()
            TC.Configuration.CodeMask = "#####";
            TC.Configuration.DefaultCodes.Clear();
            TC.Configuration.MidnightTint = Color.CornflowerBlue;
            TC.Configuration.RoundCurrentTimeToMinutes = 5;
            TC.Configuration.TimeMask = "00:00";
            TC.Configuration.TimeSeparator = ':';
            TC.Configuration.Use24HourTime = true;

            // for control over date/time during testing
            TC.Configuration.TestMode = true;
        }

        public static void DeleteTestDatabase()
        {
            // force garbage collector to run finalizers to clean up SQLite's open file handles
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // delete the test database if it exists
            var connStringName = TestAppConstants.ConnectionStringName;
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
