//-----------------------------------------------------------------------------
// <copyright file="VHttpFileNotFoundException.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/21/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Web;

    /// <summary>
    ///     An exception that occurred during the processing of HTTP requests.
    /// The exception that is thrown when a null reference is passed to a method that does not accept it as a valid argument.
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Ensure\HttpExceptions\VHttpFileNotFoundException.cs" title="VHttpFileNotFoundException.cs" lang="C#" />
    /// </example>
    [Serializable]
    [DebuggerStepThroughAttribute]
    public sealed class VHttpFileNotFoundException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VHttpFileNotFoundException"/> class.
        /// </summary>
        /// <param name="statuscode">The status code.</param>
        public VHttpFileNotFoundException(int statuscode = (int)HttpStatusCode.NotFound)
            : base(statuscode, "The requested resource does not exist on the server")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VHttpFileNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statuscode">The status code.</param>
        public VHttpFileNotFoundException(string message, int statuscode = (int)HttpStatusCode.NotFound)
            : base(statuscode, message)
        {
        }
    }
}