using System.Data.SqlClient;
using SalatTrack.DAL.Interfaces;
using SalatTrack.Models;

namespace SalatTrack.DAL.Repositories
{
    /// <summary>
    /// Handles all database operations for Users.
    /// Uses ADO.NET with parameterized queries only.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT UserID, Username, PasswordHash, CityID, CreatedAt FROM Users";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        users.Add(MapUser(reader));
                }
            }
            return users;
        }

        public User? GetById(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT UserID, Username, PasswordHash, CityID, CreatedAt FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapUser(reader);
                    }
                }
            }
            return null;
        }

        public User? GetByUsername(string username)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "SELECT UserID, Username, PasswordHash, CityID, CreatedAt FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapUser(reader);
                    }
                }
            }
            return null;
        }

        public void Add(User user)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"INSERT INTO Users (Username, PasswordHash, CityID) 
                                 VALUES (@Username, @PasswordHash, @CityID)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@CityID", (object?)user.CityID ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(User user)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = @"UPDATE Users 
                                 SET Username = @Username, PasswordHash = @PasswordHash, CityID = @CityID 
                                 WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@CityID", (object?)user.CityID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserID", user.UserID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = DBHelper.GetConnection())
            {
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private User MapUser(SqlDataReader reader)
        {
            return new User
            {
                UserID = (int)reader["UserID"],
                Username = reader["Username"].ToString()!,
                PasswordHash = reader["PasswordHash"].ToString()!,
                CityID = reader["CityID"] == DBNull.Value ? null : (int?)reader["CityID"],
                CreatedAt = (DateTime)reader["CreatedAt"]
            };
        }
    }
}