using System.Data.SqlClient;
using SalatTrack.DAL.Interfaces;
using SalatTrack.Models;

namespace SalatTrack.DAL.Repositories
{
    /// <summary>
    /// Handles all database operations for PrayerTimes.
    /// Uses ADO.NET with parameterized queries only.
    /// </summary>
    public class PrayerTimeRepository : IPrayerTimeRepository
    {
        public List<PrayerTime> GetAll()
        {
            List<PrayerTime> prayerTimes = new List<PrayerTime>();
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT PrayerTimeID, CityID, Date, Fajr, Dhuhr, Asr, Maghrib, Isha FROM PrayerTimes";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        prayerTimes.Add(MapPrayerTime(reader));
                }
            }
            return prayerTimes;
        }

        public PrayerTime? GetById(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT PrayerTimeID, CityID, Date, Fajr, Dhuhr, Asr, Maghrib, Isha FROM PrayerTimes WHERE PrayerTimeID = @PrayerTimeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrayerTimeID", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapPrayerTime(reader);
                    }
                }
            }
            return null;
        }

        public PrayerTime? GetByCityAndDate(int cityId, DateTime date)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"SELECT PrayerTimeID, CityID, Date, Fajr, Dhuhr, Asr, Maghrib, Isha 
                                 FROM PrayerTimes 
                                 WHERE CityID = @CityID AND Date = @Date";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityID", cityId);
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapPrayerTime(reader);
                    }
                }
            }
            return null;
        }

        public void Add(PrayerTime prayerTime)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"INSERT INTO PrayerTimes (CityID, Date, Fajr, Dhuhr, Asr, Maghrib, Isha) 
                                 VALUES (@CityID, @Date, @Fajr, @Dhuhr, @Asr, @Maghrib, @Isha)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityID", prayerTime.CityID);
                    cmd.Parameters.AddWithValue("@Date", prayerTime.Date.Date);
                    cmd.Parameters.AddWithValue("@Fajr", prayerTime.Fajr);
                    cmd.Parameters.AddWithValue("@Dhuhr", prayerTime.Dhuhr);
                    cmd.Parameters.AddWithValue("@Asr", prayerTime.Asr);
                    cmd.Parameters.AddWithValue("@Maghrib", prayerTime.Maghrib);
                    cmd.Parameters.AddWithValue("@Isha", prayerTime.Isha);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(PrayerTime prayerTime)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"UPDATE PrayerTimes 
                                 SET CityID = @CityID, Date = @Date, Fajr = @Fajr, 
                                     Dhuhr = @Dhuhr, Asr = @Asr, Maghrib = @Maghrib, Isha = @Isha 
                                 WHERE PrayerTimeID = @PrayerTimeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityID", prayerTime.CityID);
                    cmd.Parameters.AddWithValue("@Date", prayerTime.Date.Date);
                    cmd.Parameters.AddWithValue("@Fajr", prayerTime.Fajr);
                    cmd.Parameters.AddWithValue("@Dhuhr", prayerTime.Dhuhr);
                    cmd.Parameters.AddWithValue("@Asr", prayerTime.Asr);
                    cmd.Parameters.AddWithValue("@Maghrib", prayerTime.Maghrib);
                    cmd.Parameters.AddWithValue("@Isha", prayerTime.Isha);
                    cmd.Parameters.AddWithValue("@PrayerTimeID", prayerTime.PrayerTimeID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "DELETE FROM PrayerTimes WHERE PrayerTimeID = @PrayerTimeID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrayerTimeID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private PrayerTime MapPrayerTime(SqlDataReader reader)
        {
            return new PrayerTime
            {
                PrayerTimeID = (int)reader["PrayerTimeID"],
                CityID = (int)reader["CityID"],
                Date = (DateTime)reader["Date"],
                Fajr = (TimeSpan)reader["Fajr"],
                Dhuhr = (TimeSpan)reader["Dhuhr"],
                Asr = (TimeSpan)reader["Asr"],
                Maghrib = (TimeSpan)reader["Maghrib"],
                Isha = (TimeSpan)reader["Isha"]
            };
        }
    }
}