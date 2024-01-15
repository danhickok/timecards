namespace TimecardsTesting.CoreTests
{
    [SetUpFixture]
    public class CoreInit
    {
        [OneTimeSetUp]
        public void CoreSetup()
        {
            // Do login here.
        }

        [OneTimeTearDown]
        public void CoreTeardown()
        {
            // Do logout here
        }
    }
}
