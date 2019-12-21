using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using ci = TimecardsCore.Interfaces;
using cl = TimecardsCore.Logic;
using core = TimecardsCore.Models;
using cx = TimecardsCore.Exceptions;
using data = TimecardsData;
using ioc = TimecardsIOC;

namespace TimecardsTesting.IntegrationTests
{
    [TestClass]
    public class DataIntegrationTests
    {
        private ioc.Factory _factory = null;

        [TestInitialize]
        public void Initialize()
        {
            // make the IOC container
            _factory = new ioc.Factory();

            // register a data repository like outermost layer would do
            _factory.Register<ci.IAppConstants>(typeof(Base.TestAppConstants), false);
            _factory.Register<ci.IRepository>(typeof(data.Repository), true, typeof(ci.IAppConstants));
        }

        [TestMethod]
        public void CoreDataTest()
        {
            core.Timecard tcIn;
            core.Timecard tcOut;

            // use it to get a reference to the repo like core would do
            ci.IRepository repoIn = _factory.Resolve<ci.IRepository>();

            // make a core timecard object
            tcIn = new core.Timecard { Date = new DateTime(2019, 6, 1) };
            Assert.AreEqual(tcIn.ID, 0, "Timecard did not start with ID 0 as expected");

            tcIn.Activities.AddRange(new[]
            {
                new core.Activity("00000", "Arrived; made coffee", "08:00"),
                new core.Activity("00100", "Called a client", "08:15"),
                new core.Activity("00200", "Worked on a project", "09:00"),
                new core.Activity("", "Lunch", "12:00"),
                new core.Activity("00200", "More work", "13:00"),
                new core.Activity("", "Departed", "17:00"),
            });

            // save it
            repoIn.SaveTimecard(tcIn);
            Assert.IsTrue(tcIn.ID > 0, "Didn't get a valid ID after saving timecard");

            // get another reference to the repo
            ci.IRepository repoOut = _factory.Resolve<ci.IRepository>();

            // retrieve the saved timecard
            tcOut = repoOut.GetTimecard(tcIn.ID);
            Assert.AreEqual(tcIn.ID, tcOut.ID, "Retrieved timecard ID doesn't match saved timecard ID");
            Assert.AreEqual(tcOut.Activities.Count, 6, "Didn't receive expected number of activity children");

            // cleanup at end of test
            repoOut.DeleteAllTimecards();
        }

