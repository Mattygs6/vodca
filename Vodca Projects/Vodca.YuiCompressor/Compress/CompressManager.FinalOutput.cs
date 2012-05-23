//-----------------------------------------------------------------------------
// <copyright file="CompressManager.FinalOutput.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------

namespace Vodca.YuiCompressor
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class CompressManager
    {
        /// <summary>
        /// Generates final the output.
        /// </summary>
        /// <param name="args">The args.</param>
        public void FinalOutput(CompressFileGroupArgs args)
        {
            /* Allocate 128Kb*/
            var builder = new StringBuilder(8096 * 16);

            builder.AppendFormat("/** UTC Date: {0}", DateTime.UtcNow).AppendLine();
            builder.AppendLine(" *  Summary:");
            foreach (var file in args.Files)
            {
                builder.Append(" *      ").Append(file.FileName).Append(" => [Action: ").Append(
                    file.CompressorOutputStreamAction).Append("ed ],").Append("[Modified: ").Append(
                        file.FileInfo.LastWriteTimeUtc).Append(" ],").AppendLine();
            }

            builder.AppendLine(" */");

            foreach (var file in args.Files)
            {
                builder.AppendLine();

                builder.Append("/* ").Append(file.FileName).AppendLine(" */");

                builder.Append(file.Content);

                builder.AppendLine();
            }

            args.Output = builder.ToString();
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="args">The args.</param>
        public void SaveFile(CompressFileGroupArgs args)
        {
            string file = this.CurrentDirectory + args.XmlFileGroup.MinifiedFileName;
            string folder = Path.GetDirectoryName(file);

            /* ReSharper disable AssignNullToNotNullAttribute */

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            /* ReSharper restore AssignNullToNotNullAttribute */

            File.WriteAllText(file, args.Output);
        }
    }
}