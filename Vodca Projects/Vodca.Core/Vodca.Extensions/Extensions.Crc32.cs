//-----------------------------------------------------------------------------
// <copyright file="Extensions.Crc32.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/12/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Computes the CRC32 value.
        /// </summary>
        /// <param name="data">The data in byte format.</param>
        /// <returns>The CRC32 value</returns>
        [CLSCompliant(false)]
        public static string ComputeCrc32(this byte[] data)
        {
            if (data != null)
            {
                using (var crc32 = new Crc32())
                {
                    var builder = new StringBuilder();

                    foreach (byte b in crc32.ComputeHash(data))
                    {
                        builder.Append(b.ToString("x2"));
                    }

                    return builder.ToString().ToLowerInvariant();
                }
            }

            return string.Empty;
        }

        /// <summary>
        ///     Computes the CRC32 value.
        /// </summary>
        /// <param name="data">The data in string format.</param>
        /// <returns>The CRC32 value</returns>
        [CLSCompliant(false)]
        public static string ComputeCrc32(this string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                var bytes = data.ToBytes();

                return bytes.ComputeCrc32();
            }

            return string.Empty;
        }
    }
}