        [TestMethod]
        public void CoreTimecardLogicTest()
        {
            core.Timecard tc;
            var logic = new cl.TimecardLogic(_factory);
            var ids = new int[4];

            var dates = new DateTime[4];
            dates[0] = new DateTime(2019, 7, 1);
            dates[1] = new DateTime(2019, 7, 2);
            dates[2] = new DateTime(2019, 7, 3);
            dates[3] = DateTime.Today;

            Assert.IsTrue(DateTime.Today > dates[2], $"These tests must be run after {dates[2]}");

            // examine current (empty) timecard
            tc = logic.GetCurrentTimecard();
            Assert.IsTrue(tc != null, "new logic object doesn't have a timecard");
            Assert.AreEqual(0, tc.ID, "new logic object doesn't have a new timecard");
            Assert.AreEqual(false, tc.IsDirty, "new logic object does not have clean timecard");

            // populate and save timecard
            tc.Date = dates[0];
            logic.SaveTimecard();
            Assert.AreNotEqual(0, tc.ID, "saved timecard still has zero ID");
            ids[0] = tc.ID;

            // create second timecard
            tc = logic.GetNewTimecard();
            Assert.AreEqual(0, tc.ID, "logic-generated new timecard does not have zero ID");
            Assert.AreEqual(false, tc.IsDirty, "logic-generated new timecard isn't clean");

            // populate and save second timecard
            tc.Date = dates[1];
            logic.SaveTimecard();
            Assert.AreNotEqual(ids[0], tc.ID, "second timecard has same ID as the first");
            ids[1] = tc.ID;

            // create, populate, and save third timecard
            tc = logic.GetNewTimecard();
            tc.Date = dates[2];
            logic.SaveTimecard();
            Assert.AreNotEqual(ids[0], tc.ID, "third timecard has same ID as the first");
            Assert.AreNotEqual(ids[1], tc.ID, "third timecard has same ID as the second");
            ids[2] = tc.ID;

            // get earliest timecard
            tc = logic.GetEarliestTimecard();
            Assert.AreEqual(ids[0], tc.ID, "logic did not return first timecard as the earliest");
            Assert.AreEqual(dates[0], tc.Date, "logic did not return expected date in earliest timecard");

            // get latest timecard
            tc = logic.GetLatestTimecard();
            Assert.AreEqual(ids[2], tc.ID, "logic did not return third timecard as the latest");
            Assert.AreEqual(dates[2], tc.Date, "logic did not return expected date in latest timecard");

            // get timecard by ID
            tc = logic.GetSpecificTimecard(ids[1]);
            Assert.AreEqual(ids[1], tc.ID, "logic did not return specific timecard");
            Assert.AreEqual(dates[1], tc.Date, "logic did not return expected date in specific timecard");
            Assert.ThrowsException<cx.TimecardNotFoundException>(() => { logic.GetSpecificTimecard(987654321); },
                "Did not get expected exception when retrieving nonexistent timecard");

            // get today's timecard (should return a new, empty timecard)
            tc = logic.GetTodaysTimecard();
            Assert.AreEqual(0, tc.ID, "logic did not return a new timecard as today's card");
            Assert.AreEqual(false, tc.IsDirty, "logic did not return a clean timecard as today's card");

            // set a date, add some activity, and save
            tc.Date = dates[3];
            tc.Activities.Add(new core.Activity("00000", "Arrived", "8:00"));
            tc.Activities.Add(new core.Activity("00100", "Worked on first project", "8:15"));
            tc.Activities.Add(new core.Activity("00200", "Worked on second project", "10:30"));
            tc.Activities.Add(new core.Activity("", "Lunch break", "12:00"));
            tc.Activities.Add(new core.Activity("00200", "Worked more on second project", "13:00"));
            tc.Activities.Add(new core.Activity("", "Departed", "17:00"));
            logic.SaveTimecard();
            ids[3] = tc.ID;

            // make a new timecard, then get latest card again
            logic.GetNewTimecard();
            tc = logic.GetLatestTimecard();
            Assert.IsTrue(tc.ID != ids[0] && tc.ID != ids[1] && tc.ID != ids[2],
                "logic did not return expected ID for populated latest card");
            Assert.AreEqual(dates[3], tc.Date,
                "logic did not return expected date for populated latest card");
            Assert.AreEqual(6, tc.Activities.Count,
                "logic did not return expected number of activities for populated latest card");

            // delete an activity
            logic.DeleteActivity(2);
            logic.GetNewTimecard();
            tc = logic.GetLatestTimecard();
            Assert.AreEqual(5, tc.Activities.Count, "Count wrong after deleting an activity");

            // get count of timecards
            var count = logic.GetTimecardCount();
            Assert.AreEqual(4, count, "logic did not return expected number of timecards");

            // test next/previous
            logic.GetSpecificTimecard(ids[1]);
            tc = logic.GetPreviousTimecard();
            Assert.AreEqual(ids[0], tc.ID, "Did not get expected previous timecard");
            tc = logic.GetPreviousTimecard();
            Assert.AreEqual(ids[0], tc.ID, "Navigation before earliest timecard not get earliest timecard");

            logic.GetSpecificTimecard(ids[1]);
            tc = logic.GetNextTimecard();
            Assert.AreEqual(ids[2], tc.ID, "Did not get expected next timecard");
            logic.GetTodaysTimecard();
            tc = logic.GetNextTimecard();
            Assert.AreEqual(ids[3], tc.ID, "Navigation after latest timecard not get latest timecard");

            // test timecard list
            var tclist = logic.GetTimecardList();
            Assert.AreEqual(4, tclist.Count, "Did not receive expected number of items in timecard list");
            Assert.AreEqual(ids[3], tclist[0].Key, "First item in timecard list isn't latest timecard");
            Assert.AreEqual(ids[0], tclist[3].Key, "Last item in timecard list isn't first timecard");

            // test deleting timecard
            logic.GetTodaysTimecard();
            logic.DeleteTimecard();
            tc = logic.GetCurrentTimecard();
            Assert.AreEqual(ids[2], tc.ID, "Did not end up on previous timecard after deletion");

            // wipe out all timecards
            logic.DeleteAllTimecards();
            count = logic.GetTimecardCount();
            Assert.AreEqual(0, count, "logic failed to delete all timecards");
        }

