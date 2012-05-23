//-----------------------------------------------------------------------------
// <copyright file="VHttpArgumentNullException.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/21/2010
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
    /// <code source="..\Vodca.Core\Vodca.Ensure\HttpExceptions\VHttpArgumentNullException.cs" title="VHttpArgumentNullException.cs" lang="C#" />
    /// </example>
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute]
    public sealed class VHttpArgumentNullException : HttpException
    {
        /// <summary>
        ///     The error message
        /// </summary>
        private const string DefaultMessage = "Value cannot be null.";

        /// <summary>
        /// Initializes a new instance of the <see cref="VHttpArgumentNullException"/> class.
        /// </summary>
        /// <param name="statuscode">The status code.</param>
        public VHttpArgumentNullException(int statuscode = VHttpStatusCodeExtension.ArgumentNullException)
            : base(statuscode, DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VHttpArgumentNullException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statuscode">The status code.</param>
        public VHttpArgumentNullException(string message, int statuscode = VHttpStatusCodeExtension.ArgumentNullException)
            : base(statuscode, message)
        {
        }
    }
}