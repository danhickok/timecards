using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using core = TimecardsCore;

namespace TimecardsTesting.CoreTests
{
    [TestClass]
    public class TimecardCoreTests
    {
        private DateTime _orginalDateTime;

        [TestInitialize]
        public void Initialize()
        {
            _orginalDateTime = core.Configuration.CurrentDateTime;
        }

        [TestMethod]
        public void PropertiesAndContainmentTest()
        {
            core.Configuration.CurrentDateTime = new DateTime(2019, 11, 1, 8, 0, 0);
            var timecard = new core.Models.Timecard();

            timecard.Date = core.Configuration.CurrentDateTime.Date;

            var someDay = new DateTime(2018, 7, 9);
            timecard.Date = someDay;
            Assert.AreEqual(someDay, timecard.Date);

            core.Configuration.CurrentDateTime = new DateTime(2019, 11, 1, 8, 5, 0);
            timecard.Activities.Add(new core.Models.Activity("00000", "First activity at 8:05am"));

            core.Configuration.CurrentDateTime = new DateTime(2019, 11, 1, 17, 0, 0);
            timecard.Activities.Add(new core.Models.Activity("00001", "Second activity at 5pm"));

            core.Configuration.CurrentDateTime = new DateTime(2019, 11, 2, 1, 30, 0);
            timecard.Activities.Add(new core.Models.Activity("00002", "Third activity at 1:30am the next day"));

            Assert.AreEqual(3, timecard.Activities.Count);
            Assert.IsTrue(!timecard.Activities[0].IsAfterMidnight, "Morning same day has 'after midnight' flag set");
            Assert.IsTrue(!timecard.Activities[1].IsAfterMidnight, "Afternoon same day has 'after midnight' flag set");
            Assert.IsTrue(timecard.Activities[2].IsAfterMidnight, "Morning next day does not have 'after midnight' flag set");

            core.Configuration.CurrentDateTime = new DateTime(2019, 11, 1, 11, 15, 0);
            timecard.Activities.Insert(1, new core.Models.Activity("00042", "After first activity at 11:15am", "11:15"));

            Assert.AreEqual(4, timecard.Activities.Count);
            Assert.AreEqual("00042", timecard.Activities[1].Code, "Inserted activity not in expected place");
            Assert.IsTrue(!timecard.Activities[1].IsAfterMidnight, "Later morning has 'after midnight' flag set");

            core.Configuration.CurrentDateTime = new DateTime(2019, 11, 2, 2, 52, 0);
            timecard.Activities.Add(new core.Models.Activity("00084", "Next day activity with user-provided time", "2:45"));

            Assert.AreEqual(5, timecard.Activities.Count);
            Assert.AreEqual("00084", timecard.Activities[4].Code, "Appended activity not in expected place");
            Assert.AreEqual(1605, timecard.Activities[4].StartMinute, "User-provided time for next day has incorrect start minute");
            Assert.IsTrue(timecard.Activities[4].IsAfterMidnight, "User-provided time for next day does not have 'after midnight' flag set");

            timecard.Activities.RemoveAll(a => true);

            Assert.AreEqual(0, timecard.Activities.Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            core.Configuration.CurrentDateTime = _orginalDateTime;
        }
    }
}
