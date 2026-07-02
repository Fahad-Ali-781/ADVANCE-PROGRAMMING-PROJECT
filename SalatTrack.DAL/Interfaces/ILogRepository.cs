using SalatTrack.Models;

namespace SalatTrack.DAL.Interfaces
{
    public interface ILogRepository
    {
        List<Log> GetAll();
        Log GetById(int id);
        void Add(Log log);
        void Update(Log log);
        void Delete(int id);
        List<Log> GetByUserAndDateRange(int userId, DateTime from, DateTime to);
    }
}