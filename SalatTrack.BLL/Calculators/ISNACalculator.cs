using SalatTrack.Models;

namespace SalatTrack.BLL.Calculators
{
    /// <summary>
    /// Islamic Society of North America (ISNA) prayer time calculation method.
    /// Fajr angle: 15°, Isha angle: 15°
    /// Demonstrates POLYMORPHISM — same Calculate() method, different angles than MWL.
    /// </summary>
    public class ISNACalculator : PrayerTimeCalculator
    {
        // ISNA standard angles — different from MWL, same structure
        private const double FajrAngle = 15.0;
        private const double IshaAngle = 15.0;
        private const double MaghribAngle = 0.833;

        /// <summary>
        /// Calculates all 5 prayer times using ISNA angles.
        /// Overrides the abstract method from PrayerTimeCalculator.
        /// </summary>
        public override PrayerTime Calculate(City city, DateTime date)
        {
            // Pakistan is UTC+5
            double utcOffset = 5.0;

            double julianDate = GetJulianDate(date);
            var (declination, equationOfTime) = GetSolarPosition(julianDate);
            double solarNoon = GetSolarNoon(city.Longitude, equationOfTime, utcOffset);

            // Calculate hour angles using ISNA angles
            double fajrHour = GetHourAngle(-FajrAngle, city.Latitude, declination);
            double maghribHour = GetHourAngle(-MaghribAngle, city.Latitude, declination);
            double ishaHour = GetHourAngle(-IshaAngle, city.Latitude, declination);
            double asrHour = GetAsrHourAngle(city.Latitude, declination);

            return new PrayerTime
            {
                CityID = city.CityID,
                Date = date.Date,
                Fajr = HoursToTimeSpan(solarNoon - fajrHour),
                Dhuhr = HoursToTimeSpan(solarNoon + 0.033),
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