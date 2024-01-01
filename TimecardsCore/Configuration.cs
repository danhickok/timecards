using System.Text;
using System.Configuration;
using System.Drawing;

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
        public static Color MidnightTint { get; set; }

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

            RoundCurrentTimeToMinutes = 0;
            CodeMask = "";
            TimeMask = "";
            TimeSeparator = ':';
            Use24HourTime = false;
            MidnightTint = Color.White;
            ImportFileType = "";
            ExportFileType = "";
            MinutesPerReportUnit = 0;

            DefaultCodes = new Dictionary<string, string>();
        }

        #endregion

        /// <summary>
        /// Loads the configuration data from the config file
        /// </summary>
        public static void Load()
        {
            RoundCurrentTimeToMinutes = ReadAppSetting("RoundCurrentTimeToMinutes", 1);
            CodeMask = ReadAppSetting("CodeMask", "");
            TimeMask = ReadAppSetting("TimeMask", "");
            TimeSeparator = ReadAppSetting("TimeSeparator", ':');
            Use24HourTime = ReadAppSetting("Use24HourTime", false);
            MidnightTint = ReadAppSetting("MidnightTint", Color.LightGray);
            ImportFileType = ReadAppSetting("ImportFileType", "JSON");
            ExportFileType = ReadAppSetting("ExportFileType", "JSON");
            MinutesPerReportUnit = ReadAppSetting("MinutesPerReportUnit", 1);

            DefaultCodes.Clear();
            var dfString = ReadAppSetting("DefaultCodes", "");
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
            WriteAppSetting("RoundCurrentTimeToMinutes", RoundCurrentTimeToMinutes);
            WriteAppSetting("CodeMask", CodeMask);
            WriteAppSetting("TimeMask", TimeMask);
            WriteAppSetting("TimeSeparator", TimeSeparator);
            WriteAppSetting("Use24HourTime", Use24HourTime);
            WriteAppSetting("MidnightTint", MidnightTint);
            WriteAppSetting("ImportFileType", ImportFileType);
            WriteAppSetting("ExportFileType", ExportFileType);
            WriteAppSetting("MinutesPerReportUnit", MinutesPerReportUnit);

            var dfStringBuilder = new StringBuilder();
            foreach (var key in DefaultCodes.Keys)
            {
                if (dfStringBuilder.Length > 0)
                    dfStringBuilder.Append(SEP1);
                dfStringBuilder.Append($"{key}{SEP2}{DefaultCodes[key]}");
            }
            WriteAppSetting("DefaultCodes", dfStringBuilder.ToString());

            SaveAppSettings();
        }

        private static T ReadAppSetting<T>(string key, T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(key))
                return defaultValue;

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(key))
                return defaultValue;

            // Special case for Color type
            if (typeof(T) == typeof(Color))
            {
                if (int.TryParse(key, out int colorValue))
                {
                    return (T)Convert.ChangeType(Color.FromArgb(colorValue), typeof(T));
                }
                else
                {
                    return defaultValue;
                }
            }

            // All other types
            T? value = (T?)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            return value ?? defaultValue;
        }

        private static void WriteAppSetting<T>(string key, T value)
        {
            string? storeValue;
            if (typeof(T) == typeof(Color))
            {
                // Special case for Color type
                var color = (Color?)Convert.ChangeType(value, typeof(Color));
                storeValue = color?.ToArgb().ToString();
            }
            else
            {
                // All other types
                storeValue = value?.ToString();
            }

            ConfigurationManager.AppSettings[key] = storeValue;
        }

        private static void SaveAppSettings()
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var settings = configFile.AppSettings.Settings;

                foreach (var key in ConfigurationManager.AppSettings.AllKeys)
                {
                    if (settings[key] == null)
                    {
                        settings.Add(key, ConfigurationManager.AppSettings[key]);
                    }
                    else
                    {
                        settings[key].Value = ConfigurationManager.AppSettings[key];
                    }
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (Exception)
            {
                //TODO: log error
            }
        }
    }
}
