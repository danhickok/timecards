using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimecardsData
{
    /// <summary>
    /// DTO class for activity
    /// </summary>
    [Table("Activities")]
    public class Activity
    {
        [Key]
        public int ID { get; set; }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public int StartMinute { get; set; }

        [ForeignKey("Timecard")]
        public int TimecardID { get; set; }

        public Timecard? Timecard { get; set; }
    }
}
