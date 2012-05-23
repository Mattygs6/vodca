//-----------------------------------------------------------------------------
// <copyright file="VRegisterRequiredConnectionStringAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
[assembly: Vodca.VRegistrationManagerAction(typeof(Vodca.VRegisterRequiredConnectionStringAction), MustRunOnApplicationStartup = true, Order = 110)]

namespace Vodca
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;

    /// <summary>
    /// The VRegistration Manager Action
    /// </summary>
    internal sealed partial class VRegisterRequiredConnectionStringAction : IVRegisterAction
    {
        /// <summary>
        /// Verify all required app settings in web.config
        /// </summary>
        /// <param name="attributecollection">The attribute collection.</param>
        public void Run(IEnumerable<VRegisterAttribute> attributecollection)
        {
            foreach (var attr in attributecollection.OfType<VRegisterRequiredConnectionStringByNameAttribute>().OrderBy(x => x.Order))
            {
                var connection = WebConfigurationManager.ConnectionStrings[attr.ConnectionStringName];
                if (connection == null || string.IsNullOrWhiteSpace(connection.ConnectionString))
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
            return "#VRegisterRequiredConnectionStringAction#".GetHashCode();
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