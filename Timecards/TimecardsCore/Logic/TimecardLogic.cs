using System;
using System.Collections.Generic;
using System.Linq;
using TimecardsCore.Models;
using TimecardsCore.Interfaces;
using TimecardsCore.Exceptions;

namespace TimecardsCore.Logic
{
    public class TimecardLogic
    {
        private readonly IFactory _factory;
        private Timecard _timecard;
        
        public TimecardLogic(IFactory factory)
        {
            _factory = factory;
            _timecard = new Timecard();
        }

        public int GetTimecardCount()
        {
            var repo = _factory.Resolve<IRepository>();
            return repo.GetTimecardCount();
        }

        public Timecard GetCurrentTimecard()
        {
            return _timecard;
        }

        public Timecard GetNewTimecard()
        {
            _timecard = new Timecard();
            return _timecard;
        }

        public Timecard GetSpecificTimecard(int key)
        {
            var repo = _factory.Resolve<IRepository>();
            var timecard  = repo.GetTimecard(key);
            _timecard = timecard
                ?? throw new TimecardNotFoundException();
            return _timecard;
        }

        private void RetrieveTimecardFromEdge(bool latest)
        {
            var repo = _factory.Resolve<IRepository>();
            var list = repo.GetTimecards(0, 1, latest);

            if (list.Count > 0)
                _timecard = repo.GetTimecard(list[0].ID);
            else
                throw new TimecardNotFoundException();
        }

        public Timecard GetLatestTimecard()
        {
            RetrieveTimecardFromEdge(true);
            return _timecard;
        }

        public Timecard GetEarliestTimecard()
        {
            RetrieveTimecardFromEdge(false);
            return _timecard;
        }

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

        public  Timecard GetPreviousTimecard()
        {
            var repo = _factory.Resolve<IRepository>();
            _timecard = repo.GetNearestTimecard(_timecard.Date, false)
                ?? throw new TimecardNotFoundException();
            return _timecard;
        }

        public Timecard GetNextTimecard()
        {
            var repo = _factory.Resolve<IRepository>();
            _timecard = repo.GetNearestTimecard(_timecard.Date, true)
                ?? throw new TimecardNotFoundException();
            return _timecard;
        }

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

        public void SaveTimecard()
        {
            var repo = _factory.Resolve<IRepository>();

            if (_timecard.IsDirty)
            {
                repo.SaveTimecard(_timecard);
            }

            foreach (var activity in _timecard.Activities)
            {
                if (activity.IsDirty)
                {
                    activity.TimecardID = _timecard.ID;
                    repo.SaveActivity(activity);
                }
            }
        }

        public void DeleteTimecard()
        {
            var id = _timecard.ID;
            var date = _timecard.Date;

            var repo = _factory.Resolve<IRepository>();
            repo.DeleteTimecard(id);
            _timecard = repo.GetNearestTimecard(date, false)
                ?? throw new TimecardNotFoundException();
        }

        public void DeleteAllTimecards()
        {
            var repo = _factory.Resolve<IRepository>();
            repo.DeleteAllTimecards();
        }
    }
}
