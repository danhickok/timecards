using TC = TimecardsCore;
using TM = TimecardsCore.Models;

namespace TimecardsTesting.CoreTests
{
    public class TimecardCoreTests
    {
        private DateTime _orginalDateTime;

        [SetUp]
        public void Initialize()
        {
            _orginalDateTime = TC.Configuration.CurrentDateTime;
        }

        [Test]
        public void PropertiesAndContainmentTest()
        {
            TC.Configuration.CurrentDateTime = new DateTime(2019, 11, 1, 8, 0, 0);

            TM.Timecard timecard = new();

            timecard.Date = TC.Configuration.CurrentDateTime.Date;
            Assert.That(timecard.Date, Is.EqualTo(TC.Configuration.CurrentDateTime.Date));

            TC.Configuration.CurrentDateTime = new DateTime(2019, 11, 1, 8, 5, 0);
            timecard.Activities.Add(new TM.Activity("00000", "First activity at 8:05am"));

            TC.Configuration.CurrentDateTime = new DateTime(2019, 11, 1, 17, 0, 0);
            timecard.Activities.Add(new TM.Activity("00001", "Second activity at 5pm"));

            TC.Configuration.CurrentDateTime = new DateTime(2019, 11, 2, 1, 30, 0);
            timecard.Activities.Add(new TM.Activity("00002", "Third activity at 1:30am the next day"));

            Assert.Multiple(() =>
            {
                Assert.That(timecard.Activities, Has.Count.EqualTo(3));
                Assert.That(timecard.Activities[0].IsAfterMidnight, Is.False, "Morning same day has 'after midnight' flag set");
                Assert.That(timecard.Activities[1].IsAfterMidnight, Is.False, "Afternoon same day has 'after midnight' flag set");
                Assert.That(timecard.Activities[2].IsAfterMidnight, Is.True, "Morning next day does not have 'after midnight' flag set");
            });

            TC.Configuration.CurrentDateTime = new DateTime(2019, 11, 1, 11, 15, 0);
            timecard.Activities.Insert(1, new TM.Activity("00042", "After first activity at 11:15am", "11:15"));

            Assert.Multiple(() =>
            {
                Assert.That(timecard.Activities, Has.Count.EqualTo(4));
                Assert.That(timecard.Activities[1].Code, Is.EqualTo("00042"), "Inserted activity not in expected place");
                Assert.That(!timecard.Activities[1].IsAfterMidnight, Is.True, "Later morning has 'after midnight' flag set");
            });

            TC.Configuration.CurrentDateTime = new DateTime(2019, 11, 2, 2, 52, 0);
            timecard.Activities.Add(new TM.Activity("00084", "Next day activity with user-provided time", "2:45"));

            Assert.Multiple(() =>
            {
                Assert.That(timecard.Activities, Has.Count.EqualTo(5));
                Assert.That(timecard.Activities[4].Code, Is.EqualTo("00084"), "Appended activity not in expected place");
                Assert.That(timecard.Activities[4].StartMinute, Is.EqualTo(1605), "User-provided time for next day has incorrect start minute");
                Assert.That(timecard.Activities[4].IsAfterMidnight, Is.True, "User-provided time for next day does not have 'after midnight' flag set");
            });

            timecard.Activities.RemoveAll(a => true);
            Assert.That(timecard.Activities, Is.Empty, "Failed to remove all activities");
        }

        [TearDown]
        public void Cleanup()
        {
            TC.Configuration.CurrentDateTime = _orginalDateTime;
        }
    }
}
