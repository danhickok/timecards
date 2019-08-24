using System;
using System.Configuration;
using System.IO;

namespace TimecardsTesting.Base
{
    public class DataTestBase
    {
        protected void DeleteTestDatabase()
        {
            // force garbage collector to run finalizers to clean up SqlLite's open file handles
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // delete the test database if it exists
            var connStringName = new TestConnectionInfo().ConnectionStringName;
            var connString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
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
