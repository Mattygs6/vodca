//-----------------------------------------------------------------------------
// <copyright file="Extensions.Compression.GZip.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.IO;
    using System.IO.Compression;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     The GZIP compress.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <returns>The compressed data</returns>
        public static byte[] GZipCompress(this byte[] data)
        {
            byte[] bytes = null;
            if (data != null)
            {
                using (var output = new MemoryStream(data.Length))
                {
                    var gzip = new GZipStream(output, CompressionMode.Compress);
                    gzip.Write(data, 0, data.Length);

                    bytes = output.ToArray();
                }
            }

            return bytes;
        }

        /// <summary>
        ///     The GZIP decompress.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <returns>The decompressed data</returns>
        public static byte[] GZipDecompress(this byte[] data)
        {
            byte[] bytes = null;

            if (data != null)
            {
                using (var input = new MemoryStream(data.Length))
                {
                    input.Write(data, 0, data.Length);
                    input.Position = 0;

                    var gzip = new GZipStream(input, CompressionMode.Decompress);

                    using (var output = new MemoryStream(data.Length))
                    {
                        var buff = new byte[64];
                        int read = gzip.Read(buff, 0, buff.Length);

                        while (read > 0)
                        {
                            output.Write(buff, 0, buff.Length);
                            read = gzip.Read(buff, 0, buff.Length);
                        }

                        bytes = output.ToArray();
                    }
                }
            }

            return bytes;
        }
    }
}
