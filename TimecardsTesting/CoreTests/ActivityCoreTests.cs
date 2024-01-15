using TC = TimecardsCore;
using TM = TimecardsCore.Models;

namespace TimecardsTesting.CoreTests
{
    public class ActivityCoreTests
    {
        private string _originalTimeMask;
        private char _originalTimeSeparator;
        private bool _originalUses24HourTime;

        [SetUp]
        public void Initialize()
        {
            _originalTimeMask = TC.Configuration.TimeMask;
            _originalTimeSeparator = TC.Configuration.TimeSeparator;
            _originalUses24HourTime = TC.Configuration.Use24HourTime;
        }

        [Test]
        public void TimeTest()
        {
            TM.Activity activity;

            //
            // test time-string property 
            //

            // 12-hour time
            TC.Configuration.TimeMask = TC.CoreConstants.TimeFormats.TwelveHourWithColon;
            TC.Configuration.TimeSeparator = ':';
            TC.Configuration.Use24HourTime = false;
            activity = new();

            activity.Time = " 5:04a";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 5:04a"), "12-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(304), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "5:4a";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 5:04a"), "12-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(304), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "3:07p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 3:07p"), "12-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(907), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "3:7p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 3:07p"), "12-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(907), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "11:7p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("11:07p"), "12-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(1387), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "12:00a";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("12:00a"), "12-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(0), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = ":";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("12:00a"), "12-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(0), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "0:75";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 1:15a"), "12-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(75), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "15:15a";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 3:15p"), "12-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(915), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = " 6:05a";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 6:05a"), "12-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(365), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "12:00p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("12:00p"), "12-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(720), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "11:59p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("11:59p"), "12-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(1439), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });

            // 12-hour time with decimal separator
            TC.Configuration.TimeMask = TC.CoreConstants.TimeFormats.TwelveHourWithDecimal;
            TC.Configuration.TimeSeparator = '.';
            TC.Configuration.Use24HourTime = false;
            activity = new();

            activity.Time = "5.4a";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 5.04a"), "12-hour decimal time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(304), "12-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "3.7p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 3.07p"), "12-hour decimal time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(907), "12-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "11.7p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("11.07p"), "12-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(1387), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = ".";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("12.00a"), "12-hour decimal time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(0), "12-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "0.75";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 1.15a"), "12-hour decimal time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(75), "12-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "15.15a";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 3.15p"), "12-hour decimal time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(915), "12-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = " 6.05a";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo(" 6.05a"), "12-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(365), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "12.00p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("12.00p"), "12-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(720), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });
            activity.Time = "11.59p";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("11.59p"), "12-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(1439), "12-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 12-hour time");
            });

            // 24-hour time
            TC.Configuration.TimeMask = TC.CoreConstants.TimeFormats.TwentyFourHourWithColon;
            TC.Configuration.TimeSeparator = ':';
            TC.Configuration.Use24HourTime = true;
            activity = new();

            activity.Time = "5:4";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("05:04"), "24-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(304), "24-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = ":";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("00:00"), "24-hour time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(0), "24-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = "0:75";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("01:15"), "24-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(75), "24-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = "06:05";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("06:05"), "24-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(365), "24-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = "00:00";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("00:00"), "24-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(0), "24-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = "23:59";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("23:59"), "24-hour time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(1439), "24-hour time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });

            // 24-hour time with decimal separator
            TC.Configuration.TimeMask = TC.CoreConstants.TimeFormats.TwentyFourHourWithDecimal;
            TC.Configuration.TimeSeparator = '.';
            TC.Configuration.Use24HourTime = true;
            activity = new();

            activity.Time = "5.4";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("05.04"), "24-hour decimal time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(304), "24-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = ".";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("00.00"), "24-hour decimal time is not being padded correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(0), "24-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = "0.75";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("01.15"), "24-hour decimal time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(75), "24-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = "06.05";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("06.05"), "24-hour decimal time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(365), "24-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = "00.00";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("00.00"), "24-hour decimal time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(0), "24-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });
            activity.Time = "23.59";
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("23.59"), "24-hour decimal time is not being normalized correctly");
                Assert.That(activity.StartMinute, Is.EqualTo(1439), "24-hour decimal time is not being interpreted correctly");
                Assert.That(activity.IsAfterMidnight, Is.False, "IsAfterMidnight is not set properly for 24-hour time");
            });

            //
            // test start-minute property
            //

            // 24-hour time
            TC.Configuration.TimeMask = TC.CoreConstants.TimeFormats.TwentyFourHourWithColon;
            TC.Configuration.TimeSeparator = ':';
            TC.Configuration.Use24HourTime = true;
            activity = new();

            activity.StartMinute = 0;
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("00:00"), "Starting minute 0 should be 00:00");
                Assert.That(activity.IsAfterMidnight, Is.False, "'After midnight' flag not cleared for zero StartMinute value");
            });
            activity.StartMinute = 158;
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("02:38"), "Starting minute 158 should be 02:38");
                Assert.That(activity.IsAfterMidnight, Is.False, "'After midnight' flag not cleared for small StartMinute value");
            });
            activity.StartMinute = 2345;
            Assert.Multiple(() =>
            {
                Assert.That(activity.Time, Is.EqualTo("15:05"), "Starting minute 2345 should wrap to 15:05 the next day");
                Assert.That(activity.IsAfterMidnight, Is.True, "'After midnight' flag not set for large StartMinute value");
            });
        }

        [Test]
        public void PropertiesTest()
        {
            TM.Activity activity;

            TC.Configuration.TimeMask = TC.CoreConstants.TimeFormats.TwentyFourHourWithColon;
            TC.Configuration.TimeSeparator = ':';
            TC.Configuration.Use24HourTime = true;

            activity = new TM.Activity();
            Assert.Multiple(() =>
            {
                Assert.That(activity.Code, Is.EqualTo(string.Empty), "Initial code should be empty string");
                Assert.That(activity.Description, Is.EqualTo(string.Empty), "Initial description should be empty string");
            });
            activity = new TM.Activity("12345");
            Assert.That(activity.Code, Is.EqualTo("12345"), "Code not assigned correctly in constructor");

            activity = new TM.Activity("234", "Shiny");
            Assert.Multiple(() =>
            {
                Assert.That(activity.Code, Is.EqualTo("234"), "Code not assigned correctly in constructor");
                Assert.That(activity.Description, Is.EqualTo("Shiny"), "Description not assigned correctly in constructor");
            });
            activity = new TM.Activity("3", "Dull", "2:0");
            Assert.Multiple(() =>
            {
                Assert.That(activity.Code, Is.EqualTo("3"), "Code not assigned correctly in constructor");
                Assert.That(activity.Description, Is.EqualTo("Dull"), "Description not assigned correctly in constructor");
                Assert.That(activity.Time, Is.EqualTo("02:00"), "24-hour time not assigned correctly in constructor");
            });
            activity = new TM.Activity("01010", "Matte", 180);
            Assert.That(activity.StartMinute, Is.EqualTo(180), "Start minute not assigned correctly in constructor");
        }

        [TearDown]
        public void Cleanup()
        {
            TC.Configuration.TimeMask = _originalTimeMask;
            TC.Configuration.TimeSeparator = _originalTimeSeparator;
            TC.Configuration.Use24HourTime = _originalUses24HourTime;
        }
    }
}
