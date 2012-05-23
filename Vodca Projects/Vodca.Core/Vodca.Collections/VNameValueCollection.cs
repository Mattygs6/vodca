//-----------------------------------------------------------------------------
// <copyright file="VNameValueCollection.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/03/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Web;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Vodca.SDK.Newtonsoft.Json;

    /// <summary>
    ///     For some reason, the generic NameValueCollection in .net  is not XML serializable.  
    /// The following code is a xml serializable generic dictionary. 
    /// The NameValueCollection is serializable by implementing the IXmlSerializable interface. 
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Collections\VNameValueCollection.cs" title="VNameValueCollection.cs" lang="C#" />
    /// </example>
    [XmlRoot("VNameValueCollection")]
    [Serializable]
    [GuidAttribute("E87B8E54-BABA-4676-96E0-E9D6E44C4ED6")]
    [DebuggerTypeProxy(typeof(Diagnostics.CollectionDebuggerProxy))]
    [DebuggerDisplay("Count = {Count} # VNameValueCollection = {ToString()}")]
    [DebuggerVisualizerAttribute("System.Web.Visualizers.NameValueCollectionVisualizer")]
    public sealed partial class VNameValueCollection : NameValueCollection, IXmlSerializable, IToXElement
    {
        /// <summary>
        ///      Initializes a new instance of the VNameValueCollection class.
        /// Class has the default initial capacity and uses the default 
        /// case-insensitive hash code provider and the default case-insensitive comparer.
        /// </summary>
        /// <remarks>
        ///     The default VNameValueCollection key comparison isn't case sensitive
        /// </remarks>
        public VNameValueCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the VNameValueCollection class.
        /// Class has the specified initial capacity and uses the default
        ///     case-insensitive hash code provider and the default case-insensitive comparer.
        /// </summary>
        /// <param name="capacity">The initial number of entries that the  VNameValueCollection can contain.</param>
        public VNameValueCollection(int capacity)
            : base(capacity, StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the VNameValueCollection class.
        ///     Copies the entries from the specified System.Collections.Specialized.NameValueCollection
        ///     to a new VNameValueCollection the same initial capacity as the number of entries copied and using the same hash
        ///     code provider and the same comparer as the source collection.
        /// </summary>
        /// <param name="col">
        ///     The System.Collections.Specialized.NameValueCollection to copy to the new
        ///     VNameValueCollection instance.
        /// </param>
        public VNameValueCollection(NameValueCollection col)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            this.Add(col);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VNameValueCollection"/> class.
        /// </summary>
        /// <param name="querystring">The query string.</param>
        public VNameValueCollection(string querystring)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            if (!string.IsNullOrWhiteSpace(querystring))
            {
                NameValueCollection col = HttpUtility.ParseQueryString(querystring);
                this.Add(col);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VNameValueCollection"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public VNameValueCollection(IEnumerable<VKeyValue> collection)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        /// <summary>
        ///      Initializes a new instance of the VNameValueCollection class.
        /// Class has the default initial capacity and uses the default 
        /// case-insensitive hash code provider and the default case-insensitive comparer.
        /// </summary>
        /// <param name="info">Stores all the data needed to serialize or de-serialize an object.</param>
        /// <param name="context">
        ///     Describes the source and destination of a given serialized stream, and provides
        /// an additional caller-defined context
        /// </param>
        /// <remarks>
        ///     The default VNameValueCollection key comparison isn't case sensitive
        /// </remarks>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        private VNameValueCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="VKeyValue"/> to <see cref="VNameValueCollection"/>.
        /// </summary>
        /// <param name="item">The VKeyValue array.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator VNameValueCollection(VKeyValue[] item)
        {
            return new VNameValueCollection(item);
        }

        /// <summary>
        ///      Adds an entry with the specified name and value to the VNameValueCollection.
        /// </summary>
        /// <param name="item">The VKeyValue to add</param>
        public void Add(VKeyValue item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Key))
            {
                this.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        ///     Adds the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Add(IEnumerable<VKeyValue> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        /// <summary>
        ///      Appends an entry with the specified name and value to the VNameValueCollection.
        /// </summary>
        /// <param name="item">The VKeyValue to add</param>
        public void Append(VKeyValue item)
        {
            if (item != null && !string.IsNullOrEmpty(item.Key))
            {
                string value = string.Concat(string.Empty, this[item.Key], item.Value).Trim();
                this[item.Key] = value;
            }
        }

        /// <summary>
        /// Adds the key value pair or overrides value if exists.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AddOrOverride(NameValueCollection collection)
        {
            if (collection != null)
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    string key = collection.GetKey(i);
                    string value = collection[i];
                    this[key] = value;
                }
            }
        }

        /// <summary>
        ///     The String representation of the object
        /// </summary>
        /// <returns>Object in well-formed XML string form</returns>
        public override string ToString()
        {
            return this.ToXElement().ToString();
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current Object(VKeyValue).</returns>
        public override int GetHashCode()
        {
            return this.ToString().ToUpper(CultureInfo.InvariantCulture).GetHashCode();
        }

        /// <summary>
        ///     Converts Object to LINQ XElemnent
        /// </summary>
        /// <returns>Represents an XML element of the object</returns>
        public XElement ToXElement()
        {
            return this.ToXElement("XItemCollection");
        }

        /// <summary>
        /// Converts Object to LINQ XElemnent
        /// </summary>
        /// <param name="root">The root element name.</param>
        /// <returns>Represents an XML element of the object</returns>
        public XElement ToXElement(string root)
        {
            var xelement = new XElement(root);

            for (int i = 0; i < this.Count; i++)
            {
                xelement.Add(new XElement(
                    "VKeyValue",
                    new XAttribute("Key", this.GetKey(i)),
                    new XElement("Value", this[i])));
            }

            return xelement;
        }

        /// <summary>
        ///     Get all elements of the VNameValueCollection to the <![CDATA[System.Collections.Generic.List<VKeyValue>]]>
        /// </summary>
        /// <returns>The generic  <![CDATA[System.Collections.Generic.List<VKeyValue>]]>
        /// </returns>
        public IList<VKeyValue> ToList()
        {
            var collection = new List<VKeyValue>(this.Count);

            for (int i = 0; i < this.Count; i++)
            {
                collection.Add(new VKeyValue { Key = this.GetKey(i), Value = this[i] });
            }

            return collection;
        }

        /// <summary>
        ///     Serialize VNameValueCollection to the JavaScript Notation Object (JSON)
        /// </summary>
        /// <returns>Serialized JSON string</returns>
        public string SerializeToJson()
        {
            var dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            for (int i = 0; i < this.Count; i++)
            {
                dictionary.Add(this.GetKey(i), this[i]);
            }

            return JsonConvert.SerializeObject(dictionary);
        }

        /// <summary>
        /// Removes the specified keys.
        /// </summary>
        /// <param name="keys">The dictionary keys.</param>
        public void Remove(params string[] keys)
        {
            if (keys != null)
            {
                foreach (var key in keys)
                {
                    base.Remove(key);
                }
            }
        }

        /// <summary>
        /// The current VNameValueCollection to the query string.
        /// </summary>
        /// <param name="urlencode">if set to <c>true</c> url encode.</param>
        /// <param name="prependquestionmark">if set to <c>true</c> prepend question marks.</param>
        /// <returns>
        /// The current collection to the query string
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Collections\VNameValueCollection.cs" title="VNameValueCollection.cs" lang="C#" />
        /// </example>
        public string ToQueryString(bool urlencode = true, bool prependquestionmark = false)
        {
            var querystring = new StringBuilder(64);
            var keys = this.BaseGetAllKeys();

            for (int i = 0; i < keys.Length; i++)
            {
                if (prependquestionmark && i == 0)
                {
                    querystring.Append('?');
                }

                var key = keys[i];
                var value = this[key];

                if (!string.IsNullOrEmpty(value))
                {
                    var urlitem = key + "=" + value;
                    querystring.Append(urlitem);

                    // edited by Freid
                    if (i != keys.Length - 1)
                    {
                        querystring.Append('&');
                    }
                }
            }

            if (urlencode)
            {
                return querystring.ToString().UrlDecode();
            }

            return querystring.ToString();
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
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"></see> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            bool isEmptyElement = reader.IsEmptyElement;
            reader.Read();

            if (!isEmptyElement)
            {
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    var item = (VKeyValue)VKeyValue.XmlSerializer.Deserialize(reader);
                    this[item.Key] = item.Value;
                    reader.Read();
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
            if (this.HasKeys())
            {
                // Order by key for better "human" readability only
                var keyscollection = from key in this.BaseGetAllKeys() orderby key select key;

                foreach (var key in keyscollection)
                {
                    VKeyValue.XmlSerializer.Serialize(writer, new VKeyValue { Key = key, Value = this[key] }, new XmlSerializerNamespaces());
                }
            }
        }

        #endregion
    }
}
