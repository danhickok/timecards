namespace TimecardsCore
{
    public static class Constants
    {
        public static (string Format, string Description)[] TimeFormatChoices = new[]
        {
            ("90:00<Lm", "12-hour (##:##am)"),
            ("00:00",    "24-hour (##:##)"),
            ("90.00<Lm", "12-hour with decimal (##.##am)"),
            ("00.00",    "24-hour with decimal (##.##)"),
        };
    }
}
