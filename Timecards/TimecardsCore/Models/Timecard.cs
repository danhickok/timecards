using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Models
{
    public class Timecard
    {
        #region Public properties

        public int ID { get; set; }
        public DateTime Date { get; set; }
        public List<Activity> Activities { get; private set; }

        #endregion

        #region Constructors

        public Timecard()
        {
            Date = DateTime.Today;
            Activities = new List<Activity>();
        }

        #endregion
    }
}
