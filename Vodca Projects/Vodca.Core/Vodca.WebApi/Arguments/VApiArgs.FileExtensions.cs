//-----------------------------------------------------------------------------
// <copyright file="VApiArgs.FileExtensions.cs" company="genuine">
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
        /// The strongly typed file extensions
        /// </summary>
        public static class FileExtensions
        {
            /// <summary>
            /// The file extension
            /// </summary>
            public static readonly string Extensionless = string.Empty;

            /// <summary>
            /// The file extension
            /// </summary>
            public const string Json = ".json";

            /// <summary>
            /// The file extension
            /// </summary>
            public const string Xml = ".xml";
        }
    }
}
