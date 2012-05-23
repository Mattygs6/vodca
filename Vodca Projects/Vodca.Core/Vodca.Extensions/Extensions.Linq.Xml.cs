//-----------------------------------------------------------------------------
// <copyright file="Extensions.LINQ.Xml.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/02/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Xml.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        /// Toes the XC data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string as XData object instance</returns>
        public static XCData ToXCData(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return new XCData(value);
        }

        /// <summary>
        /// Converts Object the XCData.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string as XData object instance</returns>
        public static XCData ToXCData(this XElement value)
        {
            if (value != null)
            {
                return new XCData(string.Concat(value));
            }

            return null;
        }

        /* ReSharper restore InconsistentNaming */
    }
}
