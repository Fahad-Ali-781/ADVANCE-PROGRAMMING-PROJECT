namespace SalatTrack.Models
{
    public class PrayerTime
    {
        public int PrayerTimeID { get; set; }
        public int CityID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Fajr { get; set; }
        public TimeSpan Dhuhr { get; set; }
        public TimeSpan Asr { get; set; }
        public TimeSpan Maghrib { get; set; }
        public TimeSpan Isha { get; set; }
    }
}
