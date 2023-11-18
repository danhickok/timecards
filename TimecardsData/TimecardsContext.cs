using Microsoft.EntityFrameworkCore;

namespace TimecardsData
{
    /// <summary>
    /// This is the Entity Framework context class for the timecards database
    /// </summary>
    public class TimecardsContext : DbContext
    {
        public TimecardsContext(DbContextOptions<TimecardsContext> options) : base(options)
        {
            //TODO: when this is instantiated, we'll want to pass options as options.UseSqlite($"Data Source={DbPath}")
            // see for examples:
            // https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
        }

        public DbSet<Timecard> Timecards => Set<Timecard>();
        public DbSet<Activity> Activities => Set<Activity>();
    }
}
