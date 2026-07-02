using System.Configuration;
using System.Data.SqlClient;

namespace SalatTrack.DAL
{
    /// <summary>
    /// Static helper class that provides SQL Server connections.
    /// All repositories use this — never hardcode connection strings elsewhere.
    /// </summary>
    public static class DBHelper
    {
        /// <summary>
        /// Returns a new open SqlConnection. Caller is responsible for closing it.
        /// Always use inside a 'using' block.
        /// </summary>
        public static SqlConnection GetConnection()
        {
            try
            {
                var csSetting = ConfigurationManager.ConnectionStrings["SalatTrackDB"];
                if (csSetting == null || string.IsNullOrWhiteSpace(csSetting.ConnectionString))
                    throw new Exception("Connection string 'SalatTrackDB' not found in configuration.");

                SqlConnection connection = new SqlConnection(csSetting.ConnectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Database connection failed: " + ex.Message);
            }
        }
    }
}