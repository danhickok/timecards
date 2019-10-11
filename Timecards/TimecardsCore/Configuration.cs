using System.Collections.Generic;
using System.Text;

namespace TimecardsCore
{
    public static class Configuration
    {
        private static readonly char SEP1 = '\n';
        private static readonly char SEP2 = '\t';

        public static string DatabasePath { get; set; }
        public static int RoundCurrentTimeToMinutes { get; set; }
        public static string CodeMask { get; set; }
        public static string TimeMask { get; set; }
        public static System.Drawing.Color MidnightTint { get; set; }
        public static Dictionary<string, string> DefaultCodes { get; private set; }

        static Configuration()
        {
            DefaultCodes = new Dictionary<string, string>();
        }

        public static void Load()
        {
            DatabasePath = Properties.Settings.Default.DatabasePath;
            RoundCurrentTimeToMinutes = Properties.Settings.Default.RoundCurrentTimeToMinutes;
            CodeMask = Properties.Settings.Default.CodeMask;
            TimeMask = Properties.Settings.Default.TimeMask;
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
            Properties.Settings.Default.DatabasePath = DatabasePath;
            Properties.Settings.Default.RoundCurrentTimeToMinutes = RoundCurrentTimeToMinutes;
            Properties.Settings.Default.CodeMask = CodeMask;
            Properties.Settings.Default.TimeMask = TimeMask;
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
