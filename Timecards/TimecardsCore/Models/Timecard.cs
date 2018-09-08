using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Models
{
    public class Timecard
    {
        public DateTime Day { get; set; }
        public List<Action> Actions { get; private set; }

        public Timecard()
        {
            Day = DateTime.Today;
            Actions = new List<Action>();
        }
    }
}
