namespace SalatTrack.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TimeZone { get; set; } = string.Empty;
    }
}
