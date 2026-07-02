using SalatTrack.Models;

namespace SalatTrack.DAL.Interfaces
{
    /// <summary>
    /// Defines all database operations for the User entity.
    /// </summary>
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);

        // Special method for login
        User GetByUsername(string username);
    }
}