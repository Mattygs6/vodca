//-----------------------------------------------------------------------------
// <copyright file="HtmlNode.Xpath.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    using System;
    using System.Collections.Generic;
    using System.Xml.XPath;

    /// <summary>
    /// The html node.
    /// </summary>
    public partial class HtmlNode : IXPathNavigable
    {
        /// <summary>
        /// Creates a new XPathNavigator object for navigating this HTML node.
        /// </summary>
        /// <returns>
        /// An XPathNavigator object. The XPathNavigator is positioned on the node from which the method was called. It is not positioned on the root of the document. 
        /// </returns>
        public XPathNavigator CreateNavigator()
        {
            return new HtmlNodeNavigator(this.OwnerDocument, this);
        }

        /// <summary>
        /// Creates an XPathNavigator using the root of this document.
        /// </summary>
        /// <returns>The XPath Navigator</returns>
        public XPathNavigator CreateRootNavigator()
        {
            return new HtmlNodeNavigator(this.OwnerDocument, this.OwnerDocument.DocumentNode);
        }

        /// <summary>
        /// Selects a list of nodes matching the <see cref="XPath"/> expression.
        /// </summary>
        /// <param name="xpath">
        /// The XPath expression. 
        /// </param>
        /// <returns>
        /// An <see cref="HtmlNodeCollection"/> containing a collection of nodes matching the <see cref="XPath"/> query, or <c>null</c> if no node matched the XPath expression. 
        /// </returns>
        public IEnumerable<HtmlNode> SelectNodes(string xpath)
        {
            var nav = new HtmlNodeNavigator(this.OwnerDocument, this);
            XPathNodeIterator it = nav.Select(xpath);
            while (it.MoveNext())
            {
                var n = (HtmlNodeNavigator)it.Current;
                if (n != null)
                {
                    yield return n.CurrentNode;
                }
            }
        }

        /// <summary>
        /// Selects the first XmlNode that matches the XPath expression.
        /// </summary>
        /// <param name="xpath">
        /// The XPath expression. May not be null. 
        /// </param>
        /// <returns>
        /// The first <see cref="HtmlNode"/> that matches the XPath query or a null reference if no matching node was found. 
        /// </returns>
        public HtmlNode SelectSingleNode(string xpath)
        {
            if (xpath == null)
            {
                throw new ArgumentNullException("xpath");
            }

            var nav = new HtmlNodeNavigator(this.OwnerDocument, this);
            XPathNodeIterator it = nav.Select(xpath);
            if (!it.MoveNext())
            {
                return null;
            }

            var node = (HtmlNodeNavigator)it.Current;
            if (node != null)
            {
                return node.CurrentNode;
            }

            return null;
        }
    }
}