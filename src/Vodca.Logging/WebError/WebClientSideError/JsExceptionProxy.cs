//-----------------------------------------------------------------------------
// <copyright file="JsExceptionProxy.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2010
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;

    /// <summary>
    ///     The JsException Proxy class needed for de-serialization of JSON string to C# entity and initialize JsException properties 
    /// </summary>
    [Serializable]
    // ReSharper disable ClassNeverInstantiated.Global
    internal partial class JsExceptionProxy : IJsException
    // ReSharper restore ClassNeverInstantiated.Global
    {
        #region IJsException Members

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>The error description.</value>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets the error line number.
        /// </summary>
        /// <value>The error line number.</value>
        public int? ErrorLineNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the error.
        /// </summary>
        /// <value>The name of the error.</value>
        public string ErrorName { get; set; }

        /// <summary>
        /// Gets or sets the error number.
        /// </summary>
        /// <value>The error number.</value>
        public int? ErrorNumber { get; set; }

        /// <summary>
        /// Gets or sets the error URL.
        /// </summary>
        /// <value>The error URL.</value>
        public string ErrorUrl { get; set; }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.ErrorName) && this.ErrorNumber.HasValue;
        }

        #endregion
    }
}
