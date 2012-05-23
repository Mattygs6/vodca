//-----------------------------------------------------------------------------
// <copyright file="VLogError.Methods.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Web;

    /// <summary>
    ///     A logical application error utility methods 
    /// </summary>
    public abstract partial class VLogError
    {
        /// <summary>
        /// Sets an object of a uniform resource identifier properties and IP's
        /// </summary>
        /// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <remarks>
        /// <pre>
        /// Explanation:
        /// This class will have for ErrorUrl properties as consequence of UrlRewrite/Redirect possibility
        /// and for best error analyzing during development/maintenance
        /// [ErrorUrl]              /products/electronics/laptops.aspx
        /// [ErrorRawUrl]           <![CDATA[/products/electronics/laptops.aspx?ID=5 (on the browser)]]>
        /// [ErrorUrlAbsolutePath]  /productpage.aspx
        /// [ErrorUrlPathAndQuery]  <![CDATA[/productpage.aspx?category=electronics&subcategory=laptops&id=5]]>
        /// </pre>
        /// </remarks>
        protected void SetAdditionalHttpContextInfo(HttpContext context)
        {
            Ensure.IsNotNull(context, "WebError.SetAdditionalHttpContextInfo-context");

            var request = context.Request;
            this.Url = request.Url.ToString();
            this.UrlAbsolutePath = request.Url.AbsolutePath;
            this.UrlPathAndQuery = request.Url.PathAndQuery;
            this.UsersIpAddress = request.UserIPAddress();
            this.UrlQuery = request.Url.Query;
            this.HostsIpAddress = request.Params["LOCAL_ADDR"];
        }

        /// <summary>
        ///     Sets the additional exception info.
        /// </summary>
        /// <param name="exception">The exception.</param>
        protected void SetAdditionalExceptionInfo(Exception exception)
        {
            Ensure.IsNotNull(exception, "WebError.SetAdditionalExceptionInfo-exception");

            this.ErrorHelp = exception.HelpLink;

            var keys = exception.Data.Keys;
            var dictionary = exception.Data;

            foreach (var key in keys)
            {
                this.ErrorAdditionalData[string.Concat(key)] = string.Concat(dictionary[key]);
            }

            this.SetExceptionAddtionalInformation(exception);
        }
    }
}
