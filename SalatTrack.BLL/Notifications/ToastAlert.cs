using System.Windows.Forms;
namespace SalatTrack.BLL.Notifications
{
    /// <summary>
    /// Toast notification implementation using MessageBox.
    /// Implements INotifiable — demonstrates POLYMORPHISM.
    /// </summary>
    public class ToastAlert : INotifiable
    {
        /// <summary>
        /// Shows a MessageBox popup for the prayer alert.
        /// </summary>
        public void Notify(string prayerName, DateTime prayerTime)
        {
            string message = $"It's time for {prayerName}!\n" +
                             $"Prayer Time: {prayerTime:hh:mm tt}\n\n" +
                             $"Assalamu Alaikum — please prepare for Salah.";

            MessageBox.Show(
                message,
                $"{prayerName} Prayer Time",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}