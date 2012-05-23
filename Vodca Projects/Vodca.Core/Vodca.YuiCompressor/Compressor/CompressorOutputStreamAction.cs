//-----------------------------------------------------------------------------
// <copyright file="CompressorOutputStreamAction.cs" company="genuine">
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
    /// The stream output flags
    /// </summary>
    [Serializable]
    public enum CompressorOutputStreamAction : byte
    {
        /// <summary>
        /// The flag how to write stream
        /// </summary>
        Compress = 0,

        /// <summary>
        /// The flag how to write stream
        /// </summary>
        Append = 1
    }
}
