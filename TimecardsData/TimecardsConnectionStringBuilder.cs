using Microsoft.Extensions.Configuration;
using System.IO;

namespace TimecardsData
{
    /// <summary>
    /// This static class returns a SQLite-flavored DbConnection object based on the named
    /// connection string found in the config file
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
