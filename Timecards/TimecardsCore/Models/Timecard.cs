using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Models
{
    public class Timecard
    {
        public DateTime Date { get; set; }
        public List<Activity> Activities { get; private set; }

        public Timecard()
        {
            Date = DateTime.Today;
            Activities = new List<Activity>();
        }
    }
}
