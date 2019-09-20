using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardsCore.Models;
using ci = TimecardsCore.Interfaces;

namespace TimecardsCore.Logic
{
    public static class TimecardLogic
    {
        public static Timecard GetSpecificTimecard(int key)
        {
            throw new NotImplementedException();
        }

        public static Timecard GetLatestTimecard()
        {
            throw new NotImplementedException();
        }

        public static Timecard GetEarliestTimecard()
        {
            throw new NotImplementedException();
        }

        public static Timecard GetTodaysTimecard()
        {
            throw new NotImplementedException();
        }

        public static Timecard GetPreviousTimecard(Timecard currentTimecard)
        {
            throw new NotImplementedException();
        }

        public static Timecard GetNextTimecard(Timecard currentTimecard)
        {
            throw new NotImplementedException();
        }

        public static (int Key, DateTime Date) GetTimecardList()
        {
            throw new NotImplementedException();
        }

        public static void SaveTimecard(Timecard currentTimecard)
        {

        }
    }
}
