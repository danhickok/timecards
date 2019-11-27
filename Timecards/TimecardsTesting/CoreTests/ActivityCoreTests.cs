using Microsoft.VisualStudio.TestTools.UnitTesting;
using tc = TimecardsCore;
using tcm = TimecardsCore.Models;

namespace TimecardsTesting.CoreTests
{
    [TestClass]
    public class ActivityCoreTests
    {
        private string _originalTimeMask;
        private char _originalTimeSeparator;
        private bool _originalUses24HourTime;

        [TestInitialize]
        public void Initialize()
        {
            _originalTimeMask = tc.Configuration.TimeMask;
            _originalTimeSeparator = tc.Configuration.TimeSeparator;
            _originalUses24HourTime = tc.Configuration.Use24HourTime;
        }

        [TestMethod]
        public void TimeTest()
        {
            tcm.Activity activity;

            //
            // test time-string property 
            //

            // 12-hour time
            tc.Configuration.TimeMask = tc.Constants.TimeFormats.TwelveHourWithColon;
            tc.Configuration.TimeSeparator = ':';
            tc.Configuration.Use24HourTime = false;
            activity = new tcm.Activity();

            activity.Time = " 5:04a";
            Assert.AreEqual(" 5:04a", activity.Time, "12-hour time is not being padded correctly");
            Assert.AreEqual(304, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = "5:4a";
            Assert.AreEqual(" 5:04a", activity.Time, "12-hour time is not being padded correctly");
            Assert.AreEqual(304, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = "3:07p";
            Assert.AreEqual(" 3:07p", activity.Time, "12-hour time is not being padded correctly");
            Assert.AreEqual(907, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = "3:7p";
            Assert.AreEqual(" 3:07p", activity.Time, "12-hour time is not being padded correctly");
            Assert.AreEqual(907, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = "11:7p";
            Assert.AreEqual("11:07p", activity.Time, "12-hour time is not being padded correctly");
            Assert.AreEqual(1387, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = "12:00a";
            Assert.AreEqual("12:00a", activity.Time, "12-hour time is not being padded correctly");
            Assert.AreEqual(0, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = ":";
            Assert.AreEqual("12:00a", activity.Time, "12-hour time is not being padded correctly");
            Assert.AreEqual(0, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = "0:75";
            Assert.AreEqual(" 1:15a", activity.Time, "12-hour time is not being normalized correctly");
            Assert.AreEqual(75, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = "15:15a";
            Assert.AreEqual(" 3:15p", activity.Time, "12-hour time is not being normalized correctly");
            Assert.AreEqual(915, activity.StartMinute, "12-hour time is not being interpreted correctly");

            // 12-hour time with decimal separator
            tc.Configuration.TimeMask = tc.Constants.TimeFormats.TwelveHourWithDecimal;
            tc.Configuration.TimeSeparator = '.';
            tc.Configuration.Use24HourTime = false;
            activity = new tcm.Activity();

            activity.Time = "5.4a";
            Assert.AreEqual(" 5.04a", activity.Time, "12-hour decimal time is not being padded correctly");
            Assert.AreEqual(304, activity.StartMinute, "12-hour decimal time is not being interpreted correctly");
            activity.Time = "3.7p";
            Assert.AreEqual(" 3.07p", activity.Time, "12-hour decimal time is not being padded correctly");
            Assert.AreEqual(907, activity.StartMinute, "12-hour decimal time is not being interpreted correctly");
            activity.Time = "11.7p";
            Assert.AreEqual("11.07p", activity.Time, "12-hour time is not being padded correctly");
            Assert.AreEqual(1387, activity.StartMinute, "12-hour time is not being interpreted correctly");
            activity.Time = ".";
            Assert.AreEqual("12.00a", activity.Time, "12-hour decimal time is not being padded correctly");
            Assert.AreEqual(0, activity.StartMinute, "12-hour decimal time is not being interpreted correctly");
            activity.Time = "0.75";
            Assert.AreEqual(" 1.15a", activity.Time, "12-hour decimal time is not being normalized correctly");
            Assert.AreEqual(75, activity.StartMinute, "12-hour decimal time is not being interpreted correctly");
            activity.Time = "15.15a";
            Assert.AreEqual(" 3.15p", activity.Time, "12-hour decimal time is not being normalized correctly");
            Assert.AreEqual(915, activity.StartMinute, "12-hour decimal time is not being interpreted correctly");

            // 24-hour time
            tc.Configuration.TimeMask = tc.Constants.TimeFormats.TwentyFourHourWithColon;
            tc.Configuration.TimeSeparator = ':';
            tc.Configuration.Use24HourTime = true;
            activity = new tcm.Activity();

            activity.Time = "5:4";
            Assert.AreEqual("05:04", activity.Time, "24-hour time is not being padded correctly");
            Assert.AreEqual(304, activity.StartMinute, "24-hour time is not being interpreted correctly");
            activity.Time = ":";
            Assert.AreEqual("00:00", activity.Time, "24-hour time is not being padded correctly");
            Assert.AreEqual(0, activity.StartMinute, "24-hour time is not being interpreted correctly");
            activity.Time = "0:75";
            Assert.AreEqual("01:15", activity.Time, "24-hour time is not being normalized correctly");
            Assert.AreEqual(75, activity.StartMinute, "24-hour time is not being interpreted correctly");
            activity.Time = "06:05";
            Assert.AreEqual(365, activity.StartMinute, "24-hour time is not being interpreted correctly");
            activity.Time = "00:00";
            Assert.AreEqual(0, activity.StartMinute, "24-hour time is not being interpreted correctly");
            activity.Time = "23:59";
            Assert.AreEqual(1439, activity.StartMinute, "24-hour time is not being interpreted correctly");

            // 24-hour time with decimal separator
            tc.Configuration.TimeMask = tc.Constants.TimeFormats.TwentyFourHourWithDecimal;
            tc.Configuration.TimeSeparator = '.';
            tc.Configuration.Use24HourTime = true;
            activity = new tcm.Activity();

            activity.Time = "5.4";
            Assert.AreEqual("05.04", activity.Time, "24-hour decimal time is not being padded correctly");
            Assert.AreEqual(304, activity.StartMinute, "24-hour decimal time is not being interpreted correctly");
            activity.Time = ".";
            Assert.AreEqual("00.00", activity.Time, "24-hour decimal time is not being padded correctly");
            Assert.AreEqual(0, activity.StartMinute, "24-hour decimal time is not being interpreted correctly");
            activity.Time = "0.75";
            Assert.AreEqual("01.15", activity.Time, "24-hour decimal time is not being normalized correctly");
            Assert.AreEqual(75, activity.StartMinute, "24-hour decimal time is not being interpreted correctly");
            activity.Time = "06.05";
            Assert.AreEqual(365, activity.StartMinute, "24-hour decimal time is not being interpreted correctly");
            activity.Time = "00.00";
            Assert.AreEqual(0, activity.StartMinute, "24-hour decimal time is not being interpreted correctly");
            activity.Time = "23.59";
            Assert.AreEqual(1439, activity.StartMinute, "24-hour decimal time is not being interpreted correctly");

            //
            // test start-minute property
            //

            // 24-hour time
            tc.Configuration.TimeMask = tc.Constants.TimeFormats.TwentyFourHourWithColon;
            tc.Configuration.TimeSeparator = ':';
            tc.Configuration.Use24HourTime = true;
            activity = new tcm.Activity();

            activity.StartMinute = 0;
            Assert.AreEqual("00:00", activity.Time, "Starting minute 0 should be 00:00");
            Assert.IsTrue(!activity.IsAfterMidnight, "'After midnight' flag not cleared for zero StartMinute value");
            
            activity.StartMinute = 158;
            Assert.AreEqual("02:38", activity.Time, "Starting minute 158 should be 02:38");
            Assert.IsTrue(!activity.IsAfterMidnight, "'After midnight' flag not cleared for small StartMinute value");
            
            activity.StartMinute = 2345;
            Assert.AreEqual("15:05", activity.Time, "Starting minute 2345 should wrap to 15:05 the next day");
            Assert.IsTrue(activity.IsAfterMidnight, "'After midnight' flag not set for large StartMinute value");
        }

        [TestMethod]
        public void PropertiesTest()
        {
            tcm.Activity activity;

            tc.Configuration.TimeMask = tc.Constants.TimeFormats.TwentyFourHourWithColon;
            tc.Configuration.TimeSeparator = ':';
            tc.Configuration.Use24HourTime = true;

            activity = new tcm.Activity();
            Assert.AreEqual(string.Empty, activity.Code, "Initial code should be empty string");
            Assert.AreEqual(string.Empty, activity.Description, "Initial description should be empty string");

            activity = new tcm.Activity("12345");
            Assert.AreEqual("12345", activity.Code, "Code not assigned correctly in constructor");

            activity = new tcm.Activity("234", "Shiny");
            Assert.AreEqual("234", activity.Code, "Code not assigned correctly in constructor");
            Assert.AreEqual("Shiny", activity.Description, "Description not assigned correctly in constructor");

            activity = new tcm.Activity("3", "Dull", "2:0");
            Assert.AreEqual("3", activity.Code, "Code not assigned correctly in constructor");
            Assert.AreEqual("Dull", activity.Description, "Description not assigned correctly in constructor");
            Assert.AreEqual("02:00", activity.Time, "24-hour time not assigned correctly in constructor");

            activity = new tcm.Activity("01010", "Matte", 180);
            Assert.AreEqual(180, activity.StartMinute, "Start minute not assigned correctly in constructor");
        }

        [TestCleanup]
        public void Cleanup()
        {
            tc.Configuration.TimeMask = _originalTimeMask;
            tc.Configuration.TimeSeparator = _originalTimeSeparator;
            tc.Configuration.Use24HourTime = _originalUses24HourTime;
        }
    }
}
