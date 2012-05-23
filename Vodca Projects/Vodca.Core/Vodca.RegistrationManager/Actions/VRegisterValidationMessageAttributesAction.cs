//-----------------------------------------------------------------------------
// <copyright file="VRegisterValidationMessageAttributesAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
[assembly: Vodca.VRegistrationManagerAction(typeof(Vodca.VRegisterValidationMessageAttributesAction), MustRunOnApplicationStartup = true, Order = 300)]

namespace Vodca
{
    using System.Collections.Generic;
    using System.Linq;
    using Vodca.VForms;

    /// <summary>
    /// The VRegistration Manager Action
    /// </summary>
    internal sealed partial class VRegisterValidationMessageAttributesAction : IVRegisterAction
    {
        /// <summary>
        /// Verify mandatory folders
        /// </summary>
        /// <param name="attributecollection">The attribute collection.</param>
        public void Run(IEnumerable<VRegisterAttribute> attributecollection)
        {
            IEnumerable<IValidateDisplayMessage> collection = attributecollection.OfType<VRegisterValidationMessageAttribute>().OrderBy(x => x.Order).ToArray();
            VFormMessageCacheManager.AddMessages(collection);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return "#VRegisterValidationMessageAttributesAction#".GetHashCode();
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