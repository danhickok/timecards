using System;
using System.Collections.Generic;

namespace TimecardsCore.Models
{
    public class ActivityList : List<Activity>
    {
        private Timecard _timecard;

        public ActivityList(Timecard timecard) : base()
        {
            _timecard = timecard;
        }

        ~ActivityList()
        {
            _timecard = null;
        }

        public new void Add(Activity activity)
        {
            activity.RequestTimecardDate = new Func<DateTime>(delegate { return _timecard.Date; });
            base.Add(activity);
        }

        public new void Insert(int index, Activity activity)
        {
            activity.RequestTimecardDate = new Func<DateTime>(delegate { return _timecard.Date; });
            base.Insert(index, activity);
        }
    }
}
