//-----------------------------------------------------------------------------
// <copyright file="IAddressOrLocation.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/13/2012
//-----------------------------------------------------------------------------
namespace Vodca.GoogleMapsApi
{
    /// <summary>
    /// The mandatory property for Google Map request
    /// </summary>
    public interface IAddressOrLocation : IValidate
    {
        /// <summary>
        /// Gets the formatted address.
        /// </summary>
        /// <returns>The formatted address string</returns>
        string GetFormattedAddress();
    }
}