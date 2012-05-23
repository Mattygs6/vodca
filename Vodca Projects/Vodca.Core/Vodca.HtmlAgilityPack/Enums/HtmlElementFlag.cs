//-----------------------------------------------------------------------------
// <copyright file="HtmlElementFlag.cs" company="genuine">
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

    /// <summary>
    /// Flags that describe the behavior of an Element node.
    /// </summary>
    [Flags]
    public enum HtmlElementFlag
    {
        /// <summary>
        ///   The node is a CDATA node.
        /// </summary>
        CData = 1,

        /// <summary>
        ///   The node is empty. META or IMG are example of such nodes.
        /// </summary>
        Empty = 2,

        /// <summary>
        ///   The node will automatically be closed during parsing.
        /// </summary>
        Closed = 4,

        /// <summary>
        ///   The node can overlap.
        /// </summary>
        CanOverlap = 8
    }
}