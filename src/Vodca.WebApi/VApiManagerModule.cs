//-----------------------------------------------------------------------------
// <copyright file="VApiManagerModule.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca.WebApi
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Text;
    using System.Web;

    /// <summary>
    /// The Web API Manager module
    /// </summary>
    public sealed partial class VApiManagerModule : IHttpModule
    {
        /// <summary>
        /// The Url Prefix
        /// </summary>
        public const string UrlPrefixTrigger = "/vapi/";

        /// <summary>
        /// The Url Prefix
        /// </summary>
        public static readonly string UrlPrefixPublicAccessTrigger = string.Concat(UrlPrefixTrigger, VApiAccessPermission.Public, '/');

        /// <summary>
        /// The Url Prefix
        /// </summary>
        public static readonly string UrlPrefixSecuredAccessTrigger = string.Concat(UrlPrefixTrigger, VApiAccessPermission.Secured, '/');

        /// <summary>
        /// The  controller collection
        /// </summary>
        private static readonly ConcurrentDictionary<string, VApiController> ControllerCollection = new ConcurrentDictionary<string, VApiController>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Initializes static members of the <see cref="VApiManagerModule"/> class. 
        /// </summary>
        static VApiManagerModule()
        {

            SetBeginRequestGetMethodtHttpResponseHeaders += (context, response) =>
                {
#if !DEBUG
                    if (string.Equals(context.Request.HttpMethod, "GET", StringComparison.InvariantCultureIgnoreCase))
                    {
                        response.SetHeaderCacheControl();
                        response.SetHeaderLastModified(DateTime.UtcNow.Date);
                    }

                    response.Headers["Vary"] = "Content-Encoding";
#endif
                };

            SetPreSendHttpResponseHeaders += (context, response) =>
                {
#if !DEBUG
                    response.Headers.Remove("X-AspNet-Version");

                    response.Headers.Remove("Server");

                    response.Headers.Remove("X-Powered-By");
#endif
                };

        }

        /// <summary>
        /// Gets or sets the execution error.
        /// </summary>
        /// <value>
        /// The execution error.
        /// </value>
        public static Action<HttpContext, Exception> ExecutionError { get; set; }

        /// <summary>
        /// Gets or sets the set pre send HTTP response headers.
        /// </summary>
        /// <value>
        /// The set HTTP response headers.
        /// </value>
        public static Action<HttpContext, HttpResponse> SetPreSendHttpResponseHeaders { get; set; }

        /// <summary>
        /// Gets or sets the set begin request HTTP response headers for GET METHOD.
        /// </summary>
        /// <value>
        /// The set begin request HTTP response headers.
        /// </value>
        /// <remarks>GET METHOD ONLY</remarks>
        public static Action<HttpContext, HttpResponse> SetBeginRequestGetMethodtHttpResponseHeaders { get; set; }

        /// <summary>
        /// Adds the controller.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="json">The constructor JSON.</param>
        public static void AddController(Type type, string json = null)
        {
            var instance = VApiController.New(type, json);

            if (instance != null)
            {
                ControllerCollection[instance.Key] = instance;
            }
        }

        /// <summary>
        /// Adds the controller.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public static void AddController(VRegisterApiControllerAttribute attribute)
        {
            if (attribute != null)
            {
                AddController(attribute.Type, attribute.Json);
            }
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.ContextOnBeginRequest;
            context.PostAuthorizeRequest += this.ContextOnMapRequestHandler;
#if !DEBUG
            context.PreSendRequestHeaders += this.ContextPreSendRequestHeaders;
#endif
        }

        /// <summary>
        /// Handles the execution error.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="exception">The exception.</param>
        private static void HandleExecutionError(HttpContext httpContext, Exception exception)
        {
#if DEBUG
            string message = VApiResponse.ErrorMessage(exception.ToString());
#else
            string message = VApiResponse.ErrorMessage(exception.Message);  
#endif
            httpContext.Response.Write(message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Sets the method not allowed status code.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="methodname">The method name.</param>
        private static void SetMethodNotAllowedStatusCode(HttpContext context, VApiHttpMethod methodname)
        {
            context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            string message = VApiResponse.ErrorMessage(string.Format("The Vodca Web API controller does not implement '{0}' method!", methodname));
            context.Response.Write(message);
        }

        /// <summary>
        /// Resolves the HTTP method.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The Web API Http Method</returns>
        private static VApiHttpMethod? TryResolveHttpMethod(HttpContext context)
        {
            string methodname = context.Request.HttpMethod;
            if (!string.IsNullOrWhiteSpace(methodname))
            {
                VApiHttpMethod method;
                if (!Enum.TryParse(methodname, /* ignoreCase */ true, out method))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    string message = VApiResponse.ErrorMessage(string.Format("The method '{0}' not supported by the Vodca Web API", methodname));
                    context.Response.Write(message);
                }

                return method;
            }

            return VApiHttpMethod.Get;
        }

        /// <summary>
        /// Runs the specified application request.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="url">The URL.</param>
        /// <param name="authenticated">if set to <c>true</c> authenticated.</param>
        private static void Run(HttpApplication application, Uri url, bool authenticated)
        {
            var context = application.Context;
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.BufferOutput = true;

            VApiController controller;
            if (ControllerCollection.TryGetValue(url.AbsolutePath, out controller))
            {
                /* Set header to something like 'application/json' */
                context.Response.SetHeaderContentType(controller.ActionController.FileContentType);

                try
                {
                    if (controller.ActionController.IsSecured == authenticated)
                    {
                        RunController(application, controller);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        string message = VApiResponse.ErrorMessage("The user is Unauthorized");
                        context.Response.Write(message);
                    }
                }
                catch (Exception exception)
                {
                    /* VLog. Keep a record */
                    exception.LogException();

                    if (ExecutionError == null)
                    {
                        ExecutionError += HandleExecutionError;
                    }

                    ExecutionError(context, exception);
                }
                finally
                {
                    context.EndRequest();
                }
            }
        }

        /// <summary>
        /// Runs the controller.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="controller">The WebAPI controller.</param>
        private static void RunController(HttpApplication application, VApiController controller)
        {
            var context = application.Context;
            var method = TryResolveHttpMethod(context);
            if (method.HasValue)
            {
                var args = new VApiArgs(context);

                switch (method.Value)
                {
                    case VApiHttpMethod.Get:
                        var getcontroller = controller.ActionController as IApiGetActionController;
                        if (getcontroller != null)
                        {
                            /* Set Common headers */
                            SetBeginRequestGetMethodtHttpResponseHeaders(context, context.Response);

                            string json = getcontroller.Get(args);

#if DEBUG
                            /* Disable the compression for JSON Debug Viewers  */
                            ContextOnPreSendRequestContent(application, false, json);
#else
                            ContextOnPreSendRequestContent(application, !getcontroller.IsCompressionDisabled, json);
#endif
                        }
                        else
                        {
                            SetMethodNotAllowedStatusCode(context, method.Value);
                        }

                        break;

                    case VApiHttpMethod.Post:
                        var postcontroller = controller.ActionController as IApiPostActionController;
                        if (postcontroller != null)
                        {
                            postcontroller.Post(args);
                        }
                        else
                        {
                            SetMethodNotAllowedStatusCode(context, method.Value);
                        }

                        break;

                    case VApiHttpMethod.Put:
                        var putcontroller = controller.ActionController as IApiPutActionController;
                        if (putcontroller != null)
                        {
                            putcontroller.Put(args);
                        }
                        else
                        {
                            SetMethodNotAllowedStatusCode(context, method.Value);
                        }

                        break;

                    case VApiHttpMethod.Delete:
                        var deletecontroller = controller.ActionController as IApiDeleteActionController;
                        if (deletecontroller != null)
                        {
                            deletecontroller.Delete(args);
                        }
                        else
                        {
                            SetMethodNotAllowedStatusCode(context, method.Value);
                        }

                        break;
                    default:
                        SetMethodNotAllowedStatusCode(context, method.Value);
                        // ReSharper disable RedundantJumpStatement
                        break;
                    // ReSharper restore RedundantJumpStatement
                }
            }
        }

        /// <summary>
        /// Contexts the content of the on pre send request.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="iscompressionenabled">if set to <c>true</c> is compression enabled.</param>
        /// <param name="json">The JSON.</param>
        private static void ContextOnPreSendRequestContent(HttpApplication application, bool iscompressionenabled, string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                var request = application.Request;
                var response = application.Response;

                string encodings = request.Headers["Accept-Encoding"];

                if (iscompressionenabled /* Disable compression during developing to allow use JSON View tools */
                    && !string.IsNullOrWhiteSpace(encodings) /* Check for Fiddler */
                    && response.StatusCode == 200 /* Only Ok status */
                    && json.Length > 5000 /* Skip small files 3kB*/)
                {
                    var bytes = Encoding.UTF8.GetBytes(json);
                    using (var outstream = new MemoryStream(bytes.Length))
                    {
                        if (encodings.Contains("gzip", StringComparison.InvariantCultureIgnoreCase))
                        {
                            using (var gzip = new GZipStream(outstream, CompressionMode.Compress))
                            {
                                gzip.Write(bytes, 0, bytes.Length);
                                response.BinaryWrite(outstream.ToArray());
                                response.AppendHeader("Content-Encoding", "gzip");
                            }
                        }
                        else if (encodings.Contains("deflate", StringComparison.InvariantCultureIgnoreCase))
                        {
                            using (var deflate = new DeflateStream(outstream, CompressionMode.Compress))
                            {
                                deflate.Write(bytes, 0, bytes.Length);
                                response.BinaryWrite(outstream.ToArray());
                                response.AppendHeader("Content-Encoding", "deflate");
                            }
                        }
                        else
                        {
                            response.Write(json);
                        }
                    }
                }
                else
                {
                    response.Write(json);
                }
            }
        }

        /// <summary>
        /// Contexts the on begin request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContextOnBeginRequest(object sender, EventArgs eventArgs)
        {
            var application = (HttpApplication)sender;
            var url = application.Request.Url;
            if (url.AbsolutePath.StartsWith(UrlPrefixPublicAccessTrigger, StringComparison.InvariantCultureIgnoreCase))
            {
                Run(application, url, authenticated: false);
            }
        }

        /// <summary>
        /// Contexts the on map request handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContextOnMapRequestHandler(object sender, EventArgs eventArgs)
        {
            var application = (HttpApplication)sender;
            var url = application.Request.Url;
            if (url.AbsolutePath.StartsWith(UrlPrefixSecuredAccessTrigger, StringComparison.InvariantCultureIgnoreCase))
            {
                Run(application, url, authenticated: true);
            }
        }

        // ReSharper disable UnusedParameter.Local

        /// <summary>
        /// Contexts the pre send request headers.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContextPreSendRequestHeaders(object sender, EventArgs e)
        // ReSharper restore UnusedParameter.Local
        {
            var application = (HttpApplication)sender;

            /* Add custom headers logics */
            SetPreSendHttpResponseHeaders(application.Context, application.Context.Response);
        }
    }
}