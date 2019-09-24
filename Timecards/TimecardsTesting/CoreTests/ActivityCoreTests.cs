using Microsoft.VisualStudio.TestTools.UnitTesting;
using core = TimecardsCore.Models;

namespace TimecardsTesting.CoreTests
{
    [TestClass]
    public class ActivityCoreTests
    {
        [TestMethod]
        public void TimeTest()
        {
            var activity = new core.Activity();

            activity.Time = "5:4";
            Assert.AreEqual("05:04", activity.Time, "Time is not being padded correctly");
            activity.Time = ":";
            Assert.AreEqual("00:00", activity.Time, "Time is not being padded correctly");
            activity.Time = "0:75";
            Assert.AreEqual("01:15", activity.Time, "Time is not being normalized correctly");

            activity.Time = "06:05";
            Assert.AreEqual(365, activity.StartMinute, "06:05 should be starting at minute 365");
            activity.Time = "00:00";
            Assert.AreEqual(0, activity.StartMinute, "00:00 should be starting at minute 0");
            activity.Time = "23:59";
            Assert.AreEqual(1439, activity.StartMinute, "23:59 should be starting at minute 1439");

            activity.StartMinute = 0;
            Assert.AreEqual("00:00", activity.Time, "Starting minute 0 should be 00:00");
            activity.StartMinute = 158;
            Assert.AreEqual("02:38", activity.Time, "Starting minute 158 should be 02:38");
            activity.StartMinute = 2345;
            Assert.AreEqual("15:05", activity.Time, "Starting minute 2345 should wrap to 15:05 the next day");
        }

        [TestMethod]
        public void PropertiesTest()
        {
            core.Activity activity;

            activity = new core.Activity();
            Assert.AreEqual(string.Empty, activity.Code, "Initial code should be empty string");
            Assert.AreEqual(string.Empty, activity.Description, "Initial description should be empty string");
            Assert.AreEqual("00:00", activity.Time, "Initial time should be 00:00");

            activity = new core.Activity("12345");
            Assert.AreEqual("12345", activity.Code, "Code not assigned correctly in constructor");

            activity = new core.Activity("234", "Shiny");
            Assert.AreEqual("234", activity.Code, "Code not assigned correctly in constructor");
            Assert.AreEqual("Shiny", activity.Description, "Description not assigned correctly in constructor");

            activity = new core.Activity("3", "Dull", "2:0");
            Assert.AreEqual("3", activity.Code, "Code not assigned correctly in constructor");
            Assert.AreEqual("Dull", activity.Description, "Description not assigned correctly in constructor");
            Assert.AreEqual("02:00", activity.Time, "Time not assigned correctly in constructor");

            activity = new core.Activity("01010", "Matte", 180);
            Assert.AreEqual(180, activity.StartMinute, "Start minute not assigned correctly in constructor");

            activity = new core.Activity();
        }
    }
}
