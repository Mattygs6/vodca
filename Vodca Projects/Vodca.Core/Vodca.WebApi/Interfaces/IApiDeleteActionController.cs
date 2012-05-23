//-----------------------------------------------------------------------------
// <copyright file="IApiDeleteActionController.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// The Web API action controller for DELETE action
    /// </summary>
    public interface IApiDeleteActionController : IVApiActionController
    {
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// The response JSON
        /// </returns>
        string Delete(VApiArgs arguments);
    }
}