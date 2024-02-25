using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	/// <summary>
	/// Singleton for database access.
	/// </summary>
    public class DatabaseSingleton
    {
		private static SqlConnection conn = null;
		private DatabaseSingleton()
		{
		}
		public static SqlConnection GetInstance()
		{
			if (conn == null)
			{
				SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
				consStringBuilder.UserID = ReadSetting("Name");
				consStringBuilder.Password = ReadSetting("Password");
				consStringBuilder.InitialCatalog = ReadSetting("Database");
				consStringBuilder.DataSource = ReadSetting("DataSource");
				consStringBuilder.ConnectTimeout = 30;
				consStringBuilder.MultipleActiveResultSets = true;
				conn = new SqlConnection(consStringBuilder.ConnectionString);
				conn.Open();
			}
			return conn;
		}

		public static void CloseConnection()
		{
			if (conn != null)
			{
				conn.Close();
				conn.Dispose();
			}
		}

		/// <summary>
		/// Reads a value from App.config.
		/// </summary>
		/// <param name="key">The config key to read.</param>
		/// <returns>The config value read.</returns>
		private static string ReadSetting(string key)
		{
			var appSettings = ConfigurationManager.AppSettings;
			string result = appSettings[key] ?? "Not Found";
			return result;
		}
	}
}
