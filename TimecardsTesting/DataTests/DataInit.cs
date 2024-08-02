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
            TestCommon.DeleteTestDatabase();
            TestCommon.CreateTestDatabase();
        }

        [OneTimeTearDown]
        public void DataTeardown()
        {
            TestContext.Progress.WriteLine("in DataTeardown()");
            TestCommon.DeleteTestDatabase();
        }
    }
}
