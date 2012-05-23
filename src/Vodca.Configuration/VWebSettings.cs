//-----------------------------------------------------------------------------
// <copyright file="VWebSettings.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/03/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Web.Hosting;
    using System.Xml.Serialization;

    /// <summary>
    ///    This is base class for all WebSetting objects. 
    /// This class allows to store and retrieve the unique single instance objects for the website. 
    /// </summary>
    /// <typeparam name="TObject">The generic type for deserialization</typeparam>
    /// <remarks>
    /// <pre>
    ///     The Caching mechanism is implemented. 
    /// Also the path will be:
    ///     string path = HostingEnvironment.MapPath(string.Concat("~/App_Settings/", Type.FullName, ".config"));
    /// where
    ///     Type.FullName is equal Namespace.ClassName.
    /// </pre>
    /// </remarks>
    /// <example>
    /// <code title="C# File" lang="C#">
    /// public class Address : VWebSettings<![CDATA[<Address>]]>
    /// {
    ///     public string CompanyName { get; set; }
    ///     public string AddressLine1 { get; set; }
    ///     public string Phone { get; set; }
    ///     public string Fax { get; set; }
    /// }
    /// // Mimic input from ASP.NET form
    /// Address address = new Address();
    /// address.CompanyName = "Genuine";
    /// address.AddressLine1 = "500 Harrison";
    /// address.Phone = "508-698-6669";
    /// address.Fax = "508-699-5555";
    /// // Save to App_Settings folder as '~/App_Settings/Website.Address.config'
    /// address.SerializeAndSave(); 
    /// // Deserialize
    /// <![CDATA[Address address2 = Address.ReadAndDeserialize();]]>
    /// -------------------------------------------------------------------------------------------
    /// Website.Address.config file output:
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8"?>
    /// <Address xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    ///   <CompanyName>Genuine</CompanyName>
    ///   <AddressLine1>500 Harrison</AddressLine1>
    ///   <Phone>508-698-6669</Phone>
    ///   <Fax>508-699-5555</Fax>
    /// </Address>
    /// ]]>
    /// </code>
    /// <code source="..\Vodca.Core\Vodca.Configuration\VWebSettings.cs" title="VWebSettings.cs" lang="C#" />
    /// </example>
    [Serializable]
    [DataContract]
    public class VWebSettings<TObject> where TObject : VWebSettings<TObject>, new()
    {
        /// <summary>
        /// The descendant class object
        /// </summary>
        private static readonly TObject Descendant = new TObject();

        /// <summary>
        ///     The file Virtual folder
        /// </summary>
        private const string FileFolder = "/App_Config/Vodca.Settings/";

        /// <summary>
        ///     The File extension
        /// </summary>
        private const string FileExtension = ".config";

        /// <summary>
        ///     Tread Safe synchronizing object
        /// </summary>
        /* ReSharper disable StaticFieldInGenericType */
        private static readonly object SyncRoot = new object();
        /* ReSharper restore StaticFieldInGenericType */

        /// <summary>
        ///     The File cache
        /// </summary>
        private static TObject cache;

        /// <summary>
        /// Gets the settings file path.
        /// </summary>
        public static string SettingsFilePath
        {
            get
            {
                return GetFilePath(typeof(TObject));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="VWebSettings{TObject}"/> is file exists.
        /// </summary>
        /// <value>
        ///     <c>true</c> if exists; otherwise, <c>false</c>.
        /// </value>
        public static bool FileExists
        {
            get
            {
                var path = GetFilePath(typeof(TObject));
                return File.Exists(path);
            }
        }

        /// <summary>
        /// Reads the config.
        /// </summary>
        /// <returns>The config object</returns>
        /// <example>
        /// <code title="C# File" lang="C#">
        /// public class Address : VWebSettings<![CDATA[<Address>]]>
        /// {
        ///     public string CompanyName { get; set; }
        ///     public string AddressLine1 { get; set; }
        ///     public string Phone { get; set; }
        ///     public string Fax { get; set; }
        /// }
        /// // Mimic input from ASP.NET form
        /// Address address = new Address();
        /// address.CompanyName = "Genuine";
        /// address.AddressLine1 = "500 Harrison";
        /// address.Phone = "508-698-6669";
        /// address.Fax = "508-699-5555";
        /// // Save to App_Settings folder '~/App_Settings/Website.Address.config'
        /// address.SerializeAndSave(); 
        /// // Deserialize
        /// <![CDATA[Address address2 = Address.ReadAndDeserialize();]]>
        /// </code>
        /// </example>
        public static TObject ReadConfig()
        {
            if (cache == null)
            {
                Type type = typeof(TObject);

                // Lock the file
                lock (SyncRoot)
                {
                    // Create a TextReader to read the file.
                    string path = GetFilePath(type);

                    if (File.Exists(path))
                    {
                        StringReader reader = null;
                        try
                        {
                            string xml = File.ReadAllText(path);

                            var serializer = new XmlSerializer(type);
                            reader = new StringReader(xml);
                            cache = (TObject)serializer.Deserialize(reader);
                        }
                        catch (Exception exception)
                        {
                            exception.LogException();
                        }
                        finally
                        {
                            if (reader != null)
                            {
                                reader.Dispose();
                            }
                        }
                    }
                }
            }

            return cache;
        }

        /// <summary>
        ///     Serializes the specified VWebSettings and writes the XML document to a file in App_Settings folder
        /// </summary>
        /// <remarks>
        ///     The file will be written to '~/App_Settings/Type-FullName.config'
        /// </remarks>
        /// <example>
        /// <code title="C# File" lang="C#">
        /// public class Address : VWebSettings<![CDATA[<Address>]]>
        /// {
        ///     public string CompanyName { get; set; }
        ///     public string AddressLine1 { get; set; }
        ///     public string Phone { get; set; }
        ///     public string Fax { get; set; }
        /// }
        /// // Mimic input from ASP.NET form
        /// Address address = new Address();
        /// address.CompanyName = "Genuine";
        /// address.AddressLine1 = "500 Harrison";
        /// address.Phone = "508-698-6669";
        /// address.Fax = "508-699-5555";
        /// // Save to App_Settings folder '~/App_Settings/Website.Address.config'
        /// address.SerializeAndSave(); 
        /// // Deserialize
        /// <![CDATA[Address address2 = Address.ReadAndDeserialize();]]>
        /// </code>
        /// </example> 
        public virtual void SerializeAndSave()
        {
            Type type = this.GetType();
            string path = GetFilePath(type);

            FileStream filewriter = null;
            try
            {
                FileFolder.EnsureFolderExistsOrCreate();
                filewriter = new FileStream(path, FileMode.Create, FileAccess.Write); // bugfix: when using FileMode.OpenOrCreate and writing less lines than currently existing, extra lines were left behind creating an invalid xml file.

                var xmlserializer = new XmlSerializer(type);

                var xmlnamespace = new XmlSerializerNamespaces();
                xmlnamespace.Add(string.Empty, string.Empty);

                xmlserializer.Serialize(filewriter, this, xmlnamespace);
            }
            finally
            {
                if (filewriter != null)
                {
                    filewriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <returns>The folder path</returns>
        public virtual string GetFolderPath()
        {
            return FileFolder;
        }

        /// <summary>
        ///     Gets the file path.
        /// </summary>
        /// <param name="type">The object type.</param>
        /// <returns>The file path</returns>
        private static string GetFilePath(Type type)
        {
            return HostingEnvironment.MapPath(string.Concat(ResolveFolderPath(), type.FullName, FileExtension));
        }

        /// <summary>
        /// Resolves the folder path.
        /// </summary>
        /// <returns>The folder path</returns>
        private static string ResolveFolderPath()
        {
            var name = Descendant.GetFolderPath();
            if (!string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            return FileFolder;
        }
    }
}