using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimecardsData
{
    /// <summary>
    /// This is the Entity Framework context class for the timecards database
    /// </summary>
    public class TimecardsContext() : DbContext()
    {
        public DbSet<Timecard> Timecards { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
