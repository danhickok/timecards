namespace TimecardsCore
{
    public static class Constants
    {
        public static class TimeFormats
        {
            public static string TwelveHourWithColon = "90:00<L";
            public static string TwentyFourHourWithColon = "00:00";
            public static string TwelveHourWithDecimal = "90.00<L";
            public static string TwentyFourHourWithDecimal = "00.00";
        }

        public static (string Format, string Description)[] TimeFormatChoices = new[]
        {
            (TimeFormats.TwelveHourWithColon,       "12-hour (##:##am)"),
            (TimeFormats.TwentyFourHourWithColon,   "24-hour (##:##)"),
            (TimeFormats.TwelveHourWithDecimal,     "12-hour with decimal (##.##am)"),
            (TimeFormats.TwentyFourHourWithDecimal, "24-hour with decimal (##.##)"),
        };
    }
}
