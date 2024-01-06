using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimecardsData
{
    /// <summary>
    /// DTO class for timecard
    /// </summary>
    [Table("Timecards")]
    [Index(nameof(Date), IsUnique = true)]
    public class Timecard
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public List<Activity> Activities { get; } = [];
    }
}
