//-----------------------------------------------------------------------------
// <copyright file="CompressPipeline.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.YuiCompressor
{
    /// <summary>
    /// The Compressor delegate
    /// </summary>
    /// <param name="args">The args.</param>
    public delegate void CompressPipeline(CompressFileGroupArgs args);
}