//-----------------------------------------------------------------------------
// <copyright file="VExtensionlessWebFormsModule.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/06/2012
//-----------------------------------------------------------------------------
namespace Vodca.HttpModules
{
    using System;
    using System.IO;
    using System.Web;

    /// <summary>
    /// The Simple extension less ASP.NET web forms
    /// </summary>
    public sealed class VExtensionlessWebFormsModule : IHttpModule
    {
        /// <summary>
        /// The flag
        /// </summary>
        private static readonly bool IsIntegratedPipeline = HttpRuntime.UsingIntegratedPipeline;

        #region IHttpModule Members

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
            context.BeginRequest += this.ContextBeginRequest;
        }

        /// <summary>
        /// Contexts the begin request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContextBeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var request = application.Context.Request;
            var extension = Path.GetExtension(request.RawUrl);
            if (string.IsNullOrEmpty(extension))
            {
                if (IsIntegratedPipeline)
                {
                    /* IIS7 */
                    application.Server.Transfer(request.RawUrl + ".aspx", preserveForm: true);
                }
                else
                {
                    /* Development Cassini server*/
                    application.Response.Redirect(request.RawUrl + ".aspx");
                    application.CompleteRequest();
                }
            }
        }

        #endregion
    }
}
