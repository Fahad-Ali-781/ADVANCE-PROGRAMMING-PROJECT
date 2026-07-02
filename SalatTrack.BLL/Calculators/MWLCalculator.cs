using SalatTrack.Models;

namespace SalatTrack.BLL.Calculators
{
    /// <summary>
    /// Muslim World League (MWL) prayer time calculation method.
    /// Fajr angle: 18°, Isha angle: 17°
    /// Demonstrates INHERITANCE — extends PrayerTimeCalculator.
    /// </summary>
    public class MWLCalculator : PrayerTimeCalculator
    {
        // MWL standard angles
        private const double FajrAngle = 18.0;
        private const double IshaAngle = 17.0;
        private const double MaghribAngle = 0.833; // Sun just below horizon

        /// <summary>
        /// Calculates all 5 prayer times for a given city and date.
        /// Overrides the abstract method from PrayerTimeCalculator.
        /// </summary>
        public override PrayerTime Calculate(City city, DateTime date)
        {
            // Pakistan is UTC+5
            double utcOffset = 5.0;

            double julianDate = GetJulianDate(date);
            var (declination, equationOfTime) = GetSolarPosition(julianDate);
            double solarNoon = GetSolarNoon(city.Longitude, equationOfTime, utcOffset);

            // Calculate hour angles
            double fajrHour = GetHourAngle(-FajrAngle, city.Latitude, declination);
            double maghribHour = GetHourAngle(-MaghribAngle, city.Latitude, declination);
            double ishaHour = GetHourAngle(-IshaAngle, city.Latitude, declination);
            double asrHour = GetAsrHourAngle(city.Latitude, declination);

            return new PrayerTime
            {
                CityID = city.CityID,
                Date = date.Date,
                Fajr = HoursToTimeSpan(solarNoon - fajrHour),
                Dhuhr = HoursToTimeSpan(solarNoon + 0.033), // Slight offset after solar noon
                Asr = HoursToTimeSpan(solarNoon + asrHour),
                Maghrib = HoursToTimeSpan(solarNoon + maghribHour),
                Isha = HoursToTimeSpan(solarNoon + ishaHour)
            };
        }

        /// <summary>
        /// Calculates Asr hour angle using Shafi'i shadow ratio (factor = 1).
        /// </summary>
        private double GetAsrHourAngle(double latitude, double declination)
        {
            double latRad = ToRadians(latitude);
            double angle = Math.Atan(1.0 / (1.0 + Math.Tan(Math.Abs(latRad - declination))));
            double cosHourAngle = (Math.Sin(angle) - Math.Sin(latRad) * Math.Sin(declination))
                                / (Math.Cos(latRad) * Math.Cos(declination));
            cosHourAngle = Math.Max(-1.0, Math.Min(1.0, cosHourAngle));
            return ToDegrees(Math.Acos(cosHourAngle)) / 15.0;
        }
    }
}