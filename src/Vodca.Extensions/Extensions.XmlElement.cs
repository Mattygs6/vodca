//-----------------------------------------------------------------------------
// <copyright file="Extensions.XmlElement.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Xml;
    using System.Xml.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Converts an XDocument to an XmlDocument.
        /// </summary>
        /// <param name="xdoc">The XDocument to convert.</param>
        /// <returns>The equivalent XmlDocument.</returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlElement.cs" title="C# Source File" lang="C#" />
        public static XmlDocument ToXmlDocument(this XDocument xdoc)
        {
            if (xdoc != null)
            {
                var xmldoc = new XmlDocument();
                xmldoc.Load(xdoc.CreateReader());

                return xmldoc;
            }

            return null;
        }

        /// <summary>
        /// Converts an XmlDocument to an XDocument.
        /// </summary>
        /// <param name="xmldoc">The XmlDocument to convert.</param>
        /// <returns>The equivalent XDocument.</returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlElement.cs" title="C# Source File" lang="C#" />
        public static XDocument ToXDocument(this XmlDocument xmldoc)
        {
            if (xmldoc != null)
            {
                return XDocument.Load(xmldoc.CreateNavigator().ReadSubtree());
            }

            return null;
        }

        /// <summary>
        /// Converts an XElement to an XmlElement.
        /// </summary>
        /// <param name="xelement">The XElement to convert.</param>
        /// <returns>The equivalent XmlElement.</returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlElement.cs" title="C# Source File" lang="C#" />
        public static XmlElement ToXmlElement(this XElement xelement)
        {
            if (xelement != null)
            {
                return new XmlDocument().ReadNode(xelement.CreateReader()) as XmlElement;
            }

            return null;
        }

        /// <summary>
        /// Converts an XmlElement to an XElement.
        /// </summary>
        /// <param name="xmlelement">The XmlElement to convert.</param>
        /// <returns>The equivalent XElement.</returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.XmlElement.cs" title="C# Source File" lang="C#" />
        public static XElement ToXElement(this XmlElement xmlelement)
        {
            if (xmlelement != null)
            {
                return XElement.Load(xmlelement.CreateNavigator().ReadSubtree());
            }

            return null;
        }
    }
}
