//-----------------------------------------------------------------------------
// <copyright file="IVExecute.cs" company="genuine">
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
    public interface IVExecute
    {
        /// <summary>
        /// Executes this instance functionality.
        /// </summary>
        [UsedImplicitly]
        void Execute();
    }
}