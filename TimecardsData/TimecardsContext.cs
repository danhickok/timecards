using Microsoft.EntityFrameworkCore;

namespace TimecardsData
{
    /// <summary>
    /// This is the Entity Framework context class for the timecards database
    /// </summary>
    public class TimecardsContext : DbContext
    {
        private readonly string _connectionString;

        public TimecardsContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        // For building migrations, this parameterless constructor is needed for sake of the
        // "dotnet ef migrations" commands.  Don't use this in production!
        public TimecardsContext() : base()
        {
            _connectionString = TimecardsConnectionStringBuilder.BuildConnectionString("TestDb");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_connectionString);
            base.OnConfiguring(options);
        }

        public DbSet<Timecard> Timecards { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
