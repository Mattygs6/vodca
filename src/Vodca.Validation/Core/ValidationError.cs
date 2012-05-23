//-----------------------------------------------------------------------------
// <copyright file="ValidationError.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       08/09/2010
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    /// JavaScript JSON message
    /// </summary>
    [Serializable]
    public partial class ValidationError : IValidationError
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the  error message.
        /// </summary>
        /// <value>The error message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the JavaScript ID (Optional).
        /// </summary>
        /// <value>The JavaScript ID.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Js", Justification = "The Js stands for JavaScript")]
        public string JsId { get; set; }

        /// <summary>
        /// Gets or sets the ordinal for sorting.
        /// </summary>
        /// <value>The ordinal.</value>
        public byte Ordinal { get; set; }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement(string rootname = "ValidationError")
        {
            return new XElement(
                        rootname,
                        new XAttribute("Property", this.Property),
                        new XAttribute("Message", this.Message),
                        new XAttribute("JsID", this.JsId),
                        new XAttribute("Ordinal", this.Ordinal));
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ToXElement().ToString();
        }
    }
}
