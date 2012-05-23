//-----------------------------------------------------------------------------
// <copyright file="VLogServerSideError.cs" company="genuine">
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
    ///     Represents a logical application error (as opposed to the actual 
    /// exception it may be representing).
    /// </summary>
    [Serializable]
    public sealed partial class VLogServerSideError : VLogError
    {
        /// <summary>
        ///     Initializes a new instance of the VLogServerSideError class.
        /// </summary>
        public VLogServerSideError()
        {
            this.ErrorType = VLogErrorTypes.ServerSideIIS;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VLogServerSideError"/> class.
        /// from a given <see cref="Exception"/> instance and 
        /// <see cref="HttpContext"/> instance representing the HTTP 
        /// context during the exception.
        /// </summary>
        /// <param name="exception">The occurred server side exception</param>
        public VLogServerSideError(Exception exception)
            : this()
        {
            HttpContext context = HttpContext.Current;
            if (exception != null && context != null)
            {
                Exception baseException = exception.GetBaseException();

                this.ErrorMessage = baseException.GetErrorMessage();
                this.ErrorDetails = exception.GetErrorErrorDetails();

                // Sets an object of a uniform resource identifier properties
                this.SetAdditionalHttpContextInfo(context);
                this.SetAdditionalExceptionInfo(exception);

                // If this is an HTTP exception, then get the status code
                // and detailed HTML message provided by the host.
                var httpException = exception as HttpException;
                if (httpException != null)
                {
                    this.ErrorCode = httpException.GetHttpCode();

                    VLogErrorCode errorcoderule;

                    if (VLog.WebErrorCodes.TryGetValue(this.ErrorCode, out errorcoderule) && !errorcoderule.ExcludeFromLogging)
                    {
                        this.AddHttpExceptionData(errorcoderule, context, httpException, errorcoderule);
                    }
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VLogServerSideError"/> class.
        /// from a given <see cref="HttpException"/> instance and 
        /// <see cref="HttpContext"/> instance representing the HTTP 
        /// context during the exception.
        /// </summary>
        /// <param name="httpexception">The occurred server side exception</param>
        public VLogServerSideError(HttpException httpexception)
            : this()
        {
            HttpContext context = HttpContext.Current;
            if (httpexception != null && context != null)
            {
                this.ErrorMessage = httpexception.GetErrorMessage();
                this.ErrorDetails = httpexception.GetErrorErrorDetails();

                // Sets an object of a uniform resource identifier properties
                this.SetAdditionalHttpContextInfo(context);
                this.SetAdditionalExceptionInfo(httpexception);

                // If this is an HTTP exception, then get the status code
                // and detailed HTML message provided by the host.
                this.ErrorCode = httpexception.GetHttpCode();

                VLogErrorCode errorcoderule;

                if (VLog.WebErrorCodes.TryGetValue(this.ErrorCode, out errorcoderule) && !errorcoderule.ExcludeFromLogging)
                {
                    this.AddHttpExceptionData(errorcoderule, context, httpexception, errorcoderule);
                }
            }
        }

        /// <summary>
        /// Add Exception data to the object fields
        /// </summary>
        /// <param name="errorcoderule">The error code rule.</param>
        /// <param name="context">An HttpContext object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        /// <param name="httpException">an HTTP exception thrown</param>
        /// <param name="rules">WebErrorCode rule from dictionary</param>
        private void AddHttpExceptionData(VLogErrorCode errorcoderule, HttpContext context, HttpException httpException, VLogErrorCode rules)
        {
            // Mandatory Fields
            this.HttpStatusCodeDescription = errorcoderule.Message;
            this.WebHostHtmlMessage = new VXmlCData(httpException.GetHtmlErrorMessage());
            this.ErrorPriority = errorcoderule.Priority;

            // If the HTTP context is available, then capture the
            // collections that represent the state request.
            HttpRequest request = context.Request;

            this.AddHeader(rules, request);

            this.AddServerVariables(rules, request);

            this.AddContextItems(rules, context);

            this.AddQueryStringVariables(rules, request);

            this.AddFormVariables(rules, request);

            this.AddCookies(rules, request);

            this.AddApplicationStateVariables(context, rules);

            this.AddSessionStateVariables(context, rules);
        }
    }
}
