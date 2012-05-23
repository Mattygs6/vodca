//-----------------------------------------------------------------------------
// <copyright file="Ensure.Folder.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/01/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics;
    using System.IO;
    using System.Web;
    using System.Web.Hosting;
    using Vodca.Annotations;

    /// <content>
    ///     Contains Design by Contract ensure/guarantee methods
    /// </content>
    public static partial class Ensure
    {
        /// <summary>
        ///     The specified file must exists on server  
        /// </summary>
        /// <param name="absolutepath">The path to validate</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Folder.cs" title="Ensure.Folder.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void FolderExists([NotNull]string absolutepath, string paramName, int statuscode = 404)
        {
            IsNotNullOrEmpty(absolutepath, "absolutepath");

            if (!Directory.Exists(absolutepath))
            {
                throw new HttpException(statuscode, string.Concat("The param ", paramName, "' is not valid! The Folder '", absolutepath, "' not found on the file system!"));
            }
        }

        /// <summary>
        ///    The specified file must exists on server  
        /// </summary>
        /// <param name="virtualpath">The virtual path (absolute or relative).</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Folder.cs" title="Ensure.Folder.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void FolderMapPathExists([NotNull]string virtualpath, string paramName, int statuscode = 404)
        {
            IsNotNullOrEmpty(virtualpath, "virtualpath");

            string path = HostingEnvironment.MapPath(virtualpath);

            // ReSharper disable AssignNullToNotNullAttribute
            if (!Directory.Exists(path))
            // ReSharper restore AssignNullToNotNullAttribute
            {
                throw new HttpException(statuscode, string.Concat("The param '", paramName, "' is not valid! The Folder '", path, "' not found on the file system!"));
            }
        }
    }
}
