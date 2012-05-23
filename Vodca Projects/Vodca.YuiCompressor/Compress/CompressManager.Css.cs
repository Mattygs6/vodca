//-----------------------------------------------------------------------------
// <copyright file="CompressManager.Css.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.YuiCompressor
{
    using System.Diagnostics.CodeAnalysis;
    using Yahoo.Yui.Compressor;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class CompressManager
    {
        /// <summary>
        /// The Css Pipeline
        /// </summary>
        public readonly CompressPipeline CssPipeline;

        /// <summary>
        /// The Css file compressing.
        /// </summary>
        /// <param name="args">The args.</param>
        public void CssFileCompress(CompressFileGroupArgs args)
        {
            if (args.IsRelease)
            {
                foreach (var file in args.Files)
                {
                    if (file.CompressorOutputStreamAction == CompressorOutputStreamAction.Compress)
                    {
                        file.Content = CssCompressor.Compress(
                            file.Content,
                            columnWidth: 120,
                            cssCompressionType: CssCompressionType.StockYuiCompressor,
                            removeComments: true);
                    }
                }
            }
        }
    }
}