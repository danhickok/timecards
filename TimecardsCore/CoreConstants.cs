namespace TimecardsCore
{
    /// <summary>
    /// Application-wide constants are defined in this class
    /// </summary>
    public static class CoreConstants
    {
        public static class TimeFormats
        {
            public static readonly string TwelveHourWithColon = "90:00<L";
            public static readonly string TwentyFourHourWithColon = "00:00";
            public static readonly string TwelveHourWithDecimal = "90.00<L";
            public static readonly string TwentyFourHourWithDecimal = "00.00";
        }

        public static readonly (string Format, string Description)[] TimeFormatChoices =
        [
            (TimeFormats.TwelveHourWithColon,       "12-hour (##:##a)"),
            (TimeFormats.TwentyFourHourWithColon,   "24-hour (##:##)"),
            (TimeFormats.TwelveHourWithDecimal,     "12-hour with decimal (##.##a)"),
            (TimeFormats.TwentyFourHourWithDecimal, "24-hour with decimal (##.##)"),
        ];
    }
}
