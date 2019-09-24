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

        public int ID
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
                IsDirty = true;
            }
        }

        public DateTime Date
        {
            get
            {
                return Date;
            }
            set
            {
                Date = value;
                IsDirty = true;
            }
        }

        public readonly List<Activity> Activities;

        public bool IsDirty { get; private set; }

        #endregion

        #region Constructors

        public Timecard()
        {
            ID = 0;
            Date = DateTime.Today;
            Activities = new List<Activity>();
            IsDirty = false;
        }

        public Timecard(int id, DateTime date)
        {
            ID = id;
            Date = date;
            Activities = new List<Activity>();
            IsDirty = false;
        }

        #endregion
    }
}
