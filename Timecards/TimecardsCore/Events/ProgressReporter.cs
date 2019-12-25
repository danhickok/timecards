using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Events
{
    public class ProgressReporter
    {
        private int _goal;
        private int _current;

        public int Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        public ProgressReporter(int goal)
        {
            _goal = goal;
            _current = 0;
        }
    }
}
