//-----------------------------------------------------------------------------
// <copyright file="GeoAddressOrLocation.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/13/2012
//-----------------------------------------------------------------------------
namespace Vodca.GoogleMapsApi
{
    using System;
    using System.Text;

    /// <summary>
    /// The Google Api Entity
    /// </summary>
    [Serializable]
    public partial class GeoAddressOrLocation : IAddressOrLocation
    {
        /// <summary>
        /// Gets or sets the street line.
        /// </summary>
        /// <value>
        /// The street line.
        /// </value>
        public string StreetLine { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the additional info.
        /// </summary>
        /// <value>
        /// The additional info.
        /// </value>
        public string OptionalLine { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether convert whitespaces to plus characters.
        /// </summary>
        /// <value>
        /// <c>true</c> if [convert whitespaces to plus characters]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>This for Google map links like <![CDATA[http://maps.google.com/maps?q=165+Dartmouth+Street+Boston,+MA+02116]]></remarks>
        public bool ConvertWhitespacesToPlusCharacters { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Address: {0}".ToFormat(this.GetFormattedAddress());
        }

        /// <summary>
        /// Gets a flag indicating whether object is valid or not
        /// </summary>
        /// <returns>
        /// True if valid otherwise false
        /// </returns>
        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(this.GetFormattedAddress());
        }

        /// <summary>
        /// Gets the formatted address.
        /// </summary>
        /// <returns>
        /// The formatted address string
        /// </returns>
        public string GetFormattedAddress()
        {
            var builder = new StringBuilder(64);
            if (!string.IsNullOrWhiteSpace(this.StreetLine))
            {
                builder.Append(this.StreetLine);
            }

            if (!string.IsNullOrWhiteSpace(this.City))
            {
                builder.Append(", ").Append(this.City).Append(',');
            }

            if (!string.IsNullOrWhiteSpace(this.State))
            {
                builder.Append(' ').Append(this.State);
            }

            if (!string.IsNullOrWhiteSpace(this.ZipCode))
            {
                builder.Append(' ').Append(this.ZipCode);
            }

            if (!string.IsNullOrWhiteSpace(this.OptionalLine))
            {
                builder.Append(", ").Append(this.OptionalLine);
            }

            var address = builder.ToString().Trim(',', ' ').TrimDuplicateEmptySpaces();

            if (this.ConvertWhitespacesToPlusCharacters)
            {
                address = address.Replace(" ", "+");
            }

            return address;
        }
    }
}