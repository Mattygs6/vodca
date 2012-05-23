//-----------------------------------------------------------------------------
// <copyright file="VRegisterEmbeddedFileAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    /// <summary>
    /// The Register WebResource Attribute (copies to the file system)
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class VRegisterEmbeddedFileAttribute : VRegisterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterEmbeddedFileAttribute"/> class.
        /// </summary>
        /// <param name="webresourcepath">The web resource path.</param>
        /// <param name="virtualpath">The virtual path.</param>
        public VRegisterEmbeddedFileAttribute(string webresourcepath, string virtualpath)
        {
            Ensure.IsNotNullOrEmpty(webresourcepath, "webresourcepath");
            Ensure.IsNotNullOrEmpty(virtualpath, "virtualpath");

            this.WebResourcePath = webresourcepath;
            this.VirtualPath = virtualpath;
            this.MustRunOnApplicationStartup = true;
        }

        /// <summary>
        /// Gets the web resource path.
        /// </summary>
        /// <value>
        /// The web resource path.
        /// </value>
        public string WebResourcePath { get; private set; }

        /// <summary>
        /// Gets the virtual path.
        /// </summary>
        /// <value>
        /// The virtual path.
        /// </value>
        public string VirtualPath { get; private set; }
    }
}
