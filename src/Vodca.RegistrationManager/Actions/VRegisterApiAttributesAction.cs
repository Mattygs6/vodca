//-----------------------------------------------------------------------------
// <copyright file="VRegisterApiAttributesAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
[assembly: Vodca.VRegistrationManagerAction(typeof(Vodca.VRegisterApiAttributesAction), MustRunOnApplicationStartup = false)]

namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Vodca.WebApi;

    /// <summary>
    /// The VRegister VLogError Codes Action
    /// </summary>
    internal sealed partial class VRegisterApiAttributesAction : IVRegisterAction
    {
        /// <summary>
        /// Get specific attributes from assemblies and run equivalent action
        /// </summary>
        /// <param name="attributecollection">The attribute collection.</param>
        public void Run(IEnumerable<VRegisterAttribute> attributecollection)
        {
            foreach (var attr in attributecollection.OfType<VRegisterApiControllerAttribute>().OrderBy(x => x.Order))
            {
                try
                {
                    VApiManagerModule.AddController(attr);
                }
                catch (Exception exception)
                {
                    exception.LogException();
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
            return "#VRegisterApiAttributesAction#".GetHashCode();
        }

        /// <summary>
        /// Runs the on application startup.
        /// </summary>
        /// <returns>
        /// The flag then to run on application start up (without httpContext) or on HttpModule Init method (with httpContext)
        /// </returns>
        public bool RunOnApplicationStartup()
        {
            return false;
        }
    }
}