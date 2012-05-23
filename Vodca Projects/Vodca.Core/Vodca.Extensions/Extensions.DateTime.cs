//-----------------------------------------------------------------------------
// <copyright file="Extensions.DateTime.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///  Regular expression
        /// </summary>
        private const string RegexExpressionIsoDateTime = @"^(?<Year>\d{4})-{0,1}(?<Month>\d{2})-{0,1}(?<Day>\d{2})(([tT:dD]{0,1})(?<Hour>\d{2}))?(:{0,1}(?<Minutes>\d{2}))?(:{0,1}(?<Seconds>\d{2}))?";

        /// <summary>
        /// The REGEX ISO date time.
        /// </summary>
        private static Regex regexIsoDateTime;

        /// <summary>
        /// Gets the of REGEX ISO date time.
        /// </summary>
        /// <value>The REGEX ISO date time.</value>
        /// <example>View code: <br />
        /// <code>
        /// <![CDATA[
        ///  A description of the regular expression:
        ///  Beginning of line or string
        ///  [Year]: A named capture group. [\d{4}]
        ///      Any digit, exactly 4 repetitions
        ///  -, between 0 and 1 repetitions
        ///  [Month]: A named capture group. [\d{2}]
        ///      Any digit, exactly 2 repetitions
        ///  -, between 0 and 1 repetitions
        ///  [Day]: A named capture group. [\d{2}]
        ///      Any digit, exactly 2 repetitions
        ///  [1]: A numbered capture group. [([tT:dD]{0,1})(?<Hour>\d{2})], zero or one repetitions
        ///      ([tT:dD]{0,1})(?<Hour>\d{2})
        ///          [2]: A numbered capture group. [[tT:dD]{0,1}]
        ///              Any character in this class: [tT:dD], between 0 and 1 repetitions
        ///          [Hour]: A named capture group. [\d{2}]
        ///              Any digit, exactly 2 repetitions
        ///  [3]: A numbered capture group. [:{0,1}(?<Minutes>\d{2})], zero or one repetitions
        ///      :{0,1}(?<Minutes>\d{2})
        ///          :, between 0 and 1 repetitions
        ///          [Minutes]: A named capture group. [\d{2}]
        ///              Any digit, exactly 2 repetitions
        ///  [4]: A numbered capture group. [:{0,1}(?<Seconds>\d{2})], zero or one repetitions
        ///      :{0,1}(?<Seconds>\d{2})
        ///          :, between 0 and 1 repetitions
        ///          [Seconds]: A named capture group. [\d{2}]
        ///              Any digit, exactly 2 repetitions
        /// ]]>
        /// </code>
        /// </example>
        public static Regex RegexIsoDateTime
        {
            get
            {
                return Extensions.regexIsoDateTime ?? (Extensions.regexIsoDateTime = new Regex(Extensions.RegexExpressionIsoDateTime, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        /// Convert date to the US date.
        /// </summary>
        /// <param name="date">The specified date.</param>
        /// <returns>The specified date formatted as a 'MM/dd/yyyy' date string</returns>
        /* ReSharper disable InconsistentNaming */
        public static string ToUSDate(this DateTime date)
        /* ReSharper restore InconsistentNaming */
        {
            return date.ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// Formats date time to correct ISO date.
        /// </summary>
        /// <param name="datetime">The date time.</param>
        /// <param name="ignoretime">if set to <c>true</c> ignore time part.</param>
        /// <returns>The date time in ISO format</returns>
        public static string ToIsoDateFormat(this DateTime datetime, bool ignoretime = true)
        {
            if (ignoretime)
            {
                return datetime.Date.ToString("yyyyMMddTHHmmss");
            }

            return datetime.ToString("yyyyMMddTHHmmss");
        }

        /// <summary>
        /// Converts from the ISO date string to DateTime.
        /// </summary>
        /// <param name="isodatetime">The ISO date time.</param>
        /// <param name="ignoretime">if set to <c>true</c> ignore time part</param>
        /// <returns>The date time object or null</returns>
        public static DateTime? ToIsoDate(this string isodatetime, bool ignoretime = true)
        {
            if (!string.IsNullOrEmpty(isodatetime))
            {
                Match match = RegexIsoDateTime.Match(isodatetime);
                if (match.Success)
                {
                    var groups = match.Groups;
                    if (groups.Count > 2)
                    {
                        int year = Convert.ToInt32(groups["Year"].Value);
                        int month = Convert.ToInt32(groups["Month"].Value);
                        int day = Convert.ToInt32(groups["Day"].Value);

                        if (ignoretime)
                        {
                            return new DateTime(year, month, day);
                        }

                        int hours = Convert.ToInt32(groups["Hour"].Value);
                        int minutes = Convert.ToInt32(groups["Minutes"].Value);
                        int seconds = Convert.ToInt32(groups["Seconds"].Value);

                        return new DateTime(year, month, day, hours, minutes, seconds);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Formats the HTTP cookie date time.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns>The Date as string for cookie time</returns>
        public static string FormatHttpCookieDateTime(this DateTime datetime)
        {
            if (datetime < DateTime.MaxValue.AddDays(-1.0) && datetime > DateTime.MinValue.AddDays(1.0))
            {
                datetime = datetime.ToUniversalTime();
            }

            return datetime.ToString("ddd, dd-MMM-yyyy HH':'mm':'ss 'GMT'", DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a DateTime value to w3c format
        /// based on http://www.w3.org/TR/NOTE-datetime notes
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// The date in W3C format
        /// </returns>
        /// <see href="http://www.bytechaser.com/en/functions/k4s6frpca5/convert-date-time-to-w3c-datetime-format.aspx"/>
        public static string ConvertDateToW3CTime(this DateTime date)
        {
            // Get the UTC offset from the date value
            var utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);

            string time = date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss");

            // append the offset e.g. z=0, add 1 hour is +01:00
            time += utcOffset == TimeSpan.Zero ? "Z" : string.Format("{0}{1:00}:{2:00}", utcOffset > TimeSpan.Zero ? "+" : "-", utcOffset.Hours, utcOffset.Minutes);

            return time;
        }
    }
}
