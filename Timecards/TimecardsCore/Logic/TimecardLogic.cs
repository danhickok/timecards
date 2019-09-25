using System;
using TimecardsCore.Models;
using ci = TimecardsCore.Interfaces;

namespace TimecardsCore.Logic
{
    public class TimecardLogic
    {
        private readonly ci.IFactory _factory;
        private Timecard _timecard;
        
        public TimecardLogic(ci.IFactory factory)
        {
            _factory = factory;
            _timecard = new Timecard();
        }

        public int GetTimecardCount()
        {
            var repo = _factory.Resolve<ci.IRepository>();
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
            var repo = _factory.Resolve<ci.IRepository>();
            _timecard = repo.GetTimecard(key);
            return _timecard;
        }

        private void RetrieveTimecardFromEdge(bool latest)
        {
            var repo = _factory.Resolve<ci.IRepository>();
            var list = repo.GetTimecards(0, 1, latest);

            if (list.Count == 0)
            {
                _timecard = new Timecard();
            }
            else
            {
                _timecard = repo.GetTimecard(list[0].ID);
            }
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
            RetrieveTimecardFromEdge(true);
            if (_timecard.ID != 0 && _timecard.Date != DateTime.Today)
                _timecard = new Timecard();

            return _timecard;
        }

        public  Timecard GetPreviousTimecard()
        {
            throw new NotImplementedException();
        }

        public Timecard GetNextTimecard()
        {
            throw new NotImplementedException();
        }

        public (int Key, DateTime Date) GetTimecardList()
        {
            throw new NotImplementedException();
        }

        public void SaveTimecard()
        {
            throw new NotImplementedException();
        }

        public void DeleteTimecard()
        {
            throw new NotImplementedException();
        }

        public void DeleteAllTimecards()
        {
            throw new NotImplementedException();
        }
    }
}
