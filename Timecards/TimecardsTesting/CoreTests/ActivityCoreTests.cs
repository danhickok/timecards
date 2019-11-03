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

            tc.Configuration.TimeMask = "90:00";
            tc.Configuration.TimeSeparator = ':';
            tc.Configuration.Use24HourTime = true;

            //TODO: test 12-hour formats
            //TODO: test decimal time formats

            tc.Configuration.TimeMask = "90:00";
            tc.Configuration.TimeSeparator = ':';
            tc.Configuration.Use24HourTime = true;
            
            activity = new tcm.Activity();

            activity.Time = "5:4";
            Assert.AreEqual("05:04", activity.Time, "24-hour time is not being padded correctly");
            activity.Time = ":";
            Assert.AreEqual("00:00", activity.Time, "24-hour time is not being padded correctly");
            activity.Time = "0:75";
            Assert.AreEqual("01:15", activity.Time, "24-hour time is not being normalized correctly");

            activity.Time = "06:05";
            Assert.AreEqual(365, activity.StartMinute, "06:05 should be starting at minute 365");
            activity.Time = "00:00";
            Assert.AreEqual(0, activity.StartMinute, "00:00 should be starting at minute 0");
            activity.Time = "23:59";
            Assert.AreEqual(1439, activity.StartMinute, "23:59 should be starting at minute 1439");

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

            tc.Configuration.TimeMask = "90:00";
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

            //TODO: test 12-hour formats
            //TODO: test decimal time formats

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
