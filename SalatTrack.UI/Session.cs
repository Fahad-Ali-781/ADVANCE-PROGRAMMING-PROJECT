namespace SalatTrack.UI
{
    /// <summary>
    /// Holds the currently logged-in user's data for the session.
    /// Static so it's accessible from any form without passing references.
    /// </summary>
    internal static class Session
    {
        public static int CurrentUserID { get; set; }
        public static int CurrentCityID { get; set; }
        public static string CurrentUsername { get; set; } = string.Empty;

        /// <summary>
        /// Clears all session data on logout.
        /// </summary>
        public static void Logout()
        {
            CurrentUserID = 0;
            CurrentCityID = 0;
            CurrentUsername = string.Empty;
        }
    }
}