//-----------------------------------------------------------------------------
// <copyright file="VRegisterTaskPipelineAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using Vodca.Pipelines;

    /// <summary>
    /// The VTaskPipeline Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class VRegisterTaskPipelineAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterTaskPipelineAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public VRegisterTaskPipelineAttribute(Type type)
            : this(type, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterTaskPipelineAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="json">The JSON.</param>
        public VRegisterTaskPipelineAttribute(Type type, string json)
        {
            this.Json = json;
            Ensure.IsNotNull(type, "type");

            this.Configuration = type.GetInstance(this.Json) as IVTaskPipelineConfiguration;

            Ensure.IsNotNull(this.Configuration, "The type not implement the IVTaskPipelineConfiguration");
        }

        /// <summary>
        /// Gets the constructor JSON.
        /// </summary>
        /// <value>
        /// The JSON.
        /// </value>
        public string Json { get; private set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IVTaskPipelineConfiguration Configuration { get; private set; }
    }
}
