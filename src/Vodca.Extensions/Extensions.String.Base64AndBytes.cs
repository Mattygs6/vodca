//-----------------------------------------------------------------------------
// <copyright file="Extensions.String.Base64AndBytes.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Converts the string to a byte-array using the default encoding
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The created byte array</returns>
        public static byte[] ToBytes(this string value)
        {
            if (value != null)
            {
                return value.ToBytes(Encoding.Default);
            }

            return null;
        }

        /// <summary>
        /// Converts the string to a byte-array using the supplied encoding
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="encoding">The encoding to be used.</param>
        /// <returns>The created byte array</returns>
        /// <example>View code: <br /><code title="C# File" lang="C#">
        /// var value = "Hello World";
        /// var ansiBytes = value.ToBytes(Encoding.GetEncoding(1252)); // 1252 = ANSI
        /// var utf8Bytes = value.ToBytes(Encoding.UTF8);
        /// </code></example>
        public static byte[] ToBytes(this string value, Encoding encoding)
        {
            if (value != null)
            {
                encoding = encoding ?? Encoding.Default;
                return encoding.GetBytes(value);
            }

            return null;
        }

        /// <summary>
        ///     Encodes the input value to a Base64 string using the default encoding.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string EncodeBase64(this string value)
        {
            if (value != null)
            {
                return value.EncodeBase64(Encoding.UTF8);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Encodes the input value to a Base64 string using the supplied encoding.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The Base 64 encoded string</returns>
        public static string EncodeBase64(this string value, Encoding encoding)
        {
            if (value != null)
            {
                encoding = encoding ?? Encoding.UTF8;
                var bytes = encoding.GetBytes(value);

                return Convert.ToBase64String(bytes);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Decodes a Base 64 encoded value to a string using the default encoding.
        /// </summary>
        /// <param name="encodedValue">The Base 64 encoded value.</param>
        /// <returns>The decoded string</returns>
        public static string DecodeBase64(this string encodedValue)
        {
            if (encodedValue != null)
            {
                return encodedValue.DecodeBase64(Encoding.UTF8);
            }

            return string.Empty;
        }

        /// <summary>
        ///     Decodes a Base 64 encoded value to a string using the supplied encoding.
        /// </summary>
        /// <param name="encodedValue">The Base 64 encoded value.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The decoded string</returns>
        public static string DecodeBase64(this string encodedValue, Encoding encoding)
        {
            if (encodedValue != null)
            {
                encoding = encoding ?? Encoding.UTF8;

                var bytes = Convert.FromBase64String(encodedValue);

                return encoding.GetString(bytes);
            }

            return string.Empty;
        }
    }
}
