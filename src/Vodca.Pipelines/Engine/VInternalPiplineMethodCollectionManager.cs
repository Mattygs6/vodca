//-----------------------------------------------------------------------------
// <copyright file="VInternalPiplineMethodCollectionManager.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Internal Pipline Method Collection manager
    /// </summary>
    /// <seealso cref="http://msdn.microsoft.com/en-us/magazine/cc163759.aspx"/>
    internal sealed partial class VInternalPiplineMethodCollectionManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VInternalPiplineMethodCollectionManager"/> class.
        /// </summary>
        public VInternalPiplineMethodCollectionManager()
        {
            this.Collection = new ConcurrentDictionary<object, MethodInfo>();
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        public IDictionary<object, MethodInfo> Collection { get; private set; }

        /// <summary>
        /// Adds the single tasks.
        /// </summary>
        /// <param name="singletasks">The singletasks.</param>
        /// <param name="pipelineargumenttype">The pipelineargumenttype.</param>
        /// <returns>The self instance</returns>
        public VInternalPiplineMethodCollectionManager AddSingleTasks(IEnumerable<VSingleTaskConfiguration> singletasks, Type pipelineargumenttype)
        {
            Ensure.IsNotNull(singletasks, "singletasks");
            Ensure.IsNotNull(pipelineargumenttype, "pipelineargumenttype");

            foreach (var task in singletasks)
            {
                this.AddSingleTask(task.GetInstance(), task.GetGenericSingleTaskMethodInfo(pipelineargumenttype));
            }

            return this;
        }

        /// <summary>
        /// Adds the single tasks.
        /// </summary>
        /// <param name="singletask">The singletask.</param>
        /// <param name="pipelineargumenttype">The pipelineargumenttype.</param>
        /// <returns>The self instance</returns>
        public VInternalPiplineMethodCollectionManager AddSingleTask(VSingleTaskConfiguration singletask, Type pipelineargumenttype)
        {
            Ensure.IsNotNull(singletask, "singletask");
            Ensure.IsNotNull(pipelineargumenttype, "pipelineargumenttype");

            this.AddSingleTask(singletask.GetInstance(), singletask.GetGenericSingleTaskMethodInfo(pipelineargumenttype));

            return this;
        }

        /// <summary>
        /// Executes the specified argument instance.
        /// </summary>
        /// <param name="argumentinstance">The argument instance.</param>
        public void Execute(object argumentinstance)
        {
            var args = new[] { argumentinstance };
            foreach (KeyValuePair<object, MethodInfo> singletask in this.Collection)
            {
                singletask.Value.Invoke(singletask.Key, args);
            }
        }

        /// <summary>
        /// Adds the single task.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="methodinfo">The method info.</param>
        private void AddSingleTask(object instance, MethodInfo methodinfo)
        {
            Ensure.IsNotNull(instance, "instance");
            Ensure.IsNotNull(methodinfo, "methodinfo");

            this.Collection.Add(instance, methodinfo);
        }
    }
}