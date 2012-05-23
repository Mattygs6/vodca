//-----------------------------------------------------------------------------
// <copyright file="VStringDictionary.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/25/2010
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
    using System.Web.Script.Serialization;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Vodca.SDK.Newtonsoft.Json;

    /// <summary>
    ///     Not tread safe. Implements a Dictionary with the key and the value strongly typed to be strings rather than objects.    
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Collections\VStringDictionary.cs" title="VStringDictionary.cs" lang="C#" />
    /// </example>
    [XmlRoot("VStringDictionary")]
    [GuidAttribute("ABB2F66E-8AE2-46B6-AF35-2E1EC76E16EB")]
    [DebuggerDisplay("Count = {Count} # VStringDictionary = {ToString()}")]
    [Serializable]
    [DebuggerTypeProxy(typeof(Diagnostics.DictionaryDebugView<,>))]
    [DebuggerVisualizerAttribute("System.Web.Visualizers.NameValueCollectionVisualizer")]
    public sealed partial class VStringDictionary : Dictionary<string, string>, IXmlSerializable, IToXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VStringDictionary"/> class.
        /// </summary>
        public VStringDictionary()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VStringDictionary"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public VStringDictionary(IEnumerable<VKeyValue> items)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            Ensure.IsNotNull(items, "items");

            foreach (VKeyValue item in items)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VStringDictionary"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The serialization context.</param>
        private VStringDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets a collection of keys in the <see cref="VStringDictionary"/>.
        /// </summary>
        /// <value>The collection keys.</value>
        [XmlIgnore]
        [ScriptIgnore]
        public new IEnumerable<string> Keys
        {
            get
            {
                return base.Keys;
            }
        }

        /// <summary>
        ///  Gets a collection of values in the <see cref="VStringDictionary"/>.
        /// </summary>
        /// <value> Gets a collection of values.</value>
        [XmlIgnore]
        [ScriptIgnore]
        public new IEnumerable<string> Values
        {
            get
            {
                return base.Values;
            }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Collections.Specialized.NameValueCollection"/> to <see cref="VStringDictionary"/>.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator VStringDictionary(NameValueCollection collection)
        {
            if (collection != null)
            {
                var dictionary = new VStringDictionary();

                for (int i = 0; i < collection.Count; i++)
                {
                    var key = collection.GetKey(i);
                    if (!string.IsNullOrWhiteSpace(key) && !dictionary.ContainsKey(key))
                    {
                        dictionary.Add(collection.GetKey(i), collection[i]);
                    }
                }

                return dictionary;
            }

            return null;
        }

        /// <summary>
        /// Performs an implicit conversion from System.Web.VKeyValue> array to <see cref="VStringDictionary"/>.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator VStringDictionary(VKeyValue[] collection)
        {
            if (collection != null)
            {
                var dictionary = new VStringDictionary();

                foreach (var item in collection)
                {
                    if (!string.IsNullOrWhiteSpace(item.Key) && !dictionary.ContainsKey(item.Key))
                    {
                        dictionary.Add(item);
                    }
                }

                return dictionary;
            }

            return null;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="VStringDictionary"/> to <see cref="System.Collections.Specialized.NameValueCollection"/>.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator NameValueCollection(VStringDictionary dictionary)
        {
            if (dictionary != null)
            {
                var collection = new NameValueCollection(StringComparer.OrdinalIgnoreCase);

                foreach (KeyValuePair<string, string> item in dictionary)
                {
                    collection[item.Key] = item.Value;
                }

                return collection;
            }

            return null;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="VStringDictionary"/> to <see cref="VNameValueCollection"/>.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator VNameValueCollection(VStringDictionary dictionary)
        {
            if (dictionary != null)
            {
                var collection = new VNameValueCollection();

                foreach (KeyValuePair<string, string> item in dictionary)
                {
                    collection[item.Key] = item.Value;
                }

                return collection;
            }

            return null;
        }

        /// <summary>
        /// Adds the unique item.
        /// </summary>
        /// <param name="key">The dictionary key.</param>
        /// <param name="value">The dictionary value.</param>
        /// <returns>True if added otherwise false</returns>
        public bool AddUnique(string key, string value)
        {
            bool contains = false;

            if (!string.IsNullOrWhiteSpace(key))
            {
                if (!this.ContainsKey(key))
                {
                    this.Add(key, value);

                    contains = true;
                }
            }

            return contains;
        }

        /// <summary>
        ///      Adds an entry with the specified name and value to the dictionary.
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
            if (items != null)
            {
                foreach (var item in items)
                {
                    this.Add(item);
                }
            }
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
        /// Removes the specified keys.
        /// </summary>
        /// <param name="keys">The dictionary keys.</param>
        public void Remove(IEnumerable<string> keys)
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
        /// Removes the key(s) pairs with empty values.
        /// </summary>
        public void Trim()
        {
            var list = (from item in this where string.IsNullOrEmpty(item.Value) select item.Key).ToArray();

            this.Remove(list);
        }

        /// <summary>
        ///     Serialize dictionary to the JavaScript Notation Object (JSON)
        /// </summary>
        /// <returns>Serialized JSON string</returns>
        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(this.ToArray());
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
            Ensure.IsNotNullOrEmpty(root, "root");

            var xelement = new XElement("XItemCollection");

            foreach (KeyValuePair<string, string> item in this)
            {
                xelement.Add(new XElement(
                      "VKeyValue",
                      new XAttribute("Key", item.Key),
                      new XElement("Value", item.Value)));
            }

            return xelement;
        }

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
            bool isEmptyElement = reader.IsEmptyElement;
            reader.Read();

            if (!isEmptyElement)
            {
                while (string.Equals(reader.Name, "VKeyValue", StringComparison.OrdinalIgnoreCase))
                {
                    var item = (VKeyValue)VKeyValue.XmlSerializer.Deserialize(reader);
                    this[item.Key] = item.Value;
                    reader.Read();
                }
            }
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            if (this.Count > 0)
            {
                // Order by key for better "human" readability only
                var keyscollection = from key in this.Keys orderby key select key;

                foreach (var key in keyscollection)
                {
                    VKeyValue.XmlSerializer.Serialize(writer, new VKeyValue { Key = key, Value = this[key] }, new XmlSerializerNamespaces());
                }
            }
        }

        #endregion

        /// <summary>
        /// The collection item array.
        /// </summary>
        /// <returns>The array of  VKeyValue elements</returns>
        public VKeyValue[] ToArray()
        {
            return this.Select(item => new VKeyValue { Key = item.Key, Value = item.Value }).ToArray();
        }
    }
}
