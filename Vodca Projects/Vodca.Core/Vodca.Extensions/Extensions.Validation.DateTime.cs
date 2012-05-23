//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.DateTime.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///  A description of the Date regular expression: 
        ///  Beginning of line or string
        ///  [1]: A numbered capture group. [[1-9]|0[1-9]|1[012]]
        ///      Select from 3 alternatives
        ///          Any character in this class: [1-9]
        ///          0[1-9]
        ///              0
        ///              Any character in this class: [1-9]
        ///          1[012]
        ///              1
        ///              Any character in this class: [012]
        ///  Any character in this class: [- /.]
        ///  [2]: A numbered capture group. [[1-9]|0[1-9]|[12][0-9]|3[01]]
        ///      Select from 4 alternatives
        ///          Any character in this class: [1-9]
        ///          0[1-9]
        ///              0
        ///              Any character in this class: [1-9]
        ///          [12][0-9]
        ///              Any character in this class: [12]
        ///              Any character in this class: [0-9]
        ///          3[01]
        ///              3
        ///              Any character in this class: [01]
        ///  Any character in this class: [- /.]
        ///  [3]: A numbered capture group. [19|20]
        ///      Select from 2 alternatives
        ///          19
        ///              19
        ///          20
        ///              20
        ///  Any character in this class: [0-9], exactly 2 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternDate = @"^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)[0-9]{2}$";

        /// <summary>
        ///  A description of the Date Time regular expression: 
        ///  Beginning of line or string
        ///  [1]: A numbered capture group. [[1-9]|0[1-9]|1[012]]
        ///      Select from 3 alternatives
        ///          Any character in this class: [1-9]
        ///          0[1-9]
        ///              0
        ///              Any character in this class: [1-9]
        ///          1[012]
        ///              1
        ///              Any character in this class: [012]
        ///  Any character in this class: [- /.]
        ///  [2]: A numbered capture group. [[1-9]|0[1-9]|[12][0-9]|3[01]]
        ///      Select from 4 alternatives
        ///          Any character in this class: [1-9]
        ///          0[1-9]
        ///              0
        ///              Any character in this class: [1-9]
        ///          [12][0-9]
        ///              Any character in this class: [12]
        ///              Any character in this class: [0-9]
        ///          3[01]
        ///              3
        ///              Any character in this class: [01]
        ///  Any character in this class: [- /.]
        ///  [3]: A numbered capture group. [19|20]
        ///      Select from 2 alternatives
        ///          19
        ///              19
        ///          20
        ///              20
        ///  Any character in this class: [0-9], exactly 2 repetitions
        ///  [4]: A numbered capture group. [\s\d{1,2}:\d{1,2}:\d{1,2}(\s(AM|PM))?], zero or one repetitions
        ///      \s\d{1,2}:\d{1,2}:\d{1,2}(\s(AM|PM))?
        ///          Whitespace
        ///          Any digit, between 1 and 2 repetitions
        ///          :
        ///          Any digit, between 1 and 2 repetitions
        ///          :
        ///          Any digit, between 1 and 2 repetitions
        ///          [5]: A numbered capture group. [\s(AM|PM)], zero or one repetitions
        ///              \s(AM|PM)
        ///                  Whitespace
        ///                  [6]: A numbered capture group. [AM|PM]
        ///                      Select from 2 alternatives
        ///                          AM
        ///                              AM
        ///                          PM
        ///                              PM
        ///  End of line or string
        /// </summary>
        private const string RegexPatternDateTime = @"^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)[0-9]{2}(\s\d{1,2}:\d{1,2}:\d{1,2}(\s(AM|PM))?)?$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexDate;

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexDateTime;

        /// <summary>
        ///    Gets an Date Regular Expression
        /// </summary>
        private static Regex RegexDate
        {
            get
            {
                return Extensions.regexDate ?? (Extensions.regexDate = new Regex(Extensions.RegexPatternDate, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///    Gets an DateTime Regular Expression
        /// </summary>
        private static Regex RegexDateTime
        {
            get
            {
                return Extensions.regexDateTime ?? (Extensions.regexDateTime = new Regex(Extensions.RegexPatternDateTime, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Date regular.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDate(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexDate.IsMatch(input);
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Date regular or Empty or Null string.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDateOptional(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Extensions.RegexDate.IsMatch(input);
            }

            return true;
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Date regular.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDateTime(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexDateTime.IsMatch(input);
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Date regular OR Empty or Null string.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDateTimeOptional(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Extensions.RegexDateTime.IsMatch(input);
            }

            return true;
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Date in the defined range.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <param name="minvalue">Min date time target</param>
        /// <param name="maxvalue">Max date time target</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInRangeDate(this string input, DateTime minvalue, DateTime maxvalue)
        {
            bool isvalid = false;
            DateTime date;
            if (!string.IsNullOrEmpty(input) && DateTime.TryParse(input, out date))
            {
                isvalid = (minvalue < date) && (maxvalue > date);
            }

            return isvalid;
        }
    }
}