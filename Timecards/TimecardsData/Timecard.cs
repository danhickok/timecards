using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
