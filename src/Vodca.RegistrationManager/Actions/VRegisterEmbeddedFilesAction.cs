//-----------------------------------------------------------------------------
// <copyright file="VRegisterEmbeddedFilesAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
[assembly: Vodca.VRegistrationManagerAction(typeof(Vodca.VRegisterEmbeddedFilesAction), MustRunOnApplicationStartup = true, Order = 140)]

namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The Web Infrastructure manager
    /// </summary>
    internal sealed partial class VRegisterEmbeddedFilesAction : IVRegisterAction
    {
        /// <summary>
        /// Register the Physical folders and files from web resources.
        /// </summary>
        /// <param name="attributecollection">The attribute collection.</param>
        public void Run(IEnumerable<VRegisterAttribute> attributecollection)
        {
            foreach (var attr in attributecollection.OfType<VRegisterEmbeddedFileAttribute>().Where(x => x.RunOnApplicationStartup()).OrderBy(x => x.Order))
            {
                try
                {
                    var path = attr.VirtualPath.MapPath();
                    if (!File.Exists(path))
                    {
                        var file = attr.GetType().Assembly.GetFileBytesFromAssembly(attr.WebResourcePath);
                        File.WriteAllBytes(path, file);
                    }
                }
                catch (IOException iox)
                {
                    iox.LogException();
                    Debug.Fail(iox.Message);
                }
                catch (UnauthorizedAccessException uex)
                {
                    uex.LogException();
                    Debug.Fail(uex.Message);
                }
                catch (Exception exception)
                {
                    exception.LogException();
                    Debug.Fail(exception.Message);
                }
            }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return "#VRegisterEmbeddedBinaryFilesAction#".GetHashCode();
        }

        /// <summary>
        /// Runs the on application startup.
        /// </summary>
        /// <returns>
        /// The flag then to run on application start up (without httpContext) or on HttpModule Init method (with httpContext)
        /// </returns>
        public bool RunOnApplicationStartup()
        {
            return true;
        }
    }
}
