//-----------------------------------------------------------------------------
// <copyright file="IGeoLocation.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/13/2012
//-----------------------------------------------------------------------------
namespace Vodca.GoogleMapsApi
{
    using Vodca.SDK.Newtonsoft.Json;

    /// <summary>
    /// The Geo location interface
    /// </summary>
    public interface IGeoLocation : IValidate
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [JsonProperty("lat")]
        double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty("lng")]
        double Longitude { get; set; }
    }
}