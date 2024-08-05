using System.Text.Json.Serialization;

namespace TimecardsCore.Models
{
    /// <summary>
    /// This class holds a timecard and its corresponding list of activities
    /// </summary>
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

        // Note: While it's undesirable to expose the "set" functionality here, both get
        // and set are required if System.Text.Json.Deserialize is going to work.
        public List<Activity> Activities { get; set; }

        [JsonIgnore]
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

        #region Public Methods

        /// <summary>
        /// This method will set the activity's copy of the timecard date after adding it to the timecard's collection.
        /// Use this instead of accessing the Activities property directly to make sure the IsAfterMidnight flag is set.
        /// </summary>
        /// <param name="activity">Activity object</param>
        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
            activity.SetTimecardDate(Date);
        }

        /// <summary>
        /// This will set the activity's copy of the timecard date after inserting it to the timecard's collection.
        /// Use this instead of accessing the Activities property directly to make sure the IsAfterMidnight flag is set.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="activity"></param>
        public void InsertActivity(int index, Activity activity)
        {
            Activities.Insert(index, activity);
            activity.SetTimecardDate(Date);
        }

        #endregion
    }
}
