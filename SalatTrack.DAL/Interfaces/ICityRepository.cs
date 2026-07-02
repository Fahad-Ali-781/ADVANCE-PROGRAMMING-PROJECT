using SalatTrack.Models;

namespace SalatTrack.DAL.Interfaces
{
    /// <summary>
    /// Defines all database operations for the City entity.
    /// </summary>
    public interface ICityRepository
    {
        List<City> GetAll();
        City GetById(int id);
        void Add(City city);
        void Update(City city);
        void Delete(int id);
    }
}