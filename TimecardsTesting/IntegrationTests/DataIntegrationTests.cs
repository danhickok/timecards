using Newtonsoft.Json;
using System.Text;
using System.Xml;
using TD = TimecardsData;
using TM = TimecardsCore.Models;
using TI = TimecardsCore.Interfaces;
using IC = TimecardsIOC;
using TE = TimecardsCore.Exceptions;
using TL = TimecardsCore.Logic;

namespace TimecardsTesting.IntegrationTests
{
    public class DataIntegrationTests
    {
        private IC.Factory _factory;

        [SetUp]
        public void Initialize()
        {
            // make the IOC container
            _factory = new IC.Factory();

            // register a data repository like outermost layer would do
            _factory.Register<TI.IAppConstants>(typeof(TestAppConstants), false);
            _factory.Register<TI.IRepository>(typeof(TD.Repository), false, typeof(TI.IAppConstants));
        }

        [Test]
        public void CoreDataTest()
        {
            TM.Timecard tcIn;
            TM.Timecard? tcOut;

            // use it to get a reference to the repo like core would do
            TI.IRepository repoIn = _factory.Resolve<TI.IRepository>();

            // make a core timecard object
            tcIn = new TM.Timecard { Date = new DateTime(2019, 6, 1) };
            Assert.That(tcIn.ID, Is.Zero, "Timecard did not start with ID 0 as expected");

            tcIn.Activities.AddRange(
            [
                new TM.Activity("00000", "Arrived; made coffee", "08:00"),
                new TM.Activity("00100", "Called a client", "08:15"),
                new TM.Activity("00200", "Worked on a project", "09:00"),
                new TM.Activity("", "Lunch", "12:00"),
                new TM.Activity("00200", "More work", "13:00"),
                new TM.Activity("", "Departed", "17:00"),
            ]);

            // save it
            repoIn.SaveTimecard(tcIn);
            Assert.That(tcIn.ID, Is.GreaterThan(0), "Didn't get a valid ID after saving timecard");

            // get another reference to the repo
            TI.IRepository repoOut = _factory.Resolve<TI.IRepository>();

            // retrieve the saved timecard
            tcOut = repoOut.GetTimecard(tcIn.ID);
            Assert.Multiple(() =>
            {
                Assert.That(tcOut, Is.Not.Null, "Failed to retrieve timecard that was just saved");
                Assert.That(tcIn.ID, Is.EqualTo(tcOut?.ID), "Retrieved timecard ID doesn't match saved timecard ID");
                Assert.That(tcOut?.Activities.Count, Is.EqualTo(6), "Didn't receive expected number of activity children");
            });

            // cleanup at end of test
            repoOut.DeleteAllTimecards();
        }

