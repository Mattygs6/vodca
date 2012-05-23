//-----------------------------------------------------------------------------
// <copyright file="Extensions.HashCode.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/03/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Xml.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Case insensitive hash for small strings
        /// </summary>
        /// <param name="input">string to get hash code</param>
        /// <returns>The hash value of the string</returns>
        /// <remarks>This method were modified for .NET</remarks>
        [DebuggerHidden]
        public static int ToHashCode(this string input)
        {
            unchecked
            {
                if (!string.IsNullOrEmpty(input))
                {
                    return input.ToUpper(CultureInfo.InvariantCulture).GetHashCode();
                }

                return string.Empty.GetHashCode();
            }
        }

        /// <summary>
        ///     Case insensitive hash for small size XElement
        /// </summary>
        /// <param name="xelement">XElement to get hash code</param>
        /// <returns>The hash value of the XElement</returns>
        /// <remarks>This method were modified for .NET</remarks>
        [DebuggerHidden]
        public static int ToHashCode(this XElement xelement)
        {
            unchecked
            {
                if (xelement != null)
                {
                    return xelement.ToString().ToUpper(CultureInfo.InvariantCulture).GetHashCode();
                }

                return string.Empty.GetHashCode();
            }
        }

        /// <summary>
        ///     Case insensitive hash for small size XContainer
        /// </summary>
        /// <param name="xelement">XContainer to get hash code</param>
        /// <returns>The hash value of the XContainer</returns>
        /// <remarks>This method were modified for .NET</remarks>
        [DebuggerHidden]
        public static int ToHashCode(this XContainer xelement)
        {
            unchecked
            {
                if (xelement != null)
                {
                    return xelement.ToString(SaveOptions.None).ToUpper(CultureInfo.InvariantCulture).GetHashCode();
                }

                return "XContainer".GetHashCode();
            }
        }

        /// <summary>
        ///     Case insensitive hash for small size XNode
        /// </summary>
        /// <param name="xelement">XNode to get hash code</param>
        /// <returns>The hash value of the XNode</returns>
        /// <remarks>This method were modified for .NET</remarks>
        [DebuggerHidden]
        public static int ToHashCode(this XNode xelement)
        {
            unchecked
            {
                if (xelement != null)
                {
                    return xelement.ToString(SaveOptions.None).ToUpper(CultureInfo.InvariantCulture).GetHashCode();
                }

                return "XNode".GetHashCode();
            }
        }
    }
}
