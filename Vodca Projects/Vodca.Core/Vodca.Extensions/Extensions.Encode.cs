//-----------------------------------------------------------------------------
// <copyright file="Extensions.Encode.cs" company="genuine">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Modified by:     J.Baltikauskas
//  Date:            03/18/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Text;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Encodes input strings for use in HTML
        /// </summary>
        /// <param name="input">String input to HtmlEncode</param>
        /// <returns>Encoded  string</returns>
        public static string HtmlEncode(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(string.Empty, input.Length * 2);
                foreach (char ch in input)
                {
                    if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch == ' ') || ((ch > '/') && (ch < ':'))) || (((ch == '.') || (ch == ',')) || ((ch == '-') || (ch == '_')))))
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        builder.Append("&#").Append(((int)ch).ToString(CultureInfo.InvariantCulture)).Append(";");
                    }
                }

                return builder.ToString();
            }

            return input;
        }

        /// <summary>
        ///     Encodes input strings for use in XML file
        /// </summary>
        /// <param name="input">String input to XmlEncode</param>
        /// <returns>Encoded  string</returns>
        public static string XmlEncode(this string input)
        {
            return input.HtmlEncode();
        }

        /// <summary>
        ///     Encodes input strings for use in Universal Resource Locators (URLs)
        /// </summary>
        /// <param name="input">Url as string</param>
        /// <returns>Encoded Url</returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Should be similar to Microsoft's AntiXss Library API")]
        public static string UrlEncode(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var builder = new StringBuilder(string.Empty, input.Length * 2);
                foreach (char ch in input)
                {
                    if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch > '/') && (ch < ':')) || (((ch == '.') || (ch == '-')) || (ch == '_'))))
                    {
                        builder.Append(ch);
                    }
                    else if (ch > '\x007f')
                    {
                        builder.Append("%u" + Extensions.TwoByteHex(ch));
                    }
                    else
                    {
                        builder.Append("%" + Extensions.SingleByteHex(ch));
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }
    }
}
