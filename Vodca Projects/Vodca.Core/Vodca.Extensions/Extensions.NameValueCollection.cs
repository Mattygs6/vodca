//-----------------------------------------------------------------------------
// <copyright file="Extensions.NameValueCollection.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       09/29/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Gets a double from the specified key
        /// </summary>
        /// <param name="collection">The NameValueCollection collection instance</param>
        /// <param name="key">The NameValueCollection collection key</param>
        /// <returns>The converted value or NULL</returns>
        public static double? GetDouble(this NameValueCollection collection, string key)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToDouble();
            }

            return null;
        }

        /// <summary>
        ///     Gets a float from the specified key
        /// </summary>
        /// <param name="collection">The NameValueCollection collection instance</param>
        /// <param name="key">The NameValueCollection collection key</param>
        /// <returns>The converted value or NULL</returns>
        public static float? GetFloat(this NameValueCollection collection, string key)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToFloat();
            }

            return null;
        }

        /// <summary>
        ///     Gets an int from the specified key
        /// </summary>
        /// <param name="collection">The NameValueCollection collection instance</param>
        /// <param name="key">The NameValueCollection collection key</param>
        /// <returns>The converted value or NULL</returns>
        public static int? GetInt(this NameValueCollection collection, string key)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToInt();
            }

            return null;
        }

        /// <summary>
        ///     Gets an long from the specified key
        /// </summary>
        /// <param name="collection">The NameValueCollection collection instance</param>
        /// <param name="key">The NameValueCollection collection key</param>
        /// <returns>The converted value or NULL</returns>
        public static long? GetLong(this NameValueCollection collection, string key)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToLong();
            }

            return null;
        }

        /// <summary>
        /// Gets a Boolean from the specified key
        /// </summary>
        /// <param name="collection">The NameValueCollection collection instance</param>
        /// <param name="key">The NameValueCollection collection key</param>
        /// <returns>The converted value or NULL</returns>
        public static bool? GetBoolean(this NameValueCollection collection, string key)
        {
            if (collection != null && !string.IsNullOrWhiteSpace(key))
            {
                return collection[key].ConvertToBoolean();
            }

            return null;
        }

        /// <summary>
        ///     Gets the keys containing partial key.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key to match.</param>
        /// <returns>The IEnumerable of strings matching key</returns>
        public static IEnumerable<string> GetKeysContainingPartialKey(this NameValueCollection collection, string key)
        {
            if (collection != null && !string.IsNullOrEmpty(key))
            {
                key = key.ToLowerInvariant();
                IEnumerable<string> keys = collection.AllKeys;

                return from value in keys where !string.IsNullOrEmpty(value) && value.ToLowerInvariant().Contains(key) select value;
            }

            return new string[] { };
        }

        /// <summary>
        ///     Gets the first or default key containing partial key.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key to match.</param>
        /// <returns>The value containing key or null</returns>
        public static string GetFirstOrDefaultPartialKey(this NameValueCollection collection, string key)
        {
            return collection.GetKeysContainingPartialKey(key).FirstOrDefault();
        }

        /// <summary>
        /// The current VNameValueCollection to the query string.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="prependquestionmark">if set to <c>true</c> prepend question marks.</param>
        /// <returns>
        /// The current collection to the query string
        /// </returns>
        public static string ToQueryString(this NameValueCollection collection, bool prependquestionmark = false)
        {
            var querystring = new StringBuilder(64);
            if (collection != null)
            {
                int count = collection.Count;
                for (int i = 0; i < count; i++)
                {
                    if (prependquestionmark && i == 0)
                    {
                        querystring.Append('?');
                    }

                    string key = collection.GetKey(i);
                    string value = collection[key];

                    if (!string.IsNullOrEmpty(value))
                    {
                        string urlitem = (key + "=" + value).UrlEncode();
                        querystring.Append(urlitem);
                    }

                    if (i != count - 1)
                    {
                        querystring.Append('&');
                    }
                }
            }

            return querystring.ToString();
        }
    }
}
