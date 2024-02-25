using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TimecardsData;
using TC = TimecardsCore;


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

        public static void CreateTestDatabase()
        {
            // create the database and perform all necessary migrations
            var connectionString = TimecardsConnectionStringBuilder.BuildConnectionString((new TestAppConstants()).SystemName);
            using var context = new TimecardsContext(connectionString);
            context.Database.Migrate();
        }

        public static void DeleteTestDatabase()
        {
            // force garbage collector to run finalizers to clean up SQLite's open file handles
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // destroy the database
            var connectionString = TimecardsConnectionStringBuilder.BuildConnectionString((new TestAppConstants()).SystemName);
            using var context = new TimecardsContext(connectionString);
            context.Database.EnsureDeleted();
        }
    }
}
