//-----------------------------------------------------------------------------
// <copyright file="IApiActionControllers.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// The complete Web API action controller (GET, POST, DELETE, PUT actions)
    /// </summary>
    public interface IApiActionControllers : IApiGetActionController, IApiPostActionController, IApiPutActionController, IApiDeleteActionController
    {
    }
}