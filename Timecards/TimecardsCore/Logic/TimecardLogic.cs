using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimecardsCore.Models;

namespace TimecardsCore.Logic
{
    public class TimecardLogic
    {
        public Timecard CurrentTimecard { get; private set; }

        public TimecardLogic()
        {
            CurrentTimecard = null;
        }


    }
}
