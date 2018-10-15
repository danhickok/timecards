using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimecardsCore
{
    public static class Configuration
    {
        private static readonly char SEP1 = '\n';
        private static readonly char SEP2 = '\t';

        public static int RoundCurrentTimeToMinutes { get; set; }
        public static string TicketNumberMask { get; set; }
        public static int MainFormTop { get; set; }
        public static int MainFormLeft { get; set; }
        public static int MainFormHeight { get; set; }
        public static int MainFormWidth { get; set; }
        public static Dictionary<string, string> DefaultCodes { get; private set; }

        static Configuration()
        {
            DefaultCodes = new Dictionary<string, string>();
        }

        public static void Load()
        {
            RoundCurrentTimeToMinutes = Properties.Settings.Default.RoundCurrentTimeToMinutes;
            TicketNumberMask = Properties.Settings.Default.TicketNumberMask;
            MainFormTop = Properties.Settings.Default.MainFormTop;
            MainFormLeft = Properties.Settings.Default.MainFormLeft;
            MainFormHeight = Properties.Settings.Default.MainFormHeight;
            MainFormWidth = Properties.Settings.Default.MainFormWidth;

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

        public static void Save(bool formDimensionsOnly = false)
        {
            if (!formDimensionsOnly)
            {
                Properties.Settings.Default.RoundCurrentTimeToMinutes = RoundCurrentTimeToMinutes;
                Properties.Settings.Default.TicketNumberMask = TicketNumberMask;

                var dfStringBuilder = new StringBuilder();
                foreach (var key in DefaultCodes.Keys)
                {
                    if (dfStringBuilder.Length > 0)
                        dfStringBuilder.Append(SEP1);
                    dfStringBuilder.Append($"{key}{SEP2}{DefaultCodes[key]}");
                }
                Properties.Settings.Default.DefaultCodes = dfStringBuilder.ToString();
            }

            Properties.Settings.Default.MainFormTop = MainFormTop;
            Properties.Settings.Default.MainFormLeft = MainFormLeft;
            Properties.Settings.Default.MainFormHeight = MainFormHeight;
            Properties.Settings.Default.MainFormWidth = MainFormWidth;

            Properties.Settings.Default.Save();
        }
    }
}
