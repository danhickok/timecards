using TimecardsCore.Interfaces;

namespace TimecardsTesting.Base
{
    public class TestAppConstants : IAppConstants
    {
        public string ConnectionStringName => "TestDb";
    }
}
