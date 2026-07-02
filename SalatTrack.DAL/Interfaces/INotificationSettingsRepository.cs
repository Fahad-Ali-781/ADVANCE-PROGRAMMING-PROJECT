using SalatTrack.Models;

namespace SalatTrack.DAL.Interfaces
{
    /// <summary>
    /// Defines all database operations for the NotificationSetting entity.
    /// </summary>
    public interface INotificationSettingsRepository
    {
        List<NotificationSetting> GetAll();
        NotificationSetting GetById(int id);
        void Add(NotificationSetting setting);
        void Update(NotificationSetting setting);
        void Delete(int id);

        // Special method to get all settings for a specific user
        List<NotificationSetting> GetByUserId(int userId);
    }
}