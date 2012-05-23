//-----------------------------------------------------------------------------
// <copyright file="VContentTypes.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/17/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    ///     Most used HTTP MIME types of the output stream in website
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Web\Misc\VContentTypes.cs" title="VContentTypes.cs" lang="C#" />
    /// </example>
    public static class VContentTypes
    {
        /// <summary>
        ///     Xml document format
        /// </summary>
        public const string Xml = "application/xml";

        /// <summary>
        ///     Portable Document Format (PDF).
        /// </summary>
        public const string Pdf = "application/pdf";

        /// <summary>
        ///     MS Office Word format
        /// </summary>
        public const string Word = "application/msword";

        /// <summary>
        ///     MS Office Excel format
        /// </summary>
        public const string Excel = "application/vnd.xls";

        /// <summary>
        ///     MS Office PowerPoint format
        /// </summary>
        public const string PowerPoint = "application/vnd.memoryStream-powerpoint";

        /// <summary>
        ///      Joint Photographic Experts Group (JPEG) format
        /// </summary>
        public const string Jpeg = "image/pjpeg";

        /// <summary>
        ///      Joint Photographic Experts Group (JPEG) format
        /// </summary>
        public const string Jpg = "image/jpeg";

        /// <summary>
        ///     Specifies that the data is in Graphics Interchange Format (GIF) form.
        /// </summary>
        public const string Gif = "image/gif";

        /// <summary>
        ///     Specifies that the data is in Portable Network Graphics (PNG) format.
        /// </summary>
        public const string Png = "image/png";

        /// <summary>
        ///     Specifies that the data is in data compression format.
        /// </summary>
        public const string Zip = "application/zip";

        /// <summary>
        ///  Specifies that the data is in JS format
        /// </summary>
        public const string JavaScript = "text/javascript";

        /// <summary>
        ///  Specifies that the data is in Css format
        /// </summary>
        public const string Css = "text/css";
    }
}