        [Test]
        public void CoreTimecardLogicTest()
        {
            TM.Timecard tc;
            TM.Timecard? tcNullable;

            var logic = new TL.TimecardLogic(_factory);
            var ids = new int[4];

            var dates = new DateTime[4];
            dates[0] = new DateTime(2019, 7, 1);
            dates[1] = new DateTime(2019, 7, 2);
            dates[2] = new DateTime(2019, 7, 3);
            dates[3] = DateTime.Today;

            Assert.That(DateTime.Today, Is.GreaterThan(dates[2]), $"These tests must be run after {dates[2]}");

            // examine current (empty) timecard
            tc = logic.GetCurrentTimecard();
            Assert.Multiple(() =>
            {
                Assert.That(tc, Is.Not.Null, "new logic object doesn't have a timecard");
                Assert.That(tc.ID, Is.Zero, "new logic object doesn't have a new timecard");
                Assert.That(tc.IsDirty, Is.False, "new logic object does not have clean timecard");
            });

            // populate and save timecard
            tc.Date = dates[0];
            logic.SaveTimecard();
            Assert.That(tc.ID, Is.Not.Zero, "saved timecard still has zero ID");
            ids[0] = tc.ID;

            // create second timecard
            tc = logic.GetNewTimecard();
            Assert.Multiple(() =>
            {
                Assert.That(tc.ID, Is.Zero, "logic-generated new timecard does not have zero ID");
                Assert.That(tc.IsDirty, Is.False, "logic-generated new timecard isn't clean");
            });

            // populate and save second timecard
            tc.Date = dates[1];
            logic.SaveTimecard();
            Assert.That(tc.ID, Is.Not.EqualTo(ids[0]), "second timecard has same ID as the first");
            ids[1] = tc.ID;

            // create, populate, and save third timecard
            tc = logic.GetNewTimecard();
            tc.Date = dates[2];
            logic.SaveTimecard();
            Assert.Multiple(() =>
            {
                Assert.That(tc.ID, Is.Not.EqualTo(ids[0]), "third timecard has same ID as the first");
                Assert.That(tc.ID, Is.Not.EqualTo(ids[1]), "third timecard has same ID as the second");
            });
            ids[2] = tc.ID;

            // get earliest timecard
            tc = logic.GetEarliestTimecard();
            Assert.Multiple(() =>
            {
                Assert.That(tc.ID, Is.EqualTo(ids[0]), "logic did not return first timecard as the earliest");
                Assert.That(tc.Date, Is.EqualTo(dates[0]), "logic did not return expected date in earliest timecard");
            });

            // get latest timecard
            tc = logic.GetLatestTimecard();
            Assert.Multiple(() =>
            {
                Assert.That(tc.ID, Is.EqualTo(ids[2]), "logic did not return third timecard as the latest");
                Assert.That(tc.Date, Is.EqualTo(dates[2]), "logic did not return expected date in latest timecard");
            });

            // get timecard by ID
            tc = logic.GetSpecificTimecard(ids[1]);
            Assert.Multiple(() =>
            {
                Assert.That(tc.ID, Is.EqualTo(ids[1]), "logic did not return specific timecard");
                Assert.That(tc.Date, Is.EqualTo(dates[1]), "logic did not return expected date in specific timecard");
            });

            Assert.Throws<TE.TimecardNotFoundException>(() => { logic.GetSpecificTimecard(987654321); },
                "Did not get expected exception when retrieving nonexistent timecard");

            // get today's timecard (should return a new, empty timecard)
            tc = logic.GetTodaysTimecard();
            Assert.Multiple(() =>
            {
                Assert.That(tc.ID, Is.Zero, "logic did not return a new timecard as today's card");
                Assert.That(tc.IsDirty, Is.EqualTo(false), "logic did not return a clean timecard as today's card");
            });

            // set a date, add some activity, and save
            tc.Date = dates[3];
            tc.Activities.Add(new TM.Activity("00000", "Arrived", "8:00"));
            tc.Activities.Add(new TM.Activity("00100", "Worked on first project", "8:15"));
            tc.Activities.Add(new TM.Activity("00200", "Worked on second project", "10:30"));
            tc.Activities.Add(new TM.Activity("", "Lunch break", "12:00"));
            tc.Activities.Add(new TM.Activity("00200", "Worked more on second project", "13:00"));
            tc.Activities.Add(new TM.Activity("", "Departed", "17:00"));
            logic.SaveTimecard();
            ids[3] = tc.ID;

            // make a new timecard, then get latest card again
            logic.GetNewTimecard();
            tc = logic.GetLatestTimecard();
            Assert.Multiple(() =>
            {
                Assert.That(tc.ID != ids[0] && tc.ID != ids[1] && tc.ID != ids[2], Is.True,
                    "logic did not return expected ID for populated latest card");
                Assert.That(tc.Date, Is.EqualTo(dates[3]),
                    "logic did not return expected date for populated latest card");
                Assert.That(tc.Activities, Has.Count.EqualTo(6),
                    "logic did not return expected number of activities for populated latest card");
            });

            // delete an activity
            logic.DeleteActivity(2);
            logic.GetNewTimecard();
            tc = logic.GetLatestTimecard();
            Assert.That(tc.Activities, Has.Count.EqualTo(5), "Count wrong after deleting an activity");

            // get count of timecards
            var count = logic.GetTimecardCount();
            Assert.That(count, Is.EqualTo(4), "logic did not return expected number of timecards");

            // test next/previous
            logic.GetSpecificTimecard(ids[1]);
            tcNullable = logic.GetPreviousTimecard();
            Assert.That(tcNullable?.ID, Is.EqualTo(ids[0]), "Did not get expected previous timecard");

            tcNullable = logic.GetPreviousTimecard();
            Assert.That(tcNullable?.ID, Is.EqualTo(ids[0]), "Navigation before earliest timecard not get earliest timecard");

            logic.GetSpecificTimecard(ids[1]);
            tcNullable = logic.GetNextTimecard();
            Assert.That(tcNullable?.ID, Is.EqualTo(ids[2]), "Did not get expected next timecard");
            
            logic.GetTodaysTimecard();
            tcNullable = logic.GetNextTimecard();
            Assert.That(tcNullable?.ID, Is.EqualTo(ids[3]), "Navigation after latest timecard not get latest timecard");

            // test timecard list
            var tclist = logic.GetTimecardList();
            Assert.Multiple(() =>
            {
                Assert.That(tclist, Has.Count.EqualTo(4), "Did not receive expected number of items in timecard list");
                Assert.That(tclist[0].Key, Is.EqualTo(ids[3]), "First item in timecard list isn't latest timecard");
                Assert.That(tclist[3].Key, Is.EqualTo(ids[0]), "Last item in timecard list isn't first timecard");
            });

            // test deleting timecard
            logic.GetTodaysTimecard();
            logic.DeleteTimecard();
            tc = logic.GetCurrentTimecard();
            Assert.That(tc.ID, Is.EqualTo(ids[2]), "Did not end up on previous timecard after deletion");

            // wipe out all timecards
            logic.DeleteAllTimecards();
            count = logic.GetTimecardCount();
            Assert.That(count, Is.Zero, "logic failed to delete all timecards");
        }

