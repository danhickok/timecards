using TC = TimecardsCore.Interfaces;

namespace Timecards
{
    /// <summary>
    /// Use this constants class in the IOC container as a singleton to inform the application
    /// where to find the database and log files
    /// </summary>
    public class ProductionAppConstants : TC.IAppConstants
    {
        public string? SystemName => "TimecardsDb";

        public string? LogFilePath => @"C:\Temp\Timecards.log";
    }
}
