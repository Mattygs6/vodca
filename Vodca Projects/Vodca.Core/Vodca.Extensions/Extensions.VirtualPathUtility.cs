//-----------------------------------------------------------------------------
// <copyright file="Extensions.VirtualPathUtility.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics;
    using System.IO;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the directory portion of a virtual path.
        /// </summary>
        /// <param name="path">The virtual/absolute path.</param>
        /// <param name="isvirtalpath">if set to <c>true</c> isvirtalpath.</param>
        /// <returns>
        /// Returns the directory portion of a virtual/absolute path.
        /// </returns>
        [DebuggerHidden]
        public static string GetDirectory(this string path, bool isvirtalpath = true)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (isvirtalpath)
                {
                    return VirtualPathUtility.GetDirectory(path);
                }

                return Path.GetDirectoryName(path).EnsureEndsWith(@"\");
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the extension of the file that is referenced in the virtual path.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <param name="isvirtalpath">if set to <c>true</c> [isvirtalpath].</param>
        /// <returns>
        /// The file name extension string literal, including the period (.), null, or an empty string ("").
        /// </returns>
        [DebuggerHidden]
        public static string GetExtension(this string path, bool isvirtalpath = true)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (isvirtalpath)
                {
                    return VirtualPathUtility.GetExtension(path);
                }

                return Path.GetExtension(path);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the file name of the file that is referenced in the virtual path.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <param name="isvirtalpath">if set to <c>true</c> [isvirtalpath].</param>
        /// <returns>
        /// The file name literal after the last directory character in virtualPath; otherwise, an empty string (""), if the last character of virtualPath is a directory or volume separator character.
        /// </returns>
        [DebuggerHidden]
        public static string GetFileName(this string path, bool isvirtalpath = true)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (isvirtalpath)
                {
                    return VirtualPathUtility.GetFileName(path);
                }

                return Path.GetFileName(path);
            }

            return string.Empty;
        }
    }
}
