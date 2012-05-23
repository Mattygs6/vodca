//-----------------------------------------------------------------------------
// <copyright file="Extensions.LINQ.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/02/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Paginates the specified <![CDATA[IQueryable<T>]]> collection  using LINQ query.
        /// </summary>
        /// <typeparam name="T">The type of the collection item</typeparam>
        /// <param name="source">The query source.</param>
        /// <param name="skip">The skip items number.</param>
        /// <param name="take">The take items number.</param>
        /// <returns>The paged collection items</returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, byte skip, byte take = 10)
        {
            if (source != null && skip > 0 && take > 0)
            {
                return source.Skip(skip).Take(take);
            }

            return source;
        }

        /// <summary>
        /// Pages the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pageindex">The page.</param>
        /// <param name="pagesize">The page size.</param>
        /// <returns>The Page collection records</returns>
        /// <remarks>This extension method is design for relatively small collections ~2550 size with safety in mind. What's why page index and page size are bytes[0-255]</remarks>
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, byte pageindex, byte pagesize = 10)
        {
            if (source != null && pageindex > 0 && pagesize > 0)
            {
                return source.Skip((pageindex - 1) * pagesize).Take(pagesize);
            }

            return source;
        }

        /// <summary>
        /// Pages the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pageindex">The page  index.</param>
        /// <param name="pagesize">The page size.</param>
        /// <returns>The Page collection records</returns>
        /// <remarks>This extension method is design for relatively small collections ~2550 size with safety in mind. What's why page index and page size are bytes[0-255]</remarks>
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, byte pageindex, byte pagesize = 10)
        {
            if (source != null && pageindex > 0 && pagesize > 0)
            {
                return source.Skip((pageindex - 1) * pagesize).Take(pagesize);
            }

            return source;
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified collection].
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified collection]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this IEnumerable collection)
        {
            return collection == null || collection.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified collection].
        /// </summary>
        /// <typeparam name="T">The Generic Type</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified collection]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return true;
            }

            using (var enumerator = collection.GetEnumerator())
            {
                return !enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Shuffles the specified source.
        /// </summary>
        /// <typeparam name="T">The Generic Type</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A shuffled IEnumerable</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                yield break;
            }

            using (var provider = new RNGCryptoServiceProvider())
            {
                T[] items = source.ToArray();
                int n = items.Length;

                while (n > 1)
                {
                    var box = new byte[1];
                    do
                    {
                        provider.GetBytes(box);
                    }
                    while (!(box[0] < n * (byte.MaxValue / n)));

                    int k = box[0] % n;

                    yield return items[k];
                    items[k] = items[--n];
                }

                yield return items[0];
            }
        }

        /// <summary>
        /// Gets a random element.
        /// </summary>
        /// <typeparam name="T">The Generic Type</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The Random Element</returns>
        public static T GetRandomElement<T>(this IEnumerable<T> source)
        {
            return source.Shuffle().FirstOrDefault();
        }

        /// <summary>
        /// Gets <paramref name="count"/> number of repeatable random elements.
        /// </summary>
        /// <typeparam name="T">The Generic Type</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="count">The count.</param>
        /// <returns>The Random Elements</returns>
        public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T> source, int count)
        {
            if (count > 0 && source != null)
            {
                var src = source.ToArray();
                while (count-- != 0)
                {
                    yield return src.GetRandomElement();
                }
            }
        }

        /// <summary>
        /// Gets <paramref name="count"/> number of non-repeatable random elements.
        /// </summary>
        /// <typeparam name="T">The Generic Type</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="count">The count.</param>
        /// <returns>The Random Subset</returns>
        public static IEnumerable<T> GetRandomSubset<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        /// <summary>
        /// Extension method to make life easier.
        /// </summary>
        /// <typeparam name="T">Type of enumerable</typeparam>
        /// <param name="source">Source enumerable</param>
        /// <returns>A new VEnumerable of the appropriate type</returns>
        public static VEnumerable<T> AsVEnumerable<T>(this IEnumerable<T> source)
        {
            if (source != null)
            {
                return new VEnumerable<T>(source);
            }

            return new VEnumerable<T>(Enumerable.Empty<T>());
        }
    }
}
