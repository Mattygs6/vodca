//-----------------------------------------------------------------------------
// <copyright file="VHttpNotSupportedException.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/21/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Web;

    /// <summary>
    ///     An exception that occurred during the processing of HTTP requests.
    /// The exception that is thrown when a null reference is passed to a method that does not accept it as a valid argument.
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Ensure\HttpExceptions\VHttpNotSupportedException.cs" title="VHttpNotSupportedException.cs" lang="C#" />
    /// </example>
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    public sealed class VHttpNotSupportedException : HttpException
    {
        /// <summary>
        ///     The error message
        /// </summary>
        private const string DefaultMessage = "The operation not supported!";

        /// <summary>
        /// Initializes a new instance of the <see cref="VHttpNotSupportedException"/> class.
        /// </summary>
        /// <param name="statuscode">The status code.</param>
        public VHttpNotSupportedException(int statuscode = VHttpStatusCodeExtension.NotSupportedException)
            : base(statuscode, DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VHttpNotSupportedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statuscode">The status code.</param>
        public VHttpNotSupportedException(string message, int statuscode = VHttpStatusCodeExtension.NotSupportedException)
            : base(statuscode, message)
        {
        }
    }
}