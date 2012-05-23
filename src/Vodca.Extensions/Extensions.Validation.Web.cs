//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Web.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///  A description of the Domain regular expression:
        ///  Beginning of line or string
        ///  [1]: A numbered capture group. [http|https]
        ///      Select from 2 alternatives
        ///          http
        ///              http
        ///          https
        ///              https
        ///  :\/\/
        ///      :
        ///      Literal /
        ///      Literal /
        ///  Any character in this class: [\w-_], one or more repetitions
        ///  [2]: A numbered capture group. [\.[\w-_]+], one or more repetitions
        ///      \.[\w-_]+
        ///          Literal .
        ///          Any character in this class: [\w-_], one or more repetitions
        ///  [3]: A numbered capture group. [:[\w-_]+], zero or one repetitions
        ///      :[\w-_]+
        ///          :
        ///          Any character in this class: [\w-_], one or more repetitions
        ///  /
        /// </summary>
        private const string RegexPatternDomain = @"^(http|https):\/\/[\w-_]+(\.[\w-_]+)+(:[\w-_]+)?/";

        /// <summary>
        /// Email Regular Expression
        /// </summary>
        /// <remarks>
        /// See more on line <![CDATA[http://www.regexlib.com/Search.aspx?k=email&c=1&m=4&ps=100]]>
        /// </remarks>
        private const string RegexPatternEmail = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";

        /// <summary>
        ///  A description of the IP Address regular expression:
        ///  Beginning of line or string
        ///  Match expression but don't capture it. [(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.], exactly 3 repetitions
        ///      (?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.
        ///          Match expression but don't capture it. [25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?]
        ///              Select from 3 alternatives
        ///                  25[0-5]
        ///                      25
        ///                      Any character in this class: [0-5]
        ///                  2[0-4][0-9]
        ///                      2
        ///                      Any character in this class: [0-4]
        ///                      Any character in this class: [0-9]
        ///                  [01]?[0-9][0-9]?
        ///                      Any character in this class: [01], zero or one repetitions
        ///                      Any character in this class: [0-9]
        ///                      Any character in this class: [0-9], zero or one repetitions
        ///          Literal .
        ///  Match expression but don't capture it. [25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?]
        ///      Select from 3 alternatives
        ///          25[0-5]
        ///              25
        ///              Any character in this class: [0-5]
        ///          2[0-4][0-9]
        ///              2
        ///              Any character in this class: [0-4]
        ///              Any character in this class: [0-9]
        ///          [01]?[0-9][0-9]?
        ///              Any character in this class: [01], zero or one repetitions
        ///              Any character in this class: [0-9]
        ///              Any character in this class: [0-9], zero or one repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternIpAddress = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        /// <summary>   
        ///  A description of the Url regular expression: 
        ///  Beginning of line or string
        ///  [1]: A numbered capture group. [(((http|https):\/\/)|^)[\w-_]+(\.[\w-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?]
        ///      (((http|https):\/\/)|^)[\w-_]+(\.[\w-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?
        ///          [2]: A numbered capture group. [((http|https):\/\/)|^]
        ///              Select from 2 alternatives
        ///                  [3]: A numbered capture group. [(http|https):\/\/]
        ///                      (http|https):\/\/
        ///                          [4]: A numbered capture group. [http|https]
        ///                              Select from 2 alternatives
        ///                                  http
        ///                                      http
        ///                                  https
        ///                                      https
        ///                          :
        ///                          Literal /
        ///                          Literal /
        ///                  Beginning of line or string
        ///          Any character in this class: [\w-_], one or more repetitions
        ///          [5]: A numbered capture group. [\.[\w-_]+], one or more repetitions
        ///              \.[\w-_]+
        ///                  Literal .
        ///                  Any character in this class: [\w-_], one or more repetitions
        ///          [6]: A numbered capture group. [[\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#]], zero or one repetitions
        ///              [\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#]
        ///                  Any character in this class: [\w\-\.,@?^=%&amp;:/~\+#], any number of repetitions
        ///                  Any character in this class: [\w\-\@?^=%&amp;/~\+#]
        ///  End of line or string
        /// </summary>
        private const string RegexPatternUrl = @"^((((http|https):\/\/)|^)[\w-_]+(\.[\w-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)$";

        /// <summary>
        ///  A description of the Strong Password regular expression:
        ///  Match if suffix is absent. [^[0-9]*$]
        ///      ^[0-9]*$
        ///          Beginning of line or string
        ///          Any character in this class: [0-9], any number of repetitions
        ///          End of line or string
        ///  Match if suffix is absent. [^[a-zA-Z]*$]
        ///      ^[a-zA-Z]*$
        ///          Beginning of line or string
        ///          Any character in this class: [a-zA-Z], any number of repetitions
        ///          End of line or string
        ///  Beginning of line or string
        ///  [1]: A numbered capture group. [[a-zA-Z0-9]{8,}]
        ///      Any character in this class: [a-zA-Z0-9], at least 8 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternStrongPassword = @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,})$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexEmail;

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexUrl;

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexDomain;

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexIpAddress;

        /// <summary>
        ///   Gets an Email Regular Expression 
        /// </summary>
        private static Regex RegexEmail
        {
            get
            {
                return Extensions.regexEmail ?? (Extensions.regexEmail = new Regex(Extensions.RegexPatternEmail, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///   Gets a Url Regular Expression
        /// </summary>
        private static Regex RegexUrl
        {
            get
            {
                return Extensions.regexUrl ?? (Extensions.regexUrl = new Regex(Extensions.RegexPatternUrl, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///    Gets a Domain Regular Expression  
        /// </summary>
        private static Regex RegexDomain
        {
            get
            {
                return Extensions.regexDomain ?? (Extensions.regexDomain = new Regex(Extensions.RegexPatternDomain, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///    Gets a IP Address Regular Expression  
        /// </summary>
        private static Regex RegexIpAddress
        {
            get
            {
                return Extensions.regexIpAddress ?? (Extensions.regexIpAddress = new Regex(Extensions.RegexPatternIpAddress, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Email and NOT empty or null.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmail(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexEmail.IsMatch(input);
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Email or empty string.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmailOptional(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Extensions.RegexEmail.IsMatch(input);
            }

            return true;
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Url.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidUrl(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexUrl.IsMatch(input);
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Domain.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDomain(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexDomain.IsMatch(input);
        }

        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Rule determines whether the specified string is a valid IP address.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidIPAddress(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexIpAddress.IsMatch(input);
        }

        /* ReSharper restore InconsistentNaming */

        /// <summary>
        ///     Rule determines whether the specified string is consider a strong password based on the supplied string.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <remarks>
        ///     Validates a strong password. It must be between 8 and 10 characters, contain at least one digit and one alphabetic character, and must not contain special characters.
        /// </remarks>
        /// <returns>
        ///     <c>true</c> if strong; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidStrongPassword(this string password)
        {
            return !string.IsNullOrEmpty(password) && Regex.IsMatch(password, Extensions.RegexPatternStrongPassword, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        }
    }
}