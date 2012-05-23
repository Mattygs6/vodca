//-----------------------------------------------------------------------------
// <copyright file="IJsException.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2010
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    /// <summary>
    ///     The common properties between JsException and  JsExceptionProxy (JSON de-serialization utility class)
    /// </summary>
    internal interface IJsException
    {
        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>The error description.</value>
        string ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets the error line number.
        /// </summary>
        /// <value>The error line number.</value>
        int? ErrorLineNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the error.
        /// </summary>
        /// <value>The name of the error.</value>
        string ErrorName { get; set; }

        /// <summary>
        /// Gets or sets the error number.
        /// </summary>
        /// <value>The error number.</value>
        int? ErrorNumber { get; set; }

        /// <summary>
        /// Gets or sets the error URL.
        /// </summary>
        /// <value>The error URL.</value>
        string ErrorUrl { get; set; }
    }
}
