//-----------------------------------------------------------------------------
// <copyright file="Extensions.Serialize.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Serialize Object to the Xml string
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>The Xml string</returns>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.</exception>
        public static string SerializeToXml(this object obj)
        {
            if (obj != null)
            {
                var builder = new StringBuilder(4096);
                using (var writer = new StringWriter(builder))
                {
                    var xmlserializer = new XmlSerializer(obj.GetType());

                    var xmlnamespace = new XmlSerializerNamespaces();
                    xmlnamespace.Add(string.Empty, string.Empty);

                    xmlserializer.Serialize(writer, obj, xmlnamespace);
                }

                return builder.ToString();
            }

            return null;
        }

        /// <summary>
        /// Serializes to XML file.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <param name="virtualpath">The virtual path.</param>
        /// <returns>True if success, otherwise false</returns>
        public static bool SerializeToXmlFile(this object obj, string virtualpath)
        {
            if (obj != null && !string.IsNullOrWhiteSpace(virtualpath))
            {
                byte[] data = SerializeToXmlBytes(obj);
                File.WriteAllBytes(virtualpath.MapPath(), data);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Serializes Object to SQL XML (without namespace, NewLines and indent).
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>The Xml string</returns>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.</exception>
        public static string SerializeToSqlXml(this object obj)
        {
            if (obj != null)
            {
                var builder = new StringBuilder(4096);

                var settings = new XmlWriterSettings
                {
                    Indent = false,
                    NewLineHandling = NewLineHandling.None,
                    OmitXmlDeclaration = true,
                    CheckCharacters = true,
                    CloseOutput = true
                };

                using (XmlWriter writer = XmlWriter.Create(builder, settings))
                {
                    var xmlserializer = new XmlSerializer(obj.GetType());

                    var xmlnamespace = new XmlSerializerNamespaces();
                    xmlnamespace.Add(string.Empty, string.Empty);

                    xmlserializer.Serialize(writer, obj, xmlnamespace);
                }

                return builder.ToString();
            }

            return null;
        }

        /// <summary>
        ///     Serialize Object to the Xml byte array
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>The Xml byte array</returns>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.</exception>
        public static byte[] SerializeToXmlBytes(this object obj)
        {
            if (obj != null)
            {
                using (var stream = new MemoryStream(4096))
                {
                    var settings = new XmlWriterSettings
                                       {
                                           Indent = false,
                                           NewLineHandling = NewLineHandling.None,
                                           OmitXmlDeclaration = false,
                                           CheckCharacters = true,
                                       };

                    XmlWriter writer = XmlWriter.Create(stream, settings);

                    var xmlserializer = new XmlSerializer(obj.GetType());

                    var xmlnamespace = new XmlSerializerNamespaces();
                    xmlnamespace.Add(string.Empty, string.Empty);

                    xmlserializer.Serialize(writer, obj, xmlnamespace);

                    return stream.ToArray();
                }
            }

            return null;
        }

        /// <summary>
        ///     Serializes an instance of a type into an XML stream
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>The Xml string</returns>
        public static string SerializeToDataContract(this object obj)
        {
            if (obj != null)
            {
                var builder = new StringBuilder(4096);

                using (XmlWriter writer = XmlWriter.Create(builder))
                {
                    var dataContractSerializer = new DataContractSerializer(obj.GetType());

                    // Serialize from an object to a stream
                    dataContractSerializer.WriteObject(writer, obj);
                }

                return builder.ToString();
            }

            return null;
        }

        /// <summary>
        ///     Deserializes an instance of a type into an object from XML stream
        /// </summary>
        /// <param name="xml">The XLM as string</param>
        /// <typeparam name="TObject">Object type to de-serialize</typeparam>
        /// <returns>The XML string</returns>
        public static TObject DeserializeFromDataContract<TObject>(this string xml) where TObject : class
        {
            if (!string.IsNullOrEmpty(xml))
            {
                using (XmlReader reader = XElement.Parse(xml).CreateReader())
                {
                    var dataContractSerializer = new DataContractSerializer(typeof(TObject));

                    // Serialize from an object to a stream
                    return (TObject)dataContractSerializer.ReadObject(reader);
                }
            }

            return default(TObject);
        }

        /// <summary>
        ///     Deserializes an instance of a type into an object from XML stream
        /// </summary>
        /// <param name="xml">The XML as string</param>
        /// <param name="type">The type of the object.</param>
        /// <returns>The Xml string</returns>
        public static object DeserializeFromDataContract(this string xml, Type type)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                using (XmlReader reader = XElement.Parse(xml).CreateReader())
                {
                    var dataContractSerializer = new DataContractSerializer(type);

                    // Serialize from an object to a stream
                    return dataContractSerializer.ReadObject(reader);
                }
            }

            return null;
        }

        /// <summary>
        ///     Serialize Object to the Xml string
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="type">The object Type</param>
        /// <returns>The Xml string</returns>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.</exception>
        public static string SerializeToXml(this object obj, Type type)
        {
            if (obj != null)
            {
                var builder = new StringBuilder(4096);

                using (var writer = new StringWriter(builder))
                {
                    var xmlserializer = new XmlSerializer(type);
                    xmlserializer.Serialize(writer, obj, new XmlSerializerNamespaces());
                }

                return builder.ToString();
            }

            return null;
        }

        /// <summary>
        ///     Deserialize Object from XML
        /// </summary>
        /// <typeparam name="TObject">Generic type to de-serialize to</typeparam>
        /// <param name="xml">The xml in string format</param>
        /// <returns>The instance of new TObject</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     An error occurred during deserialization. The original exception is available
        /// using the System.Exception.InnerException property.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "No restriction on object type")]
        public static TObject DeserializeFromXml<TObject>(this string xml) where TObject : class
        {
            if (!string.IsNullOrEmpty(xml))
            {
                Type type = typeof(TObject);
                var serializer = new XmlSerializer(type);

                using (var reader = new StringReader(xml))
                {
                    return (TObject)serializer.Deserialize(reader);
                }
            }

            return default(TObject);
        }

        /// <summary>
        /// Deserializes from XML file.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="filepath">The virtual path.</param>
        /// <param name="virtualpath">if set to <c>true</c> [virtualpath].</param>
        /// <returns>
        /// The deserialized object instance or null
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "No restriction on object type")]
        public static TObject DeserializeFromXmlFile<TObject>(this string filepath, bool virtualpath = true) where TObject : class
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                if (virtualpath)
                {
                    filepath = filepath.MapPath();
                }

                if (File.Exists(filepath))
                {
                    byte[] data = File.ReadAllBytes(filepath);

                    return data.DeserializeFromXmlBytes<TObject>();
                }
            }

            return default(TObject);
        }

        /// <summary>
        ///     Deserialize Object from XML byte array
        /// </summary>
        /// <typeparam name="TObject">Generic type to de-serialize to</typeparam>
        /// <param name="xml">The xml in byte array format</param>
        /// <returns>The instance of new TObject</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     An error occurred during deserialization. The original exception is available
        /// using the System.Exception.InnerException property.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "No restriction on object type")]
        public static TObject DeserializeFromXmlBytes<TObject>(this byte[] xml) where TObject : class
        {
            if (xml != null)
            {
                Type type = typeof(TObject);
                var serializer = new XmlSerializer(type);

                using (var reader = new MemoryStream(xml))
                {
                    return (TObject)serializer.Deserialize(reader);
                }
            }

            return default(TObject);
        }

        /// <summary>
        ///     Deserialize Object from XML
        /// </summary>
        /// <param name="xml">The xml in string format</param>
        /// <param name="type">The Type of the deserialized object</param>
        /// <returns>The instance of new TObject</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     An error occurred during deserialization. The original exception is available
        /// using the System.Exception.InnerException property.
        /// </exception>
        public static object DeserializeFromXml(this string xml, Type type)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                var serializer = new XmlSerializer(type);
                using (var reader = new StringReader(xml))
                {
                    return serializer.Deserialize(reader);
                }
            }

            return null;
        }

        #region Serialization

        /// <summary>
        ///     Deserialize object from byte array
        /// </summary>
        /// <typeparam name="TObject">The generic object type</typeparam>
        /// <param name="bytes">The object instance as byte array</param>
        /// <returns>The instance of new TObject</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It's converts the object stored in byte array tp specific user object.")]
        public static TObject DeserializeFromBinary<TObject>(this byte[] bytes)
        {
            if (bytes != null)
            {
                using (var memorystream = new MemoryStream(bytes))
                {
                    var formatter = new BinaryFormatter();
                    return (TObject)formatter.Deserialize(memorystream);
                }
            }

            return default(TObject);
        }

        /// <summary>
        ///     Serialize object to byte array
        /// </summary>
        /// <param name="serializableobject">The serializable object</param>
        /// <returns>The object instance as byte array</returns>
        public static byte[] SerializeToBinary(this object serializableobject)
        {
            if (serializableobject != null)
            {
                // Make initial size 4kb
                using (var memorystream = new MemoryStream(4096))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(memorystream, serializableobject);

                    return memorystream.ToArray();
                }
            }

            return null;
        }

        #endregion
    }
}
