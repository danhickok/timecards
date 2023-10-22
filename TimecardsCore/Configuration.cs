using System.Text;

namespace TimecardsCore
{
    /// <summary>
    /// This class holds configuration data stored in the application's config file
    /// </summary>
    public static class Configuration
    {
        private static readonly char SEP1 = '\n';
        private static readonly char SEP2 = '\t';

        /// <summary>
        /// Indicates how default time for new activities is determined
        /// </summary>
        public static int RoundCurrentTimeToMinutes { get; set; }

        /// <summary>
        /// Used by UI for conforming entered code values
        /// </summary>
        public static string CodeMask { get; set; }

        /// <summary>
        /// Used by UI for conforming entered time values
        /// </summary>
        public static string TimeMask { get; set; }

        /// <summary>
        /// Hour/minute separator character used for time values
        /// </summary>
        public static char TimeSeparator { get; set; }

        /// <summary>
        /// True if time is entered in 24-hour values
        /// </summary>
        public static bool Use24HourTime { get; set; }

        /// <summary>
        /// Tint used by UI to mark activity that starts in the day after the timecard's date
        /// </summary>
        public static System.Drawing.Color MidnightTint { get; set; }

        /// <summary>
        /// List of descriptions for certain codes, used by UI for automatically populating description field
        /// </summary>
        public static Dictionary<string, string> DefaultCodes { get; private set; }

        /// <summary>
        /// Set this to true so that the current time can be overridden for testing purposes
        /// </summary>
        public static bool TestMode { get; set; }

        /// <summary>
        /// Retrieves either current time (in production) or given time (in test mode)
        /// </summary>
        private static DateTime _testTime;
        public static DateTime CurrentDateTime
        {
            get
            {
                if (TestMode)
                    return _testTime;
                else
                    return DateTime.Now;
            }
            set
            {
                if (TestMode)
                    _testTime = value;
                else
                    throw new Exception("Cannot set time when not in test mode");
            }
        }

        /// <summary>
        /// Holds the last import file format used
        /// </summary>
        public static string ImportFileType { get; set; }

        /// <summary>
        /// Holds the last export file format used
        /// </summary>
        public static string ExportFileType { get; set; }

        /// <summary>
        /// Divisor for desired units per hour in reporting tab (e.g., 15 => report in quarter hours)
        /// </summary>
        public static int MinutesPerReportUnit { get; set; }

        #region Static class constructor

        static Configuration()
        {
            TestMode = false;
            _testTime = DateTime.Now;

            DefaultCodes = new Dictionary<string, string>();
        }

        #endregion

        /// <summary>
        /// Loads the configuration data from the config file
        /// </summary>
        public static void Load()
        {
            RoundCurrentTimeToMinutes = Properties.Settings.Default.RoundCurrentTimeToMinutes;
            CodeMask = Properties.Settings.Default.CodeMask;
            TimeMask = Properties.Settings.Default.TimeMask;
            TimeSeparator = Properties.Settings.Default.TimeSeparator;
            Use24HourTime = Properties.Settings.Default.Use24HourTime;
            MidnightTint = Properties.Settings.Default.MidnightTint;
            ImportFileType = Properties.Settings.Default.ImportFileType;
            ExportFileType = Properties.Settings.Default.ExportFileType;
            MinutesPerReportUnit = Properties.Settings.Default.MinutesPerReportUnit;

            DefaultCodes.Clear();
            var dfString = Properties.Settings.Default.DefaultCodes;
            if (!string.IsNullOrWhiteSpace(dfString))
            {
                var pairs = dfString.Split(SEP1);
                foreach (var pair in pairs)
                {
                    var parts = pair.Split(SEP2);
                    DefaultCodes[parts[0]] = parts[1];
                }
            }
        }

        /// <summary>
        /// Saves the configuration data to the config file
        /// </summary>
        public static void Save()
        {
            Properties.Settings.Default.RoundCurrentTimeToMinutes = RoundCurrentTimeToMinutes;
            Properties.Settings.Default.CodeMask = CodeMask;
            Properties.Settings.Default.TimeMask = TimeMask;
            Properties.Settings.Default.TimeSeparator = TimeSeparator;
            Properties.Settings.Default.Use24HourTime = Use24HourTime;
            Properties.Settings.Default.MidnightTint = MidnightTint;
            Properties.Settings.Default.ImportFileType = ImportFileType;
            Properties.Settings.Default.ExportFileType = ExportFileType;
            Properties.Settings.Default.MinutesPerReportUnit = MinutesPerReportUnit;

            var dfStringBuilder = new StringBuilder();
            foreach (var key in DefaultCodes.Keys)
            {
                if (dfStringBuilder.Length > 0)
                    dfStringBuilder.Append(SEP1);
                dfStringBuilder.Append($"{key}{SEP2}{DefaultCodes[key]}");
            }
            Properties.Settings.Default.DefaultCodes = dfStringBuilder.ToString();

            Properties.Settings.Default.Save();
        }
    }
}
