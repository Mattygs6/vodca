//-----------------------------------------------------------------------------
// <copyright file="Extensions.HtmlAgilityPack.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//   Date:      11/08/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Xml.Linq;
    using HtmlAgilityPack;

    /// <content>
    /// The Html parsing related extensions
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.HtmlAgilityPack.cs" title="Extensions.HtmlAgilityPack.cs" lang="C#" />
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Adds WYSIWYG content to an XElement node
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="wysiwyg">The WYSIWYG string.</param>
        /// <param name="roottagattributes">The root tag attributes.</param>
        /// <returns>
        /// The XElement instance
        /// </returns>
        /// <remarks>The Html WYSIWIG must be encased in container (div) tag.</remarks>
        public static XElement AddWysiwyg(this XElement xelement, string wysiwyg, params XAttribute[] roottagattributes)
        {
            if (xelement != null)
            {
                var xml = HtmlToXElement(wysiwyg);

                if (roottagattributes != null)
                {
                    foreach (var attr in roottagattributes)
                    {
                        xml.Add(attr);
                    }
                }

                xelement.Add(xml);
            }

            return xelement;
        }

        /// <summary>
        /// Adds the HTML.
        /// </summary>
        /// <param name="xelement">The XElement.</param>
        /// <param name="wysiwyg">The WYSIWYG.</param>
        /// <returns>The parsed XElement</returns>
        public static XElement AddHtml(this XElement xelement, string wysiwyg)
        {
            if (xelement != null)
            {
                var xml = HtmlToXElement(wysiwyg);

                foreach (var node in xml.Elements())
                {
                    xelement.Add(node);
                }
            }

            return xelement;
        }

        /// <summary>
        /// Converts the HTML to plain text. Uses HtmlAgilityPack.
        /// Removes script and comment tags first before converting to text.
        /// </summary>
        /// <param name="html">The HTML string.</param>
        /// <param name="removenodes">The remove nodes. XPath expression.</param>
        /// <returns>
        /// The plain text from html
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.Extensions\Extensions.cs" title="Extensions.cs" lang="C#"/>
        /// </example>
        public static string ConvertHtmlToText(this string html, string removenodes = "//comment()|//script|//link")
        {
            if (string.IsNullOrWhiteSpace(html) || (html.IndexOf('<') == -1 || html.IndexOf('>') == -1))
            {
                return html;
            }

            var htmlnode = LoadHtmlNode(html, removenodes);

            if (htmlnode != null)
            {
                return htmlnode.InnerText;
            }

            return string.Empty;
        }

        /// <summary>
        /// Loads the HTML using HtmlAgilityPack.
        /// Removes script and comment tags.
        /// </summary>
        /// <param name="html">The HTML string.</param>
        /// <param name="removenodes">The remove nodes. XPath expression.</param>
        /// <returns>
        /// The HtmlAgilityPack HtmlNode
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.Extensions\Extensions.cs" title="Extensions.cs" lang="C#"/>
        /// </example>
        [System.CLSCompliant(false)]
        public static HtmlNode LoadHtmlNode(this string html, string removenodes = "//comment()|//script|//link")
        {
            if (!string.IsNullOrWhiteSpace(html))
            {
                var doc = new HtmlDocument
                {
                    /* http://stackoverflow.com/questions/5556089/html-agility-pack-removes-break-tag-close */
                    OptionWriteEmptyNodes = true
                };

                doc.LoadHtml(html);

                IEnumerable<HtmlNode> scriptnodes = doc.DocumentNode.SelectNodes(removenodes);

                foreach (HtmlNode script in scriptnodes)
                {
                    script.RemoveAll();
                }

                return doc.DocumentNode;
            }

            return null;
        }

        /// <summary>
        /// Converts the plain text to HTML.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="newlineasbreaktag">if set to <c>true</c> new line as break tag added instead of '\n' char.</param>
        /// <returns>The text as html</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.Extensions\Extensions.cs" title="Extensions.cs" lang="C#"/>
        /// </example>
        public static string ConvertPlainTextToHtml(this string input, bool newlineasbreaktag = true)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                string innerrtext = ConvertHtmlToText(input);
                if (!string.IsNullOrWhiteSpace(innerrtext))
                {
                    int length = innerrtext.Length;
                    var htmlbuilder = new StringBuilder(length + 32);

                    char previuos = '\0';

                    for (int i = 0; i < length; i++)
                    {
                        char current = innerrtext[i];
                        switch (current)
                        {
                            case '\n':
                                if (newlineasbreaktag)
                                {
                                    htmlbuilder.Append("<br />");
                                }

                                break;

                            case '\r':
                                break;

                            case ' ':
                                if (previuos != ' ')
                                {
                                    htmlbuilder.Append(' ');
                                }

                                break;

                            case '"':
                                htmlbuilder.Append("&quot;");
                                break;

                            case '&':
                                htmlbuilder.Append("&amp;");
                                break;

                            default:
                                if ((current >= '\x00a0') && (current < 'Ā'))
                                {
                                    htmlbuilder.Append("&#");
                                    htmlbuilder.Append(((int)current).ToString(NumberFormatInfo.InvariantInfo));
                                    htmlbuilder.Append(';');
                                }
                                else
                                {
                                    htmlbuilder.Append(current);
                                }

                                break;
                        }

                        previuos = current;
                    }

                    return htmlbuilder.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Loads the XElement.
        /// </summary>
        /// <param name="wysiwyg">The WYSIWYG.</param>
        /// <returns>The parsed XElements</returns>
        private static XElement HtmlToXElement(this string wysiwyg)
        {
            var doc = new HtmlDocument
                {
                    /* http://stackoverflow.com/questions/5556089/html-agility-pack-removes-break-tag-close */
                    OptionWriteEmptyNodes = true,
                    OptionCheckSyntax = true,
                    OptionOutputAsXml = true
                };

            var html = string.Concat("<div>", wysiwyg, "</div>");
            doc.LoadHtml(html);

            var xml = XElement.Parse(doc.DocumentNode.OuterHtml);
            return xml;
        }
    }
}
