using System.Data.SqlClient;
using SalatTrack.DAL.Interfaces;
using SalatTrack.Models;

namespace SalatTrack.DAL.Repositories
{
    /// <summary>
    /// Handles all database operations for Cities.
    /// Uses ADO.NET with parameterized queries only.
    /// </summary>
    public class CityRepository : ICityRepository
    {
        public List<City> GetAll()
        {
            List<City> cities = new List<City>();

            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT CityID, CityName, Latitude, Longitude, TimeZone FROM Cities";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        cities.Add(MapCity(reader));
                }
            }

            return cities;
        }

        public City? GetById(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT CityID, CityName, Latitude, Longitude, TimeZone FROM Cities WHERE CityID = @CityID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityID", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapCity(reader);
                    }
                }
            }

            return null;
        }

        public void Add(City city)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"INSERT INTO Cities (CityName, Latitude, Longitude, TimeZone) 
                                 VALUES (@CityName, @Latitude, @Longitude, @TimeZone)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityName", city.CityName);
                    cmd.Parameters.AddWithValue("@Latitude", city.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", city.Longitude);
                    cmd.Parameters.AddWithValue("@TimeZone", city.TimeZone);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(City city)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"UPDATE Cities 
                                 SET CityName = @CityName, Latitude = @Latitude, 
                                     Longitude = @Longitude, TimeZone = @TimeZone 
                                 WHERE CityID = @CityID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityName", city.CityName);
                    cmd.Parameters.AddWithValue("@Latitude", city.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", city.Longitude);
                    cmd.Parameters.AddWithValue("@TimeZone", city.TimeZone);
                    cmd.Parameters.AddWithValue("@CityID", city.CityID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "DELETE FROM Cities WHERE CityID = @CityID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CityID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private City MapCity(SqlDataReader reader)
        {
            return new City
            {
                CityID = (int)reader["CityID"],
                CityName = reader["CityName"].ToString()!,
                Latitude = (double)reader["Latitude"],
                Longitude = (double)reader["Longitude"],
                TimeZone = reader["TimeZone"].ToString()!
            };
        }
    }
}