//-----------------------------------------------------------------------------
// <copyright file="GeoStatus.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/13/2012
//-----------------------------------------------------------------------------
namespace Vodca.GoogleMapsApi
{
    using System;

    /// <summary>
    /// The "status" field within the Geocoding response object contains the status of the request, and may contain debugging information to help you track down why Geocoding is not working. The "status" field may contain the following values:
    /// </summary>
    [Serializable]
    public enum GeoStatus
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The unknow flag
        /// </summary>
        None = 0,

        /// <summary>
        /// indicates that no errors occurred; the address was successfully parsed and at least one geocode was returned.
        /// </summary>
        Ok,

        /// <summary>
        /// indicates that the geocode was successful but returned no results. This may occur if the geocode was passed a non-existent address or a latlng in a remote location.
        /// </summary>
        ZERO_RESULTS,

        /// <summary>
        /// indicates that you are over your quota.
        /// </summary>
        OVER_QUERY_LIMIT,

        /// <summary>
        /// indicates that your request was denied, generally because of lack of a sensor parameter.
        /// </summary>
        REQUEST_DENIED,

        /// <summary>
        /// generally indicates that the query (address or latlng) is missing.
        /// </summary>
        INVALID_REQUEST

        // ReSharper restore InconsistentNaming
    }
}
