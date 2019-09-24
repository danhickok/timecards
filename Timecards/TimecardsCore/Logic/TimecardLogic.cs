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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Timecard GetLatestTimecard()
        {
            throw new NotImplementedException();
        }

        public Timecard GetEarliestTimecard()
        {
            throw new NotImplementedException();
        }

        public Timecard GetTodaysTimecard()
        {
            throw new NotImplementedException();
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
