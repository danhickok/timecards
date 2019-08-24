using TimecardsCore.Interfaces;

namespace TimecardsData
{
    public class ConnectionInfo : IConnectionInfo
    {
        public string ConnectionStringName => "TimecardsDb";
    }
}
