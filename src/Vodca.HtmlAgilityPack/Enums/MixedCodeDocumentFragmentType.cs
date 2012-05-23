//-----------------------------------------------------------------------------
// <copyright file="MixedCodeDocumentFragmentType.cs" company="genuine">
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
    /// Represents the type of fragment in a mixed code document.
    /// </summary>
    public enum MixedCodeDocumentFragmentType
    {
        /// <summary>
        ///   The fragment contains code.
        /// </summary>
        Code,

        /// <summary>
        ///   The fragment contains text.
        /// </summary>
        Text,
    }
}