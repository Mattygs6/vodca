//-----------------------------------------------------------------------------
// <copyright file="VRegisterRequiredAppSettingsAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
[assembly: Vodca.VRegistrationManagerAction(typeof(Vodca.VRegisterRequiredAppSettingsAction), MustRunOnApplicationStartup = true, Order = 100)]

namespace Vodca
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;

    /// <summary>
    /// The VRegistration Manager Action
    /// </summary>
    internal sealed partial class VRegisterRequiredAppSettingsAction : IVRegisterAction
    {
        /// <summary>
        /// Verify all required app settings in web.config
        /// </summary>
        /// <param name="attributecollection">The attribute collection.</param>
        public void Run(IEnumerable<VRegisterAttribute> attributecollection)
        {
            foreach (var attr in attributecollection.OfType<VRegisterRequiredAppSettingByNameAttribute>().OrderBy(x => x.Order))
            {
                if (string.IsNullOrWhiteSpace(WebConfigurationManager.AppSettings[attr.AppSettingName]))
                {
                    throw new HttpException(attr.ExceptionMessage);
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
            return "#VRegisterRequiredAppSettingsAction#".GetHashCode();
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