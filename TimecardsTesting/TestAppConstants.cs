using TC = TimecardsCore.Interfaces;

namespace TimecardsTesting
{
    /// <summary>
    /// Use this constants class in the IOC container as a singleton (or as a standalone
    /// object) to direct integration tests to use the test database path instead of the
    /// production path
    /// </summary>
    public class TestAppConstants : TC.IAppConstants
    {
        public string? SystemName => "TestDb";

        public string? LogFilePath => @"C:\Temp\Timecards.log";
    }
}
