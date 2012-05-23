//-----------------------------------------------------------------------------
// <copyright file="Extensions.HttpCache.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/18/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Caching;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// All cached items keys
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <returns>All cache keys</returns>
        public static IEnumerable<string> AllKeys(this Cache cache)
        {
            var keys = new List<string>();

            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                keys.Add(cacheEnum.Key.ToString());
            }

            return keys.ToArray();
        }

        /// <summary>
        /// Clear All cached all items
        /// </summary>
        /// <param name="cache">The cache.</param>
        public static void Clear(this Cache cache)
        {
            var keys = new List<string>();
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                keys.Add(cacheEnum.Key.ToString());
            }

            foreach (string key in keys)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// Clear All cached all items with specific partial key within cache keys
        /// </summary>
        /// <param name="cache">The cache for a Web application</param>
        /// <param name="key">The partial Cache key</param>
        public static void RemoveKeysContaining(this Cache cache, string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                var keys = new List<string>();
                IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
                while (cacheEnum.MoveNext())
                {
                    keys.Add(cacheEnum.Key.ToString());
                }

                foreach (string collectionkey in keys)
                {
                    if (collectionkey.IndexOf(key, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        cache.Remove(collectionkey);
                    }
                }
            }
        }

        /// <summary>
        /// Inserts the specified  object to the cache.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="cacheobject">The object to cache.</param>
        /// <param name="time">The cache time.</param>
        /// <param name="cachedependency">The cache dependency.</param>
        public static void Insert(this Cache cache, string key, object cacheobject, VCacheTime time = VCacheTime.Normal, CacheDependency cachedependency = null)
        {
            if (!string.IsNullOrWhiteSpace(key) && null != cacheobject)
            {
                cache.Insert(key, cacheobject, cachedependency, DateTime.Now.AddMinutes((double)time), Cache.NoSlidingExpiration);
            }
        }

        /// <summary>
        /// Caches the insert.
        /// </summary>
        /// <param name="cacheobject">The cache object.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="time">The cache  time.</param>
        /// <param name="cachedependency">The cache dependency.</param>
        public static void CacheInsert(this object cacheobject, string key, VCacheTime time = VCacheTime.Normal, CacheDependency cachedependency = null)
        {
            Insert(HttpRuntime.Cache, key, cacheobject, time, cachedependency);
        }

        /// <summary>
        /// Gets the specified object by key from cache.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="cache">The cache instance .</param>
        /// <param name="key">The cache key.</param>
        /// <returns>The object instance or null</returns>
        public static TObject Get<TObject>(this Cache cache, string key) where TObject : class
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                return cache[key] as TObject;
            }

            return null;
        }
    }
}
