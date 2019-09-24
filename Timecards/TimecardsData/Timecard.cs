using System;
using System.Collections.Generic;

namespace TimecardsData
{
    public class Timecard
    {
        public Timecard()
        {
            Activities = new HashSet<Activity>();
        }

        public int ID { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
