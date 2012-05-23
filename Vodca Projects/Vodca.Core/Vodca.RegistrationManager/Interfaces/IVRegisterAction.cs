//-----------------------------------------------------------------------------
// <copyright file="IVRegisterAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;

    /// <summary>
    /// The VRegistration Manager Plug in attribute
    /// </summary>
    public interface IVRegisterAction : IVRegister
    {
        /// <summary>
        /// Get specific attributes from assemblies and run equivalent action
        /// </summary>
        /// <param name="attributecollection">The attribute collection.</param>
        void Run(IEnumerable<VRegisterAttribute> attributecollection);
    }
}