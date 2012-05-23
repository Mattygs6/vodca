//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Html.cs" company="genuine">
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
        ///     Regular expression to check if string contains HTML tags  
        ///  A description of the regular expression:
        ///  char open the tag
        ///  Any character in this class: [a-zA-Z\/]
        ///  Any character that is NOT in this class: [>], any number of repetitions
        ///  char close the tag
        /// </summary>
        private const string RegexPatternContainsHtml = @"<[a-zA-Z\/][^>]*>";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexContainsHtml;

        /// <summary>
        ///    Gets a Contains HTML Regular Expression  
        /// </summary>
        private static Regex RegexContainsHtml
        {
            get
            {
                return Extensions.regexContainsHtml ?? (Extensions.regexContainsHtml = new Regex(Extensions.RegexPatternContainsHtml, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule checks for a HTML tags in the string.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if strong; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsHtmlTags(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexContainsHtml.IsMatch(input);
        }
    }
}