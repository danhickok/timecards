namespace TimecardsCore.Models
{
    /// <summary>
    /// This class exposes the parents' date value to each child by way of a delegate
    /// </summary>
    public class ActivityList(Timecard timecard) : List<Activity>()
    {
        private readonly Timecard _timecard = timecard;

        /// <summary>
        /// Set this property to True to prevent activities from being marked as "after midnight" when
        /// adding them to timecards with old dates
        /// </summary>
        public bool DataImportMode = false;

        public new void Add(Activity activity)
        {
            if (!DataImportMode)
            {
                activity.RequestTimecardDate = new Func<DateTime>(delegate { return _timecard.Date; });
            }
            base.Add(activity);
        }

        public new void Insert(int index, Activity activity)
        {
            if (!DataImportMode)
            {
                activity.RequestTimecardDate = new Func<DateTime>(delegate { return _timecard.Date; });
            }
            base.Insert(index, activity);
        }
    }
}
