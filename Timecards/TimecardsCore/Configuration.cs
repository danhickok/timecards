using System;
using System.Collections.Generic;
using System.Text;

namespace TimecardsCore
{
    public static class Configuration
    {
        private static readonly char SEP1 = '\n';
        private static readonly char SEP2 = '\t';

        public static int RoundCurrentTimeToMinutes { get; set; }
        public static string CodeMask { get; set; }
        public static string TimeMask { get; set; }
        public static char TimeSeparator { get; set; }
        public static System.Drawing.Color MidnightTint { get; set; }
        public static Dictionary<string, string> DefaultCodes { get; private set; }
        public static bool TestMode { get; set; }

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

        static Configuration()
        {
            TestMode = false;
            _testTime = DateTime.Now;

            DefaultCodes = new Dictionary<string, string>();
        }

        public static void Load()
        {
            RoundCurrentTimeToMinutes = Properties.Settings.Default.RoundCurrentTimeToMinutes;
            CodeMask = Properties.Settings.Default.CodeMask;
            TimeMask = Properties.Settings.Default.TimeMask;
            TimeSeparator = Properties.Settings.Default.TimeSeparator;
            MidnightTint = Properties.Settings.Default.MidnightTint;

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

        public static void Save()
        {
            Properties.Settings.Default.RoundCurrentTimeToMinutes = RoundCurrentTimeToMinutes;
            Properties.Settings.Default.CodeMask = CodeMask;
            Properties.Settings.Default.TimeMask = TimeMask;
            Properties.Settings.Default.TimeSeparator = TimeSeparator;
            Properties.Settings.Default.MidnightTint = MidnightTint;

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
