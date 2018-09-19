using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using core = TimecardsCore.Models;
using data = TimecardsData;

namespace TimecardsTesting.DataTests
{
    [TestClass]
    public class TimecardDataTests
    {
        private const string TEST_CONNECTION_STRING_NAME = "TestDb";

        [TestInitialize]
        public void Initialize()
        {
            // delete the test database if it exists - it will be recreated on first use of EF
            var connString = ConfigurationManager.ConnectionStrings[TEST_CONNECTION_STRING_NAME].ConnectionString;
            var pieces = connString.Split(';');
            foreach (var piece in pieces)
            {
                if (piece.ToLower().Contains("data source="))
                {
                    var fileName = piece.Substring(piece.IndexOf("=") + 1).Trim();
                    File.Delete(fileName);
                    break;
                }
            }
        }

        [TestMethod]
        public void LoadSaveReportTest()
        {
            using (var repo = new data.Repository(TEST_CONNECTION_STRING_NAME))
            {
                DateTime FirstDate = new DateTime(2018, 9, 10);
                DateTime SecondDate = new DateTime(2018, 9, 11);

                //
                // test saving, retrieving, and updating a timecard
                //

                var savedTimecard = new core.Timecard();
                Assert.AreEqual(0, savedTimecard.ID, "Unsaved timecard should have ID = 0");

                repo.SaveTimecard(savedTimecard);
                Assert.AreNotEqual(0, savedTimecard.ID, "Saved timecard should not have ID = 0");

                var retrievedTimecard = repo.GetTimecard(savedTimecard.ID);
                Assert.AreEqual(savedTimecard.ID, retrievedTimecard.ID, "Retreived timecard IDs don't match");
                Assert.AreEqual(savedTimecard.Date, retrievedTimecard.Date, "Retreived timecard dates don't match");

                savedTimecard.Date = FirstDate;
                repo.SaveTimecard(savedTimecard);
                retrievedTimecard = repo.GetTimecard(FirstDate);
                Assert.AreNotEqual(null, retrievedTimecard, "Could not retrieve timecard by date");
                Assert.AreEqual(savedTimecard.ID, retrievedTimecard.ID, "ID is different on timecard retrieved by date");
                Assert.AreEqual(FirstDate, retrievedTimecard.Date, "Date is different on timecard retrieved by date");

                try
                {
                    var notUsedTimecard = repo.GetTimecard(987654321);
                }
                catch (data.NotFoundException)
                {
                    // successful if this exception is caught
                }

                //
                // test saving and retrieving set of activities
                //

                savedTimecard.Activities.AddRange(new List<core.Activity>
                {
                    new core.Activity("00000", "Got to work", "08:00"),
                    new core.Activity("00100", "Did this", "09:00"),
                    new core.Activity("00200", "Did that", "10:00"),
                    new core.Activity("", "Lunch break", "12:00"),
                    new core.Activity("00200", "Did more of that", "13:00"),
                    new core.Activity("00300", "Did something else", "14:00"),
                    new core.Activity("", "Went home", "17:00"),
                });
                Assert.IsTrue(savedTimecard.Activities.Exists(a => a.ID == 0), "Unsaved activities should have ID = 0");

                repo.SaveActivities(savedTimecard);
                Assert.IsFalse(savedTimecard.Activities.Exists(a => a.ID == 0), "Saved activities should not have ID = 0");

                // this next test expects the new activities to be saved in the order they exist in the list
                repo.GetActivities(retrievedTimecard);
                Assert.AreEqual(savedTimecard.Activities.Count, retrievedTimecard.Activities.Count,
                    "The retrieved timecard does not have the same number of activities it started with");
                for (var i = 0; i < retrievedTimecard.Activities.Count; ++i)
                {
                    Assert.AreEqual(savedTimecard.Activities[i].ID, retrievedTimecard.Activities[i].ID,
                        "The retrieved list of activities has an unexpected ID value");
                    Assert.AreEqual(savedTimecard.Activities[i].Code, retrievedTimecard.Activities[i].Code,
                        "The retrieved list of activities has an unexpected Code value");
                    Assert.AreEqual(savedTimecard.Activities[i].Description, retrievedTimecard.Activities[i].Description,
                        "The retrieved list of activities has an unexpected Description value");
                    Assert.AreEqual(savedTimecard.Activities[i].Time, retrievedTimecard.Activities[i].Time,
                        "The retrieved list of activities has an unexpected Time value");
                    Assert.AreEqual(savedTimecard.Activities[i].StartMinute, retrievedTimecard.Activities[i].StartMinute,
                        "The retrieved list of activities has an unexpected StartMinute value");
                }

                //
                // test saving, retrieving, and updating an activity
                //

                var savedActivity = new core.Activity("00400", "One more thing", "15:00")
                {
                    TimecardID = savedTimecard.ID
                };
                Assert.AreEqual(0, savedActivity.ID, "Unsaved activity should have ID = 0");

                repo.SaveActivity(savedActivity);
                Assert.AreNotEqual(0, savedActivity.ID, "Saved activity should not have ID = 0");

                var retrievedActivity = repo.GetActivity(savedActivity.ID);
                Assert.AreEqual(savedActivity.ID, retrievedActivity.ID,
                    "Retrieved activity has different ID");
                Assert.AreEqual(savedActivity.Code, retrievedActivity.Code,
                    "Retrieved activity has different Code");
                Assert.AreEqual(savedActivity.Description, retrievedActivity.Description,
                    "Retrieved activity has different Description");
                Assert.AreEqual(savedActivity.Time, retrievedActivity.Time,
                    "Retrieved activity has different Time");
                Assert.AreEqual(savedActivity.StartMinute, retrievedActivity.StartMinute,
                    "Retrieved activity has different StartMinute");

                savedActivity.Description = "Worked on one more thing";
                savedActivity.Time = "16:00";
                repo.SaveActivity(savedActivity);
                retrievedActivity = repo.GetActivity(savedActivity.ID);

                Assert.AreEqual(savedActivity.ID, retrievedActivity.ID,
                    "Updated activity does not have the same ID");
                Assert.AreEqual(savedActivity.Code, retrievedActivity.Code,
                    "Updated activity does not have the same Code");
                Assert.AreEqual(savedActivity.Description, retrievedActivity.Description,
                    "Updated activity does not have the same Description");
                Assert.AreEqual(savedActivity.Time, retrievedActivity.Time,
                    "Updated activity does not have the same Time");
                Assert.AreEqual(savedActivity.StartMinute, retrievedActivity.StartMinute,
                    "Updated activity does not have the same StartMinute");

                try
                {
                    var notUsedActivity = repo.GetActivity(987654321);
                }
                catch (data.NotFoundException)
                {
                    // successful if this exception is caught
                }

                //
                // test reporting on activity by code
                //

                var anotherTimecard = new core.Timecard()
                {
                    Date = SecondDate
                };
                anotherTimecard.Activities.AddRange(new List<core.Activity>
                {
                    new core.Activity("00000", "Got to work", "09:00"),
                    new core.Activity("00200", "Half day on that", "9:00"),
                    new core.Activity("", "Went home", "12:00"),
                });
                repo.SaveTimecard(anotherTimecard);

                var report = repo.GetReport(FirstDate, SecondDate);
                Assert.AreEqual(5, report.Count, $"Was expecting 5 codes in report, but got {report.Count}");

                Assert.AreEqual("00000", report[0].Code, $"Unexpected code {report[0].Code} in first report item");
                Assert.AreEqual("00100", report[1].Code, $"Unexpected code {report[0].Code} in second report item");
                Assert.AreEqual("00200", report[2].Code, $"Unexpected code {report[0].Code} in third report item");
                Assert.AreEqual("00300", report[3].Code, $"Unexpected code {report[0].Code} in fourth report item");
                Assert.AreEqual("00400", report[4].Code, $"Unexpected code {report[0].Code} in fifth report item");

                Assert.AreEqual(FirstDate, report[0].EarliestDate, "Earliest date is wrong in first report item");
                Assert.AreEqual(FirstDate, report[1].EarliestDate, "Earliest date is wrong in second report item");
                Assert.AreEqual(FirstDate, report[2].EarliestDate, "Earliest date is wrong in third report item");
                Assert.AreEqual(FirstDate, report[3].EarliestDate, "Earliest date is wrong in fourth report item");
                Assert.AreEqual(FirstDate, report[4].EarliestDate, "Earliest date is wrong in fifth report item");

                Assert.AreEqual(SecondDate, report[0].LatestDate, "Latest date is wrong in first report item");
                Assert.AreEqual(FirstDate, report[1].LatestDate, "Latest date is wrong in second report item");
                Assert.AreEqual(SecondDate, report[2].LatestDate, "Latest date is wrong in third report item");
                Assert.AreEqual(FirstDate, report[3].LatestDate, "Latest date is wrong in fourth report item");
                Assert.AreEqual(FirstDate, report[4].LatestDate, "Latest date is wrong in fifth report item");

                Assert.AreEqual(60, report[0].TotalMinutes,
                    $"Unexpected {report[0].TotalMinutes} TotalMinutes in first report item");
                Assert.AreEqual(60, report[1].TotalMinutes,
                    $"Unexpected {report[1].TotalMinutes} TotalMinutes in second report item");
                Assert.AreEqual(360, report[2].TotalMinutes,
                    $"Unexpected {report[2].TotalMinutes} TotalMinutes in third report item");
                Assert.AreEqual(120, report[3].TotalMinutes,
                    $"Unexpected {report[3].TotalMinutes} TotalMinutes in fourth report item");
                Assert.AreEqual(60, report[4].TotalMinutes,
                    $"Unexpected {report[4].TotalMinutes} TotalMinutes in fifth report item");

                //
                // test timecard list and deletes
                //

                var timecardList = repo.GetTimecards();
                Assert.AreEqual(2, timecardList.Count, "Did not retrieve list of two timecards");

                retrievedTimecard = repo.GetTimecard(anotherTimecard.ID);
                var activityCountBefore = retrievedTimecard.Activities.Count;
                Assert.AreNotEqual(0, activityCountBefore, "Retrieving timecard did not retrieve activities");

                repo.DeleteActivity(retrievedTimecard.Activities[0].ID);
                retrievedTimecard = repo.GetTimecard(retrievedTimecard.ID);
                var activityCountAfter = retrievedTimecard.Activities.Count;
                Assert.AreEqual(1, activityCountBefore - activityCountAfter,
                    "Did not see a difference of 1 in tally of activities after delete");

                repo.DeleteTimecard(timecardList[0].ID);
                repo.DeleteTimecard(timecardList[1].ID);
                timecardList = repo.GetTimecards();
                Assert.AreEqual(0, timecardList.Count, "Still some timecards after deletion");
            }
        }
    }
}
