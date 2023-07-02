using TimecardsCore.Interfaces;

namespace Timecards
{
    public class ProductionAppConstants : IAppConstants
    {
        public string ConnectionStringName => "TimecardsDb";

        public string LogFilePath => @"c:\Temp\Timecards.log";
    }
}
