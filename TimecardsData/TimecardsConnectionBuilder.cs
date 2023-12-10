using Microsoft.Data.Sqlite;
using System.Configuration;
using System.Data.Common;


namespace TimecardsData
{
    /// <summary>
    /// This static class returns a SQLite-flavored DbConnection object based on the named
    /// connection string found in the config file
    /// </summary>
    public static class TimecardsConnectionBuilder
    {
        /// <summary>
        /// Builds a connection string with environment variable name replaced with user's path
        /// </summary>
        /// <param name="connectionStringName">Connection string name from App.config</param>
        /// <returns></returns>
        public static DbConnection BuildConnection(string connectionStringName)
        {
            var cs = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString
                .Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            return new SqliteConnection(cs);
        }
    }
}
