using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core = TimecardsCore.Models;

namespace TimecardsData
{
    public class Repository : IDisposable
    {
        private TimecardsContext _context = null;

        #region Constructors

        public Repository() : this("TimecardsDb")
        {
        }

        public Repository(string connectionStringName)
        {
            _context = new TimecardsContext(connectionStringName);
        }

        #endregion

        public List<core.Timecard> GetTimecards()
        {
            return _context.Timecards
                .Select(t => t.ToCore())
                .ToList();
        }

        public core.Timecard GetTimecard(int id)
        {
            return _context.Timecards
                .Where(t => t.ID == id)
                .Select(t => t.ToCore())
                .FirstOrDefault();
        }

        public core.Timecard GetTimecard(DateTime date)
        {
            return _context.Timecards
                .Where(t => t.Date == date)
                .Select(t => t.ToCore())
                .FirstOrDefault();
        }

        public void SaveTimecard(core.Timecard timecard)
        {
            Timecard data;

            if (timecard.ID != 0)
            {
                data = _context.Timecards.Find(timecard.ID);
                if (data == null)
                    throw new NotFoundException();
                data.UpdateFromCore(timecard);
                _context.SaveChanges();
            }
            else
            {
                data = timecard.ToData();
                _context.Timecards.Add(data);
                _context.SaveChanges();
            }

            timecard.UpdateFromData(data);
        }

        public void DeleteTimecard(int id)
        {
            var data = _context.Timecards.Find(id);
            if (data != null)
            {
                _context.Timecards.Remove(data);
                _context.SaveChanges();
            }
        }

        public void GetActivities(core.Timecard timecard)
        {
            var activities = _context.Activities
                .Where(a => a.TimecardID == timecard.ID)
                .OrderBy(a => a.Time)
                .Select(a => a.ToCore())
                .ToList();

            timecard.Activities.Clear();
            timecard.Activities.AddRange(activities);
        }

        public void SaveActivities(core.Timecard timecard)
        {
            var oldActivities = _context.Activities
                .Where(a => a.TimecardID == timecard.ID)
                .Select(a => a);
            _context.Activities.RemoveRange(oldActivities);
            _context.SaveChanges();

            foreach (var activity in timecard.Activities)
            {
                var data = activity.ToData();
                data.ID = 0;
                data.TimecardID = timecard.ID;
                _context.Activities.Add(data);
            }
            _context.SaveChanges();

            GetActivities(timecard);
        }

        public core.Activity GetActivity(int id)
        {
            return _context.Activities
                .Where(a => a.ID == id)
                .Select(a => a.ToCore())
                .FirstOrDefault();
        }

        public void SaveActivity(core.Activity activity)
        {
            Activity data;

            if (activity.ID != 0)
            {
                data = _context.Activities.Find(activity.ID);
                if (data == null)
                    throw new NotFoundException();
                data.UpdateFromCore(activity);
                _context.SaveChanges();
            }
            else
            {
                data = activity.ToData();
                _context.Activities.Add(data);
                _context.SaveChanges();
            }

            activity.UpdateFromData(data);
        }

        public void DeleteActivity(int id)
        {
            var data = _context.Activities.Find(id);
            if (data != null)
            {
                _context.Activities.Remove(data);
                _context.SaveChanges();
            }
        }

        public List<core.ReportItem> GetReport(DateTime startDate, DateTime endDate)
        {
            // query doesn't get exact matches on dates, so expand range by one second on each end
            var minDate = startDate.AddSeconds(-1);
            var maxDate = endDate.AddSeconds(1);

            // get raw data for report
            var query = from t in _context.Timecards
                        where t.Date >= minDate && t.Date <= maxDate
                        join a in _context.Activities on t.ID equals a.TimecardID
                        orderby t.Date, a.Time
                        select new
                        {
                            Timecard = t.ToCore(),
                            Activity = a.ToCore(),
                        };

            var rawData = query.ToList();

            // consolidate raw data into a dictionary
            var result = new Dictionary<string, core.ReportItem>();

            // (we go one short of total list because we're interested
            // in elapsed time from item to item)
            for (var i = 0; i < rawData.Count - 1; ++i)
            {
                var code = rawData[i].Activity.Code;
                if (string.IsNullOrWhiteSpace(code))
                    continue;

                if (!result.ContainsKey(code))
                    result[code] = new core.ReportItem
                    {
                        Code = code,
                        EarliestDate = rawData[i].Timecard.Date,
                        LatestDate = rawData[i].Timecard.Date,
                        TotalMinutes = 0,
                    };

                if (rawData[i].Timecard.Date < result[code].EarliestDate)
                    result[code].EarliestDate = rawData[i].Timecard.Date;

                if (rawData[i].Timecard.Date > result[code].LatestDate)
                    result[code].LatestDate = rawData[i].Timecard.Date;

                // elapsed time is only valid within the same day
                if (rawData[i].Timecard.Date == rawData[i + 1].Timecard.Date)
                {
                    result[code].TotalMinutes += rawData[i + 1].Activity.StartMinute -
                        rawData[i].Activity.StartMinute;
                }
            }

            // return list from dictionary
            return result.Values
                .OrderBy(item => item.Code)
                .ToList();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
