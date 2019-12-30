using System;
using System.Collections.Generic;
using System.Linq;
using tm = TimecardsCore.Models;
using TimecardsCore.Interfaces;
using TimecardsCore.Exceptions;

namespace TimecardsData
{
    // Note:  Throughout these methods you'll see ToList() between the queries and
    // the end result.  This is because we're using extension methods to map between
    // the EF (data) classes and the core classes the application works with, and EF
    // complains that the extension methods do not map to any stored procedures in
    // the database.  (This limitation is because we're using using SQLite as the
    // data store.  It may not occur for other data storage engines, such as MS SQL
    // Server.)  For large volumes of data, forcing the result early like this would
    // be a bad practice; but for this application, the amount of data retrieved is
    // expected to be manageable.

    /// <summary>
    /// See IRepository for descriptions of the properties and methods of this class
    /// </summary>
    public class Repository : IRepository, IDisposable
    {
        private readonly TimecardsContext _context = null;

        #region Constructors

        public Repository(IAppConstants info)
        {
            var dbConnection = TimecardsConnectionBuilder.BuildConnection(info.ConnectionStringName);
            _context = new TimecardsContext(dbConnection);
        }

        #endregion

        public int GetTimecardCount()
        {
            return _context.Timecards.Count();
        }

        public List<tm.Timecard> GetTimecards(int offset, int limit, bool descending)
        {
            IOrderedQueryable<Timecard> query;
            if (descending)
                query = _context.Timecards
                    .OrderByDescending(t => t.Date)
                    .ThenByDescending(t => t.ID);
            else
                query = _context.Timecards
                    .OrderBy(t => t.Date)
                    .ThenBy(t => t.ID);

            return query
                .Skip(offset)
                .Take(limit)
                .ToList()
                .Select(t => t.ToCore())
                .ToList();
        }

        public List<tm.Timecard> GetTimecards(DateTime? startDate, DateTime? endDate)
        {
            // query may not get exact matches on dates, so expand range by one second on each end
            DateTime minDate = new DateTime(9999, 12, 31);
            DateTime maxDate = new DateTime(1900, 1, 1);

            if (startDate.HasValue)
                minDate = startDate.Value.AddSeconds(-1);
            if (endDate.HasValue)
                maxDate = endDate.Value.AddSeconds(1);

            return _context.Timecards
                .Include("Activities")
                .Where(tc =>
                    (startDate == null || tc.Date >= minDate) &&
                    (endDate == null || tc.Date <= maxDate))
                .OrderBy(tc => tc.Date)
                .ToList()
                .Select(t => t.ToCore())
                .ToList();
        }

        public tm.Timecard GetTimecard(int id)
        {
            var timecard = _context.Timecards
                .Where(t => t.ID == id)
                .ToList()
                .Select(t => t.ToCore())
                .FirstOrDefault();

            if (timecard != null)
                GetActivities(timecard);

            return timecard;
        }

        public tm.Timecard GetNearestTimecard(DateTime date, bool after)
        {
            tm.Timecard timecard = null;
            IOrderedQueryable<Timecard> query;

            if (after)
                query = _context.Timecards
                    .Where(t => t.Date > date)
                    .OrderBy(t => t.Date)
                    .ThenBy(t => t.ID);
            else
                query = _context.Timecards
                    .Where(t => t.Date < date)
                    .OrderByDescending(t => t.Date)
                    .ThenByDescending(t => t.ID);

            var data = query
                .FirstOrDefault();
            if (data != null)
                timecard = data.ToCore();

            if (timecard != null)
                GetActivities(timecard);

            return timecard;
        }

        public void SaveTimecard(tm.Timecard timecard)
        {
            Timecard data;

            if (timecard.ID != 0)
            {
                data = _context.Timecards.Find(timecard.ID);
                if (data == null)
                    throw new RecordNotFoundException();
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

            if (timecard.Activities.Any())
                SaveActivities(timecard);
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

        public void DeleteAllTimecards()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM Activities;");
            _context.Database.ExecuteSqlCommand("DELETE FROM Timecards;");
        }

        public void GetActivities(tm.Timecard timecard)
        {
            var activities = _context.Activities
                .Where(a => a.TimecardID == timecard.ID)
                .OrderBy(a => a.StartMinute)
                .ToList()
                .Select(a => a.ToCore())
                .ToList();

            timecard.Activities.Clear();
            timecard.Activities.AddRange(activities);
        }

        public void SaveActivities(tm.Timecard timecard)
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

        public tm.Activity GetActivity(int id)
        {
            return _context.Activities
                .Where(a => a.ID == id)
                .ToList()
                .Select(a => a.ToCore())
                .FirstOrDefault();
        }

        public void SaveActivity(tm.Activity activity)
        {
            Activity data;

            if (activity.ID != 0)
            {
                data = _context.Activities.Find(activity.ID);
                if (data == null)
                    throw new RecordNotFoundException();
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

        public List<tm.ReportItem> GetReport(DateTime startDate, DateTime endDate)
        {
            // query may not get exact matches on dates, so expand range by one second on each end
            var minDate = startDate.AddSeconds(-1);
            var maxDate = endDate.AddSeconds(1);

            // get raw data for report
            var query = from t in _context.Timecards
                        where t.Date >= minDate && t.Date <= maxDate
                        join a in _context.Activities on t.ID equals a.TimecardID
                        orderby t.Date, a.StartMinute
                        select new
                        {
                            Timecard = t,
                            Activity = a,
                        };

            var rawData = query
                .ToList()
                .OrderBy(q => q.Timecard.Date)
                .ThenBy(q => q.Activity.StartMinute)
                .Select(q => new
                {
                    Timecard = q.Timecard.ToCore(),
                    Activity = q.Activity.ToCore(),
                })
                .ToList();

            // consolidate raw data into a dictionary
            var result = new Dictionary<string, tm.ReportItem>();

            // (we go one short of total list because we're interested
            // in elapsed time from item to item)
            for (var i = 0; i < rawData.Count - 1; ++i)
            {
                var code = rawData[i].Activity.Code;
                if (string.IsNullOrWhiteSpace(code))
                    continue;

                if (!result.ContainsKey(code))
                    result[code] = new tm.ReportItem
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
