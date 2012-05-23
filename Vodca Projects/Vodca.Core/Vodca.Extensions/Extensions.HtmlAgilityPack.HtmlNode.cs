//-----------------------------------------------------------------------------
// <copyright file="Extensions.HtmlAgilityPack.HtmlNode.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//   Date:      12/08/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using HtmlAgilityPack;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the attribute value.
        /// </summary>
        /// <param name="htmlnode">The html node.</param>
        /// <param name="xpath">The XPath.</param>
        /// <param name="attributename">The attribute name.</param>
        /// <param name="attributevalue">The attribute value.</param>
        [CLSCompliant(false)]
        public static void SetAttributeValue(this HtmlNode htmlnode, string xpath, string attributename, string attributevalue)
        {
            if (htmlnode != null && !string.IsNullOrWhiteSpace(xpath) && !string.IsNullOrWhiteSpace(attributename))
            {
                var tag = htmlnode.SelectSingleNode(xpath);
                if (tag != null)
                {
                    var attr = tag.Attributes[attributename];
                    if (attr == null)
                    {
                        tag.Attributes.Add(attributename, attributevalue);
                    }
                    else
                    {
                        attr.Value = attributevalue;
                    }
                }
            }
        }
    }
}
