//-----------------------------------------------------------------------------
// <copyright file="IValidationError.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       08/09/2010
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    /// <summary>
    /// The Validation error mandatory properties
    /// </summary>
    public interface IValidationError : IToXElement
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        string Property { get; set; }

        /// <summary>
        /// Gets or sets the  error message.
        /// </summary>
        /// <value>The error message.</value>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the JavaScript ID (Optional).
        /// </summary>
        /// <value>The JavaScript ID.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Js", Justification = "The Js stands for JavaScript")]
        string JsId { get; set; }

        /// <summary>
        /// Gets or sets the ordinal for sorting.
        /// </summary>
        /// <value>The ordinal.</value>
        byte Ordinal { get; set; }
    }
}