//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.SocialSecurity.cs" company="genuine">
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
        ///  A description of the Social Security regular expression:
        ///  ^\d{3}
        ///      Beginning of line or string
        ///      Any digit, exactly 3 repetitions
        ///  Any character in this class: [-\s], zero or one repetitions
        ///  Any digit, exactly 2 repetitions
        ///  Any character in this class: [-\s], zero or one repetitions
        ///  \d{4}$
        ///      Any digit, exactly 4 repetitions
        ///      End of line or string
        /// </summary>
        private const string RegexPatternSocialSecurity = @"^\d{3}[-\s]?\d{2}[-\s]?\d{4}$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexSocialSecurity;

        /// <summary>
        ///    Gets a Social Security Regular Expression  
        /// </summary>
        private static Regex RegexSocialSecurity
        {
            get
            {
                return Extensions.regexSocialSecurity ?? (Extensions.regexSocialSecurity = new Regex(Extensions.RegexPatternSocialSecurity, RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid Social Security number. Dashes are optional.
        /// </summary>
        /// <param name="socialSecurityNumber">The Social Security Number</param>
        /// <returns>
        ///     <c>true</c> if it is a valid Social Security number; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidSocialSecurityNumber(this string socialSecurityNumber)
        {
            return !string.IsNullOrEmpty(socialSecurityNumber) && Extensions.RegexSocialSecurity.IsMatch(socialSecurityNumber);
        }
    }
}
