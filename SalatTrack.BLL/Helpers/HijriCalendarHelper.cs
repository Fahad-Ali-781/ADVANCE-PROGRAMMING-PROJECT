using System.Globalization;

namespace SalatTrack.BLL.Helpers
{
    /// <summary>
    /// Converts Gregorian dates to Hijri (Islamic) calendar dates.
    /// Uses built-in System.Globalization.HijriCalendar — no external library needed.
    /// </summary>
    public static class HijriCalendarHelper
    {
        // Month names in English transliteration
        private static readonly string[] HijriMonthNames =
        {
            "Muharram", "Safar", "Rabi al-Awwal", "Rabi al-Thani",
            "Jumada al-Awwal", "Jumada al-Thani", "Rajab", "Sha'ban",
            "Ramadan", "Shawwal", "Dhul-Qa'dah", "Dhul-Hijjah"
        };

        /// <summary>
        /// Converts a Gregorian date to a formatted Hijri date string.
        /// Example output: "12 Dhul-Qa'dah 1447 AH"
        /// Today's Hijri date: 14 Dhul-Qa'dah 1447 AH
        /// </summary>
        public static string GetHijriDate(DateTime gregorianDate)
        {
            HijriCalendar hijri = new HijriCalendar();

            int day = hijri.GetDayOfMonth(gregorianDate);
            int month = hijri.GetMonth(gregorianDate);
            int year = hijri.GetYear(gregorianDate);

            string monthName = HijriMonthNames[month - 1];

            return $"{day} {monthName} {year} AH";
        }

        /// <summary>
        /// Returns today's Hijri date as a formatted string.
        /// </summary>
        public static string GetTodayHijriDate()
        {
            return GetHijriDate(DateTime.Today);
        }
    }
}