using System.Data.SqlClient;
using SalatTrack.DAL.Interfaces;
using SalatTrack.Models;

namespace SalatTrack.DAL.Repositories
{
    /// <summary>
    /// Handles all database operations for NotificationSettings.
    /// Uses ADO.NET with parameterized queries only.
    /// </summary>
    public class NotificationSettingsRepository : INotificationSettingsRepository
    {
        public List<NotificationSetting> GetAll()
        {
            List<NotificationSetting> settings = new List<NotificationSetting>();
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT SettingID, UserID, PrayerName, AlertType, IsEnabled FROM NotificationSettings";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        settings.Add(MapSetting(reader));
                }
            }
            return settings;
        }

        public NotificationSetting? GetById(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT SettingID, UserID, PrayerName, AlertType, IsEnabled FROM NotificationSettings WHERE SettingID = @SettingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SettingID", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapSetting(reader);
                    }
                }
            }
            return null;
        }

        public List<NotificationSetting> GetByUserId(int userId)
        {
            List<NotificationSetting> settings = new List<NotificationSetting>();
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT SettingID, UserID, PrayerName, AlertType, IsEnabled FROM NotificationSettings WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            settings.Add(MapSetting(reader));
                    }
                }
            }
            return settings;
        }

        public void Add(NotificationSetting setting)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"INSERT INTO NotificationSettings (UserID, PrayerName, AlertType, IsEnabled) 
                                 VALUES (@UserID, @PrayerName, @AlertType, @IsEnabled)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", setting.UserID);
                    cmd.Parameters.AddWithValue("@PrayerName", setting.PrayerName);
                    cmd.Parameters.AddWithValue("@AlertType", setting.AlertType);
                    cmd.Parameters.AddWithValue("@IsEnabled", setting.IsEnabled);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(NotificationSetting setting)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"UPDATE NotificationSettings 
                                 SET PrayerName = @PrayerName, AlertType = @AlertType, IsEnabled = @IsEnabled 
                                 WHERE SettingID = @SettingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrayerName", setting.PrayerName);
                    cmd.Parameters.AddWithValue("@AlertType", setting.AlertType);
                    cmd.Parameters.AddWithValue("@IsEnabled", setting.IsEnabled);
                    cmd.Parameters.AddWithValue("@SettingID", setting.SettingID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "DELETE FROM NotificationSettings WHERE SettingID = @SettingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SettingID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private NotificationSetting MapSetting(SqlDataReader reader)
        {
            return new NotificationSetting
            {
                SettingID = (int)reader["SettingID"],
                UserID = (int)reader["UserID"],
                PrayerName = reader["PrayerName"].ToString()!,
                AlertType = reader["AlertType"].ToString()!,
                IsEnabled = (bool)reader["IsEnabled"]
            };
        }
    }
}