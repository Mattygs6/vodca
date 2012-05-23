//-----------------------------------------------------------------------------
// <copyright file="GeoLocation.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/13/2012
//-----------------------------------------------------------------------------
namespace Vodca.GoogleMapsApi
{
    using System;
    using Vodca.SDK.Newtonsoft.Json;

    /// <summary>
    /// The Map location entity
    /// </summary>
    [Serializable]
    public partial struct GeoLocation : IGeoLocation
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty("lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// Distances the specified latitude first.
        /// </summary>
        /// <param name="lattitudefirst">The latitude first.</param>
        /// <param name="longitudefirst">The longitude first.</param>
        /// <param name="latitudesecond">The latitude second.</param>
        /// <param name="longitudesecond">The longitude second.</param>
        /// <param name="distanceinmiles">if set to <c>true</c> [distance in miles].</param>
        /// <returns>The distance between points</returns>
        /// <remarks>The modified version of the Code project article</remarks>
        /// <seealso href="http://www.codeproject.com/Articles/12269/Distance-between-locations-using-latitude-and-long"/>
        public static double Distance(double lattitudefirst, double longitudefirst, double latitudesecond, double longitudesecond, bool distanceinmiles = true)
        {
            /*
                The Haversine formula according to Dr. Math.
                http://mathforum.org/library/drmath/view/51879.html
                
                dlon = lon2 - lon1
                dlat = lat2 - lat1
                a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
                c = 2 * atan2(sqrt(a), sqrt(1-a)) 
                d = R * c
                
                Where
                    * dlon is the change in longitude
                    * dlat is the change in latitude
                    * c is the great circle distance in Radians.
                    * R is the radius of a spherical Earth.
                    * The locations of the two points in 
                        spherical coordinates (longitude and 
                        latitude) are lon1,lat1 and lon2, lat2.
            */

            double lat1InRad = lattitudefirst * (Math.PI / 180.0);
            double long1InRad = longitudefirst * (Math.PI / 180.0);
            double lat2InRad = latitudesecond * (Math.PI / 180.0);
            double long2InRad = longitudesecond * (Math.PI / 180.0);

            double longitude = long2InRad - long1InRad;

            double latitude = lat2InRad - lat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(latitude / 2.0), 2.0) + (Math.Cos(lat1InRad) * Math.Cos(lat2InRad) * Math.Pow(Math.Sin(longitude / 2.0), 2.0));

            // Intermediate result c (great circle distance in Radians).
            double circledistance = 2.0 * Math.Asin(Math.Sqrt(a));

            // Distance.
            const double EarthRadiusKms = 6376.5;
            const double EarthRadiusMiles = 3956.0;

            if (distanceinmiles)
            {
                return EarthRadiusMiles * circledistance;
            }

            return EarthRadiusKms * circledistance;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0},{1}", this.Latitude, this.Longitude);
        }

        /// <summary>
        /// Gets a flag indicating whether object is valid or not
        /// </summary>
        /// <returns>
        /// True if valid otherwise false
        /// </returns>
        public bool Validate()
        {
            var lat = Math.Abs(this.Latitude);
            var lng = Math.Abs(this.Longitude);

            return lat < 180 && lat > 0 && lng < 180 && lng > 0;
        }

        /// <summary>
        /// Distances the specified second location.
        /// </summary>
        /// <param name="secondlocation">The second location.</param>
        /// <returns>The distance between two points </returns>
        /// <remarks>Intended for generic/loose short distance searches</remarks>
        public double? GetDistance(GeoLocation secondlocation)
        {
            if (this.Validate() && secondlocation.Validate())
            {
                return Distance(this.Latitude, this.Longitude, secondlocation.Latitude, secondlocation.Longitude);
            }

            return null;
        }
    }
}
