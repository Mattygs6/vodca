//-----------------------------------------------------------------------------
// <copyright file="HtmlParseErrorCode.cs" company="genuine">
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
    /// Represents the type of parsing error.
    /// </summary>
    public enum HtmlParseErrorCode
    {
        /// <summary>
        ///   A tag was not closed.
        /// </summary>
        TagNotClosed,

        /// <summary>
        ///   A tag was not opened.
        /// </summary>
        TagNotOpened,

        /// <summary>
        ///   There is a character set mismatch between stream and declared (META) encoding.
        /// </summary>
        CharsetMismatch,

        /// <summary>
        ///   An end tag was not required.
        /// </summary>
        EndTagNotRequired,

        /// <summary>
        ///   An end tag is invalid at this position.
        /// </summary>
        EndTagInvalidHere
    }
}