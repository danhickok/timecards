using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using core = TimecardsCore.Models;
using data = TimecardsData;

namespace TimecardsTesting.DataTests
{
    [TestClass]
    public class TimecardDataTests
    {
        [TestInitialize]
        public void Initialize()
        {
            //TODO: figure out how to get the path and delete "TestDb"
        }

        [TestCleanup]
        public void Cleanup()
        {
            //TODO: same code to get the path and delete the test database
        }

        [TestMethod]
        public void LoadSaveTest()
        {
            using (var repo = new data.Repository("TestDb"))
            {
                var timecard = new core.Timecard();

                repo.SaveTimecard(timecard);
            }
        }
    }
}
