//-----------------------------------------------------------------------------
// <copyright file="VKeyValue.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    ///     Each element is a key/value pair.
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Collections\VKeyValue.cs" title="VKeyValue.cs" lang="C#" />
    /// </example>
    [Serializable]
    [XmlRoot("VKeyValue"), System.Runtime.InteropServices.GuidAttribute("5EB052C1-F149-4C2C-AE53-55844EFDF0B3")]
    public sealed class VKeyValue : IXmlSerializable, ISerializable, IToXElement
    {
        /// <summary>
        ///     The VKeyValue serializes
        /// </summary>
        public static readonly XmlSerializer XmlSerializer = new XmlSerializer(typeof(VKeyValue));

        /// <summary>
        ///     The Html chars to indicate to use CData for serialization
        /// </summary>
        private static readonly char[] HtmlChars = new[] { '<', '>', '&' };

        /// <summary>
        ///     Initializes a new instance of the VKeyValue class
        /// </summary>
        public VKeyValue()
        {
            this.Key = string.Empty;
            this.Value = string.Empty;
        }

        /// <summary>
        ///     Initializes a new instance of the VKeyValue class
        /// </summary>
        /// <param name="key">The String key of the entry to add</param>
        /// <param name="value">The String value of the entry to add</param>
        public VKeyValue(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        ///     Initializes a new instance of the VKeyValue class
        /// </summary>
        /// <param name="key">The String key of the entry to add</param>
        /// <param name="value">The String value of the entry to add</param>
        public VKeyValue(string key, object value)
        {
            this.Key = key;
            this.Value = string.Concat(value);
        }

        /// <summary>
        ///     Initializes a new instance of the VKeyValue class
        /// </summary>
        /// <param name="info">Stores all the data needed to serialize or de-serialize an object.</param>
        /// <param name="context">
        ///     Describes the source and destination of a given serialized stream, and provides
        /// an additional caller-defined context
        /// </param>
        private VKeyValue(SerializationInfo info, StreamingContext context)
        {
            this.Key = info.GetString("Key");
            this.Value = info.GetString("Value");
        }

        /// <summary>
        ///     Gets or sets a key of the entry
        /// </summary>
        [XmlAttribute("Key")]
        public string Key { get; set; }

        /// <summary>
        ///     Gets or sets a value of the entry.
        /// </summary>
        [XmlAttribute("Value")]
        public string Value { get; set; }

        #region Static operators

        /// <summary>
        ///     The equality operator (==) returns true if the values of its operands are equal, false otherwise.
        /// </summary>
        /// <param name="one">Firsts Object typeof VKeyValue</param>
        /// <param name="two">Second Object typeof VKeyValue</param>
        /// <returns>True if the values of its operands are equal</returns>
        public static bool operator ==(VKeyValue one, VKeyValue two)
        {
#pragma warning disable 183
            if (two is VKeyValue && one is VKeyValue)
#pragma warning restore 183
            {
                return
                    string.Equals(one.Key, two.Key, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(one.Value, two.Value, StringComparison.OrdinalIgnoreCase);
            }

            // Use .NET comparison then one side is NULL
            /* ReSharper disable RedundantNameQualifier */
            return object.Equals(one, two);
            /* ReSharper restore RedundantNameQualifier */
        }

        /// <summary>
        ///     The equality operator (!=) returns true if the values of its operands are NOT equal, false otherwise.
        /// </summary>
        /// <param name="one">First Object typeof VKeyValue</param>
        /// <param name="two">Object Object typeof VKeyValue</param>
        /// <returns>True if the values of its operands are equal</returns>
        public static bool operator !=(VKeyValue one, VKeyValue two)
        {
#pragma warning disable 183
            if (two is VKeyValue && one is VKeyValue)
#pragma warning restore 183
            {
                return
                    !string.Equals(one.Key, two.Key, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(one.Value, two.Value, StringComparison.OrdinalIgnoreCase);
            }

            // Use .NET comparison then one side is NULL
            /* ReSharper disable RedundantNameQualifier */
            return !object.Equals(one, two);
            /* ReSharper restore RedundantNameQualifier */
        }

        #endregion

        #region ToString()

        /// <summary>
        ///     The String representation of the object
        /// </summary>
        /// <returns>Object in well-formed XML string form</returns>
        public override string ToString()
        {
            return this.ToXElement().ToString();
        }

        #endregion

        #region Equals, GetHashCode, operator ==, operator !=

        /// <summary>
        ///     Determines whether the specified Object typeof VKeyValue is equal to the current Object.
        /// </summary>
        /// <param name="obj">The Object to compare with the current Object.</param>
        /// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var webclientsideerror = obj as VKeyValue;
            return this == webclientsideerror;
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current Object(VKeyValue).</returns>
        public override int GetHashCode()
        {
            return string.Concat("VKeyValue-", this.Key).ToHashCode();
        }

        #endregion

        #region IToXElement Members

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <returns>The object instance data as XElement</returns>
        public XElement ToXElement()
        {
            return this.ToXElement("VKeyValue");
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement(string rootname)
        {
            return new XElement(
                    "VKeyValue",
                    new XAttribute("Key", this.Key),
                    new XElement("Value", this.Value));
        }

        #endregion

        #region IXmlSerializable Members

        /// <summary>  
        ///     Returns schema of the XML document representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"></see> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"></see> method.  
        /// </summary>  
        /// <returns>  
        ///     An <see cref="T:System.Xml.Schema.XmlSchema"></see> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"></see> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"></see> method.  
        /// </returns>  
        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        /// <summary>  
        ///     Generates an object from its XML representation.  
        /// </summary>  
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"></see> stream from which the object is deserialized.</param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && string.Equals(reader.LocalName, "VKeyValue", StringComparison.OrdinalIgnoreCase))
            {
                if (!reader.IsEmptyElement)
                {
                    this.Key = reader.GetAttribute("Key");
                    this.Value = reader.ReadElementString();
                }
            }
        }

        /// <summary>  
        ///     Converts an object into its XML representation.  
        /// </summary>  
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"></see> stream to which the object is serialized.</param>  
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Key))
            {
                writer.WriteAttributeString("Key", this.Key);
                if (!string.IsNullOrEmpty(this.Value))
                {
                    if (this.Value.IndexOfAny(HtmlChars) > -1)
                    {
                        writer.WriteCData(this.Value);
                    }
                    else
                    {
                        writer.WriteString(this.Value);
                    }
                }
            }
        }

        #endregion

        #region ISerializable Members

        /// <summary>
        ///     Populates a System.Runtime.Serialization.SerializationInfo with the data
        /// needed to serialize the target object.
        /// </summary>
        /// <param name="info">Stores all the data needed to serialize or deserialize an object.</param>
        /// <param name="context">
        ///     Describes the source and destination of a given serialized stream, and provides
        /// an additional caller-defined context
        /// </param>
        [SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Justification = "The compiler link security isn't critical here")]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Key", this.Key);
            info.AddValue("Value", this.Value);
        }

        #endregion
    }
}
