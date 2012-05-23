//-----------------------------------------------------------------------------
// <copyright file="Extensions.Headers.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/14/20120
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the content type header.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="contenttype">The content type.</param>
        /// <returns>
        /// The Http Response
        /// </returns>
        public static HttpResponse SetHeaderContentType(this HttpResponse response, string contenttype)
        {
            if (response != null)
            {
                if (!string.IsNullOrWhiteSpace(contenttype))
                {
                    response.AddHeader("Content-Type", contenttype);
                }
            }

            return response;
        }

        /// <summary>
        /// Sets the header Etag.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="etag">The Etag.</param>
        /// <returns>The Http Response</returns>
        public static HttpResponse SetHeaderEtag(this HttpResponse response, object etag)
        {
            if (response != null)
            {
                var value = string.Concat(etag);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    response.AddHeader("Etag", value);
                }
            }

            return response;
        }

        /// <summary>
        /// Sets the cache control.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="cacheControl">The cache control.</param>
        /// <returns>The Http Response</returns>
        public static HttpResponse SetHeaderCacheControl(this HttpResponse response, string cacheControl = "public, max-age=84600")
        {
            if (response != null)
            {
                response.AddHeader("Cache-Control", cacheControl);
            }

            return response;
        }

        /// <summary>
        /// Removes the header vary.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>
        /// The Http Response
        /// </returns>
        public static HttpResponse RemoveHeaderVary(this HttpResponse response)
        {
            if (response != null)
            {
                response.Headers.Remove("Vary");
            }

            return response;
        }

        /// <summary>
        /// Sets the header vary.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="vary">The vary.</param>
        /// <returns>The Http Response</returns>
        public static HttpResponse SetHeaderVary(this HttpResponse response, string vary = "*")
        {
            if (response != null)
            {
                response.AddHeader("Vary", vary);
            }

            return response;
        }

        /// <summary>
        /// Sets the cache control.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns>
        /// The Http Response
        /// </returns>
        public static HttpResponse SetHeaderExpires(this HttpResponse response, DateTime dateTime)
        {
            if (response != null)
            {
                response.AddHeader("Expires", dateTime.ToUniversalTime().ToLongDateString());
            }

            return response;
        }

        /// <summary>
        /// Sets the last modified header.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The Http Response</returns>
        public static HttpResponse SetHeaderLastModified(this HttpResponse response, DateTime dateTime)
        {
            if (response != null)
            {
                response.AddHeader("Last-Modified", dateTime.ConvertDateToW3CTime());
            }

            return response;
        }
    }
}
