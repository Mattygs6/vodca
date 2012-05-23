//-----------------------------------------------------------------------------
// <copyright file="Extensions.Xslt.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/22/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using System.Xml.Xsl;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// XSLTs the compiled transform.
        /// </summary>
        /// <param name="data">The data object.</param>
        /// <param name="virtualxsltpath">The virtual XSLT path.</param>
        /// <returns>The output as html string</returns>
        public static string XsltCompiledTransform(this IToXElement data, string virtualxsltpath)
        {
            return data != null ? XsltCompiledTransform(data.ToXElement(data.GetType().Name), virtualxsltpath) : string.Empty;
        }

        /// <summary>
        /// XSLs the compiled transform.
        /// </summary>
        /// <param name="data">The xml serializable data object.</param>
        /// <param name="virtualxsltpath">The virtual XSLT path.</param>
        /// <returns>The output as html string</returns>
        public static string XsltCompiledTransform(this object data, string virtualxsltpath)
        {
            if (data != null && !string.IsNullOrWhiteSpace(virtualxsltpath))
            {
                /* Create instance of XstTransform object */
                XslCompiledTransform transform = virtualxsltpath.LoadXslt();

                return XsltTransform(data, transform);
            }

            return string.Empty;
        }

        /// <summary>
        /// XSLS the compiled transform.
        /// </summary>
        /// <param name="data">The XElement data.</param>
        /// <param name="virtualxsltpath">The virtual XSLT path.</param>
        /// <returns>The Xml/XSLT output</returns>
        public static string XsltCompiledTransform(this XElement data, string virtualxsltpath)
        {
            return data.XsltCompiledTransform(virtualxsltpath, null);
        }

        /// <summary>
        /// XSLT the compiled transform.
        /// </summary>
        /// <param name="data">The XElement.</param>
        /// <param name="virtualxsltpath">The virtual XSLT path.</param>
        /// <param name="arguments">The XSLT arguments.</param>
        /// <returns>The Xml/XSLT output</returns>
        public static string XsltCompiledTransform(this XElement data, string virtualxsltpath, XsltArgumentList arguments)
        {
            if (data != null && !string.IsNullOrWhiteSpace(virtualxsltpath))
            {
                /* Create instance of XstTransform object */
                XslCompiledTransform transform = virtualxsltpath.LoadXslt();
                if (transform != null)
                {
                    return XsltTransform(data, arguments, transform);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// XSLTs the compiled transform.
        /// </summary>
        /// <param name="data">The serializable data object.</param>
        /// <param name="assembly">The assembly.</param>
        /// <param name="assemblyfile">The assembly file.</param>
        /// <returns>The XML/XSLT output</returns>
        public static string XsltCompiledTransform(this object data, Assembly assembly, string assemblyfile)
        {
            if (data != null && assembly != null && !string.IsNullOrWhiteSpace(assemblyfile))
            {
                var stream = assembly.GetManifestResourceStream(assemblyfile);
                // ReSharper disable AssignNullToNotNullAttribute
                Ensure.IsNotNull(stream, "stream != null");

                using (XmlReader xsltreader = XmlReader.Create(stream))
                // ReSharper restore AssignNullToNotNullAttribute
                {
                    /* Create instance of XstTransform object */
                    var transform = new XslCompiledTransform();
                    transform.Load(xsltreader);

                    return XsltTransform(data, transform);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// XSLTs the compiled transform.
        /// </summary>
        /// <param name="data">The serializable data object.</param>
        /// <param name="transform">The transform.</param>
        /// <returns>The XML/XSLT output</returns>
        public static string XsltTransform(object data, XslCompiledTransform transform)
        {
            if (data != null && transform != null)
            {
                var settings = new XmlWriterSettings
                                   {
                                       Indent = false,
                                       NewLineHandling = NewLineHandling.None,
                                       OmitXmlDeclaration = false,
                                       CheckCharacters = true,
                                   };

                var stringbuilder = new StringBuilder(1024);
                using (XmlWriter writer = XmlWriter.Create(stringbuilder, settings))
                {
                    var xmlserializer = new XmlSerializer(data.GetType());

                    var xmlnamespace = new XmlSerializerNamespaces();
                    xmlnamespace.Add(string.Empty, string.Empty);

                    xmlserializer.Serialize(writer, data, xmlnamespace);
                }

                using (var stringreader = new StringReader(stringbuilder.ToString()))
                {
                    var reader = XmlReader.Create(stringreader);
                    using (var memorywriter = new StringWriter())
                    {
                        // transform the XML file
                        transform.Transform(reader, null, memorywriter);
                        return memorywriter.ToString();
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// XSLTs the compiled transform.
        /// </summary>
        /// <param name="data">The serializable data object.</param>
        /// <param name="arguments">The XSLT arguments.</param>
        /// <param name="transform">The transform.</param>
        /// <returns>The XML/XSLT output</returns>
        public static string XsltTransform(XElement data, XsltArgumentList arguments, XslCompiledTransform transform)
        {
            if (data != null && transform != null)
            {
                var xml = new StringBuilder(4096);

                using (XmlReader reader = data.CreateReader())
                {
                    using (var memorywriter = new StringWriter(xml))
                    {
                        /* transform the XML file */
                        transform.Transform(reader, arguments, memorywriter);
                    }
                }

                return xml.ToString();
            }

            return string.Empty;
        }
    }
}
