namespace SalatTrack.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int? CityID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
