//-----------------------------------------------------------------------------
// <copyright file="VLog.DelegateAndEvents.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       09/26/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    /// <summary>
    /// HTTP module implementation that logs unhandled exceptions in an
    /// ASP.NET Web application to an error log.
    /// </summary>
    // ReSharper disable ClassNeverInstantiated.Global
    public sealed partial class VLog
    // ReSharper restore ClassNeverInstantiated.Global
    {
        // ReSharper disable EventNeverSubscribedTo.Global

        /// <summary>
        /// Gets or sets the on application exception commit exception to repository.
        /// </summary>
        /// <value>
        /// The on application commit exception to repository.
        /// </value>
        public static Action<dynamic> OnCommitExceptionToServerRepository { get; set; }
        // ReSharper restore EventNeverSubscribedTo.Global
    }
}
