//-----------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/24/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;
    using Vodca.GoogleMapsApi;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the Google geo location (latitude and longitude).
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>the Google geo location (latitude and longitude)</returns>
        public static GeoLocation? GetGoogleGeoLocation(this IAddressOrLocation address)
        {
            if (address != null)
            {
                var service = new GoogleMapsWebService(address);
                return service.GetLatitudeAndLongitude();
            }

            return null;
        }
    }
}