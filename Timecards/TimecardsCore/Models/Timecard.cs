using System;
using System.Collections.Generic;

namespace TimecardsCore.Models
{
    public class Timecard
    {
        #region Public properties

        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                IsDirty = true;
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                IsDirty = true;
            }
        }

        public readonly List<Activity> Activities;

        public bool IsDirty { get; private set; }

        #endregion

        #region Constructors

        public Timecard()
        {
            _id = 0;
            _date = DateTime.Today;
            Activities = new List<Activity>();
            IsDirty = false;
        }

        public Timecard(int id, DateTime date)
        {
            _id = id;
            _date = date;
            Activities = new List<Activity>();
            IsDirty = false;
        }

        #endregion
    }
}
