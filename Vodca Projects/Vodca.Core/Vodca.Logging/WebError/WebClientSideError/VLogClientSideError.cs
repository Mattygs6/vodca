//-----------------------------------------------------------------------------
// <copyright file="VLogClientSideError.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;

    /// <summary>
    ///     Represents a logical application error  on client side
    /// </summary>
    [Serializable]
    public sealed partial class VLogClientSideError : VLogError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VLogClientSideError"/> class.
        /// </summary>
        public VLogClientSideError()
        {
            this.ErrorType = VLogErrorTypes.ClientSide;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VLogClientSideError"/> class.
        /// from a given <see cref="JsException"/> instance and 
        /// <see cref="HttpContext"/> instance representing the HTTP 
        /// context during the exception.
        /// </summary>
        /// <param name="exception">The exception occurred on client side</param>
        public VLogClientSideError(JsException exception)
        {
            HttpContext context = HttpContext.Current;
            if (exception != null && context != null)
            {
                this.ErrorType = VLogErrorTypes.ClientSide;

                // Web Client error
                this.BrowserCapabilities = context.Request.Browser.ToDictionary();

                // Sets an object of a uniform resource identifier properties
                this.SetAdditionalHttpContextInfo(context);
                this.SetAdditionalExceptionInfo(exception);

                this.ErrorMessage = exception.Message;
                this.ErrorCode = exception.ErrorNumber.GetValueOrDefault(JsException.DefaultExceptionStatusCode);
            }
        }

        /// <summary>
        ///     Gets or sets a Browser Capabilities
        /// </summary>
        /// <remarks>
        ///     Set property is need for Xml serialize
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Needed for Xml Serialization"), SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Needed for deserialization")]
        public IDictionary<string, string> BrowserCapabilities { get; set; }
    }
}
