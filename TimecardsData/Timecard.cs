using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimecardsData
{
    /// <summary>
    /// DTO class for timecard
    /// </summary>
    public class Timecard
    {
        public Timecard()
        {
            Activities = new HashSet<Activity>();
        }

        [Key]
        public int ID { get; set; }

        [Index]
        public DateTime Date { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
