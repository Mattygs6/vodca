//-----------------------------------------------------------------------------
// <copyright file="Encryption.Encoder.cs" company="genuine">
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
    using System.Web.Configuration;

    /// <summary>
    ///     Encrypt strings for AuthenticationTicket, Drawings filename,...
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Encryption\Encryption.Encoder.cs" title="Encryption.Encoder.cs" lang="C#" />
    /// </example>
    public static partial class VEncryption
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Holds Encryption Key
        /// </summary>
        private static string encryptionkey;

        /// <summary>
        ///     Holds Encryption Vector
        /// </summary>
        private static string encryptionvector;

        /// <summary>
        ///     Gets a Key vector from Web.Config file AppSettings
        /// The 8-bit string for encryption. Key must exactly be a 8 Char long string!
        /// </summary>
        private static string EncryptionKey
        {
            get
            {
                if (string.IsNullOrEmpty(encryptionkey))
                {
                    encryptionkey = WebConfigurationManager.AppSettings["VEncryption.Key"];
                    Ensure.MaxStringLength(encryptionkey, 8, "Encryption.EncryptionKey");
                }

                return encryptionkey;
            }
        }

        /// <summary>
        ///     Gets a Init vector from Web.Config file AppSettings
        /// </summary>
        private static string EncryptionVector
        {
            get
            {
                if (string.IsNullOrEmpty(encryptionvector))
                {
                    encryptionvector = WebConfigurationManager.AppSettings["VEncryption.Vector"];
                    Ensure.MaxStringLength(encryptionvector, 8, "Encryption.EncryptionVector");
                }

                return encryptionvector;
            }
        }

        /// <summary>
        ///     Gets a IV which is set to a new random value whenever you create a new instance of one of the SymmetricAlgorithm classes
        /// </summary>
        private static byte[] Iv64
        {
            get
            {
                return new byte[] { 44, 26, 86, 124, 144, 171, 205, 244 };
            }
        }

        /// <summary>
        ///     MD5 hash is a method to obtain a unique "signature" from a string. 
        /// It is very useful in order to store password, but be aware that there 
        /// is no way to get back to the original string, starting from the hash.    
        /// </summary>
        /// <param name="input">The string should be encrypted.</param>
        /// <returns>returns the MD5 hash for any given string</returns>
        public static string Md5Hash(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var builder = new StringBuilder();

                using (var cryptostream = new MD5CryptoServiceProvider())
                {
                    byte[] data = cryptostream.ComputeHash(Encoding.UTF8.GetBytes(input));

                    for (int i = 0; i < data.Length; i++)
                    {
                        builder.Append(data[i].ToString("x2"));
                    }
                }

                return builder.ToString();
            }

            return null;
        }

        /// <summary>
        ///     Encrypts a string using DES algorithm.
        /// </summary>
        /// <param name="input">The string should be encrypted.</param>
        /// <returns>The encrypted string.</returns>
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
        public static string EncryptDES(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                byte[] plainText = Encoding.ASCII.GetBytes(input);
                byte[] key = Encoding.ASCII.GetBytes(EncryptionKey);
                byte[] iv = Encoding.ASCII.GetBytes(EncryptionVector);   // "init vec is big."
                byte[] cipherText = Encrypt(plainText, key, iv);

                return Convert.ToBase64String(cipherText);
            }

            return null;
        }

        /// <summary>
        /// Encrypts the DES.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The encrypted bytes</returns>
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
        public static byte[] EncryptDES(this byte[] input)
        {
            if (input != null)
            {
                byte[] key = Encoding.ASCII.GetBytes(EncryptionKey);
                byte[] iv = Encoding.ASCII.GetBytes(EncryptionVector);   // "init vec is big."
                return Encrypt(input, key, iv);
            }

            return null;
        }

        /// <summary>
        ///      Encrypt string with base 64 digits
        /// </summary>
        /// <param name="input">string to encrypt</param>
        /// <returns>Encrypted string with base 64 digits</returns>
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
        public static string Encrypt64(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
                byte[] key = Encoding.UTF8.GetBytes(EncryptionKey);

                using (var memorystream = new MemoryStream(1024))
                {
                    using (var des = new DESCryptoServiceProvider())
                    {
                        using (ICryptoTransform provider = des.CreateEncryptor(key, Iv64))
                        {
                            var stream = new CryptoStream(memorystream, provider, CryptoStreamMode.Write);
                            stream.Write(inputByteArray, 0, inputByteArray.Length);
                            stream.FlushFinalBlock();

                            byte[] bytes = memorystream.ToArray();

                            return Convert.ToBase64String(bytes);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Encrypts the specified bytes data.
        /// </summary>
        /// <param name="bytesdata">The data as bytes.</param>
        /// <param name="byteskey">The key as bytes.</param>
        /// <param name="initvec">The IV property is set to a new random value whenever you create a new instance of one of the SymmetricAlgorithm classes</param>
        /// <returns>Encrypted byte stream</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "One rule contradicts another")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "One rule contradicts another")]
        private static byte[] Encrypt(byte[] bytesdata, byte[] byteskey, byte[] initvec)
        {
            using (var memorystream = new MemoryStream(1024))
            {
                using (var des = new DESCryptoServiceProvider { Mode = CipherMode.CBC, Key = byteskey, IV = initvec })
                {
                    using (ICryptoTransform encryptor = des.CreateEncryptor())
                    {
                        using (var stream = new CryptoStream(memorystream, encryptor, CryptoStreamMode.Write))
                        {
                            stream.Write(bytesdata, 0, bytesdata.Length);
                            stream.FlushFinalBlock();

                            return memorystream.ToArray();
                        }
                    }
                }
            }
        }

        /* ReSharper restore InconsistentNaming */
    }
}
