//-----------------------------------------------------------------------------
// <copyright file="HtmlNodeType.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    /// <summary>
    /// Represents the type of a node.
    /// </summary>
    public enum HtmlNodeType
    {
        /// <summary>
        ///   The root of a document.
        /// </summary>
        Document,

        /// <summary>
        ///   An HTML element.
        /// </summary>
        Element,

        /// <summary>
        ///   An HTML comment.
        /// </summary>
        Comment,

        /// <summary>
        ///   A text node is always the child of an element or a document node.
        /// </summary>
        Text,
    }
}