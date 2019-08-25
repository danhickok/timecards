using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core=TimecardsCore;

namespace TimecardsData
{
    public class TimecardsContext : DbContext
    {
        public TimecardsContext(string connectionStringName) : base(connectionStringName)
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
