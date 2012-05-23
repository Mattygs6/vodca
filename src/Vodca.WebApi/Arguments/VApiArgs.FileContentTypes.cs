//-----------------------------------------------------------------------------
// <copyright file="VApiArgs.FileContentTypes.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// The Web API arguments
    /// </summary>
    public sealed partial class VApiArgs
    {
        /// <summary>
        /// The strongly typed response mime/content types 
        /// </summary>
        public static class FileContentTypes
        {
            /// <summary>
            /// The content/mime type
            /// </summary>
            public const string Json = "application/json";

            /// <summary>
            /// The content/mime type
            /// </summary>
            public const string Html = "text/html";

            /// <summary>
            /// The content/mime type
            /// </summary>
            public const string Xml = "text/xml";
        }
    }
}
