//-----------------------------------------------------------------------------
// <copyright file="IPipelineArgs.cs" company="genuine">
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
    public interface IPipelineArgs
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IPipelineArgs"/> is aborted.
        /// </summary>
        bool IsAborted { get; }

        /// <summary>
        /// Aborts the pipeline.
        /// </summary>
        /// <param name="message">The message.</param>
        void AbortPipeline(params object[] message);
    }
}
