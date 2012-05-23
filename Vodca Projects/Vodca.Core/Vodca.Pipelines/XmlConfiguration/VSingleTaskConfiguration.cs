//-----------------------------------------------------------------------------
// <copyright file="VSingleTaskConfiguration.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/30/2011
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Vodca Pipeline root node configuration section
    /// </summary>
    [Serializable]
    [XmlRoot("vSingleTask")]
    public sealed class VSingleTaskConfiguration : IValidate, IXmlSerializable
    {
        /// <summary>
        /// The full type name with assembly name
        /// </summary>
        private string typeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="VSingleTaskConfiguration"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public VSingleTaskConfiguration(Type type)
        {
            Ensure.IsNotNull(type, "type");

            this.Type = type;
            this.typeName = type.AssemblyQualifiedName;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="VSingleTaskConfiguration"/> class from being created.
        /// </summary>
        private VSingleTaskConfiguration()
        {
        }

        /// <summary>
        /// Gets the full type name with assembly name.
        /// </summary>
        /// <value>
        /// The full type name with assembly name.
        /// </value>
        [XmlAttribute("type")]
        public string TypeName
        {
            get
            {
                return this.typeName;
            }

            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.typeName = value.Trim();

                    this.Type = Type.GetType(typeName: this.TypeName, throwOnError: false, ignoreCase: true);
                }
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        [XmlIgnore]
        public Type Type { get; private set; }

        /// <summary>
        /// Gets or sets the single task constructor JSON.
        /// </summary>
        /// <value>
        /// The single task JSON.
        /// </value>
        [XmlIgnore]
        public string Json { get; set; }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return string.Concat('#', this.TypeName, '#', this.Json, "#").ToLowerInvariant().GetHashCode();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance of specified type</returns>
        public object GetInstance()
        {
            return this.Type.GetInstance(this.Json);
        }

        /// <summary>
        /// Gets the generic single task method info.
        /// </summary>
        /// <param name="pipelineargumenttype">The pipelineargumenttype.</param>
        /// <param name="methodname">The methodname.</param>
        /// <returns>The pipeline method to execute</returns>
        public MethodInfo GetGenericSingleTaskMethodInfo(Type pipelineargumenttype, string methodname = "Process")
        {
            var pipelinemethod = (from method in this.Type.GetMethods() where method.Name.Equals(methodname) && method.IsGenericMethod select method).FirstOrDefault();
            Debug.Assert(pipelinemethod != null, "pipelinemethod != null");
            Ensure.IsNotNull(pipelinemethod, "pipelinemethod");

            return pipelinemethod.MakeGenericMethod(pipelineargumenttype);
        }

        /// <summary>
        /// Gets a flag indicating whether object is valid or not
        /// </summary>
        /// <returns>
        /// True if valid otherwise false
        /// </returns>
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.TypeName))
            {
                throw new VHttpArgumentNullException("VSingleTaskConfiguration.TypeName property is empty!");
            }

            if (this.Type == null)
            {
                throw new VHttpArgumentException("VSingleTaskConfiguration.TypeName property reflection couldn't resolve the type!");
            }

            return true;
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (string.Equals(reader.LocalName, "vSingleTask"))
            {
                if (reader.HasAttributes)
                {
                    this.TypeName = reader.GetAttribute("type");

                    if (reader.Read() && !string.IsNullOrWhiteSpace(reader.Value))
                    {
                        this.Json = reader.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(this.TypeName))
            {
                writer.WriteStartAttribute("type");
                writer.WriteValue(this.TypeName);
                writer.WriteEndAttribute();
            }

            if (!string.IsNullOrWhiteSpace(this.Json))
            {
                writer.WriteString(this.Json);
            }
        }
    }
}