namespace TimecardsTesting.CoreTests
{
    [SetUpFixture]
    public class CoreInit
    {
        [OneTimeSetUp]
        public void CoreSetup()
        {
            TestContext.Progress.WriteLine("in CoreSetup()");
            TestCommon.InitializeTestConfiguration();
        }

        [OneTimeTearDown]
        public void CoreTeardown()
        {
            TestContext.Progress.WriteLine("in CoreTeardown()");
        }
    }
}
