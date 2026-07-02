using System.Media;

namespace SalatTrack.BLL.Notifications
{
    /// <summary>
    /// Audio notification implementation using system sounds.
    /// Implements INotifiable — demonstrates POLYMORPHISM.
    /// Same interface as ToastAlert, completely different behavior.
    /// </summary>
    public class AudioAlert : INotifiable
    {
        /// <summary>
        /// Plays a system beep sound for the prayer alert.
        /// </summary>
        public void Notify(string prayerName, DateTime prayerTime)
        {
            // Play system exclamation sound for prayer alert
            SystemSounds.Exclamation.Play();

            // Small delay then play again for emphasis
            Thread.Sleep(600);
            SystemSounds.Exclamation.Play();
        }
    }
}