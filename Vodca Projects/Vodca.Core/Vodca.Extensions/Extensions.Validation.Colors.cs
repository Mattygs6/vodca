//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Colors.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/18/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexHexColor;

        /// <summary>
        ///    Gets a Hex Color Regular Expression  
        /// </summary>
        private static Regex RegexHexColor
        {
            get
            {
                return Extensions.regexHexColor ?? (Extensions.regexHexColor = new Regex(@"^#[abcdefABCDEF\d]{3,6}$", RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid hex color. The first char '#' is mandatory
        /// </summary>
        /// <param name="hexcolor">The hex html color like #CCC or #f5f5f5</param>
        /// <returns>
        ///     <c>true</c> if it is a valid hex color; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidHexColor(this string hexcolor)
        {
            return !string.IsNullOrEmpty(hexcolor) && Extensions.RegexHexColor.IsMatch(hexcolor);
        }
    }
}
