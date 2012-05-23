//-----------------------------------------------------------------------------
// <copyright file="VHttpArgumentException.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/21/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics;
    using System.Web;

    /// <summary>
    ///     An exception that occurred during the processing of HTTP requests.
    /// The exception that is thrown when a null reference is passed to a method that does not accept it as a valid argument.
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Ensure\HttpExceptions\VHttpArgumentException.cs" title="VHttpArgumentException.cs" lang="C#" />
    /// </example>
    [Serializable]
    [DebuggerStepThroughAttribute]
    public sealed class VHttpArgumentException : HttpException
    {
        /// <summary>
        ///     The error message
        /// </summary>
        private const string DefaultMessage = "The argument is invalid!";

        /// <summary>
        /// Initializes a new instance of the <see cref="VHttpArgumentException"/> class.
        /// </summary>
        /// <param name="statuscode">The status code.</param>
        public VHttpArgumentException(int statuscode = VHttpStatusCodeExtension.ArgumentException)
            : base(statuscode, DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VHttpArgumentException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statuscode">The status code.</param>
        public VHttpArgumentException(string message, int statuscode = VHttpStatusCodeExtension.ArgumentException)
            : base(statuscode, message)
        {
        }
    }
}