namespace TimecardsCore.Interfaces
{
    /// <summary>
    /// The Logger class is for recording messages to a log file (provided in the constructor)
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes a message to the log file (provided in the constructor)
        /// </summary>
        /// <param name="message">Message to write to log file</param>
        void Log(string message);
    }
}
