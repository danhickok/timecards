using NuGet.Frameworks;
using TD = TimecardsData;
using TM = TimecardsCore.Models;

namespace TimecardsTesting.DataTests
{
    public class TimecardDataTests
    {
        [SetUp]
        public void Setup()
        {
            DeleteAllData();
        }

        [Test]
        public void LoadSaveReportTest()
        {
            using var repo = new TD.Repository(new TestAppConstants());

            DateTime FirstDate = new DateTime(2018, 9, 10);
            DateTime SecondDate = new DateTime(2018, 9, 11);

            //
            // test saving, retrieving, and updating a timecard
            //

            var savedTimecard = new TM.Timecard();
            Assert.That(savedTimecard.ID, Is.EqualTo(0), "Unsaved timecard should have ID = 0");

            repo.SaveTimecard(savedTimecard);
            Assert.That(savedTimecard.ID, Is.Not.EqualTo(0), "Saved timecard should not have ID = 0");

            var retrievedTimecard = repo.GetTimecard(savedTimecard.ID);
            Assert.That(retrievedTimecard, Is.Not.Null, "Failed to retrieve saved timecard by known ID");
            Assert.Multiple(() =>
            {
                Assert.That(retrievedTimecard.ID, Is.EqualTo(savedTimecard.ID), "Retreived timecard IDs don't match");
                Assert.That(retrievedTimecard.Date, Is.EqualTo(savedTimecard.Date), "Retreived timecard dates don't match");
            });
            savedTimecard.Date = FirstDate;
            repo.SaveTimecard(savedTimecard);

            var nonexistentTimecard = repo.GetTimecard(987654321);
            Assert.That(nonexistentTimecard, Is.Null, "Somehow retrieved a timecard that doesn't exist");

            //
            // test saving and retrieving set of activities
            //

            savedTimecard.Activities.AddRange(
                [
                    new TM.Activity("00000", "Got to work", "08:00"),
                    new TM.Activity("00100", "Did this", "09:00"),
                    new TM.Activity("00200", "Did that", "10:00"),
                    new TM.Activity("", "Lunch break", "12:00"),
                    new TM.Activity("00200", "Did more of that", "13:00"),
                    new TM.Activity("00300", "Did something else", "14:00"),
                    new TM.Activity("", "Went home", "17:00"),
                ]);
            Assert.That(savedTimecard.Activities.All(a => a.ID == 0), Is.True, "Unsaved activities should have ID = 0");

            repo.SaveActivities(savedTimecard);
            Assert.That(savedTimecard.Activities.Exists(a => a.ID == 0), Is.False, "Saved activities should not have ID = 0");

            // this next test expects the new activities to be saved in the order they exist in the list
            repo.GetActivities(retrievedTimecard);
            Assert.That(retrievedTimecard.Activities, Has.Count.EqualTo(savedTimecard.Activities.Count),
                "The retrieved timecard does not have the same number of activities it started with");
            for (var i = 0; i < retrievedTimecard.Activities.Count; ++i)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(retrievedTimecard.Activities[i].ID, Is.EqualTo(savedTimecard.Activities[i].ID),
                        "The retrieved list of activities has an unexpected ID value");
                    Assert.That(retrievedTimecard.Activities[i].Code, Is.EqualTo(savedTimecard.Activities[i].Code),
                        "The retrieved list of activities has an unexpected Code value");
                    Assert.That(retrievedTimecard.Activities[i].Description, Is.EqualTo(savedTimecard.Activities[i].Description),
                        "The retrieved list of activities has an unexpected Description value");
                    Assert.That(retrievedTimecard.Activities[i].Time, Is.EqualTo(savedTimecard.Activities[i].Time),
                        "The retrieved list of activities has an unexpected Time value");
                    Assert.That(retrievedTimecard.Activities[i].StartMinute, Is.EqualTo(savedTimecard.Activities[i].StartMinute),
                        "The retrieved list of activities has an unexpected StartMinute value");
                });
            }

            //
            // test saving, retrieving, and updating an activity
            //

            var savedActivity = new TM.Activity("00400", "One more thing", "15:00")
            {
                TimecardID = savedTimecard.ID
            };
            Assert.That(savedActivity.ID, Is.EqualTo(0), "Unsaved activity should have ID = 0");

            repo.SaveActivity(savedActivity);
            Assert.That(savedActivity.ID, Is.Not.EqualTo(0), "Saved activity should not have ID = 0");

            var retrievedActivity = repo.GetActivity(savedActivity.ID);
            Assert.That(retrievedActivity, Is.Not.Null, "Failed to retrieve saved activity by known ID");
            Assert.Multiple(() =>
            {
                Assert.That(retrievedActivity.ID, Is.EqualTo(savedActivity.ID),
                    "Retrieved activity has different ID");
                Assert.That(retrievedActivity.Code, Is.EqualTo(savedActivity.Code),
                    "Retrieved activity has different Code");
                Assert.That(retrievedActivity.Description, Is.EqualTo(savedActivity.Description),
                    "Retrieved activity has different Description");
                Assert.That(retrievedActivity.Time, Is.EqualTo(savedActivity.Time),
                    "Retrieved activity has different Time");
                Assert.That(retrievedActivity.StartMinute, Is.EqualTo(savedActivity.StartMinute),
                    "Retrieved activity has different StartMinute");
            });

            savedActivity.Description = "Worked on one more thing";
            savedActivity.Time = "16:00";
            repo.SaveActivity(savedActivity);
            retrievedActivity = repo.GetActivity(savedActivity.ID);
            Assert.Multiple(() =>
            {
                Assert.That(retrievedActivity, Is.Not.Null,
                    "Updated activity failed to be retrieved after saving");
                Assert.That(retrievedActivity?.ID, Is.EqualTo(savedActivity.ID),
                    "Updated activity does not have the same ID");
                Assert.That(retrievedActivity?.Code, Is.EqualTo(savedActivity.Code),
                    "Updated activity does not have the same Code");
                Assert.That(retrievedActivity?.Description, Is.EqualTo(savedActivity.Description),
                    "Updated activity does not have the same Description");
                Assert.That(retrievedActivity?.Time, Is.EqualTo(savedActivity.Time),
                    "Updated activity does not have the same Time");
                Assert.That(retrievedActivity?.StartMinute, Is.EqualTo(savedActivity.StartMinute),
                    "Updated activity does not have the same StartMinute");
            });

            var nonexistentActivity = repo.GetActivity(987654321);
            Assert.That(nonexistentActivity, Is.Null,
                "Somehow retrieved an activity that doesn't exist");

            //
            // test reporting on activity by code
            //

            var anotherTimecard = new TM.Timecard()
            {
                Date = SecondDate
            };
            anotherTimecard.Activities.AddRange(
                [
                    new TM.Activity("00000", "Got to work", "09:00"),
                    new TM.Activity("00200", "Half day on that", "9:00"),
                    new TM.Activity("", "Went home", "12:00"),
                ]);
            repo.SaveTimecard(anotherTimecard);

            var report = repo.GetReport(FirstDate, SecondDate);
            Assert.That(report, Is.Not.Null,
                "Failed to retrieve report");
            Assert.That(report, Has.Count.EqualTo(5),
                $"Was expecting 5 codes in report, but got {report.Count}");
            Assert.Multiple(() =>
            {
                Assert.That(report[0].Code, Is.EqualTo("00000"),
                    $"Unexpected code {report[0].Code} in first report item");
                Assert.That(report[1].Code, Is.EqualTo("00100"),
                    $"Unexpected code {report[0].Code} in second report item");
                Assert.That(report[2].Code, Is.EqualTo("00200"),
                    $"Unexpected code {report[0].Code} in third report item");
                Assert.That(report[3].Code, Is.EqualTo("00300"),
                    $"Unexpected code {report[0].Code} in fourth report item");
                Assert.That(report[4].Code, Is.EqualTo("00400"),
                    $"Unexpected code {report[0].Code} in fifth report item");

                Assert.That(report[0].EarliestDate, Is.EqualTo(FirstDate),
                    "Earliest date is wrong in first report item");
                Assert.That(report[1].EarliestDate, Is.EqualTo(FirstDate),
                    "Earliest date is wrong in second report item");
                Assert.That(report[2].EarliestDate, Is.EqualTo(FirstDate),
                    "Earliest date is wrong in third report item");
                Assert.That(report[3].EarliestDate, Is.EqualTo(FirstDate),
                    "Earliest date is wrong in fourth report item");
                Assert.That(report[4].EarliestDate, Is.EqualTo(FirstDate),
                    "Earliest date is wrong in fifth report item");

                Assert.That(report[0].LatestDate, Is.EqualTo(SecondDate),
                    "Latest date is wrong in first report item");
                Assert.That(report[1].LatestDate, Is.EqualTo(FirstDate),
                    "Latest date is wrong in second report item");
                Assert.That(report[2].LatestDate, Is.EqualTo(SecondDate),
                    "Latest date is wrong in third report item");
                Assert.That(report[3].LatestDate, Is.EqualTo(FirstDate),
                    "Latest date is wrong in fourth report item");
                Assert.That(report[4].LatestDate, Is.EqualTo(FirstDate),
                    "Latest date is wrong in fifth report item");

                Assert.That(report[0].TotalMinutes, Is.EqualTo(60),
                    $"Unexpected {report[0].TotalMinutes} TotalMinutes in first report item");
                Assert.That(report[1].TotalMinutes, Is.EqualTo(60),
                    $"Unexpected {report[1].TotalMinutes} TotalMinutes in second report item");
                Assert.That(report[2].TotalMinutes, Is.EqualTo(360),
                    $"Unexpected {report[2].TotalMinutes} TotalMinutes in third report item");
                Assert.That(report[3].TotalMinutes, Is.EqualTo(120),
                    $"Unexpected {report[3].TotalMinutes} TotalMinutes in fourth report item");
                Assert.That(report[4].TotalMinutes, Is.EqualTo(60),
                    $"Unexpected {report[4].TotalMinutes} TotalMinutes in fifth report item");
            });

            //
            // test timecard list and deletes
            //

            var timecardList = repo.GetTimecards(0, 10, true);
            Assert.That(timecardList, Is.Not.Null,
                "Failed to retrieve timecard list");
            Assert.That(timecardList, Has.Count.EqualTo(2),
                "Did not retrieve list of two timecards");
            Assert.That(timecardList[0].Date, Is.GreaterThan(timecardList[1].Date),
                "List of timecards did not retrieve in correct order");

            timecardList = repo.GetTimecards(0, 10, false);
            Assert.That(timecardList, Has.Count.EqualTo(2),
                "Did not retrieve list of two timecards");
            Assert.That(timecardList[0].Date, Is.LessThan(timecardList[1].Date),
                "List of timecards did not retrieve in correct order");

            retrievedTimecard = repo.GetTimecard(anotherTimecard.ID);
            Assert.That(retrievedTimecard, Is.Not.Null,
                "Failed to retrieve another timecard for activity deletion test");
            var activityCountBefore = retrievedTimecard.Activities.Count;
            Assert.That(activityCountBefore, Is.Not.EqualTo(0),
                "Retrieving timecard did not retrieve any activities");

            repo.DeleteActivity(retrievedTimecard.Activities[0].ID);
            retrievedTimecard = repo.GetTimecard(retrievedTimecard.ID);
            Assert.That(retrievedTimecard, Is.Not.Null,
                "Retrieving timecard a second time failed");
            var activityCountAfter = retrievedTimecard.Activities.Count;
            Assert.That(activityCountBefore - activityCountAfter, Is.EqualTo(1),
                "Did not see a difference of 1 in tally of activities after delete");

            repo.DeleteTimecard(timecardList[0].ID);
            timecardList = repo.GetTimecards(0, 10, false);
            Assert.That(timecardList, Is.Not.Null,
                "Failed to retrieve list after timecard deletion");
            Assert.That(timecardList, Has.Count.EqualTo(1),
                "Did not get expected number of timecards after a deletion");

            var numberOfTimecards = repo.GetTimecardCount();
            Assert.That(numberOfTimecards, Is.EqualTo(timecardList.Count),
                "Number of timecards by list is not the same as repo's count function");

            repo.DeleteAllTimecards();
            numberOfTimecards = repo.GetTimecardCount();
            Assert.That(numberOfTimecards, Is.EqualTo(0),
                "Timecard count not zero after deleting all timecards");
        }

        [TearDown]
        public void Teardown()
        {
            DeleteAllData();
        }

        private static void DeleteAllData()
        {
            using var repo = new TD.Repository(new TestAppConstants());

            var timecards = repo.GetTimecards(0, 9999, false);
            foreach (var timecard in timecards)
                repo.DeleteTimecard(timecard.ID);
        }
    }
}
