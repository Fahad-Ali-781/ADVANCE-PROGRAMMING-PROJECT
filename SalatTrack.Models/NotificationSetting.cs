namespace SalatTrack.Models
{
    public class NotificationSetting
    {
        public int SettingID { get; set; }
        public int UserID { get; set; }
        public string PrayerName { get; set; } = string.Empty;
        public string AlertType { get; set; } = string.Empty;
        public bool IsEnabled { get; set; } = true;
    }
}
