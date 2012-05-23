//-----------------------------------------------------------------------------
// <copyright file="VMimes.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/17/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    ///     Most used mime types in website
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Web\Misc\VMimes.cs" title="VMimes.cs" lang="C#" />
    /// </example>
    public static class VMimes
    {
        /// <summary>
        ///     Specifies that the data is in HTML format.
        /// </summary>
        public const string Html = "text/html";

        /// <summary>
        ///     Specifies that the data is NOT interpreted.
        /// </summary>
        public const string Octet = "application/octet-stream";

        /// <summary>
        ///     Specifies that the data is in Portable Document Format (PDF).
        /// </summary>
        public const string Pdf = "application/pdf";

        /// <summary>
        ///     Specifies that the data is in Rich Text Format (RTF).
        /// </summary>
        public const string Rtf = "application/rtf";

        /// <summary>
        ///     Specifies that the data is compressed.
        /// </summary>
        public const string Zip = "application/zip";

        /// <summary>
        ///     Specifies that the data is in Graphics Interchange Format (GIF) form.
        /// </summary>
        public const string Gif = "image/gif";

        /// <summary>
        ///     Specifies that the data is in Portable Network Graphics (PNG) format.
        /// </summary>
        public const string Png = "image/png";

        /// <summary>
        ///     Specifies that the data is in Joint Photographic Experts Group (JPEG) format.
        /// </summary>
        public const string Jpeg = "image/jpeg";

        /// <summary>
        ///     Specifies that the data is in Tagged Image File Format (TIFF).
        /// </summary>
        public const string Tiff = "image/tiff";

        /// <summary>
        ///     Specifies that the data is in Rich Text Format (RTF).
        /// </summary>
        public const string RichText = "text/richtext";

        /// <summary>
        ///     Specifies that the data is in XML format.
        /// </summary>
        public const string Xml = "text/xml";

        /// <summary>
        ///     Specifies that the data is plain text.
        /// </summary>
        public const string Plain = "text/plain";

        /// <summary>
        ///    Specifies that the data is CSS. 
        /// </summary>
        public const string Css = "text/css";
    }
}
