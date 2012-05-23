//-----------------------------------------------------------------------------
// <copyright file="VXmlCData.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/20/2010
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
    /// The Xml CD Data object
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.XmlSerialization\VXmlCData.cs" title="VXmlCData.cs" lang="C#" />
    /// </example>
    [Serializable]
    [XmlRoot("VXmlCData")]
    public sealed partial class VXmlCData : IXmlSerializable, ISerializable, IToXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VXmlCData"/> class.
        /// </summary>
        public VXmlCData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VXmlCData"/> class.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        public VXmlCData(object obj)
        {
            this.Html = string.Concat(obj);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VXmlCData"/> class.
        /// </summary>
        /// <param name="html">The HTML content.</param>
        public VXmlCData(string html)
        {
            this.Html = html;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VXmlCData"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The context.</param>
        private VXmlCData(SerializationInfo info, StreamingContext context)
        {
            this.Html = info.GetString("Html");
        }

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        /// <value>The HTML content.</value>
        public string Html { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="VXmlCData"/>.
        /// </summary>
        /// <param name="html">The HTML input.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator VXmlCData(string html)
        {
            return new VXmlCData(html);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="VXmlCData"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="cdata">The VXmlCData object.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(VXmlCData cdata)
        {
            if (cdata != null)
            {
                return cdata.Html;
            }

            return null;
        }

        #region IToXElement Members

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <returns>The object instance data as XElement</returns>
        public XElement ToXElement()
        {
            return this.ToXElement("VXmlCData");
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootelementname">The root element name.</param>
        /// <returns>The object instance data as XElement</returns>
        public XElement ToXElement(string rootelementname)
        {
            Ensure.IsNotNullOrEmpty(rootelementname, "root element name");

            var root = new XElement(rootelementname);

            if (!string.IsNullOrEmpty(this.Html))
            {
                root.Add(new XCData(this.Html));
            }

            return root;
        }

        #endregion

        #region IXmlSerializable Members

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                this.Html = reader.ReadElementString();
            }
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Html))
            {
                writer.WriteCData(this.Html);
            }
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Html;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (this.Html ?? string.Empty).ToHashCode();
        }

        #region ISerializable Members

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        [SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Justification = "The compiler link security isn't critical here")]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Html", this.Html);
        }

        #endregion
    }
}
