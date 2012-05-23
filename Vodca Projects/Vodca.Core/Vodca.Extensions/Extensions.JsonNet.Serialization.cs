//-----------------------------------------------------------------------------
// <copyright file="Extensions.JsonNet.Serialization.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/03/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;
    using Vodca.SDK.Newtonsoft.Json;

    /// <summary>
    ///     The JSON extension methods
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.Serialization.cs" title="Extensions.Serialization.cs" lang="C#" />
    /// </example> 
    public static partial class Extensions
    {
        /// <summary>
        ///  Uses JSON.NET instead Microsoft ASP.NET JSON. Deserializes JSON-formatted data into ECMAScript (JavaScript) types 
        /// </summary>
        /// <typeparam name="TObject">Object Type</typeparam>
        /// <param name="json">Serialized Object as string</param>
        /// <returns>deserialized object of type TObject</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.Extensions\Extensions.Serialization.cs" title="Extensions.Serialization.cs" lang="C#" />
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It must deserialize object from JSON string.")]
        public static TObject DeserializeFromJson<TObject>(this string json) where TObject : class
        {
            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<TObject>(json);
            }

            return default(TObject);
        }

        /// <summary>
        /// Uses JSON.NET instead Microsoft ASP.NET JSON. Serialize Generic Object to the JavaScript Notation Object (JSON)
        /// </summary>
        /// <param name="value">The object value.</param>
        /// <returns>Serialized JSON string</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Sitecore.Core\Vodca.Extensions\Extensions.Serialization.cs" title="Extensions.Serialization.cs" lang="C#" />
        /// </example>
        public static string SerializeToJson(this object value)
        {
            if (value != null)
            {
                return JsonConvert.SerializeObject(value);
            }

            return string.Empty;
        }

        /// <summary>
        /// Reads the encrypted JSON base64.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="str">The STR.</param>
        /// <returns>Decrypted, deserialized object of type TObject </returns>
        public static TObject ReadEncrypedJsonBase64<TObject>(this string str) where TObject : class
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.DecryptDES().DeserializeFromJson<TObject>();
            }

            return default(TObject);
        }

        /// <summary>
        /// Writes the encrypted JSON base64.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>Encrypted, base 64 encoded JSON string</returns>
        public static string WriteEncryptedJsonBase64(this object obj)
        {
            if (obj != null)
            {
                return obj.SerializeToJson().EncryptDES();
            }

            return string.Empty;
        }
    }
}
