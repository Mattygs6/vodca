//-----------------------------------------------------------------------------
// <copyright file="IVRegister.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using Vodca.Annotations;

    /// <summary>
    /// The Vodca registration
    /// </summary>
    public interface IVRegister
    {
        /// <summary>
        /// Runs the on application startup.
        /// </summary>
        /// <returns>The flag then to run on application start up (without httpContext) or on HttpModule Init method (with httpContext)</returns>
        [UsedImplicitly]
        bool RunOnApplicationStartup();
    }
}