//-----------------------------------------------------------------------------
// <copyright file="CompressorAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.YuiCompressor
{
    using System;

    /// <summary>
    /// The Cpmpression Action
    /// </summary>
    [Serializable]
    public enum CompressorAction : byte
    {
        /// <summary>
        /// How to compress flag
        /// </summary>
        JsCompression = 0,

        /// <summary>
        /// How to compress flag
        /// </summary>
        CssCompression
    }
}
