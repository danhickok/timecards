using System;

namespace TimecardsCore.Events
{
    /// <summary>
    /// This class is used by the BulkLogic class when importing data to report the progress of the import.
    /// </summary>
    public class ProgressUpdateEventArgs : EventArgs
    {
        public readonly int Current;
        public readonly int Goal;
        public bool Cancel;

        public ProgressUpdateEventArgs(int current, int goal)
        {
            Current = current;
            Goal = goal;
            Cancel = false;
        }
    }
}
