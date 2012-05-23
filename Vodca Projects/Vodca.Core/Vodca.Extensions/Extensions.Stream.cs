//-----------------------------------------------------------------------------
// <copyright file="Extensions.Stream.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/23/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.IO;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The converted stream to string</returns>
        public static string ConvertToString(this Stream stream)
        {
            if (stream != null && stream.Length > 0)
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Copies the stream.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        public static void CopyStream(this Stream input, Stream output)
        {
            var buffer = new byte[8192];

            bool hasmore;
            do
            {
                int read = input.Read(buffer, 0, buffer.Length);
                hasmore = read > 0;

                if (hasmore)
                {
                    output.Write(buffer, 0, read);
                }
            }
            while (hasmore);
        }

        /// <summary>
        /// Converts stream to the byte array.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The byte array from stream</returns>
        /// <remarks>This method designed for the Web uploaded files up to 20MB</remarks>
        public static byte[] ToByteArray(this Stream stream)
        {
            /* check readable before reading streams */
            if (stream != null && stream.CanRead)
            {
                using (var binaryreader = new BinaryReader(stream))
                {
                    return binaryreader.ReadBytes((int)stream.Length);
                }
            }

            return null;
        }
    }
}
