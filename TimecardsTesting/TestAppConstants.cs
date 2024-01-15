using TC = TimecardsCore.Interfaces;

namespace TimecardsTesting
{
    /// <summary>
    /// Use this constants class in the IOC container to direct integration tests to use
    /// the test database path instead of the production database path
    /// </summary>
    public class TestAppConstants : TC.IAppConstants
    {
        public string ConnectionStringName => "TestDb";

        public string? LogFilePath => null;

        public string SystemName => throw new NotImplementedException();
    }
}
