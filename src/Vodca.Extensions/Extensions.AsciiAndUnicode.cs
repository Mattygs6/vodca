//-----------------------------------------------------------------------------
// <copyright file="Extensions.AsciiAndUnicode.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/14/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Globalization;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Encodes/Converts the extended ASCII chars from string to Xml And Html Entities.
        ///     http://en.wikipedia.org/wiki/List_of_XML_and_HTML_character_entity_references
        /// </summary>
        /// <param name="text">The text input.</param>
        /// <returns>The text without Extended ASCII codes</returns>
        public static string ToXmlExtendedAsciiChars(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var chararray = text.ToCharArray();

                var textbuilder = new StringBuilder(chararray.Length);
                foreach (char character in chararray)
                {
                    if (character < 128)
                    {
                        textbuilder.Append(character);
                    }
                    else
                    {
                        if ((character >= '\x00a0') && (character < 'Ā'))
                        {
                            textbuilder.Append("&#");
                            textbuilder.Append(((int)character).ToString(NumberFormatInfo.InvariantInfo));
                            textbuilder.Append(';');
                        }
                        else
                        {
                            textbuilder.Append(character);
                        }
                    }
                }

                return textbuilder.ToString().Trim();
            }

            return string.Empty;
        }
    }
}
