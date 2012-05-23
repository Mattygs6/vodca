//-----------------------------------------------------------------------------
// <copyright file="Extensions.Guid.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Removes all special chars like {-} from Guid string
        /// </summary>
        /// <param name="id">The Guid instance</param>
        /// <returns>Returns string version of GUID without special chars. Which can be used for some cases like search engines</returns>
        public static string ToShortId(this Guid id)
        {
            return id.ToString("N").ToLowerInvariant();
        }

        /// <summary>
        ///     Removes all special chars like {-} from Guid string
        /// </summary>
        /// <param name="id">The Guid instance</param>
        /// <returns>Returns string version of GUID without special chars. Which can be used for some cases like search engines</returns>
        public static string ToShortId(this Guid? id)
        {
            if (id.HasValue)
            {
                return id.Value.ToString("N").ToLowerInvariant();
            }

            return string.Empty;
        }
    }
}
