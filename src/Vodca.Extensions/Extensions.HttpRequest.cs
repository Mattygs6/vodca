//-----------------------------------------------------------------------------
// <copyright file="Extensions.HttpRequest.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       09/25/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Get the client's IP address, taking into account some proxies and clusters
        /// </summary>
        /// <param name="request">The HTTP values sent by a client during a Web request</param>
        /// <remarks>
        ///     Many proxies don't expose the real client's IP address. In such cases, the proxies IP address will be returned
        /// </remarks>
        /// <returns>The best guess at a client's IP address, or an empty string</returns>
        public static string UserIPAddress(this HttpRequest request)
        {
            if (request != null)
            {
                var headers = new[] { "HTTP_X_FORWARDED_FOR", "HTTP_X_CLUSTER_CLIENT_IP", "REMOTE_ADDR", "REMOTE_HOST" };

                NameValueCollection serverVariables = request.ServerVariables;
                foreach (string ipaddress in headers.Select(t => serverVariables[t]).Where(ipaddress => !string.IsNullOrEmpty(ipaddress)))
                {
                    return ipaddress;
                }

                return request.UserHostAddress;
            }

            return string.Empty;
        }

        /* ReSharper restore InconsistentNaming */

        /// <summary>
        ///     Determines whether is layout file the specified request URL. The content type must start 'text/html'
        /// </summary>
        /// <param name="response">The http response.</param>
        /// <returns>
        ///     <c>true</c> if is layout file the specified request URL; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLayoutFile(this HttpResponse response)
        {
            Ensure.IsNotNull(response, "Extensions.IsLayoutFile-request");

            return response.ContentType.StartsWith("text/html", StringComparison.OrdinalIgnoreCase);
        }
    }
}
