namespace TimecardsCore.Interfaces
{
    /// <summary>
    /// The AppConstants class holds constant values for either production or test use
    /// </summary>
    public interface IAppConstants
    {
        /// <summary>
        /// The system name used by TimecardsConnectionStringBuilder to determine which database to use
        /// </summary>
        string? SystemName { get; }

        /// <summary>
        /// Path to log file
        /// </summary>
        string? LogFilePath { get; }
    }
}
