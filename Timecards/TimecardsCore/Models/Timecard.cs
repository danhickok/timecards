using Newtonsoft.Json;
using System;

namespace TimecardsCore.Models
{
    public class Timecard
    {
        #region Public properties

        private int _id;
        [JsonIgnore]
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

        public readonly ActivityList Activities;

        [JsonIgnore]
        public bool IsDirty { get; private set; }

        #endregion

        #region Constructors

        public Timecard()
        {
            _id = 0;
            _date = DateTime.Today;
            Activities = new ActivityList(this);
            IsDirty = false;
        }

        public Timecard(int id, DateTime date)
        {
            _id = id;
            _date = date;
            Activities = new ActivityList(this);
            IsDirty = false;
        }

        #endregion
    }
}
