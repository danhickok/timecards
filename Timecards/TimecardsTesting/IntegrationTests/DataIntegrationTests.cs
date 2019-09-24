using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using core = TimecardsCore.Models;
using ci = TimecardsCore.Interfaces;
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
            // get a reference to factory like core would do
            ci.IFactory factory = _factory;

            core.Timecard tcIn;
            core.Timecard tcOut;

            // use it to get a reference to the repo like core would do
            ci.IRepository repoIn = factory.Resolve<ci.IRepository>();
            
            // make a core timecard object
            tcIn = new core.Timecard { Date = DateTime.Today };
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
            ci.IRepository repoOut = factory.Resolve<ci.IRepository>();

            // retrieve the saved timecard
            tcOut = repoOut.GetTimecard(tcIn.ID);
            Assert.AreEqual(tcIn.ID, tcOut.ID, "Retrieved timecard ID doesn't match saved timecard ID");
            Assert.AreEqual(tcOut.Activities.Count, 6, "Didn't receive expected number of activity children");
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
    }
}
