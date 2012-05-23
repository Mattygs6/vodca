//-----------------------------------------------------------------------------
// <copyright file="JsException.LoggingWebservice.cs" company="GenuineInteractive">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Web.Script.Services;
    using System.Web.Services;

    /// <summary>
    ///     Client side error  logging utility using JQuery/Ajax
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public partial class JQueryLogging : WebService
    {
        /// <summary>
        ///     LogError error occurred on the client side
        /// </summary>
        /// <param name="exception">Client side exception command of WebClientError. Its mimics server side exception.</param>
        [WebMethod]
        public void LogException(string exception)
        {
            if (exception.IsNotNullOrEmpty())
            {
                try
                {
                    var jsproxy = exception.DeserializeFromJson<JsExceptionProxy>();
                    if (jsproxy.IsValid())
                    {
                        var jsexception = new JsException(jsproxy);
                        VLog.LogException(jsexception);
                    }
                }
                catch (Exception ex)
                {
                    VLog.Logger.InfoException(ex.Message, ex);
                }
            }
        }
    }
}