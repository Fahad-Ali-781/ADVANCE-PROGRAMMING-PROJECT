using SalatTrack.BLL.Notifications;
using SalatTrack.Models;

namespace SalatTrack.BLL
{
    /// <summary>
    /// Checks current time against prayer times every 60 seconds.
    /// Fires the appropriate INotifiable alert — once per prayer per day.
    /// Called by a System.Windows.Forms.Timer in MainForm.
    /// </summary>
    public class NotificationScheduler
    {
        // Injected via constructor — depends on interface, not concrete class
        private readonly INotifiable _notifier;

        // Tracks which prayers have already been notified today
        // Key: "Fajr", "Dhuhr", etc. — Value: date it was last notified
        private readonly Dictionary<string, DateTime> _lastNotified;

        // Prayer names in order
        private static readonly string[] PrayerNames =
            { "Fajr", "Dhuhr", "Asr", "Maghrib", "Isha" };

        public NotificationScheduler(INotifiable notifier)
        {
            _notifier = notifier;
            _lastNotified = new Dictionary<string, DateTime>();

            // Initialize all prayers as never notified
            foreach (string prayer in PrayerNames)
                _lastNotified[prayer] = DateTime.MinValue;
        }

        /// <summary>
        /// Called every 60 seconds by the timer in MainForm.
        /// Checks if any prayer time is within 1 minute of now.
        /// Only notifies ONCE per prayer per day.
        /// </summary>
        public void CheckAndNotify(PrayerTime todaysPrayers)
        {
            if (todaysPrayers == null) return;

            DateTime now = DateTime.Now;

            // Map prayer names to their times
            var prayers = new Dictionary<string, TimeSpan>
            {
                { "Fajr",    todaysPrayers.Fajr },
                { "Dhuhr",   todaysPrayers.Dhuhr },
                { "Asr",     todaysPrayers.Asr },
                { "Maghrib", todaysPrayers.Maghrib },
                { "Isha",    todaysPrayers.Isha }
            };

            foreach (var prayer in prayers)
            {
                string name = prayer.Key;
                TimeSpan prayerTime = prayer.Value;

                // Build today's prayer DateTime for comparison
                DateTime prayerDateTime = DateTime.Today.Add(prayerTime);

                // Check if within 1 minute window
                double minutesDiff = (now - prayerDateTime).TotalMinutes;
                bool isWithinWindow = minutesDiff >= 0 && minutesDiff <= 1;

                // Check if already notified today
                bool alreadyNotified = _lastNotified[name].Date == DateTime.Today;

                if (isWithinWindow && !alreadyNotified)
                {
                    // Fire the notification
                    _notifier.Notify(name, prayerDateTime);

                    // Mark as notified today
                    _lastNotified[name] = DateTime.Now;
                }
            }
        }
    }
}