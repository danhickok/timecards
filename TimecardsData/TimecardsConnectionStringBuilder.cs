using Microsoft.Extensions.Configuration;
using System.IO;

namespace TimecardsData
{
    /// <summary>
    /// This static class returns a Sqlite connection string for either a production or test database
    /// </summary>
    public static class TimecardsConnectionStringBuilder
    {
        /// <summary>
        /// Builds a connection string with configuration value replaced with user's path
        /// </summary>
        /// <param name="systemName">System name as stored in ConnectionStrings collection of appsettings.json file</param>
        /// <returns></returns>
        public static string BuildConnectionString(string? systemName)
        {
            // This Microsoft article explains the simplification incorporated in their move from the traditional
            // .NET Framework version of System.Data.SQLite to the Core version in Microsoft.Data.Sqlite:
            // https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/compare

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
            var config = builder.Build();

            var connectionStringsSection = config?.GetSection("ConnectionStrings");
            var connectionString = connectionStringsSection?
                .GetValue<string?>(systemName ?? "")?
                .Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            return connectionString is null ? throw new Exception("Missing system name in configuration") : connectionString;
        }
    }
}
