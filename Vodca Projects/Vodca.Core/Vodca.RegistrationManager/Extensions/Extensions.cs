//-----------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// The pipeline extensions
    /// </summary>
    internal static partial class InternalExtensions
    {
        /// <summary>
        /// Gets the assembly files.
        /// </summary>
        /// <param name="nameregex">The library name REGEX.</param>
        /// <returns>The Assemblies with custom attributes to look for</returns>
        public static IEnumerable<string> GetAssemblyFiles(string nameregex = null)
        {
            string directory = HttpRuntime.BinDirectory;

            IEnumerable<string> dlls = Directory.GetFiles(directory, @"*.dll");
            if (!string.IsNullOrWhiteSpace(nameregex))
            {
                try
                {
                    var regex = new Regex(
                        nameregex,
                        RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace);

                    /* ReSharper disable AssignNullToNotNullAttribute */
                    return dlls.Where(x => regex.IsMatch(Path.GetFileName(x)));
                    /* ReSharper restore AssignNullToNotNullAttribute */
                }
                catch (Exception exception)
                {
                    exception.LogException();
#if DEBUG
                    throw;
#endif
                }
            }

            return dlls;
        }
    }
}
