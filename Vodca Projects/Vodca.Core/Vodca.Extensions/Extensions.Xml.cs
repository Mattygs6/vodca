//-----------------------------------------------------------------------------
// <copyright file="Extensions.Xml.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/15/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Holds static instance of REGEX
        /// </summary>
        private static Regex regexForXmlAttribute;

        /// <summary>
        ///     Holds static instance of REGEX
        /// </summary>
        private static Regex regexForXmlIllegalUnicodeChars;

        /// <summary>
        ///     Gets the REGEX for XML attribute encoding.
        /// </summary>
        /// <value>The REGEX for XML attribute.</value>
        private static Regex RegexForXmlAttribute
        {
            get
            {
                return Extensions.regexForXmlAttribute ?? (Extensions.regexForXmlAttribute = new Regex(@"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Gets the REGEX for text to XML encoding.
        /// </summary>
        /// <value>The REGEX for text to XML encoding.</value>
        private static Regex RegexForXmlIllegalUnicodeChars
        {
            get
            {
                return Extensions.regexForXmlIllegalUnicodeChars ?? (Extensions.regexForXmlIllegalUnicodeChars = new Regex(@"(?<![\uD800-\uDBFF])[\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|[\x00-\x08\x0B\x0C\x0E-\x1F\x7F-\x9F\uFEFF\uFFFE\uFFFF]", RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Removes the illegal XML chars.
        /// </summary>
        /// <param name="text">The text for xml.</param>
        /// <returns>The string without illegal xml chars</returns>
        public static string RemoveIllegalXmlAttributeChars(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = Extensions.RegexForXmlAttribute.Replace(text, string.Empty);
            }

            return text;
        }

        /// <summary>
        ///     Removes the illegal XML chars.
        /// </summary>
        /// <param name="text">The text for xml.</param>
        /// <returns>The string without illegal xml chars</returns>
        public static string RemoveIllegalXmlChars(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = Extensions.RegexForXmlIllegalUnicodeChars.Replace(text, string.Empty);
            }

            return text;
        }

        /// <summary>
        ///     Loads the string into a LINQ to XML XDocument
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>The XML document object model (XDocument)</returns>
        public static XDocument ToXDocument(this string xml)
        {
            if (!string.IsNullOrWhiteSpace(xml))
            {
                return XDocument.Parse(xml);
            }

            return null;
        }

        /// <summary>
        ///     Loads the string into a LINQ to XElement
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>The XElement instance</returns>
        public static XElement ToXElement(this string xml)
        {
            if (!string.IsNullOrWhiteSpace(xml))
            {
                return XElement.Parse(xml);
            }

            return null;
        }

        /// <summary>
        ///     Loads the string into a XML DOM object (XmlDocument)
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>The XML document object model (XmlDocument)</returns>
        public static XmlDocument ToXmlDocument(this string xml)
        {
            if (!string.IsNullOrWhiteSpace(xml))
            {
                var document = new XmlDocument();
                document.LoadXml(xml);

                return document;
            }

            return null;
        }

        /// <summary>
        ///     Loads the string into a XML XPath DOM (XPathDocument)
        /// </summary>
        /// <param name="xml">The XML string.</param>
        /// <returns>The XML XPath document object model (XPathNavigator)</returns>
        public static XPathNavigator ToXPath(this string xml)
        {
            if (!string.IsNullOrWhiteSpace(xml))
            {
                using (var stream = new StringReader(xml))
                {
                    var document = new XPathDocument(stream);

                    return document.CreateNavigator();
                }
            }

            return null;
        }
    }
}
