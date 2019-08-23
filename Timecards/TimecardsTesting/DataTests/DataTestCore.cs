using System;
using System.Configuration;
using System.IO;

namespace TimecardsTesting.DataTests
{
    public class DataTestCore
    {
        protected const string TEST_CONNECTION_STRING_NAME = "TestDb";

        protected void DeleteTestDatabase()
        {
            // force garbage collector to clean up SqlLite's open file handles
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // delete the test database if it exists
            var connString = ConfigurationManager.ConnectionStrings[TEST_CONNECTION_STRING_NAME].ConnectionString;
            var pieces = connString.Split(';');
            foreach (var piece in pieces)
            {
                if (piece.ToLower().Contains("data source="))
                {
                    var fileName = piece.Substring(piece.IndexOf("=") + 1).Trim();
                    File.Delete(fileName);
                    break;
                }
            }
        }
    }
}
