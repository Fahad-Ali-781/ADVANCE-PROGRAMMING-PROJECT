using SalatTrack.Models;

namespace SalatTrack.DAL.Interfaces
{
    /// <summary>
    /// Defines all database operations for the PrayerTime entity.
    /// </summary>
    public interface IPrayerTimeRepository
    {
        List<PrayerTime> GetAll();
        PrayerTime GetById(int id);
        void Add(PrayerTime prayerTime);
        void Update(PrayerTime prayerTime);
        void Delete(int id);

        // Special method to get prayer times for a specific city and date
        PrayerTime GetByCityAndDate(int cityId, DateTime date);
    }
}