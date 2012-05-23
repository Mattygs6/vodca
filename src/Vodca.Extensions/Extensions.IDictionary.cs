//-----------------------------------------------------------------------------
// <copyright file="Extensions.IDictionary.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       09/29/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.SessionState;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Converts NameValueCollection to IDictionary
        /// </summary>
        /// <param name="collection">The NameValueCollection instance</param>
        /// <returns>IDictionary of key value pairs both type string</returns>
        public static IDictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            if (collection != null)
            {
                var dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

                foreach (string key in collection.Keys)
                {
                    if (!string.IsNullOrWhiteSpace(key) && !dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, collection[key]);
                    }
                }

                return dictionary;
            }

            return null;
        }

        /// <summary>
        /// Trims the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The trimmed collection (without keys with empty values)</returns>
        public static IDictionary<string, string> Trim(this IDictionary<string, string> collection)
        {
            if (collection != null)
            {
                var list = (from item in collection where string.IsNullOrWhiteSpace(item.Value) select item.Key).ToArray();
                foreach (var key in list)
                {
                    collection.Remove(key);
                }
            }

            return collection;
        }

        /// <summary>
        /// Removes the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="keycollection">The key collection.</param>
        /// <returns>The collection without specific keys</returns>
        public static IDictionary<string, string> Remove(this IDictionary<string, string> collection, IEnumerable<string> keycollection)
        {
            if (collection != null && keycollection != null)
            {
                foreach (var key in keycollection)
                {
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        collection.Remove(key);
                    }
                }
            }

            return collection;
        }

        /// <summary>
        /// The get dictionary value or null.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <returns>The dictionary value or default</returns>
        public static TValue GetDictionaryValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TKey : class
        {
            return dict.ContainsKey(key) ? dict[key] : default(TValue);
        }

        /// <summary>
        /// Removes the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="keycollection">The key collection.</param>
        /// <returns>The collection without specific keys</returns>
        public static IDictionary<string, string> Remove(this IDictionary<string, string> collection, params string[] keycollection)
        {
            if (collection != null && keycollection != null)
            {
                foreach (var key in keycollection)
                {
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        collection.Remove(key);
                    }
                }
            }

            return collection;
        }

        /// <summary>
        /// Converts HttpCookie collection the dictionary.
        /// </summary>
        /// <param name="cookiecollection">The cookie collection.</param>
        /// <returns>The dictionary from cookie collection</returns>
        public static IDictionary<string, string> ToDictionary(this HttpCookieCollection cookiecollection)
        {
            if (cookiecollection != null && cookiecollection.Count > 0)
            {
                var collection = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                for (int i = 0; i < cookiecollection.Count; i++)
                {
                    HttpCookie cookie = cookiecollection[i];
                    if (!string.IsNullOrWhiteSpace(cookie.Value))
                    {
                        collection[cookie.Name] = cookie.Value;
                    }
                }

                return collection;
            }

            return null;
        }

        /// <summary>
        /// Coverts browser capabilities collection to the dictionary.
        /// </summary>
        /// <param name="browsercapabilities">The browser capabilities.</param>
        /// <returns>The dictionary from browser capabilities collection</returns>
        public static IDictionary<string, string> ToDictionary(this HttpCapabilitiesBase browsercapabilities)
        {
            if (browsercapabilities != null)
            {
                var browsercollection = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                browsercollection["Browser"] = browsercapabilities.Browser;
                browsercollection["EcmaScriptVersion"] = browsercapabilities.EcmaScriptVersion.ToString();
                browsercollection["MajorVersion"] = browsercapabilities.MajorVersion.ToString(CultureInfo.InvariantCulture);
                browsercollection["MinorVersion"] = browsercapabilities.MinorVersion.ToString(CultureInfo.InvariantCulture);
                browsercollection["MSDomVersion"] = browsercapabilities.MSDomVersion.ToString();
                browsercollection["Platform"] = browsercapabilities.Platform;
                browsercollection["Type"] = browsercapabilities.Type;
                browsercollection["Version"] = browsercapabilities.Version;
                browsercollection["W3CDomVersion"] = browsercapabilities.W3CDomVersion.ToString();
                browsercollection["ActiveXControls"] = browsercapabilities.ActiveXControls.ToString(CultureInfo.InvariantCulture);
                browsercollection["AOL"] = browsercapabilities.AOL.ToString(CultureInfo.InvariantCulture);
                browsercollection["Beta"] = browsercapabilities.Beta.ToString(CultureInfo.InvariantCulture);
                browsercollection["Cookies"] = browsercapabilities.Cookies.ToString(CultureInfo.InvariantCulture);
                browsercollection["Crawler"] = browsercapabilities.Crawler.ToString(CultureInfo.InvariantCulture);
                browsercollection["Frames"] = browsercapabilities.Frames.ToString(CultureInfo.InvariantCulture);

                return browsercollection;
            }

            return null;
        }

        /// <summary>
        /// Gets a Application session state
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>The dictionary from session collection</returns>
        public static IDictionary<string, string> ToDictionary(this HttpSessionState session)
        {
            if (session != null)
            {
                var results = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

                NameObjectCollectionBase.KeysCollection keys = session.Keys;
                if (keys.Count > 0)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        results[keys[i]] = string.Concat(session[i]);
                    }
                }

                return results;
            }

            return null;
        }

        /// <summary>
        /// Gets a Application state
        /// </summary>
        /// <param name="applicationstate">The application state.</param>
        /// <returns>The dictionary from application state collection</returns>
        public static IDictionary<string, string> ToDictionary(this HttpApplicationState applicationstate)
        {
            if (applicationstate != null)
            {
                var results = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                for (int i = 0; i < applicationstate.Count; i++)
                {
                    results[applicationstate.GetKey(i)] = string.Concat(applicationstate.Get(i));
                }

                return results;
            }

            return null;
        }

        /// <summary>
        /// Adds or overwrites the value for the key in this collection
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetOrAdd(this IDictionary collection, object key, object value)
        {
            if (key != null)
            {
                collection[key] = value;
            }
        }

        /// <summary>
        /// Gets the specified object by key from collection.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>
        /// The object instance or null
        /// </returns>
        public static TObject Get<TObject>(this IDictionary collection, object key) where TObject : class
        {
            if (key != null && collection.Contains(key))
            {
                return collection[key] as TObject;
            }

            return null;
        }

        /// <summary>
        /// Gets the specified object by key from collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <returns>The object instance</returns>
        public static object Get(this IDictionary collection, object key)
        {
            if (key != null && collection.Contains(key))
            {
                return collection[key];
            }

            return null;
        }
    }
}
