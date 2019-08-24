using TimecardsCore.Interfaces;

namespace TimecardsTesting.Base
{
    public class TestConnectionInfo : IConnectionInfo
    {
        public string ConnectionStringName => "TestDb";
    }
}
