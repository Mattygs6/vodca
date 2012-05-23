//-----------------------------------------------------------------------------
// <copyright file="HtmlDocument.Xpath.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    using System.Xml.XPath;

    /// <summary>
    /// The html document.
    /// </summary>
    public partial class HtmlDocument : IXPathNavigable
    {
        /// <summary>
        /// Creates a new XPathNavigator object for navigating this HTML document.
        /// </summary>
        /// <returns>
        /// An XPathNavigator object. The XPathNavigator is positioned on the root of the document. 
        /// </returns>
        public XPathNavigator CreateNavigator()
        {
            return new HtmlNodeNavigator(this, this.documentnode);
        }
    }
}