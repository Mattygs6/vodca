//-----------------------------------------------------------------------------
// <copyright file="Encryption.Decoder.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/30/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    ///     Decrypt strings for AuthenticationTicket, Drawings filename,...
    /// </summary>
    /// <example>View code: <br />
    /// <code lang="xml" title="web.config">
    /// <![CDATA[
    /// <appSettings>
    ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
    ///    <add key="VEncryption.Key" value="JBaltika"/>
    ///    <add key="VEncryption.Vector" value="IGenuine"/>
    /// </appSettings>
    /// ]]>
    /// </code>
    /// <code title="C# File" lang="C#">
    /// string encryptedstring = "Jimbo".EncryptDES(); // "BzyRhgicmO0="
    /// string dencryptedstring = encryptedstring.DecryptDES(); // "Jimbo"
    /// string encryptedstring = "Jimbo".Encrypt64(); // "ebrThgzZnIo="
    /// string dencryptedstring = encryptedstring.Decrypt64(); // "Jimbo"
    /// </code>
    /// <code source="..\Vodca.Core\Vodca.Encryption\VEncryption.Decoder.cs" title="VEncryption.Decoder.cs" lang="C#" />
    /// </example>
    public static partial class VEncryption
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Decrypts a string. The 8-bit string for decryption. Exactly 8 Char long key
        /// </summary>
        /// <param name="input">The encrypted string to be decrypted.</param>
        /// <returns>The plain text.</returns>
        /// <example>View code: <br />
        /// <code lang="xml" title="web.config">
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </code>
        /// <code title="C# File" lang="C#">
        /// string encryptedstring = "Jimbo".EncryptDES(); // "BzyRhgicmO0="
        /// string dencryptedstring = encryptedstring.DecryptDES(); // "Jimbo"
        /// </code>
        /// </example>
        public static string DecryptDES(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                byte[] cipherText = Convert.FromBase64String(input);

                return DecryptDES(cipherText);
            }

            return null;
        }

        /// <summary>
        ///     Decrypts a string. The 8-bit string for decryption. Exactly 8 Char long key
        /// </summary>
        /// <param name="input">The encrypted byte array to be decrypted.</param>
        /// <returns>The plain text in string format.</returns>
        /// <example>View code: <br />
        /// <code lang="xml" title="web.config">
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </code>
        /// </example>
        public static string DecryptDES(this byte[] input)
        {
            if (input != null)
            {
                // The IV property is set to a new random value whenever you create a new instance of one of the SymmetricAlgorithm classes 
                byte[] iv = Encoding.ASCII.GetBytes(EncryptionVector);
                byte[] key = Encoding.ASCII.GetBytes(EncryptionKey);

                byte[] plainText = Decrypt(input, key, iv);

                return Encoding.ASCII.GetString(plainText);
            }

            return null;
        }

        /// <summary>
        ///     Decrypts a string. The 8-bit string for decryption. Exactly 8 Char long key
        /// </summary>
        /// <param name="input">The encrypted byte array to be decrypted.</param>
        /// <returns>The plain text in string format.</returns>
        /// <example>View code: <br />
        /// <code lang="xml" title="web.config">
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </code>
        /// </example>
        public static byte[] DecryptDESBytes(this byte[] input)
        {
            if (input != null)
            {
                // The IV property is set to a new random value whenever you create a new instance of one of the SymmetricAlgorithm classes 
                byte[] iv = Encoding.ASCII.GetBytes(EncryptionVector);
                byte[] key = Encoding.ASCII.GetBytes(EncryptionKey);

                return Decrypt(input, key, iv);
            }

            return null;
        }

        /// <summary>
        ///     Decrypt 64 digits based encrypted string  
        /// </summary>
        /// <param name="input">string to decrypt</param>
        /// <returns>Decrypted string</returns>
        /// <example>View code: <br />
        /// <code lang="xml" title="web.config">
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </code>
        /// <code title="C# File" lang="C#">
        /// string encryptedstring = "Jimbo".Encrypt64(); // "ebrThgzZnIo="
        /// string dencryptedstring = encryptedstring.Decrypt64(); // "Jimbo"
        /// </code>
        /// </example>
        public static string Decrypt64(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return Decrypt64(Convert.FromBase64String(input));
            }

            return null;
        }

        /// <summary>
        ///     Decrypt 64 digits based encrypted byte array  
        /// </summary>
        /// <param name="input">byte array to decrypt</param>
        /// <returns>Decrypted byte array</returns>
        /// <example>View code: <br />
        /// <code lang="xml" title="web.config">
        /// <![CDATA[
        /// <appSettings>
        ///    <!--Encryption Key and Vector values MUST be 8 chars long exactly-->
        ///    <add key="VEncryption.Key" value="JBaltika"/>
        ///    <add key="VEncryption.Vector" value="IGenuine"/>
        /// </appSettings>
        /// ]]>
        /// </code>
        /// </example>
        public static string Decrypt64(this byte[] input)
        {
            if (input != null)
            {
                byte[] key = Encoding.UTF8.GetBytes(EncryptionKey);

                using (var memorystream = new MemoryStream(1024))
                {
                    using (var des = new DESCryptoServiceProvider())
                    {
                        using (ICryptoTransform transform = des.CreateDecryptor(key, Iv64))
                        {
                            var stream = new CryptoStream(memorystream, transform, CryptoStreamMode.Write);
                            stream.Write(input, 0, input.Length);
                            stream.FlushFinalBlock();

                            byte[] bytes = memorystream.ToArray();

                            return Encoding.UTF8.GetString(bytes);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Decrypts the specified bytes data.
        /// </summary>
        /// <param name="bytesdata">The data as bytes.</param>
        /// <param name="byteskey">The key as bytes.</param>
        /// <param name="initvec">The IV property is set to a new random value whenever you create a new instance of one of the SymmetricAlgorithm classes</param>
        /// <returns>Decrypted string</returns>
        private static byte[] Decrypt(byte[] bytesdata, byte[] byteskey, byte[] initvec)
        {
            using (var memorystream = new MemoryStream(1024))
            {
                using (var provider = new DESCryptoServiceProvider())
                {
                    provider.Mode = CipherMode.CBC;
                    provider.Key = byteskey;
                    provider.IV = initvec;

                    using (ICryptoTransform transform = provider.CreateDecryptor())
                    {
                        var stream = new CryptoStream(memorystream, transform, CryptoStreamMode.Write);

                        stream.Write(bytesdata, 0, bytesdata.Length);
                        stream.FlushFinalBlock();
                    }

                    return memorystream.ToArray();
                }
            }
        }

        /* ReSharper restore InconsistentNaming */
    }
}
