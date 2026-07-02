namespace SalatTrack.Models
{
    public class Log
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public string PrayerName { get; set; } = string.Empty;
        public DateTime LoggedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
