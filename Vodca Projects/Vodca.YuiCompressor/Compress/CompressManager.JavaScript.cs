//-----------------------------------------------------------------------------
// <copyright file="CompressManager.JavaScript.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.YuiCompressor
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;
    using Yahoo.Yui.Compressor;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class CompressManager
    {
        /// <summary>
        /// The Js Pipeline
        /// </summary>
        public readonly CompressPipeline JsPipeline;

        /// <summary>
        /// The JS  'console.log' regex
        /// </summary>
        private static readonly Regex JsConsoleLog = new Regex(@"\s{0,}console.log\(.{1,}\);{0,1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// The JS  'VForms.common.log' regex
        /// </summary>
        private static readonly Regex JsVodcaConsoleLog = new Regex(@"\s{0,}VForms.common.log\(.{1,}\);{0,1}", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// The pipeline to remove console log.
        /// </summary>
        /// <param name="args">The args.</param>
        public void JsPipelineConsoleLog(CompressFileGroupArgs args)
        {
            if (args.IsRelease)
            {
                foreach (var file in args.Files)
                {
                    if (file.CompressorOutputStreamAction == CompressorOutputStreamAction.Compress)
                    {
                        file.Content = JsConsoleLog.Replace(file.Content, string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// The pipeline to remove vodca console log.
        /// </summary>
        /// <param name="args">The args.</param>
        public void JsPipelineVodcaConsoleLog(CompressFileGroupArgs args)
        {
            if (args.IsRelease)
            {
                foreach (var file in args.Files)
                {
                    if (file.CompressorOutputStreamAction == CompressorOutputStreamAction.Compress)
                    {
                        file.Content = JsVodcaConsoleLog.Replace(file.Content, string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// The JS file compressing.
        /// </summary>
        /// <param name="args">The args.</param>
        public void JsFileCompress(CompressFileGroupArgs args)
        {
            if (args.IsRelease)
            {
                foreach (var file in args.Files)
                {
                    if (file.CompressorOutputStreamAction == CompressorOutputStreamAction.Compress)
                    {
                        file.Content = JavaScriptCompressor.Compress(
                            file.Content,
                            isVerboseLogging: false,
                            isObfuscateJavascript: false,
                            preserveAllSemicolons: true,
                            disableOptimizations: true,
                            lineBreakPosition: 120);
                    }
                }
            }
        }
    }
}