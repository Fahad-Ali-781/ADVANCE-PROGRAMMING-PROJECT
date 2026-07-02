using System.Collections.Generic;
using SalatTrack.DAL.Interfaces;
using SalatTrack.DAL.Repositories;
using SalatTrack.Models;

namespace SalatTrack.BLL
{
    public class NotificationSettingsService
    {
        private readonly INotificationSettingsRepository _repo;

        public NotificationSettingsService()
        {
            _repo = new NotificationSettingsRepository();
        }

        public List<NotificationSetting> GetByUser(int userId)
        {
            return _repo.GetByUserId(userId);
        }

        public void SaveAll(int userId, List<NotificationSetting> settings)
        {
            // Delete existing, re-insert fresh
            var existing = _repo.GetByUserId(userId);
            foreach (var e in existing)
                _repo.Delete(e.SettingID);

            foreach (var s in settings)
            {
                s.UserID = userId;
                _repo.Add(s);
            }
        }
    }
}