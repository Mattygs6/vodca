//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Phone.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///  A description of the Phone regular expression:
        ///  Select from 2 alternatives
        ///      ^\d{10}
        ///          Beginning of line or string
        ///          Any digit, exactly 10 repetitions
        ///      (\(?\d{3}\)?[-\.\s]{0,1}\d{3}[-\.\s]\d{4})$
        ///          [1]: A numbered capture group. [\(?\d{3}\)?[-\.\s]{0,1}\d{3}[-\.\s]\d{4}]
        ///              \(?\d{3}\)?[-\.\s]{0,1}\d{3}[-\.\s]\d{4}
        ///                  Literal (, zero or one repetitions
        ///                  Any digit, exactly 3 repetitions
        ///                  Literal ), zero or one repetitions
        ///                  Any character in this class: [-\.\s], between 0 and 1 repetitions
        ///                  Any digit, exactly 3 repetitions
        ///                  Any character in this class: [-\.\s]
        ///                  Any digit, exactly 4 repetitions
        ///          End of line or string
        /// </summary>
        /// <remarks>
        /// <code>
        /// Valid
        ///     (500)-599-9999
        ///     500-599-9999
        ///     (500) 599-9999
        ///     (500) 599 9999
        ///     500.955.3336
        /// </code>
        /// </remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1631:DocumentationMustMeetCharacterPercentage", Justification = "Reviewed. Suppression is OK here.")]
        private const string RegexPatternUsPhone = @"^\d{10}|(\(?\d{3}\)?[-\.\s]{0,1}\d{3}[-\.\s]\d{4})$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexUsPhone;

        /// <summary>
        ///    Gets US Phone Regular Expression  
        /// </summary>
        private static Regex RegexUsPhone
        {
            get
            {
                return Extensions.regexUsPhone ?? (Extensions.regexUsPhone = new Regex(Extensions.RegexPatternUsPhone, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid US Phone.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <remarks>
        ///  <pre>
        /// Valid Entries:
        ///     508-622-1940
        ///     508-6221940
        ///     508622-1940
        ///     5086221940
        ///     (425) 555-0123
        ///     425-555-0123
        ///     425 555 0123
        ///     1-425-555-0123
        /// </pre>
        /// </remarks>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// /* An extension method attached to any typeof(string) object instance.  */
        ///     bool isvalid = "508-678-9999".IsValidPhone();
        /// OR
        ///     if(this.Page.TextBoxPhone.Text.IsValidPhone())
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsValidPhone("508-678-9999");
        /// </code>    
        /// </example>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1631:DocumentationMustMeetCharacterPercentage", Justification = "Reviewed. Suppression is OK here.")]
        public static bool IsValidPhone(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexUsPhone.IsMatch(input);
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid US Phone OR Empty OR Null strings.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <remarks>
        /// <pre>
        /// Valid Entries:
        ///     "" -- Empty string
        ///     508-622-1940
        ///     508-6221940
        ///     508622-1940
        ///     5086221940
        ///     (425) 555-0123
        ///     425-555-0123
        ///     425 555 0123
        ///     1-425-555-0123
        /// </pre>
        /// </remarks>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// /* An extension method attached to any typeof(string) object instance. */
        ///     bool isvalid = "508-678-9999".IsValidPhoneOptional();
        /// OR
        ///     if(this.Page.TextBoxPhone.Text.IsValidPhoneOptional())
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsValidPhoneOptional("508-678-9999");
        /// </example>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1631:DocumentationMustMeetCharacterPercentage", Justification = "Reviewed. Suppression is OK here.")]
        public static bool IsValidPhoneOptional(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Extensions.RegexUsPhone.IsMatch(input);
            }

            return true;
        }
    }
}