//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Text.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>   
        ///     A description of the First or Last Name regular expression:  
        ///  Beginning of line or string
        ///  Any character in this class: [a-zA-Z0-9-\s\.'], between 1 and 40 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternName = @"^[a-zA-Z0-9-\s\.']{1,40}$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexName;

        /// <summary>
        ///    Gets a Name Regular Expression  
        /// </summary>
        private static Regex RegexName
        {
            get
            {
                return Extensions.regexName ?? (Extensions.regexName = new Regex(Extensions.RegexPatternName, RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string contains only alpha(letters) characters.
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the specified string is alpha; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLetters(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                char[] inputchars = input.ToCharArray();
                return inputchars.All(char.IsLetter);
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string contains only alpha(letters) characters and space.
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the specified string is alpha; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLettersAndSpace(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                char[] inputchars = input.ToCharArray();
                return inputchars.All(t => char.IsLetter(t) && char.IsWhiteSpace(t));
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string contains alphanumeric characters and additional chars only
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <param name="additionalchars">Valid additional chars</param>
        /// <returns>
        ///     <c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLettersOrDigits(this string input, params char[] additionalchars)
        {
            if (!string.IsNullOrEmpty(input))
            {
                char[] inputchars = input.ToCharArray();

                if (additionalchars.Length == 0)
                {
                    // Case then no additional chars
                    return inputchars.All(char.IsLetterOrDigit);
                }

                // Case then has additional chars
                var list = new List<char>(additionalchars.Length);

                list.AddRange(additionalchars);

                return inputchars.All(current => char.IsLetterOrDigit(current) || list.Contains(current));
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string contains only alphanumeric characters, space and additional chars only
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <param name="additionalchars">Valid additional chars</param>
        /// <returns>
        ///     <c>true</c> if the string is alphanumeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLettersOrDigitsOrSpace(this string input, params char[] additionalchars)
        {
            if (!string.IsNullOrEmpty(input))
            {
                char[] inputchars = input.ToCharArray();

                if (additionalchars.Length == 0)
                {
                    // Case then no additional chars
                    return inputchars.All(t => char.IsLetterOrDigit(t) || char.IsWhiteSpace(t));
                }

                // Case then has additional chars
                var list = new List<char>(additionalchars.Length);

                list.AddRange(additionalchars);

                return inputchars.All(current => char.IsLetterOrDigit(current) || char.IsWhiteSpace(current) || list.Contains(current));
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string is valid First or Last Name
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the string is valid First or Last Name; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidName(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexName.IsMatch(input);
        }
    }
}