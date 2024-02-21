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
        /// <param name="systemName">System name as stored in ConnectionStrings collection</param>
        /// <returns></returns>
        public static string BuildConnectionString(string? systemName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
            
            var config = builder.Build();

            var connectionStringsSection = config.GetSection("ConnectionStrings").Get<string>("");
            if (connectionStringsSection != null)
            {
                var connString = connectionStringsSection(systemName);

            }

            //doesn't work
            //var connString = ConfigurationManager.ConnectionStrings[systemName].ConnectionString;
            //var cs = connString.Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            return cs is null ? throw new Exception("Missing system name in configuration") : cs;
        }
    }


    // https://learn.microsoft.com/en-us/answers/questions/906488/read-app-config-values-in-net-6-console
    // https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration
    // https://stackoverflow.com/questions/65110479/how-to-get-values-from-appsettings-json-in-a-console-application-using-net-core
}
