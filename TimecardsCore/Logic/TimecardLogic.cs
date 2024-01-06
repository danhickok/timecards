using TimecardsCore.Exceptions;
using TimecardsCore.Interfaces;
using TimecardsCore.Models;

namespace TimecardsCore.Logic
{
    /// <summary>
    /// This class encapsulates all business logic operations performed on timecards and their activities
    /// </summary>
    public class TimecardLogic
    {
        private readonly IFactory _factory;
        private Timecard _timecard;

        #region Constructor

        public TimecardLogic(IFactory factory)
        {
            _factory = factory;
            _timecard = new Timecard();
        }

        #endregion

        /// <summary>
        /// Returns number of timecards in database
        /// </summary>
        /// <returns>Count of existing timecards</returns>
        public int GetTimecardCount()
        {
            var repo = _factory.Resolve<IRepository>();
            return repo.GetTimecardCount();
        }

        /// <summary>
        /// Returns the current timecard held by this class
        /// </summary>
        /// <returns>The current timecard</returns>
        public Timecard GetCurrentTimecard()
        {
            return _timecard;
        }

        /// <summary>
        /// Creates a new, unsaved timecard
        /// </summary>
        /// <returns>A new timecard</returns>
        public Timecard GetNewTimecard()
        {
            _timecard = new Timecard();
            return _timecard;
        }

        /// <summary>
        /// Retrieves a timecard by its ID
        /// </summary>
        /// <param name="key">Primary key</param>
        /// <returns>Timecard retrieved from database</returns>
        public Timecard GetSpecificTimecard(int key)
        {
            var repo = _factory.Resolve<IRepository>();
            var timecard = repo.GetTimecard(key);
            _timecard = timecard
                ?? throw new TimecardNotFoundException();
            return _timecard;
        }

        /// <summary>
        /// Retrieves the very latest timecard from the database
        /// </summary>
        /// <returns>The latest timecard</returns>
        public Timecard GetLatestTimecard()
        {
            RetrieveTimecardFromEdge(true);
            return _timecard;
        }

        /// <summary>
        /// Retrieves the very earliest timecard from the database
        /// </summary>
        /// <returns>The earliest timecard</returns>
        public Timecard GetEarliestTimecard()
        {
            RetrieveTimecardFromEdge(false);
            return _timecard;
        }

        /// <summary>
        /// Retrieves today's timecard or makes a new timecard if it doesn't exist
        /// </summary>
        /// <returns></returns>
        public Timecard GetTodaysTimecard()
        {
            try
            {
                RetrieveTimecardFromEdge(true);
                if (_timecard.ID != 0 && _timecard.Date != DateTime.Today)
                    _timecard = new Timecard();
            }
            catch (TimecardNotFoundException)
            {
                _timecard = new Timecard();
            }

            return _timecard;
        }

        /// <summary>
        /// Gets the timecard just before the current timecard's date
        /// </summary>
        /// <returns>The previous timecard</returns>
        public Timecard? GetPreviousTimecard()
        {
            return GetNextOrPreviousTimecard(false);
        }

        /// <summary>
        /// Gets the timecard just after the current timecard's date
        /// </summary>
        /// <returns>The next timecard</returns>
        public Timecard? GetNextTimecard()
        {
            return GetNextOrPreviousTimecard(true);
        }

        private Timecard GetNextOrPreviousTimecard(bool next)
        {
            Timecard? timecard;
            var repo = _factory.Resolve<IRepository>();

            timecard = repo.GetNearestTimecard(_timecard.Date, next);
            if (timecard != null)
                _timecard = timecard;
            else
                RetrieveTimecardFromEdge(next);

            return _timecard;
        }

        /// <summary>
        /// Gets a complete list of tuples (keys and dates) for timecards
        /// </summary>
        /// <returns>List of tuples</returns>
        public List<(int Key, DateTime Date)> GetTimecardList()
        {
            var repo = _factory.Resolve<IRepository>();
            var timecards = repo.GetTimecards(0, 99999, true);
            var list = timecards
                .OrderByDescending(tc => tc.Date)
                .ThenByDescending(tc => tc.ID)
                .Select(tc => (tc.ID, tc.Date)).ToList();

            return list;
        }

        /// <summary>
        /// Saves the current timecard and its activities
        /// </summary>
        public void SaveTimecard()
        {
            IRepository? repo = null;

            if (_timecard.IsDirty || _timecard.ID == 0)
            {
                repo ??= _factory.Resolve<IRepository>();

                repo.SaveTimecard(_timecard);
            }

            foreach (var activity in _timecard.Activities)
            {
                if (activity.IsDirty)
                {
                    repo ??= _factory.Resolve<IRepository>();

                    activity.TimecardID = _timecard.ID;
                    repo.SaveActivity(activity);
                }
            }
        }

        /// <summary>
        /// Deletes all the activities for the current timecard
        /// </summary>
        /// <param name="index"></param>
        public void DeleteActivity(int index)
        {
            if (index < 0 || index > _timecard.Activities.Count)
                throw new ActivityNotFoundException();

            var activity = _timecard.Activities[index];

            if (activity.ID != 0)
            {
                var repo = _factory.Resolve<IRepository>();
                repo.DeleteActivity(activity.ID);
            }

            _timecard.Activities.Remove(activity);
        }

        /// <summary>
        /// Deletes the current timecard, then retrieves the most recent timecard before it
        /// </summary>
        public void DeleteTimecard()
        {
            var id = _timecard.ID;
            var date = _timecard.Date;

            var repo = _factory.Resolve<IRepository>();
            repo.DeleteTimecard(id);
            _timecard = repo.GetNearestTimecard(date, false)
                ?? throw new TimecardNotFoundException();
        }

        /// <summary>
        /// Deletes all timecards in the database
        /// </summary>
        public void DeleteAllTimecards()
        {
            var repo = _factory.Resolve<IRepository>();
            repo.DeleteAllTimecards();
        }

        #region Private methods

        private void RetrieveTimecardFromEdge(bool latest)
        {
            Timecard? timecard = null;

            var repo = _factory.Resolve<IRepository>();
            var list = repo.GetTimecards(0, 1, latest);

            if (list.Count > 0)
                timecard = repo.GetTimecard(list[0].ID);

            if (timecard != null)
                _timecard = timecard;
            else
                throw new TimecardNotFoundException();
        }

        #endregion
    }
}
