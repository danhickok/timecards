namespace TimecardsTesting.DataTests
{
    [SetUpFixture]
    internal class DataInit
    {
        [OneTimeSetUp]
        public void DataSetup()
        {
            TestContext.Progress.WriteLine("in DataSetup()");
            TestCommon.InitializeTestConfiguration();
        }

        [OneTimeTearDown]
        public void DataTeardown()
        {
            TestContext.Progress.WriteLine("in DataTeardown()");
        }
    }
}
