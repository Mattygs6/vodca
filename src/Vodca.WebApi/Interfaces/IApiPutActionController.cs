//-----------------------------------------------------------------------------
// <copyright file="IApiPutActionController.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// The Web API action controller for PUT action
    /// </summary>
    public interface IApiPutActionController : IVApiActionController
    {
        /// <summary>
        /// Puts the specified context.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// The response JSON
        /// </returns>
        string Put(VApiArgs arguments);
    }
}