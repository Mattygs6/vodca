//-----------------------------------------------------------------------------
// <copyright file="VRegisterRequiredWebFolderAttribute.cs" company="genuine">
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
    /// The Register required web folder attribute (required to run web app)
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class VRegisterRequiredWebFolderAttribute : VRegisterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterRequiredWebFolderAttribute"/> class.
        /// </summary>
        /// <param name="foldervirtualpath">The virtual path.</param>
        public VRegisterRequiredWebFolderAttribute(string foldervirtualpath)
        {
            Ensure.IsNotNullOrEmpty(foldervirtualpath, "foldervirtualpath");

            this.FolderVirtualPath = foldervirtualpath;
            this.MustRunOnApplicationStartup = true;
        }

        /// <summary>
        /// Gets the folder virtual path.
        /// </summary>
        public string FolderVirtualPath { get; private set; }
    }
}
