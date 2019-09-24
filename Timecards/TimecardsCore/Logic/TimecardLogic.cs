using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardsCore.Models;
using ci = TimecardsCore.Interfaces;

namespace TimecardsCore.Logic
{
    public class TimecardLogic
    {
        private ci.IFactory _factory;
        
        public TimecardLogic(ci.IFactory factory)
        {
            _factory = factory;
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

        public  Timecard GetPreviousTimecard(Timecard currentTimecard)
        {
            throw new NotImplementedException();
        }

        public Timecard GetNextTimecard(Timecard currentTimecard)
        {
            throw new NotImplementedException();
        }

        public (int Key, DateTime Date) GetTimecardList()
        {
            throw new NotImplementedException();
        }

        public void SaveTimecard(Timecard currentTimecard)
        {
            throw new NotImplementedException();
        }
    }
}
