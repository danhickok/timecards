using SQLite.CodeFirst;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;

namespace TimecardsData
{
    /// <summary>
    /// This is the Entity Framework context class for the timecards database
    /// </summary>
    public class TimecardsContext : DbContext
    {
        public TimecardsContext(DbConnection dbConnection) : base(dbConnection, true)
        {
        }

        public DbSet<Timecard> Timecards { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var dbInitializer = new SqliteCreateDatabaseIfNotExists<TimecardsContext>(modelBuilder);
            Database.SetInitializer(dbInitializer);
        }
    }
}
