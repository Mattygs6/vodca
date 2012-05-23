//-----------------------------------------------------------------------------
// <copyright file="IApiGetActionController.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// The Web API action controller for GET action
    /// </summary>
    public interface IApiGetActionController : IVApiActionController
    {
        /// <summary>
        /// Gets the specified context.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// The response JSON
        /// </returns>
        string Get(VApiArgs arguments);
    }
}