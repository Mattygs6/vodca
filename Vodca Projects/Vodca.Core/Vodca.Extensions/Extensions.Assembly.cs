//-----------------------------------------------------------------------------
// <copyright file="Extensions.Assembly.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/14/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Gets the XML document from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="assemblyxmlfile">The xml file embedded in the assembly.</param>
        /// <returns>The Xml document</returns>
        public static XmlDocument GetXmlDocumentFromAssembly(this Assembly assembly, string assemblyxmlfile)
        {
            if (assembly != null && !string.IsNullOrWhiteSpace(assemblyxmlfile))
            {
                using (Stream stream = assembly.GetManifestResourceStream(assemblyxmlfile))
                {
                    // ReSharper disable PossibleNullReferenceException
                    // ReSharper disable AssignNullToNotNullAttribute
                    Ensure.IsNotNull(stream, "stream != null");
                    // ReSharper restore AssignNullToNotNullAttribute
                    var bytes = new byte[stream.Length];
                    // ReSharper restore PossibleNullReferenceException
                    stream.Read(bytes, 0, (int)stream.Length);

                    var document = new XmlDocument();
                    document.Load(stream);

                    return document;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the XDocument from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="assemblyxmlfile">The xml file embedded in the assembly.</param>
        /// <returns>The XDocument instance</returns>
        public static XDocument GetXDocumentFromAssembly(this Assembly assembly, string assemblyxmlfile)
        {
            if (assembly != null && !string.IsNullOrWhiteSpace(assemblyxmlfile))
            {
                using (Stream stream = assembly.GetManifestResourceStream(assemblyxmlfile))
                {
                    /* ReSharper disable AssignNullToNotNullAttribute */
                    Ensure.IsNotNull(stream, "stream != null");

                    XmlReader xmlreader = XmlReader.Create(stream);
                    /* ReSharper restore AssignNullToNotNullAttribute */

                    return XDocument.Load(xmlreader);
                }
            }

            return null;
        }

        /// <summary>
        ///     Gets the file bytes from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="assemblyfile">The file embedded in the assembly.</param>
        /// <returns>The file bytes</returns>
        public static byte[] GetFileBytesFromAssembly(this Assembly assembly, string assemblyfile)
        {
            if (assembly != null && !string.IsNullOrWhiteSpace(assemblyfile))
            {
                using (Stream stream = assembly.GetManifestResourceStream(assemblyfile))
                {
                    // ReSharper disable PossibleNullReferenceException
                    var bytes = new byte[stream.Length];
                    // ReSharper restore PossibleNullReferenceException
                    stream.Read(bytes, 0, (int)stream.Length);

                    return bytes;
                }
            }

            return null;
        }
    }
}
