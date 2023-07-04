using System;
using System.IO;
using TimecardsCore.Interfaces;

namespace TimecardsLogger
{
    /// <summary>
    /// Use this class to write messages to the log file
    /// </summary>
    public class Logger : ILogger
    {
        private string _path;

        /// <summary>
        /// Constructor requires AppConstants, so get an instance of this object from the factory
        /// </summary>
        /// <param name="constants"></param>
        public Logger(IAppConstants constants)
        {
            _path = constants.LogFilePath;
        }

        /// <summary>
        /// Write the message to the log file
        /// </summary>
        /// <param name="message">Message to write to log file</param>
        public void Log(string message)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(_path))
                return;

            try
            {
                using (var sw = new StreamWriter(_path, true))
                {
                    sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}");
                }
            }
            catch
            {
                // it's OK to fail silently - logging isn't supposed to get in the way
            }
#endif
        }
    }
}
