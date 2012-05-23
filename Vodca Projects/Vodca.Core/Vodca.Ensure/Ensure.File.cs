//-----------------------------------------------------------------------------
// <copyright file="Ensure.File.cs" company="genuine">
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
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.File.cs" title="Ensure.File.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void FileExists(string absolutepath, string paramName, int statuscode = 404)
        {
            if (string.IsNullOrEmpty(absolutepath) || !File.Exists(absolutepath))
            {
                throw new HttpException(statuscode, string.Concat("The File '", absolutepath, "' not found on the file system! Param : ", paramName));
            }
        }

        /// <summary>
        ///    The specified file must exists on server  
        /// </summary>
        /// <param name="virtualpath">The virtual path (absolute or relative).</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.File.cs" title="Ensure.File.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void FileMapPathExists(string virtualpath, string paramName, int statuscode = 404)
        {
            IsNotNullOrEmpty(virtualpath, "virtualpath");

            string path = HostingEnvironment.MapPath(virtualpath);
            if (!File.Exists(path))
            {
                throw new HttpException(statuscode, string.Concat("The File '", path, "' not found on the file system! Param: ", paramName));
            }
        }
    }
}
