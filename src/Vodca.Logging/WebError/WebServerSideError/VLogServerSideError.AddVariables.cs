//-----------------------------------------------------------------------------
// <copyright file="VLogServerSideError.AddVariables.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.SessionState;

    /// <summary>
    ///     Represents a logical application error (as opposed to the actual 
    /// exception it may be representing).
    /// </summary>
    public sealed partial class VLogServerSideError
    {
        /// <summary>
        /// Adds the request header.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="request">The http request.</param>
        private void AddHeader(VLogErrorCode rules, HttpRequest request)
        {
            if (!rules.ExcludeHeader)
            {
                this.HeaderCollection = request.Headers.ToDictionary();

                /* Remove these Parameters and use Cookie Collection to get required data */
                this.HeaderCollection.Remove("Connection", "Cookie");
            }
        }

        /// <summary>
        /// Adds the context items.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="context">The context.</param>
        private void AddContextItems(VLogErrorCode rules, HttpContext context)
        {
            if (!rules.ExcludeContextItems && context.Items.Count > 0)
            {
                this.ContextItems = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                var keys = context.Items.Keys;
                foreach (var key in keys)
                {
                    this.ContextItems[string.Concat(key)] = string.Concat(context.Items[key]);
                }
            }
        }

        /// <summary>
        /// Adds the session state variables.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="rules">The web error rules.</param>
        private void AddSessionStateVariables(HttpContext context, VLogErrorCode rules)
        {
            HttpSessionState session = context.Session;
            if (!rules.ExcludeSessionStateVariables && session != null)
            {
                this.SessionState = session.ToDictionary();

                this.SessionState["SessionID"] = session.SessionID;
                this.SessionState["SessionIsNewSession"] = string.Concat(session.IsNewSession);
                this.SessionState["SessionIsCookieless"] = string.Concat(session.IsCookieless);
                this.SessionState["SessionIsReadOnly"] = string.Concat(session.IsReadOnly);
                this.SessionState["SessionStateMode"] = string.Concat(session.Mode);
                this.SessionState["SessionTimeout"] = string.Concat(session.Timeout);
            }
        }

        /// <summary>
        /// Adds the application state variables.
        /// </summary>
        /// <param name="context">The current context.</param>
        /// <param name="rules">The web error rules.</param>
        private void AddApplicationStateVariables(HttpContext context, VLogErrorCode rules)
        {
            if (!rules.ExcludeApplicationStateVariables)
            {
                this.ApplicationState = context.Application.ToDictionary();
            }
        }

        /// <summary>
        /// Adds the cookies.
        /// </summary>
        /// <param name="rules">The web error rules.</param>
        /// <param name="request">The http request.</param>
        private void AddCookies(VLogErrorCode rules, HttpRequest request)
        {
            if (!rules.ExcludeCookies)
            {
                this.CookiesCollection = request.Cookies.ToDictionary();
            }
        }

        /// <summary>
        /// Adds the form variables.
        /// </summary>
        /// <param name="rules">The web error rules.</param>
        /// <param name="request">The http request.</param>
        private void AddFormVariables(VLogErrorCode rules, HttpRequest request)
        {
            if (!rules.ExcludeFormVariables)
            {
                this.FormCollection = request.Form.ToDictionary();
            }
        }

        /// <summary>
        /// Adds the query string variables.
        /// </summary>
        /// <param name="rules">The web error rules.</param>
        /// <param name="request">The http request.</param>
        private void AddQueryStringVariables(VLogErrorCode rules, HttpRequest request)
        {
            if (!rules.ExcludeQueryStringVariables)
            {
                this.QueryStringCollection = request.QueryString.ToDictionary();
            }
        }

        /// <summary>
        /// Adds the server variables.
        /// </summary>
        /// <param name="rules">The web error rules.</param>
        /// <param name="request">The http request.</param>
        private void AddServerVariables(VLogErrorCode rules, HttpRequest request)
        {
            if (!rules.ExcludeServerVariables)
            {
                this.ServerVariableCollection = request.ServerVariables.ToDictionary();
                this.ServerVariableCollection.Trim();

                /* Remove these Parameters and use Header Collection to get required data */
                this.ServerVariableCollection.Remove("ALL_HTTP", "ALL_RAW", "HTTP_ACCEPT", "HTTP_ACCEPT_CHARSET", "HTTP_ACCEPT_ENCODING", "HTTP_ACCEPT_LANGUAGE", "HTTP_CONNECTION", "HTTP_COOKIE", "HTTP_USER_AGENT");

                if (this.ErrorStackData != null)
                {
                    this.ErrorStackData["RequestHttpMethod"] = request.HttpMethod;
                    this.ErrorStackData["RequestIsSecureConnection"] = string.Concat(request.IsSecureConnection);
                    this.ErrorStackData["RequestIsAuthenticated"] = string.Concat(request.IsAuthenticated);
                    this.ErrorStackData["RequestUrlReferrer"] = string.Concat(request.UrlReferrer);
                    this.ErrorStackData["RequestUrl"] = string.Concat(request.Url);
                    this.ErrorStackData["RequestUserHostName"] = request.UserHostName;

                    if (request.LogonUserIdentity != null)
                    {
                        this.ErrorStackData["RequestLogonUserIdentityName"] = request.LogonUserIdentity.Name;
                        this.ErrorStackData["RequestLogonUserIdentityUser"] = string.Concat(request.LogonUserIdentity.User);
                        this.ErrorStackData["RequestLogonUserIdentityAuthenticationType"] = request.LogonUserIdentity.AuthenticationType;
                    }
                }
            }
        }
    }
}