        [TestMethod]
        public void CoreBulkLogicTest()
        {
            List<core.Timecard> tcList;

            //
            // export tests
            //

            // define some data to be exported
            var dates = new[]
            {
                new DateTime(2019, 12, 1),
                new DateTime(2019, 12, 2),
                new DateTime(2019, 12, 3),
                new DateTime(2019, 12, 4),
                new DateTime(2019, 12, 5),
            };

            DeleteAllTimecards();
            var tally = MakeTimecardsForEachDate(dates);

            // make a bulklogic object
            var bulk = new cl.BulkLogic(_factory);

            // export all data, CSV
            var csvData = bulk.Export(null, null, cl.BulkLogic.DataFormat.CommaDelimitedText);
            var csvLines = csvData.Split('\n');
            Assert.AreEqual(tally.TimecardCount * tally.ActivityCount + 1, csvLines.Length,
                "Bulk export all data as CSV did not yield expected number of lines");

            // export all data, tab-delimited
            var tsvData = bulk.Export(null, null, cl.BulkLogic.DataFormat.TabDelimitedText);
            var tsvLines = tsvData.Split('\n');
            Assert.AreEqual(tally.TimecardCount * tally.ActivityCount + 1, tsvLines.Length,
                "Bulk export all data as TSV did not yield expected number of lines");

            // export all data, JSON
            var json = bulk.Export(null, null, cl.BulkLogic.DataFormat.JSON);
            var tcOut = JsonConvert.DeserializeObject<List<core.Timecard>>(json);
            Assert.AreEqual(tally.TimecardCount, tcOut.Count,
                "Bulk export all data as JSON did not yield expected number of timecards");

            // export all data, XML
            var xmlString = bulk.Export(null, null, cl.BulkLogic.DataFormat.XML);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(xmlString));
            Assert.AreEqual(tally.TimecardCount, xmlDoc.SelectNodes("/Timecards/Timecard").Count,
                "Bulk export all data as XML did not yield expected number of timecards");

            // export limited range, CSV
            csvData = bulk.Export(dates[2], dates[3], cl.BulkLogic.DataFormat.CommaDelimitedText);
            csvLines = csvData.Split('\n');
            Assert.AreEqual(2 * tally.ActivityCount + 1, csvLines.Length,
                "Bulk export range as CSV did not yield expected number of lines");

            // export limited range, tab-delimited
            tsvData = bulk.Export(dates[2], dates[3], cl.BulkLogic.DataFormat.TabDelimitedText);
            tsvLines = tsvData.Split('\n');
            Assert.AreEqual(2 * tally.ActivityCount + 1, tsvLines.Length,
                "Bulk export all data as TSV did not yield expected number of lines");

            // export limited range, JSON
            json = bulk.Export(dates[2], dates[3], cl.BulkLogic.DataFormat.JSON);
            tcOut = JsonConvert.DeserializeObject<List<core.Timecard>>(json);
            Assert.AreEqual(2, tcOut.Count,
                "Bulk export all data as JSON did not yield expected number of timecards");

            // export limited range, XML
            xmlString = bulk.Export(dates[2], dates[3], cl.BulkLogic.DataFormat.XML);
            xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(xmlString));
            Assert.AreEqual(2, xmlDoc.SelectNodes("/Timecards/Timecard").Count,
                "Bulk export all data as XML did not yield expected number of timecards");

            //
            // import tests
            //

            // import CSV
            DeleteAllTimecards();

            var csvPath = Path.GetTempFileName();
            tally = WriteCsvFile(csvPath);
            bulk.Import(csvPath, cl.BulkLogic.DataFormat.CommaDelimitedText);

            tcList = GetAllTimecards();
            Assert.AreEqual(tally.TimecardCount, tcList.Count,
                "Bulk import of CSV data does not produce expected number of timecards");
            Assert.IsTrue(tcList.Any(tc => tc.Activities.Count != tally.ActivityCount),
                "Bulk import of CSV data resulted in one or more timecards without correct number of activities");

            File.Delete(csvPath);

            // import TSV
            DeleteAllTimecards();

            var tsvPath = Path.GetTempFileName();
            tally = WriteTsvFile(tsvPath);
            bulk.Import(tsvPath, cl.BulkLogic.DataFormat.TabDelimitedText);

            tcList = GetAllTimecards();
            Assert.AreEqual(tally.TimecardCount, tcList.Count,
                "Bulk import of TSV data does not produce expected number of timecards");
            Assert.IsTrue(tcList.Any(tc => tc.Activities.Count != tally.ActivityCount),
                "Bulk import of TSV data resulted in one or more timecards without correct number of activities");

            File.Delete(tsvPath);

            // import JSON
            DeleteAllTimecards();

            var jsonPath = Path.GetTempFileName();
            tally = WriteJsonFile(jsonPath);
            bulk.Import(jsonPath, cl.BulkLogic.DataFormat.JSON);

            tcList = GetAllTimecards();
            Assert.AreEqual(tally.TimecardCount, tcList.Count,
                "Bulk import of JSON data does not produce expected number of timecards");
            Assert.IsTrue(tcList.Any(tc => tc.Activities.Count != tally.ActivityCount),
                "Bulk import of JSON data resulted in one or more timecards without correct number of activities");

            File.Delete(jsonPath);

