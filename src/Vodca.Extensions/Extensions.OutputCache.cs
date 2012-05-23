//-----------------------------------------------------------------------------
// <copyright file="Extensions.OutputCache.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/01/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// The extension method set OutPut Cache parameters
        /// </summary>
        /// <param name="response">The HTTP-response objects</param>
        /// <param name="seconds">The time in seconds to cache</param>
        /// <param name="varybycustom">The varybycustom.</param>
        /// <param name="cacheitemdependency">The cache item dependency.</param>
        public static void SetOutPutCache(this HttpResponse response, double seconds, string varybycustom = "RawUrl", string cacheitemdependency = "Pages")
        {
            response.Cache.SetExpires(DateTime.Now.AddSeconds(seconds));
            response.Cache.SetCacheability(HttpCacheability.Server);
            response.Cache.SetVaryByCustom(varybycustom);
            response.Cache.SetValidUntilExpires(true);
            response.AddCacheItemDependency(cacheitemdependency);
        }
    }
}
