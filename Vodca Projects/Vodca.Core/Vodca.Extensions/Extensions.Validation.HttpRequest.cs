//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.HttpRequest.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/09/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Validates that "POST" request is originated on the same server
        /// </summary>
        /// <param name="request">Enables ASP.NET to read the HTTP values sent by a client during a Web request.</param>
        /// <returns>True if "POST" originated on the same server or not. "GET" will always return True.</returns>
        public static bool IsValidPostRequest(this HttpRequest request)
        {
            try
            {
                if (request != null)
                {
                    if (string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
                    {
                        if (request.UrlReferrer != null)
                        {
                            // ip with deny access if POST request comes from other server
                            return !string.IsNullOrEmpty(request.UrlReferrer.Host) && !string.IsNullOrEmpty(request.Url.Host) && string.Equals(request.UrlReferrer.Host, request.Url.Host, StringComparison.OrdinalIgnoreCase);
                        }
                    }
                }

                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}