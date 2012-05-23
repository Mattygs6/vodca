//-----------------------------------------------------------------------------
// <copyright file="VInternalPipelineManager.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    using System.Collections.Concurrent;
    using System.Threading;

    /// <summary>
    /// Internal Pipeline Manager
    /// </summary>
    internal partial class VInternalPipelineManager : ThreadLocal<VInternalPipelineManager>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VInternalPipelineManager"/> class.
        /// </summary>
        public VInternalPipelineManager()
        {
            this.Collection = new ConcurrentDictionary<string, VInternalPiplineMethodCollectionManager>();
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <value>
        /// The collection.
        /// </value>
        public ConcurrentDictionary<string, VInternalPiplineMethodCollectionManager> Collection { get; private set; }

        /// <summary>
        /// Adds the VTaskPipeline.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void AddVTaskPipeline(VTaskPipelineConfiguration configuration)
        {
            Ensure.IsNotNull(configuration, "configuration");

            if (!this.Collection.ContainsKey(configuration.UniqueName))
            {
                this.Collection.TryAdd(configuration.UniqueName, new VInternalPiplineMethodCollectionManager().AddSingleTasks(configuration.SingleTasks, configuration.ArgsType));
            }
        }
    }
}
