//-----------------------------------------------------------------------------
// <copyright file="VApiArgs.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Specialized;
    using System.Web;

    using Vodca.WebApi;

    /// <summary>
    /// The Web API arguments
    /// </summary>
    public sealed partial class VApiArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VApiArgs"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public VApiArgs(HttpContext context)
        {
            Ensure.IsNotNull(context.Request, "context");
            this.Context = context;
            this.RequestBody = context.Request.InputStream.ConvertToString();

            this.Form = this.RequestBody.ParseQueryString();
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        public HttpContext Context { get; private set; }

        /// <summary>
        /// Gets the query string.
        /// </summary>
        public NameValueCollection QueryString
        {
            get
            {
                return this.Context.Request.QueryString;
            }
        }

        /// <summary>
        /// Gets the request body.
        /// </summary>
        public string RequestBody { get; private set; }

        /// <summary>
        /// Gets the form.
        /// </summary>
        public NameValueCollection Form { get; private set; }

        /// <summary>
        /// Gets the response.
        /// </summary>
        public HttpResponse Response
        {
            get
            {
                return this.Context.Response;
            }
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        public HttpRequest Request
        {
            get
            {
                return this.Context.Request;
            }
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        /// <remarks>This method intended to resolve common content types and not every possibility</remarks>
        public VApiContentType RequestContentType
        {
            get
            {
                var type = VApiContentType.Html;

                var contenytype = this.Context.Request.Headers["Content-Type"];
                if (!string.IsNullOrWhiteSpace(contenytype))
                {
                    switch (contenytype.ToLowerInvariant())
                    {
                        case FileContentTypes.Html:
                            type = VApiContentType.Html;
                            break;

                        case FileContentTypes.Json:
                            type = VApiContentType.Json;
                            break;

                        case FileContentTypes.Xml:
                            type = VApiContentType.Xml;
                            break;

                        default:
                            type = VApiContentType.Custom;
                            break;
                    }
                }

                return type;
            }
        }
    }
}
