//-----------------------------------------------------------------------------
// <copyright file="VLog.OnErrorrFilter.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Net;
    using System.Web;

    /// <summary>
    ///     HTTP module implementation that logs unhandled exceptions in an
    /// ASP.NET Web application to an error log.
    /// </summary>
    // ReSharper disable ClassNeverInstantiated.Global
    public sealed partial class VLog
    // ReSharper restore ClassNeverInstantiated.Global
    {
        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="httpapplication">The http application.</param>
        /// <param name="httpexception">The http exception.</param>
        private static void ApplicationOnErrorFilter(HttpApplication httpapplication, HttpException httpexception)
        {
            // Modify if need
            try
            {
                if (httpexception != null)
                {
                    switch ((HttpStatusCode)httpexception.GetHttpCode())
                    {
                        case HttpStatusCode.InternalServerError:
                            // Error caused by Search engine caching of ASP.NET assembly WebResource.axd file(s)
                            // 'WebResource.axd' Or 'ScriptResource.axd'
                            string url = httpapplication.Context.Request.Url.AbsolutePath;
                            if (url.EndsWith(".axd", StringComparison.OrdinalIgnoreCase))
                            {
                                httpapplication.Context.Server.ClearError();
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // CRITICAL: Error Log must not throw unhandled errors
                Logger.InfoException(ex.Message, ex);
            }
        }
    }
}