            // import XML
            DeleteAllTimecards();

            var xmlPath = Path.GetTempFileName();
            tally = WriteXmlFile(xmlPath);
            bulk.Import(xmlPath, cl.BulkLogic.DataFormat.XML);

            tcList = GetAllTimecards();
            Assert.AreEqual(tally.TimecardCount, tcList.Count,
                "Bulk import of XML data does not produce expected number of timecards");
            Assert.IsTrue(tcList.Any(tc => tc.Activities.Count != tally.ActivityCount),
                "Bulk import of XML data resulted in one or more timecards without correct number of activities");

            File.Delete(xmlPath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_factory != null)
            {
                _factory.Dispose();
                _factory = null;
            }
        }

        #region Private methods

        private (int TimecardCount, int ActivityCount) MakeTimecardsForEachDate(DateTime[] dates)
        {
            var repo = _factory.Resolve<ci.IRepository>();

            var count = 0;

            foreach (var date in dates)
            {
                var tc = new core.Timecard() { Date = date };
                tc.Activities.AddRange(new[]
                {
                    new core.Activity("00000", "Arrived", "08:00"),
                    new core.Activity("", "Departed", "17:00"),
                });
                repo.SaveTimecard(tc);
                count++;
            }

            return (count, 2);
        }

        private List<core.Timecard> GetAllTimecards()
        {
            var repo = _factory.Resolve<ci.IRepository>();
            return repo.GetTimecards(null, null);
        }

        private void DeleteAllTimecards()
        {
            var repo = _factory.Resolve<ci.IRepository>();
            repo.DeleteAllTimecards();
        }

        private (int TimecardCount, int ActivityCount) WriteCsvFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine("Date,Code,Description,Time,IsAfterMidnight");
                sw.WriteLine("\"2019-12-07\",\"00000\",\"Arrived\",\"08:00\",false");
                sw.WriteLine("\"2019-12-07\",\"\",\"Departed\",\"17:00\",false");
                sw.WriteLine("\"2019-12-08\",\"00000\",\"Arrived\",\"08:00\",false");
                sw.WriteLine("\"2019-12-08\",\"\",\"Departed\",\"17:00\",false");
            }

            return (2, 2);
        }

        private (int TimecardCount, int ActivityCount) WriteTsvFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine("Date\tCode\tDescription\tTime\tIsAfterMidnight");
                sw.WriteLine("2019-12-07\t00000\tArrived\t08:00\tfalse");
                sw.WriteLine("2019-12-07\t\tDeparted\t17:00\tfalse");
                sw.WriteLine("2019-12-08\t00000\tArrived\t08:00\tfalse");
                sw.WriteLine("2019-12-08\t\tDeparted\t17:00\tfalse");
            }

            return (2, 2);
        }

        private (int TimecardCount, int ActivityCount) WriteJsonFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(
@"[
  {
    ""Date"": ""2019-12-07"",
    ""Activities"": [
      {
        ""Code"": ""00000"",
        ""Description"": ""Arrived"",
        ""Time"": ""08:00"",
                ""IsAfterMidnight"": false
      },
      {
        ""Code"": """",
        ""Description"": ""Departed"",
        ""Time"": ""17:00"",
        ""IsAfterMidnight"": false
      }
    ]
  },
  {
    ""Date"": ""2019-12-08"",
    ""Activities"": [
      {
        ""Code"": ""00000"",
        ""Description"": ""Arrived"",
        ""Time"": ""08:00"",
                ""IsAfterMidnight"": false
      },
      {
        ""Code"": """",
        ""Description"": ""Departed"",
        ""Time"": ""17:00"",
        ""IsAfterMidnight"": false
      }
    ]
  }
]"
                );
            }

            return (2, 2);
        }

        private (int TimecardCount, int ActivityCount) WriteXmlFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(
@"<?xml version=""1.0"" encoding=""utf-16""?>
<Timecards>
  <Timecard Date=""2019-12-07"">
    <Activities>
      <Activity Code=""00000"" Description=""Arrived"" Time=""08:00"" IsAfterMidnight=""False"" />
      <Activity Code="""" Description=""Departed"" Time=""17:00"" IsAfterMidnight=""False"" />
    </Activities>
  </Timecard>
  <Timecard Date=""2019-12-08"">
    <Activities>
      <Activity Code=""00000"" Description=""Arrived"" Time=""08:00"" IsAfterMidnight=""False"" />
      <Activity Code="""" Description=""Departed"" Time=""17:00"" IsAfterMidnight=""False"" />
    </Activities>
  </Timecard>
</Timecards>"
                );
            }

            return (2, 2);
        }

        #endregion
    }
}
