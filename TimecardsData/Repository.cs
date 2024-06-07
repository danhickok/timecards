﻿using TimecardsCore.Exceptions;
using TimecardsCore.Interfaces;
using TM = TimecardsCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        private readonly TimecardsContext? _context = null;

        #region Constructors

        public Repository(IAppConstants info)
        {
            var connectionString = TimecardsConnectionStringBuilder.BuildConnectionString(info.SystemName);
            _context = new TimecardsContext(connectionString);
            _context.Database.EnsureCreated();
        }

        #endregion

        public int GetTimecardCount()
        {
            return _context?.Timecards.Count() ?? 0;
        }

        public List<TM.Timecard> GetTimecards(int offset, int limit, bool descending)
        {
            IOrderedQueryable<Timecard> query;

            if (_context == null)
                throw new NullContextException();

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

        public List<TM.Timecard> GetTimecards(DateTime? startDate, DateTime? endDate)
        {
            if (_context == null)
                throw new NullContextException();

            // default range limits
            DateTime minDate = new(9999, 12, 31);
            DateTime maxDate = new(1900, 1, 1);

            // query may not get exact matches on dates, so expand range by one second on each end
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

        public TM.Timecard? GetTimecard(int id)
        {
            if (_context == null)
                throw new NullContextException();

            TM.Timecard? timecard = _context.Timecards
                .Where(t => t.ID == id)
                .ToList()
                .Select(t => t.ToCore())
                .FirstOrDefault();

            if (timecard != null)
                GetActivities(timecard);

            return timecard;
        }

        public TM.Timecard? GetNearestTimecard(DateTime date, bool after)
        {
            if (_context == null)
                throw new NullContextException();

            TM.Timecard? timecard = null;
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

        public void SaveTimecard(TM.Timecard timecard)
        {
            if (_context == null)
                throw new NullContextException();

            Timecard? data;

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
            if (_context == null)
                throw new NullContextException();

            var data = _context.Timecards.Find(id);
            if (data != null)
            {
                _context.Timecards.Remove(data);
                _context.SaveChanges();
            }
        }

        public void DeleteAllTimecards()
        {
            if (_context == null)
                throw new NullContextException();

            _context.Database.ExecuteSqlRaw("DELETE FROM Activities;");
            _context.Database.ExecuteSqlRaw("DELETE FROM Timecards;");
        }

        public void GetActivities(TM.Timecard timecard)
        {
            if (_context == null)
                throw new NullContextException();

            var activities = _context.Activities
                .Where(a => a.TimecardID == timecard.ID)
                .OrderBy(a => a.StartMinute)
                .ToList()
                .Select(a => a.ToCore())
                .ToList();

            timecard.Activities.Clear();
            timecard.Activities.AddRange(activities);
        }

        public void SaveActivities(TM.Timecard timecard)
        {
            if (_context == null)
                throw new NullContextException();

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

        public TM.Activity? GetActivity(int id)
        {
            if (_context == null)
                throw new NullContextException();

            var data = _context.Activities
                .Where(a => a.ID == id)
                .ToList()
                .Select(a => a.ToCore())
                .FirstOrDefault();

            return data;
        }

        public void SaveActivity(TM.Activity activity)
        {
            if (_context == null)
                throw new NullContextException();

            Activity? data;

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
            if (_context == null)
                throw new NullContextException();

            var data = _context.Activities.Find(id);
            if (data != null)
            {
                _context.Activities.Remove(data);
                _context.SaveChanges();
            }
        }

        public List<TM.ReportItem> GetReport(DateTime startDate, DateTime endDate)
        {
            if (_context == null)
                throw new NullContextException();

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
            var result = new Dictionary<string, TM.ReportItem>();

            // (we go one short of total list because we're interested
            // in elapsed time from item to item)
            for (var i = 0; i < rawData.Count - 1; ++i)
            {
                var code = rawData[i].Activity.Code;
                if (string.IsNullOrWhiteSpace(code))
                    continue;

                if (!result.ContainsKey(code))
                    result[code] = new TM.ReportItem
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
            return [ .. result.Values.OrderBy(item => item.Code), ];
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        #endregion
    }
}