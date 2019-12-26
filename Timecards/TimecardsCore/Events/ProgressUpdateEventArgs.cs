using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore.Events
{
    public class ProgressUpdateEventArgs : EventArgs
    {
        public readonly int Current;
        public readonly int Goal;

        public ProgressUpdateEventArgs(int current, int goal)
        {
            Current = current;
            Goal = goal;
        }
    }
}
