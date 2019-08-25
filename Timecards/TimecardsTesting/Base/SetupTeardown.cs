using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace TimecardsTesting.Base
{
    [TestClass]
    public class SetupTeardown
    {
        [AssemblyInitialize]
        public static void Setup(TestContext context)
        {
            // OK to delete at the beginning because first use of EF will create it
            DeleteTestDatabase();
        }

        [AssemblyCleanup]
        public static void Teardown()
        {
            DeleteTestDatabase();
        }

        private static void DeleteTestDatabase()
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
