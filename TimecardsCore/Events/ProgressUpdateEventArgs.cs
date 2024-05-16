namespace TimecardsCore.Events
{
    /// <summary>
    /// This class is used by the BulkLogic class when importing data to report the progress of the import.
    /// </summary>
    public class ProgressUpdateEventArgs(int current, int goal) : EventArgs
    {
        public readonly int Current = current;
        public readonly int Goal = goal;
        public bool Cancel = false;
    }
}
