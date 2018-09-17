using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using core = TimecardsCore;

namespace TimecardsTesting.CoreTests
{
    [TestClass]
    public class TimecardCoreTests
    {
        [TestMethod]
        public void PropertiesAndContainmentTest()
        {
            var timecard = new core.Models.Timecard();

            timecard.Date = DateTime.Today;

            var someDay = new DateTime(2018, 7, 9);
            timecard.Date = someDay;
            Assert.AreEqual(someDay, timecard.Date);

            timecard.Activities.Add(new core.Models.Activity { Code = "00000" });
            timecard.Activities.Add(new core.Models.Activity { Code = "00001" });
            timecard.Activities.Add(new core.Models.Activity { Code = "00002" });

            Assert.AreEqual(3, timecard.Activities.Count);

            timecard.Activities.Insert(1, new core.Models.Activity { Code = "00042" });

            Assert.AreEqual(4, timecard.Activities.Count);

            timecard.Activities.RemoveAll(a => true);

            Assert.AreEqual(0, timecard.Activities.Count);
        }
    }
}
