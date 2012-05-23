//-----------------------------------------------------------------------------
// <copyright file="ILoggable.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/14/2011
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    /// <summary>
    /// The base interface for all custom Vodca pipelines
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogErrorMessage(params object[] message);

        /// <summary>
        /// Logs the info message.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogInfoMessage(params object[] message);

        /// <summary>
        /// Logs the warn message.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogWarnMessage(params object[] message);
    }
}
