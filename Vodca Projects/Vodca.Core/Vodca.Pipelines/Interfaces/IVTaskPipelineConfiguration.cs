//-----------------------------------------------------------------------------
// <copyright file="IVTaskPipelineConfiguration.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    /// <summary>
    /// The interface for the VTaskPipeline custom classes
    /// </summary>
    public interface IVTaskPipelineConfiguration
    {
        /// <summary>
        /// Gets the VTask pipeline configuration.
        /// </summary>
        /// <returns>The VTaskPipeline Configuration</returns>
        VTaskPipelineConfiguration GetVTaskPipelineConfiguration();
    }
}