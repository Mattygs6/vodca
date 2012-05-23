//-----------------------------------------------------------------------------
// <copyright file="Extensions.FilePath.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/10/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Web.Hosting;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Maps a virtual path to a physical path on the server.
        /// </summary>
        /// <param name="virtualpath">The virtual path (absolute or relative).</param>
        /// <returns>The physical path on the server specified by virtualPath.</returns>
        public static string MapPath(this string virtualpath)
        {
            if (!string.IsNullOrWhiteSpace(virtualpath))
            {
                return HostingEnvironment.MapPath(virtualpath);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Determines whether the specified file exists.
        /// </summary>
        /// <param name="virtualpath">The virtual path (absolute or relative).</param>
        /// <returns>True if the path contains the name of an existing file; otherwise, false.</returns>
        public static bool FileExists(this string virtualpath)
        {
            if (!string.IsNullOrWhiteSpace(virtualpath))
            {
                return HostingEnvironment.VirtualPathProvider.FileExists(virtualpath);
            }

            return false;
        }

        /// <summary>
        ///     Determines whether the specified folder exists.
        /// </summary>
        /// <param name="virtualpath">The virtual path (absolute or relative).</param>
        /// <returns>True if the path contains the name of an existing folder; otherwise, false.</returns>
        public static bool DirectoryExists(this string virtualpath)
        {
            if (!string.IsNullOrWhiteSpace(virtualpath))
            {
                return HostingEnvironment.VirtualPathProvider.DirectoryExists(virtualpath);
            }

            return false;
        }
    }
}
