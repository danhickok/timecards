using Microsoft.EntityFrameworkCore;

namespace TimecardsData
{
    /// <summary>
    /// This is the Entity Framework context class for the timecards database
    /// </summary>
    public class TimecardsContext(string connectionString) : DbContext()
    {
        //TODO: for building migrations, try making this connectionString an optional argument, and
        // if it's null, supply it here by some other means
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(connectionString);
            base.OnConfiguring(options);
        }

        public DbSet<Timecard> Timecards { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
