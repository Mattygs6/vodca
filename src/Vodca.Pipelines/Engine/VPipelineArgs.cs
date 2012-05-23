//-----------------------------------------------------------------------------
// <copyright file="VPipelineArgs.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    using System;
    using Vodca.SDK.Newtonsoft.Json;
    using Vodca.SDK.NLog;

    /// <summary>
    /// The Base pipeline arg class
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public abstract partial class VPipelineArgs : EventArgs, IPipelineArgs, ILoggable
    {
        /// <summary>
        ///     The generic NLog logger
        /// </summary>
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets a value indicating whether this <see cref="IPipelineArgs"/> is aborted.
        /// </summary>
        public bool IsAborted { get; private set; }

        /// <summary>
        /// Aborts the pipeline.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AbortPipeline(params object[] message)
        {
            this.IsAborted = true;
            Logger.Error(string.Concat(message));
        }

        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogErrorMessage(params object[] message)
        {
            Logger.Error(string.Concat(message));
        }

        /// <summary>
        /// Logs the info message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInfoMessage(params object[] message)
        {
            Logger.Info(string.Concat(message));
        }

        /// <summary>
        /// Logs the warn message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogWarnMessage(params object[] message)
        {
            Logger.Warn(string.Concat(message));
        }
    }
}