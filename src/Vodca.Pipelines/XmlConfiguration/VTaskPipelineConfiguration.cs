//-----------------------------------------------------------------------------
// <copyright file="VTaskPipelineConfiguration.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/30/2011
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Vodca Pipeline root node configuration section
    /// </summary>
    [Serializable]
    [XmlRoot("vTaskPipeline")]
    public sealed class VTaskPipelineConfiguration : IValidate
    {
        /// <summary>
        /// The type of the args.
        /// </summary>
        private string argsTypeName;

        /// <summary>
        /// The configuration collection
        /// </summary>
        private ConcurrentBag<VSingleTaskConfiguration> collection = new ConcurrentBag<VSingleTaskConfiguration>(/*new VSingleTaskConfigurationEqualityComparer()*/);

        /// <summary>
        /// Initializes a new instance of the <see cref="VTaskPipelineConfiguration"/> class.
        /// </summary>
        /// <param name="uniquepipelinename">The pipeline unique name.</param>
        /// <param name="argstypename">The args type name.</param>
        public VTaskPipelineConfiguration(string uniquepipelinename, string argstypename)
        {
            Ensure.IsNotNullOrEmpty(uniquepipelinename, "uniquepipelinename");
            Ensure.IsNotNullOrEmpty(argstypename, "argstypename");

            this.UniqueName = uniquepipelinename;
            this.ArgsTypeName = argstypename;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VTaskPipelineConfiguration"/> class.
        /// </summary>
        /// <param name="uniquepipelinename">The unique pipeline name.</param>
        /// <param name="argumenttype">The argument type.</param>
        public VTaskPipelineConfiguration(string uniquepipelinename, Type argumenttype)
        {
            Ensure.IsNotNullOrEmpty(uniquepipelinename, "uniquepipelinename");
            Ensure.IsNotNull(argumenttype, "argumenttype");

            this.UniqueName = uniquepipelinename;
            this.argsTypeName = argumenttype.AssemblyQualifiedName;
            this.ArgsType = argumenttype;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VTaskPipelineConfiguration"/> class.
        /// </summary>
        /// <param name="vpipelineclass">The VPipeline class.</param>
        /// <param name="argumenttype">The argument type.</param>
        public VTaskPipelineConfiguration(Type vpipelineclass, Type argumenttype)
        {
            Ensure.IsNotNull(vpipelineclass, "vpipelineclass");
            Ensure.IsNotNull(argumenttype, "argumenttype");

            this.UniqueName = vpipelineclass.FullName;
            this.argsTypeName = argumenttype.AssemblyQualifiedName;
            this.ArgsType = argumenttype;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="VTaskPipelineConfiguration"/> class from being created.
        /// </summary>
        private VTaskPipelineConfiguration()
        {
        }

        /// <summary>
        /// Gets or sets the name of the unique.
        /// </summary>
        /// <value>
        /// The name of the unique.
        /// </value>
        [XmlAttribute("uniqueName")]
        public string UniqueName { get; set; }

        /// <summary>
        /// Gets or sets the type of the args.
        /// </summary>
        /// <value>
        /// The type of the args.
        /// </value>
        [XmlAttribute("argsType")]
        public string ArgsTypeName
        {
            get
            {
                return this.argsTypeName;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.argsTypeName = value.Trim();
                    this.ArgsType = Type.GetType(typeName: this.ArgsTypeName, throwOnError: false, ignoreCase: true);
                }
            }
        }

        /// <summary>
        /// Gets the type of the args.
        /// </summary>
        /// <value>
        /// The type of the args.
        /// </value>
        [XmlIgnore]
        public Type ArgsType { get; private set; }

        /// <summary>
        /// Gets or sets the task pipelines.
        /// </summary>
        /// <value>
        /// The task pipelines.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Required for XMl serialization/deserialization")]
        [XmlArray("vSingleTasks")]
        [XmlArrayItem("vSingleTask")]
        public VSingleTaskConfiguration[] SingleTasks
        {
            get
            {
                return this.collection.ToArray();
            }

            set
            {
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        if (!this.collection.Contains(item))
                        {
                            this.collection.Add(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds the task pipeline.
        /// </summary>
        /// <param name="taskpipeline">The task pipeline.</param>
        public void AddSingleTask(VSingleTaskConfiguration taskpipeline)
        {
            if (taskpipeline != null && !this.collection.Contains(taskpipeline))
            {
                this.collection.Add(taskpipeline);
            }
        }

        /// <summary>
        /// Gets a flag indicating whether object is valid or not
        /// </summary>
        /// <returns>
        /// True if valid otherwise false
        /// </returns>
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.UniqueName))
            {
                throw new VHttpArgumentNullException("VTaskPipelineConfiguration.UniqueName property is empty!");
            }

            if (string.IsNullOrWhiteSpace(this.ArgsTypeName))
            {
                throw new VHttpArgumentNullException("VTaskPipelineConfiguration.ArgsTypeName property is empty!");
            }

            if (this.ArgsType == null)
            {
                throw new VHttpArgumentException("VTaskPipelineConfiguration.ArgsTypeName property reflection couldn't resolve the type!");
            }

            return true;
        }
    }
}