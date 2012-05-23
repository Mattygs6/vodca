//-----------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/01/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Adds the HTTP compression if client can accept it.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void AddHttpCompressionIfClientCanAcceptIt(this HttpContext context)
        {
            var acceptEncoding = context.Request.Headers["Accept-Encoding"];

            if (!string.IsNullOrEmpty(acceptEncoding))
            {
                // gzip must be first, because chrome has an issue accepting deflate data
                // when sending it json text
                if (acceptEncoding.IndexOf("gzip", StringComparison.OrdinalIgnoreCase) != -1 || acceptEncoding.IndexOf("deflate", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    context.Request.ServerVariables["IIS_EnableDynamicCompression"] = "1";

                    // Allow proxy servers to cache encoded and unencoded versions separately 
                    context.Response.AppendHeader("Vary", "Content-Encoding");
                }
            }
        }
    }
}