        [TestMethod]
        public void CoreBulkLogicTest()
        {
            List<TM.Timecard> tcList;

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
            var bulk = new tl.BulkLogic(_factory);

            // export all data, CSV
            var csvData = bulk.Export(null, null, tl.BulkLogic.DataFormat.CSV);
            var csvLines = csvData.Replace("\r", string.Empty).Split('\n');
            Assert.That(tally.TimecardCount * tally.ActivityCount + 1 + EmptyLastLine(csvLines), Is.EqualTo(csvLines.Length),
                "Bulk export all data as CSV did not yield expected number of lines");

            // export all data, tab-delimited
            var tsvData = bulk.Export(null, null, tl.BulkLogic.DataFormat.TSV);
            var tsvLines = tsvData.Replace("\r", string.Empty).Split('\n');
            Assert.That(tally.TimecardCount * tally.ActivityCount + 1 + EmptyLastLine(tsvLines), Is.EqualTo(tsvLines.Length),
                "Bulk export all data as TSV did not yield expected number of lines");

            // export all data, JSON
            var json = bulk.Export(null, null, tl.BulkLogic.DataFormat.JSON);
            var tcOut = JsonConvert.DeserializeObject<List<TM.Timecard>>(json);
            Assert.That(tally.TimecardCount, Is.EqualTo(tcOut.Count),
                "Bulk export all data as JSON did not yield expected number of timecards");

            // export all data, XML
            var xmlString = bulk.Export(null, null, tl.BulkLogic.DataFormat.XML);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(xmlString));
            Assert.That(tally.TimecardCount, Is.EqualTo(xmlDoc.SelectNodes("/Timecards/Timecard").Count),
                "Bulk export all data as XML did not yield expected number of timecards");

            // export limited range, CSV
            csvData = bulk.Export(dates[2], dates[3], tl.BulkLogic.DataFormat.CSV);
            csvLines = csvData.Replace("\r", string.Empty).Split('\n');
            Assert.That(2 * tally.ActivityCount + 1 + EmptyLastLine(csvLines), Is.EqualTo(csvLines.Length),
                "Bulk export range as CSV did not yield expected number of lines");

            // export limited range, tab-delimited
            tsvData = bulk.Export(dates[2], dates[3], tl.BulkLogic.DataFormat.TSV);
            tsvLines = tsvData.Replace("\r", string.Empty).Split('\n');
            Assert.That(2 * tally.ActivityCount + 1 + EmptyLastLine(tsvLines), Is.EqualTo(tsvLines.Length),
                "Bulk export all data as TSV did not yield expected number of lines");

            // export limited range, JSON
            json = bulk.Export(dates[2], dates[3], tl.BulkLogic.DataFormat.JSON);
            tcOut = JsonConvert.DeserializeObject<List<TM.Timecard>>(json);
            Assert.That(2, Is.EqualTo(tcOut.Count),
                "Bulk export all data as JSON did not yield expected number of timecards");

            // export limited range, XML
            xmlString = bulk.Export(dates[2], dates[3], tl.BulkLogic.DataFormat.XML);
            xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(xmlString));
            Assert.That(2, Is.EqualTo(xmlDoc.SelectNodes("/Timecards/Timecard").Count),
                "Bulk export all data as XML did not yield expected number of timecards");

            //
            // import tests
            //

            // all these import tests create two timecards with two actions each
            tally = (TimecardCount: 2, ActivityCount: 2);
            string result;

