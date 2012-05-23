//-----------------------------------------------------------------------------
// <copyright file="Extensions.Page.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/30/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Allows programmatic add to the HTML <![CDATA[<meta>]]> tag on the server.
        /// </summary>
        /// <param name="page">A Web Forms page</param>
        /// <param name="tagname">The meta data property name</param>
        /// <param name="content">The meta data property value</param>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------------------
        /// C# ASP.NET source code
        /// this.Page.AddMetaTag("author", "J.Baltika");
        /// -------------------------------------------------------------------
        /// Output:
        /// <meta name="author" content="J.Baltika" />
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = " Will be disposed at Page dispose event.")]
        public static void AddMetaTag(this Page page, string tagname, string content)
        {
            if (page != null && page.Header != null)
            {
                var metatag = new HtmlMeta { Name = tagname, Content = content };

                // Define an HTML <meta> element that is useful for search engines.
                page.Header.Controls.Add(metatag);
            }
        }

        /// <summary>
        ///     Allows programmatic add to the HTML <![CDATA[<meta name="keywords">]]> tag on the server.
        /// </summary>
        /// <param name="page">A Web Forms page</param>
        /// <param name="content">The meta data property value</param>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------------------
        /// C# ASP.NET source code
        /// this.Page.AddMetaTagKeywords("ASP.NET, Web, tutorial");
        /// -------------------------------------------------------------------
        /// Output:
        /// <meta name="keywords" content="ASP.NET, Web, tutorial" />
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = " Will be disposed at Page dispose event.")]
        public static void AddMetaTagKeywords(this Page page, string content)
        {
            if (page != null && page.Header != null)
            {
                var metatag = new HtmlMeta { Name = "keywords", Content = content };

                // Define an HTML <meta> element that is useful for search engines.
                page.Header.Controls.Add(metatag);
            }
        }

        /// <summary>
        /// Allows programmatic add to the HTML meta tag <![CDATA[<meta name="description">]]> tag on the server.
        /// </summary>
        /// <param name="page">A Web Forms page</param>
        /// <param name="content">The meta data property value</param>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------------------
        /// C# ASP.NET source code
        /// this.Page.AddMetaTagDescription("Web tutorial");
        /// -------------------------------------------------------------------
        /// Output:
        /// <meta name="description" content="Web tutorial" />
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = " Will be disposed at Page dispose event.")]
        public static void AddMetaTagDescription(this Page page, string content)
        {
            if (page != null && page.Header != null)
            {
                var metatag = new HtmlMeta { Name = "description", Content = content };

                // Define an HTML <meta> element that is useful for search engines.
                page.Header.Controls.Add(metatag);
            }
        }

        /// <summary>
        ///     Adds the CSS style sheet link to the head
        /// </summary>
        /// <param name="page">A Web Forms page</param>
        /// <param name="href">The URL target link</param>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------------------
        /// C# ASP.NET source code
        /// this.Page.AddCssStylesheetLink("/App_Styles/Internet/main.css");
        /// -------------------------------------------------------------------
        /// Output:
        /// <link  href="/App_Styles/Internet/main.css" rel="stylesheet" type="text/css" />
        /// ]]>
        /// </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Will be disposed at Page dispose event.")]
        public static void AddCssStylesheetLink(this Page page, string href)
        {
            if (page != null && page.Header != null && !string.IsNullOrEmpty(href))
            {
                // Insert CSS style sheet
                var cssLink = new HtmlLink { Href = href };
                cssLink.Attributes.Add("rel", "stylesheet");
                cssLink.Attributes.Add("type", "text/css");
                cssLink.Attributes.Add("id", string.Concat("styles", href.ToHashCode()));

                page.Header.Controls.Add(cssLink);
            }
        }

        /// <summary>
        ///     Adds the JavaScript link to the head
        /// </summary>
        /// <param name="page">A Web Forms page</param>
        /// <param name="href">The URL target link</param>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// -------------------------------------------------------------------
        /// /* C# ASP.NET source code */
        /// this.Page.AddCssStylesheetLink("/App_Scripts/Additional.js");
        /// -------------------------------------------------------------------
        /// Output:
        /// <script src="/App_Scripts/Additional.js" type="text/javascript"></script>
        /// ]]>
        ///  </code>
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Will be disposed at Page dispose event.")]
        public static void AddJavaScriptLink(this Page page, string href)
        {
            if (page != null && page.Header != null && !string.IsNullOrEmpty(href))
            {
                var js = new HtmlGenericControl("script");
                js.Attributes.Add("type", "text/javascript");
                js.Attributes.Add("src", href);
                js.Attributes.Add("id", string.Concat("js", href.ToHashCode()));

                page.Header.Controls.Add(js);
            }
        }

        /// <summary>
        ///    Add UTF Encoding to the page 
        /// </summary>
        /// <param name="page">A Web Forms page</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = " Will be disposed at Page dispose event.")]
        public static void AddUtfEncoding(this Page page)
        {
            if (page != null && page.Header != null)
            {
                var metatag = new HtmlMeta();
                metatag.Attributes.Add("http-equiv", "Content-type");
                metatag.Content = "text/html;charset=UTF-8";

                page.Header.Controls.Add(metatag);
            }
        }
    }
}
