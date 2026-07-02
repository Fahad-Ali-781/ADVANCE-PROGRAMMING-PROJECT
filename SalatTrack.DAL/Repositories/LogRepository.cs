using System.Data.SqlClient;
using SalatTrack.DAL.Interfaces;
using SalatTrack.Models;

namespace SalatTrack.DAL.Repositories
{
    /// <summary>
    /// Handles all database operations for Logs.
    /// Uses ADO.NET with parameterized queries only.
    /// </summary>
    public class LogRepository : ILogRepository
    {
        public List<Log> GetAll()
        {
            List<Log> logs = new List<Log>();
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT LogID, UserID, PrayerName, LoggedAt, Status FROM Logs";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        logs.Add(MapLog(reader));
                }
            }
            return logs;
        }

        public Log? GetById(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT LogID, UserID, PrayerName, LoggedAt, Status FROM Logs WHERE LogID = @LogID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LogID", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapLog(reader);
                    }
                }
            }
            return null;
        }

        public void Add(Log log)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"INSERT INTO Logs (UserID, PrayerName, LoggedAt, Status)
                                 VALUES (@UserID, @PrayerName, @LoggedAt, @Status)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", log.UserID);
                    cmd.Parameters.AddWithValue("@PrayerName", log.PrayerName);
                    cmd.Parameters.AddWithValue("@LoggedAt", log.LoggedAt);
                    cmd.Parameters.AddWithValue("@Status", log.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Log log)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"UPDATE Logs 
                                 SET PrayerName = @PrayerName, LoggedAt = @LoggedAt, Status = @Status
                                 WHERE LogID = @LogID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrayerName", log.PrayerName);
                    cmd.Parameters.AddWithValue("@LoggedAt", log.LoggedAt);
                    cmd.Parameters.AddWithValue("@Status", log.Status);
                    cmd.Parameters.AddWithValue("@LogID", log.LogID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "DELETE FROM Logs WHERE LogID = @LogID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LogID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Log> GetByUserAndDateRange(int userId, DateTime from, DateTime to)
        {
            List<Log> logs = new List<Log>();
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"SELECT LogID, UserID, PrayerName, LoggedAt, Status 
                                 FROM Logs 
                                 WHERE UserID = @UserID 
                                 AND CAST(LoggedAt AS DATE) BETWEEN @From AND @To
                                 ORDER BY LoggedAt DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@From", from.Date);
                    cmd.Parameters.AddWithValue("@To", to.Date);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            logs.Add(MapLog(reader));
                    }
                }
            }
            return logs;
        }

        private Log MapLog(SqlDataReader reader)
        {
            return new Log
            {
                LogID = (int)reader["LogID"],
                UserID = (int)reader["UserID"],
                PrayerName = reader["PrayerName"].ToString()!,
                LoggedAt = (DateTime)reader["LoggedAt"],
                Status = reader["Status"].ToString()!
            };
        }
    }
}