            // import CSV
            DeleteAllTimecards();
            result = bulk.Import(CsvData(), tl.BulkLogic.DataFormat.CSV);
            Assert.IsTrue(string.IsNullOrEmpty(result), $"CSV import resulted in message: {result}");

            tcList = GetAllTimecards();
            Assert.That(tally.TimecardCount, Is.EqualTo(tcList.Count),
                "Bulk import of CSV data does not produce expected number of timecards");
            Assert.IsFalse(tcList.Any(tc => tc.Activities.Count != tally.ActivityCount),
                "Bulk import of CSV data resulted in one or more timecards without correct number of activities");

            // import TSV
            DeleteAllTimecards();
            result = bulk.Import(TsvData(), tl.BulkLogic.DataFormat.TSV);
            Assert.IsTrue(string.IsNullOrEmpty(result), $"TSV import resulted in message: {result}");

            tcList = GetAllTimecards();
            Assert.That(tally.TimecardCount, Is.EqualTo(tcList.Count),
                "Bulk import of TSV data does not produce expected number of timecards");
            Assert.IsFalse(tcList.Any(tc => tc.Activities.Count != tally.ActivityCount),
                "Bulk import of TSV data resulted in one or more timecards without correct number of activities");

            // import JSON
            DeleteAllTimecards();
            result = bulk.Import(JsonData(), tl.BulkLogic.DataFormat.JSON);
            Assert.IsTrue(string.IsNullOrEmpty(result), $"JSON import resulted in message: {result}");

            tcList = GetAllTimecards();
            Assert.That(tally.TimecardCount, Is.EqualTo(tcList.Count),
                "Bulk import of JSON data does not produce expected number of timecards");
            Assert.IsFalse(tcList.Any(tc => tc.Activities.Count != tally.ActivityCount),
                "Bulk import of JSON data resulted in one or more timecards without correct number of activities");

            // import XML
            DeleteAllTimecards();
            result = bulk.Import(XmlData(), tl.BulkLogic.DataFormat.XML);
            Assert.IsTrue(string.IsNullOrEmpty(result), $"XML import resulted in message: {result}");

            tcList = GetAllTimecards();
            Assert.That(tally.TimecardCount, Is.EqualTo(tcList.Count),
                "Bulk import of XML data does not produce expected number of timecards");
            Assert.IsFalse(tcList.Any(tc => tc.Activities.Count != tally.ActivityCount),
                "Bulk import of XML data resulted in one or more timecards without correct number of activities");
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
            var repo = _factory.Resolve<TI.IRepository>();

            var count = 0;

            foreach (var date in dates)
            {
                var tc = new TM.Timecard() { Date = date };
                tc.Activities.AddRange(new[]
                {
                new TM.Activity("00000", "Arrived", "08:00"),
                new TM.Activity("", "Departed", "17:00"),
            });
                repo.SaveTimecard(tc);
                count++;
            }

            return (count, 2);
        }

        private List<TM.Timecard> GetAllTimecards()
        {
            var repo = _factory.Resolve<TI.IRepository>();
            return repo.GetTimecards(null, null);
        }

        private void DeleteAllTimecards()
        {
            var repo = _factory.Resolve<TI.IRepository>();
            repo.DeleteAllTimecards();
        }

        private string CsvData()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Date,Code,Description,Time,IsAfterMidnight");
            sb.AppendLine("\"2019-12-07\",\"00000\",\"Arrived\",\"08:00\",false");
            sb.AppendLine("\"2019-12-07\",\"\",\"Departed\",\"17:00\",false");
            sb.AppendLine("\"2019-12-08\",\"00000\",\"Arrived\",\"08:00\",false");
            sb.AppendLine("\"2019-12-08\",\"\",\"Departed\",\"17:00\",false");

            return sb.ToString();
        }

        private string TsvData()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Date\tCode\tDescription\tTime\tIsAfterMidnight");
            sb.AppendLine("2019-12-07\t00000\tArrived\t08:00\tfalse");
            sb.AppendLine("2019-12-07\t\tDeparted\t17:00\tfalse");
            sb.AppendLine("2019-12-08\t00000\tArrived\t08:00\tfalse");
            sb.AppendLine("2019-12-08\t\tDeparted\t17:00\tfalse");

            return sb.ToString();
        }

        private string JsonData()
        {
            return
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
]";
        }

        private string XmlData()
        {
            return
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
</Timecards>";
        }

        private int EmptyLastLine(string[] array)
        {
            return (array[array.Length - 1] == string.Empty ? 1 : 0);
        }

        #endregion
    }
}
