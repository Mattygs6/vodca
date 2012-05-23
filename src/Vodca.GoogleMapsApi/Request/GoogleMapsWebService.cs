//-----------------------------------------------------------------------------
// <copyright file="GoogleMapsWebService.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/13/2012
//-----------------------------------------------------------------------------
namespace Vodca.GoogleMapsApi
{
    using System;
    using System.Net;
    using System.Web;
    using Vodca.SDK.Newtonsoft.Json.Linq;

    /// <summary>
    ///  The Google Map Api request and reponse handler
    /// </summary>
    /// <example>
    /// <code>
    ///            var addressline = new GeoAddressOrLocation { State = "MA", City = "Norton", StreetLine = "Village way", ZipCode = "02766" };
    ///
    ///            var webservice = new GoogleMapsWebService(addressline);
    ///
    ///            var point1 = webservice.GetLatitudeAndLongitude();
    ///            Assert.IsNotNull(point1);
    ///
    ///            addressline = new GeoAddressOrLocation { StreetLine = "500 Harrison Avenue", City = "Boston", State = "MA" };
    ///            webservice = new GoogleMapsWebService(addressline);
    ///
    ///            var point2 = webservice.GetLatitudeAndLongitude();
    ///            Assert.IsNotNull(point2);
    ///
    ///            var distance = point1.GetDistance(point2);
    ///            /* The distance must be a little bit more the 27.7  according google earth miles */
    ///            Assert.IsNotNull(distance);
    /// </code>
    /// </example>
    [Serializable]
    public sealed partial class GoogleMapsWebService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleMapsWebService"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        public GoogleMapsWebService(IAddressOrLocation address)
        {
            Ensure.IsNotNull(address, "address");
            this.AddressOrLocation = address;

            Uri uri;
            if (!Uri.TryCreate(this.GetFormatedGoogleMapApiUrl(), UriKind.Absolute, out uri))
            {
                throw new HttpException("Couldn't parse the Uri!");
            }

            this.RequestUri = uri;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GoogleMapsWebService"/> is sensor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sensor; otherwise, <c>false</c>.
        /// </value>
        public bool Sensor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use SSL].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use SSL]; otherwise, <c>false</c>.
        /// </value>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        public IAddressOrLocation AddressOrLocation { get; private set; }

        /// <summary>
        /// Gets the request URI.
        /// </summary>
        public Uri RequestUri { get; private set; }

        /// <summary>
        /// Gets or sets the Google response JSON.
        /// </summary>
        /// <value>
        /// The Google response JSON.
        /// </value>
        private string GoogleResponseJson { get; set; }

        /// <summary>
        /// Gets the geo location JSON.
        /// </summary>
        /// <returns>The Google map API JSON response</returns>
        public string GetGeoLocationJson()
        {
            if (this.AddressOrLocation.Validate())
            {
                try
                {
                    using (var client = new WebClient())
                    {
#if!DEBUG
                        client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.CacheIfAvailable);
#endif
                        this.GoogleResponseJson = client.DownloadString(this.RequestUri);
                    }
                }
                catch (WebException webException)
                {
                    webException.LogException();
                }
                catch (Exception exception)
                {
                    exception.LogException();
                }
            }

            return this.GoogleResponseJson;
        }

        /// <summary>
        /// Gets the formatted Address.
        /// </summary>
        /// <returns>The formatted text from Google web service</returns>
        public string GetFormattedAddress()
        {
            if (!string.IsNullOrWhiteSpace(this.GoogleResponseJson))
            {
                try
                {
                    var googletokens = JObject.Parse(this.GoogleResponseJson);

                    string status = googletokens["status"].ToString();
                    GeoStatus geoStatus;

                    if (Enum.TryParse(value: status, ignoreCase: true, result: out geoStatus) && geoStatus == GeoStatus.Ok)
                    {
                        return googletokens["results"][0]["formatted_address"].ToString();
                    }
                }
                catch (Exception exception)
                {
                    /*
                     * dynamic entity = json.DeserializeFromJson();
                     * var latitude = (double?)entity.results[0].geometry.location.lat;
                     * var longitude = (double?)entity.results[0].geometry.location.lng;
                     * Originally I used MS JSON decode,but sometimes weird error. Changed to JSON.NET
                     * Using System.Web.Helpers.Json.Decode() causes unexpected exceptions when running in debugger only
                     * http://connect.microsoft.com/VisualStudio/feedback/details/684324/using-system-web-helpers-json-decode-causes-unexpected-exceptions-when-running-in-debugger-only
                     */
                    exception.LogException();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the latitude and longitude.
        /// </summary>
        /// <returns>The Address Geo location</returns>
        public GeoLocation? GetLatitudeAndLongitude()
        {
            var json = this.GetGeoLocationJson();
            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    var googletokens = JObject.Parse(json);

                    string status = googletokens["status"].ToString();
                    GeoStatus geoStatus;

                    if (Enum.TryParse(value: status, ignoreCase: true, result: out geoStatus) && geoStatus == GeoStatus.Ok)
                    {
                        var location = googletokens["results"][0]["geometry"]["location"];
                        var latitude = location["lat"].ToString().ConvertToDouble();
                        var longitude = location["lng"].ToString().ConvertToDouble();

                        if (latitude.HasValue && longitude.HasValue)
                        {
                            return new GeoLocation { Latitude = latitude.Value, Longitude = longitude.Value };
                        }
                    }
                }
                catch (Exception exception)
                {
                    /*
                     * dynamic entity = json.DeserializeFromJson();
                     * var latitude = (double?)entity.results[0].geometry.location.lat;
                     * var longitude = (double?)entity.results[0].geometry.location.lng;
                     * Originally I used MS JSON decode,but sometimes weird error. Changed to JSON.NET
                     * Using System.Web.Helpers.Json.Decode() causes unexpected exceptions when running in debugger only
                     * http://connect.microsoft.com/VisualStudio/feedback/details/684324/using-system-web-helpers-json-decode-causes-unexpected-exceptions-when-running-in-debugger-only
                     */
                    exception.LogException();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the formatted Google map API URL.
        /// </summary>
        /// <returns>The formatted</returns>
        private string GetFormatedGoogleMapApiUrl()
        {
            return string.Format(
                "{0}://maps.google.com/maps/api/geocode/json?address={1}&sensor={2}&date={3}",
                this.UseSsl ? "https" : "http",
                HttpUtility.UrlEncode(this.AddressOrLocation.GetFormattedAddress()),
                this.Sensor ? "true" : "false",
                /* For caching purposes */
                DateTime.UtcNow.ToString("dd-MM-yyyy"));
        }
    }
}
