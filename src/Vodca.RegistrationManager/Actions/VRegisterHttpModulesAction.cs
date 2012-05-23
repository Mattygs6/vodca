//-----------------------------------------------------------------------------
// <copyright file="VRegisterHttpModulesAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
[assembly: Vodca.VRegistrationManagerAction(typeof(Vodca.VRegisterHttpModulesAction), MustRunOnApplicationStartup = true, Order = 200)]

namespace Vodca
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The Web Infrastructure manager
    /// </summary>
    internal sealed partial class VRegisterHttpModulesAction : IVRegisterAction
    {
        /// <summary>
        /// Register the HTTP modules.
        /// </summary>
        /// <param name="attributecollection">The attribute collection.</param>
        public void Run(IEnumerable<VRegisterAttribute> attributecollection)
        {
            /* Let's register all  HttpModules */
            foreach (VRegisterHttpModuleAttribute registerHttpModuleAttribute in attributecollection.OfType<VRegisterHttpModuleAttribute>().OrderBy(x => x.Order))
            {
                /*  Assembly Microsoft.Web.Infrastructure.dll */
                Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(registerHttpModuleAttribute.ActionType);
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
            return "#VRegisterHttpModulesAction#".GetHashCode();
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
