using TC = TimecardsCore.Interfaces;

namespace TimecardsTesting
{
    /// <summary>
    /// Use this constants class in the IOC container to direct integration tests to use
    /// the test database path instead of the production database path
    /// </summary>
    public class TestAppConstants : TC.IAppConstants
    {
        public static string ConnectionStringName => "TestDb";

        public string? LogFilePath => @"C:\Temp\Timecards.log";

        public string SystemName => throw new NotImplementedException();
    }
}
