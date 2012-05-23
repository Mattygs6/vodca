//-----------------------------------------------------------------------------
// <copyright file="Extensions.String.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/03/2009
//-----------------------------------------------------------------------------

namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexEmptySpaces;

        /// <summary>
        ///    Gets a Us Zip Code Regular Expression 
        /// </summary>
        private static Regex RegexEmptySpaces
        {
            get
            {
                return Extensions.regexEmptySpaces
                       ??
                       (Extensions.regexEmptySpaces =
                        new Regex(
                            @"\s{2,}",
                            RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Shortcut function
        /// </summary>
        /// <param name="value">actual string value</param>
        /// <returns>True if string not empty and false otherwise</returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Determines whether the given string is null or empty or whitespace.
        /// </summary>
        /// <param name="input">The user input.</param>
        /// <returns>
        ///     <c>true</c> if the given string is null or empty or whitespace; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            return string.IsNullOrEmpty(input) || input.HasWhiteSpace();
        }

        /// <summary>
        ///     Determines whether the string is all white space. Empty string will return false.
        /// </summary>
        /// <param name="input">The string to test whether it is all white space.</param>
        /// <returns>
        ///     <c>true</c> if the string is all white space; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasWhiteSpace(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input.All(char.IsWhiteSpace);
        }

        /// <summary>
        ///     Converts First char lower case except first letter will be capital.
        /// </summary>
        /// <param name="input">The input value</param>
        /// <param name="separators">The char array to split string in tokens and capitalize first letters</param>
        /// <returns>Formatted first or last name to first capital letter and rest lower case like 'Jim' or 'Beam'</returns>
        public static string ToUpperCaseFirstLetters(this string input, params char[] separators)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (separators.Length == 0)
                {
                    return char.ToUpperInvariant(input[0]) + input.Substring(1);
                }

                char[] inputchars = input.ToCharArray();

                // Case then has additional chars
                var list = new List<char>(separators.Length);
                list.AddRange(separators);

                for (int i = 0; i < inputchars.Length; i++)
                {
                    if (i == 0)
                    {
                        inputchars[0] = char.ToUpperInvariant(inputchars[0]);
                    }
                    else
                    {
                        if (list.Contains(inputchars[i - 1]))
                        {
                            inputchars[i] = char.ToUpperInvariant(inputchars[i]);
                        }
                    }
                }

                return new string(inputchars);
            }

            return input;
        }

        /// <summary>
        ///     Compares a string to a given string. The comparison is case insensitive.
        /// </summary>      
        /// <param name="source">The string to compare</param>
        /// <param name="target">The string to compare against</param>
        /// <returns>True if the strings are the same, false otherwise.</returns>
        public static bool IsEquals(this string source, string target)
        {
            return string.Compare(source, target, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Safe removal all leading and trailing white-space characters from the current string.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns> A string without leading and trailing white-space characters.</returns>
        public static string TrimEmptySpaces(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Trim();
        }

        /// <summary>
        /// Removes all leading and trailing white-space characters from the current string and the duplicate empty spaces also.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The trimmed string and without multiple empty spaces</returns>
        public static string TrimDuplicateEmptySpaces(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return Extensions.RegexEmptySpaces.Replace(value.Trim(), " ");
        }

        /// <summary>
        /// Formats the specified format.
        /// </summary>
        /// <param name="format">The string format.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns> A copy of format in which the format items have been replaced by the string
        /// representation of the corresponding objects in args.
        /// </returns>
        public static string ToFormat(this string format, params object[] parameters)
        {
            return string.Format(format, parameters);
        }

        /// <summary>
        /// Formats the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns> A copy of format in which the format items have been replaced by the string
        /// representation of the corresponding objects in args.
        /// </returns>
        public static string ToFormat(this string format, object parameter)
        {
            return string.Format(format, parameter);
        }

        /// <summary>
        /// Formats string the phone number format.
        /// </summary>
        /// <param name="phonenumber">The phone number.</param>
        /// <param name="format">The format.</param>
        /// <returns>The formatted phone or fax number</returns>
        public static string ToPhoneFormat(this string phonenumber, string format = "###-###-####")
        {
            var cleaned = phonenumber.RemoveNonDigitsChars();
            if (!string.IsNullOrWhiteSpace(cleaned))
            {
                long phone;
                if (long.TryParse(cleaned, out phone))
                {
                    return phone.ToString(format);
                }

                return phonenumber;
            }

            return string.Empty;
        }

        /// <summary>
        /// Coverts number the phone format.
        /// </summary>
        /// <param name="phonenumber">The phone number.</param>
        /// <param name="format">The format.</param>
        /// <returns>The formatted phone number</returns>
        public static string ToPhoneFormat(this long phonenumber, string format = "###-###-####")
        {
            if (phonenumber > 1000000000 && !string.IsNullOrWhiteSpace(format))
            {
                if (phonenumber > 10000000000000)
                {
                    return phonenumber.ToString(format + " ext ####");
                }

                if (phonenumber > 1000000000000)
                {
                    return phonenumber.ToString(format + " ext ###");
                }

                return phonenumber.ToString(format);
            }

            return phonenumber.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the phone format.
        /// </summary>
        /// <param name="phonenumber">The phone number.</param>
        /// <param name="format">The format.</param>
        /// <returns>The formatted phone number</returns>
        public static string ToPhoneFormat(this long? phonenumber, string format = "###-###-####")
        {
            if (phonenumber.HasValue)
            {
                return ToPhoneFormat(phonenumber.Value, format);
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts to Us phone format. d: 555.555.5555 x0000 Or D: 555-555-5555 x0000 OR P: (555) 555-5555 x0000
        /// </summary>
        /// <param name="phonenumber">The phone number.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// The formatted phone string
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1631:DocumentationMustMeetCharacterPercentage", Justification = "Reviewed. Suppression is OK here.")]
        public static string ToUsPhoneFormat(this object phonenumber, string format = "{0:D}")
        {
            /* d: 555.555.5555 x0000
             * D: 555-555-5555 x0000
             * P: (555) 555-5555 x0000
             * */

            return string.Format(new UsPhoneNumberFormatter(), format, phonenumber);
        }

        /// <summary>
        /// Normalizes string to ISO-8859-8.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The normalized string</returns>
        public static string NormalizeToIso88598(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                /* Normalize */
                byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(input);
                return Encoding.UTF8.GetString(tempBytes);
            }

            return input;
        }

        /// <summary>
        /// Normalizes string to Utf8.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The normalized string</returns>
        public static string NormalizeToUtf8(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                /* Normalize */
                byte[] tempBytes = Encoding.GetEncoding("Utf-8").GetBytes(input);
                return Encoding.UTF8.GetString(tempBytes);
            }

            return input;
        }
    }
}