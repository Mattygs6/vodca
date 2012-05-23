//-----------------------------------------------------------------------------
// <copyright file="Extensions.RegexUtilities.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Text;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        #region HTML STRIP

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexHtml;

        /// <summary>
        ///      Regular Expression holder
        /// </summary>
        private static Regex regexHtmlScriptTag;

        /// <summary>
        ///    Gets a Html Regular Expression 
        /// </summary>
        private static Regex RegexHtml
        {
            get
            {
                return Extensions.regexHtml ?? (Extensions.regexHtml = new Regex(@"<script.*?>.*?</script>|<[^>]*=""javascript:[^""]*""[^>]*>|<[/]{0,1}\s*(?<tag>\w*)\s*(?<attr>.*?=['""].*?[""'])*?\s*[/]{0,1}>|.&nbsp;", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace));
            }
        }

        /// <summary>
        ///    Gets a Html Script tag Regular Expression 
        /// </summary>
        private static Regex RegexHtmlScriptTag
        {
            get
            {
                return Extensions.regexHtmlScriptTag ?? (Extensions.regexHtmlScriptTag = new Regex(@"<(script)([^>]*)>[\\s\\S]*?</(script)([^>]*)>", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace));
            }
        }

        /// <summary>
        ///     A helper method to remove Html.  
        /// </summary>
        /// <param name="text">string from which remove html</param>
        /// <returns>String without html</returns>
        /// <remarks>Use the HtmlAgilityPack if can. This is solution for simple operation.</remarks>
        public static string RemoveHtmlFromText(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return Extensions.RegexHtml.Replace(text, string.Empty);
            }

            return text;
        }

        /// <summary>
        ///     A helper method to remove Html Script Tag.  
        /// </summary>
        /// <param name="text">string from which remove html script tag</param>
        /// <returns>String without script tag in html</returns>
        /// <remarks>Use the HtmlAgilityPack if can. This is solution for simple operation.</remarks>
        public static string RemoveScriptTagFromHtml(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return Extensions.RegexHtmlScriptTag.Replace(text, string.Empty);
            }

            return text;
        }

        /// <summary>
        ///     Strips illegal Characters from the string for use in SQL expressions.
        /// This method is designed for DataView Filter or phrase search in database
        /// </summary>
        /// <param name="value">User input</param>
        /// <returns>User input without illegal chars</returns>
        public static string RemoveIllegalSqlChars(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var builder = new StringBuilder(value.TrimDuplicateEmptySpaces());

            builder.Replace(@"""", string.Empty);
            builder.Replace(@"'", string.Empty);
            builder.Replace(";", string.Empty);
            builder.Replace(",", string.Empty);
            builder.Replace(":", string.Empty);
            builder.Replace("%", string.Empty);
            builder.Replace("#", string.Empty);
            builder.Replace("(", string.Empty);
            builder.Replace(")", string.Empty);
            builder.Replace("*", string.Empty);
            builder.Replace("+", string.Empty);
            builder.Replace("@", string.Empty);
            builder.Replace("  ", " ");

            return builder.ToString().Trim();
        }

        #endregion
    }
}
