namespace SalatTrack.BLL.Notifications
{
    /// <summary>
    /// Interface for all notification types.
    /// Demonstrates INTERFACE usage — ToastAlert and AudioAlert both implement this.
    /// Caller code only depends on INotifiable, never on concrete classes directly.
    /// </summary>
    public interface INotifiable
    {
        /// <summary>
        /// Sends a notification for a prayer.
        /// </summary>
        /// <param name="prayerName">Name of the prayer e.g. "Fajr"</param>
        /// <param name="prayerTime">The time of the prayer</param>
        void Notify(string prayerName, DateTime prayerTime);
    }
}