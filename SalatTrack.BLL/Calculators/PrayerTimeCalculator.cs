using SalatTrack.Models;

namespace SalatTrack.BLL.Calculators
{
    /// <summary>
    /// Abstract base class for prayer time calculation.
    /// Demonstrates INHERITANCE — MWLCalculator and ISNACalculator extend this.
    /// </summary>
    public abstract class PrayerTimeCalculator
    {
        // -------------------------------------------------------
        // Abstract method — each subclass MUST implement this
        // This demonstrates POLYMORPHISM
        // -------------------------------------------------------
        public abstract PrayerTime Calculate(City city, DateTime date);

        // -------------------------------------------------------
        // Shared helper methods — available to all subclasses
        // -------------------------------------------------------

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        protected double ToRadians(double degrees) => degrees * Math.PI / 180.0;

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        protected double ToDegrees(double radians) => radians * 180.0 / Math.PI;

        /// <summary>
        /// Calculates the Julian Date from a Gregorian date.
        /// Used in solar position calculations.
        /// </summary>
        protected double GetJulianDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (month <= 2)
            {
                year -= 1;
                month += 12;
            }

            int A = year / 100;
            int B = 2 - A + (A / 4);

            return Math.Floor(365.25 * (year + 4716))
                 + Math.Floor(30.6001 * (month + 1))
                 + day + B - 1524.5;
        }

        /// <summary>
        /// Calculates the sun's declination and equation of time.
        /// These are needed to find solar noon and prayer angles.
        /// </summary>
        protected (double declination, double equationOfTime) GetSolarPosition(double julianDate)
        {
            double D = julianDate - 2451545.0; // Days since J2000.0
            double g = ToRadians(357.529 + 0.98560028 * D); // Mean anomaly
            double q = 280.459 + 0.98564736 * D;            // Mean longitude
            double L = ToRadians(q + 1.915 * Math.Sin(g) + 0.020 * Math.Sin(2 * g)); // Ecliptic longitude

            double e = ToRadians(23.439 - 0.00000036 * D);  // Obliquity

            double declination = Math.Asin(Math.Sin(e) * Math.Sin(L));
            double RA = Math.Atan2(Math.Cos(e) * Math.Sin(L), Math.Cos(L)) / (Math.PI / 12);
            double equationOfTime = q / 15.0 - (RA < 0 ? RA + 24 : RA);

            return (declination, equationOfTime);
        }

        /// <summary>
        /// Calculates solar noon (Dhuhr time) in hours.
        /// </summary>
        protected double GetSolarNoon(double longitude, double equationOfTime, double utcOffset)
        {
            return 12.0 - longitude / 15.0 - equationOfTime + utcOffset;
        }

        /// <summary>
        /// Calculates the hour angle for a given solar angle.
        /// Used for Fajr, Maghrib, and Isha calculations.
        /// </summary>
        protected double GetHourAngle(double angle, double latitude, double declination)
        {
            double latRad = ToRadians(latitude);
            double angleRad = ToRadians(angle);

            double cosHourAngle = (Math.Sin(angleRad) - Math.Sin(latRad) * Math.Sin(declination))
                                / (Math.Cos(latRad) * Math.Cos(declination));

            // Clamp to valid range to avoid NaN at extreme latitudes
            cosHourAngle = Math.Max(-1.0, Math.Min(1.0, cosHourAngle));

            return ToDegrees(Math.Acos(cosHourAngle)) / 15.0;
        }

        /// <summary>
        /// Converts decimal hours to a TimeSpan object.
        /// e.g. 5.5 → 05:30:00
        /// </summary>
        protected TimeSpan HoursToTimeSpan(double hours)
        {
            hours = hours % 24;
            if (hours < 0) hours += 24;
            int h = (int)hours;
            int m = (int)((hours - h) * 60);
            return new TimeSpan(h, m, 0);
        }
    }
}