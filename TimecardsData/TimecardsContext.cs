using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimecardsData
{
    /// <summary>
    /// This is the Entity Framework context class for the timecards database
    /// </summary>
    /// <param name="systemName">Name of system, for deciding which database to use.  See implementations of IAppConstants.</param>
    public class TimecardsContext(string systemName) : DbContext()
    {
        public string DbPath { get; } = TimecardsConnectionStringBuilder.BuildConnectionString(systemName);

        public DbSet<Timecard> Timecards { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
