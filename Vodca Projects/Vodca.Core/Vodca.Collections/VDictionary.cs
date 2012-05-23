//-----------------------------------------------------------------------------
// <copyright file="VDictionary.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    ///     For some reason, the generic Dictionary in .net 2.0 is not XML serializable.  
    /// The following code is a xml serializable generic dictionary. 
    /// The dictionary is serializable by implementing the IXmlSerializable interface. 
    /// </summary>
    /// <remarks>
    ///    Idea was taken from http://weblogs.asp.net/pwelter34/archive/2006/05/03/444961.aspx
    /// </remarks>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Collections\VDictionary.cs" title="VDictionary.cs" lang="C#" />
    /// </example>
    [XmlRoot("VDictionary")]
    [Serializable, GuidAttribute("F3686C92-B3AC-49DA-B954-AEA09D5FB294")]
    public sealed partial class VDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        // ReSharper disable StaticFieldInGenericType

        /// <summary>
        ///     Initializes a new instance of the XmlSerializer class that can serialize objects of the specified type into XML documents,
        ///     and de-serialize XML documents into objects of the specified type.
        /// </summary>
        private static readonly XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));

        /// <summary>
        ///     Initializes a new instance of the XmlSerializer class that can serialize objects of the specified type into XML documents,
        ///     and de-serialize XML documents into objects of the specified type.
        /// </summary>
        private static readonly XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

        // ReSharper restore StaticFieldInGenericType

        /// <summary>
        /// Initializes a new instance of the <see cref="VDictionary{TKey,TValue}"/> class.
        /// </summary>
        public VDictionary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VDictionary{TKey,TValue}"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        private VDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #region IXmlSerializable Members

        /// <summary>  
        ///     Returns schema of the XML document representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"></see> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"></see> method.  
        /// </summary>  
        /// <returns>  
        ///     An <see cref="T:System.Xml.Schema.XmlSchema"></see> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"></see> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"></see> method.  
        /// </returns> 
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>  
        ///     Generates an object from its XML representation.  
        /// </summary>  
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"></see> stream from which the object is de-serialized.</param>
        public void ReadXml(XmlReader reader)
        {
            bool isEmptyElement = reader.IsEmptyElement;
            reader.Read();

            if (!isEmptyElement)
            {
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.ReadStartElement("item");

                    reader.ReadStartElement("key");
                    var key = (TKey)KeySerializer.Deserialize(reader);
                    reader.ReadEndElement();

                    reader.ReadStartElement("value");
                    var value = (TValue)ValueSerializer.Deserialize(reader);
                    reader.ReadEndElement();

                    this.Add(key, value);

                    reader.ReadEndElement();
                    reader.MoveToContent();
                }

                reader.ReadEndElement();
            }
        }

        /// <summary>  
        ///     Converts an object into its XML representation.  
        /// </summary>  
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"></see> stream to which the object is serialized.</param>  
        public void WriteXml(XmlWriter writer)
        {
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                KeySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                ValueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
