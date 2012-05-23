//-----------------------------------------------------------------------------
// <copyright file="Extensions.ThirdParty.DateTime.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Date:       07/24/2010
//  Source:     .NET Extensions - Extensions Methods Library
//  Url:        http://dnpextensions.codeplex.com/
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Globalization;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Calculates the age based on today.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns>The calculated age.</returns>
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            return CalculateAge(dateOfBirth, DateTime.Today);
        }

        /// <summary>
        /// Calculates the age based on a passed reference date.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <param name="referenceDate">The reference date to calculate on.</param>
        /// <returns>The calculated age.</returns>
        public static int CalculateAge(this DateTime dateOfBirth, DateTime referenceDate)
        {
            int years = referenceDate.Year - dateOfBirth.Year;

            if (referenceDate.Month < dateOfBirth.Month || (referenceDate.Month == dateOfBirth.Month && referenceDate.Day < dateOfBirth.Day))
            {
                --years;
            }

            return years;
        }

        /// <summary>
        /// Returns the number of days in the month of the provided date.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <returns>The number of days.</returns>
        public static int CountDaysOfMonth(this DateTime date)
        {
            var nextMonth = date.AddMonths(1);
            return new DateTime(nextMonth.Year, nextMonth.Month, 1).AddDays(-1).Day;
        }

        /// <summary>
        /// Returns the first day of the month of the provided date.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <returns>The first day of the month</returns>
        public static DateTime ToFirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Returns the first day of the month of the provided date.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <param name="dayOfWeek">The desired day of week.</param>
        /// <returns>The first day of the month</returns>
        public static DateTime ToFirstDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.ToFirstDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
            {
                dt = dt.AddDays(1);
            }

            return dt;
        }

        /// <summary>
        /// Returns the last day of the month of the provided date.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <returns>The last day of the month.</returns>
        public static DateTime ToLastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, CountDaysOfMonth(date));
        }

        /// <summary>
        /// Returns the last day of the month of the provided date.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <param name="dayOfWeek">The desired day of week.</param>
        /// <returns>The date time</returns>
        public static DateTime ToLastDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            var dt = date.ToLastDayOfMonth();
            while (dt.DayOfWeek != dayOfWeek)
            {
                dt = dt.AddDays(-1);
            }

            return dt;
        }

        /// <summary>
        /// Indicates whether the date is today.
        /// </summary>
        /// <param name="dt">The date instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified date is today; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsToday(this DateTime dt)
        {
            return dt.Date == DateTime.Today;
        }

        /// <summary>
        /// Sets the time on the specified DateTime value.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="hours">The hours to be set.</param>
        /// <param name="minutes">The minutes to be set.</param>
        /// <param name="seconds">The seconds to be set.</param>
        /// <returns>The DateTime including the new time value</returns>
        public static DateTime SetTime(this DateTime date, int hours, int minutes, int seconds)
        {
            return date.SetTime(new TimeSpan(hours, minutes, seconds));
        }

        /// <summary>
        /// Sets the time on the specified DateTime value.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="time">The TimeSpan to be applied.</param>
        /// <returns>
        /// The DateTime including the new time value
        /// </returns>
        public static DateTime SetTime(this DateTime date, TimeSpan time)
        {
            return date.Date.Add(time);
        }

        /// <summary>
        /// Converts a DateTime into a DateTimeOffset using the local system time zone.
        /// </summary>
        /// <param name="localDateTime">The local DateTime.</param>
        /// <returns>The converted DateTimeOffset</returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime localDateTime)
        {
            return localDateTime.ToDateTimeOffset(null);
        }

        /// <summary>
        /// Converts a DateTime into a DateTimeOffset using the specified time zone.
        /// </summary>
        /// <param name="localDateTime">The local DateTime.</param>
        /// <param name="localTimeZone">The local time zone.</param>
        /// <returns>The converted DateTimeOffset</returns>
        public static DateTimeOffset ToDateTimeOffset(this DateTime localDateTime, TimeZoneInfo localTimeZone)
        {
            localTimeZone = localTimeZone ?? TimeZoneInfo.Local;

            if (localDateTime.Kind != DateTimeKind.Unspecified)
            {
                localDateTime = new DateTime(localDateTime.Ticks, DateTimeKind.Unspecified);
            }

            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, localTimeZone);
        }

        /// <summary>
        /// Gets the first day of the week using the current culture.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <returns>The first day of the week</returns>
        public static DateTime ToFirstDayOfWeek(this DateTime date)
        {
            return date.ToFirstDayOfWeek(null);
        }

        /// <summary>
        /// Gets the first day of the week using the specified culture.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <param name="cultureInfo">The culture to determine the first weekday of a week.</param>
        /// <returns>The first day of the week</returns>
        public static DateTime ToFirstDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            while (date.DayOfWeek != firstDayOfWeek)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

        /// <summary>
        /// Gets the last day of the week using the current culture.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <returns>The first day of the week</returns>
        public static DateTime ToLastDayOfWeek(this DateTime date)
        {
            return date.ToLastDayOfWeek(null);
        }

        /// <summary>
        /// Gets the last day of the week using the specified culture.
        /// </summary>
        /// <param name="date">The date instance.</param>
        /// <param name="cultureInfo">The culture to determine the first weekday of a week.</param>
        /// <returns>The first day of the week</returns>
        public static DateTime ToLastDayOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            return date.ToFirstDayOfWeek(cultureInfo).AddDays(6);
        }

        /// <summary>
        /// Gets the next occurrence of the specified weekday within the current week using the current culture.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="weekday">The desired weekday.</param>
        /// <returns>The calculated date.</returns>
        /// <example>View code: <br /><code title="C# File" lang="C#">
        /// var thisWeeksMonday = DateTime.Now.GetWeekday(DayOfWeek.Monday);
        /// </code></example>
        public static DateTime ToWeeksWeekday(this DateTime date, DayOfWeek weekday)
        {
            return date.ToWeeksWeekday(weekday, null);
        }

        /// <summary>
        /// Gets the next occurrence of the specified weekday within the current week using the specified culture.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="weekday">The desired weekday.</param>
        /// <param name="cultureInfo">The culture to determine the first weekday of a week.</param>
        /// <returns>The calculated date.</returns>
        /// <example>View code: <br /><code title="C# File" lang="C#">
        /// var thisWeeksMonday = DateTime.Now.GetWeekday(DayOfWeek.Monday);
        /// </code></example>
        public static DateTime ToWeeksWeekday(this DateTime date, DayOfWeek weekday, CultureInfo cultureInfo)
        {
            var firstDayOfWeek = date.ToFirstDayOfWeek(cultureInfo);
            return firstDayOfWeek.ToNextWeekday(weekday);
        }

        /// <summary>
        /// Gets the next occurrence of the specified weekday.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="weekday">The desired weekday.</param>
        /// <returns>The calculated date.</returns>
        /// <example>View code: <br /><code title="C# File" lang="C#">
        /// var lastMonday = DateTime.Now.ToNextWeekday(DayOfWeek.Monday);
        /// </code></example>
        public static DateTime ToNextWeekday(this DateTime date, DayOfWeek weekday)
        {
            while (date.DayOfWeek != weekday)
            {
                date = date.AddDays(1);
            }

            return date;
        }

        /// <summary>
        /// Gets the previous occurrence of the specified weekday.
        /// </summary>
        /// <param name="date">The base date.</param>
        /// <param name="weekday">The desired weekday.</param>
        /// <returns>The calculated date.</returns>
        /// <example>View code: <br /><code title="C# File" lang="C#">
        /// var lastMonday = DateTime.Now.ToPreviousWeekday(DayOfWeek.Monday);
        /// </code></example>
        public static DateTime ToPreviousWeekday(this DateTime date, DayOfWeek weekday)
        {
            while (date.DayOfWeek != weekday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

        /// <summary>
        /// Converts a regular DateTime to a RFC822 date string.
        /// </summary>
        /// <param name="date">The specific date.</param>
        /// <returns>
        /// The specified date formatted as a RFC822 date string.
        /// </returns>
        public static string ToRfc822Date(this DateTime date)
        {
            int offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
            string timeZone = "+" + offset.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

            if (offset < 0)
            {
                int i = offset * -1;
                timeZone = "-" + i.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
            }

            return date.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'));
        }
    }
}