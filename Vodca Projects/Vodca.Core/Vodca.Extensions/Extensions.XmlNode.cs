//-----------------------------------------------------------------------------
// <copyright file="Extensions.XmlNode.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       09/15/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics;
    using System.Xml;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Selects the single node inner text as string.
        /// </summary>
        /// <param name="node">The xml node.</param>
        /// <param name="xpath">The XPath expression.</param>
        /// <returns>The value of the inner text</returns>
        [DebuggerHidden]
        public static string SelectSingleNodeInnerText(this XmlNode node, string xpath)
        {
            if (node != null && !string.IsNullOrWhiteSpace(xpath))
            {
                var childnode = node.SelectSingleNode(xpath);
                if (childnode != null)
                {
                    return childnode.InnerText;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get specific attribute the from specified node.
        /// </summary>
        /// <param name="node">The xml node.</param>
        /// <param name="attributename">The attribute name.</param>
        /// <returns>The attribute value or empty string</returns>
        [DebuggerHidden]
        public static string Attributes(this XmlNode node, string attributename)
        {
            if (node != null && node.Attributes != null && !string.IsNullOrWhiteSpace(attributename))
            {
                var attribute = node.Attributes[attributename];
                if (attribute != null)
                {
                    return attribute.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets Xml node the named attribute item value.
        /// </summary>
        /// <param name="attributecollection">The attribute collection.</param>
        /// <param name="name">The attribute name.</param>
        /// <returns>The value of the attribute</returns>
        [DebuggerHidden]
        public static string GetNamedItemValue(this XmlAttributeCollection attributecollection, string name)
        {
            if (attributecollection != null && !string.IsNullOrWhiteSpace(name))
            {
                var attribute = attributecollection.GetNamedItem(name);
                if (attribute != null)
                {
                    return attribute.Value;
                }
            }

            return string.Empty;
        }
    }
}
