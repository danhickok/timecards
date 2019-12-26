using System.ComponentModel.DataAnnotations;

namespace TimecardsData
{
    /// <summary>
    /// DTO class for activity
    /// </summary>
    public class Activity
    {
        [Key]
        public int ID { get; set; }

        public int TimecardID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int StartMinute { get; set; }

        public Timecard Timecard { get; set; } 
    }
}
