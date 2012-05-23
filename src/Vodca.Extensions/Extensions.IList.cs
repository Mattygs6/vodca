//-----------------------------------------------------------------------------
// <copyright file="Extensions.IList.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Indicates whether the specified <see cref="System.Collections.IList"/>
        /// is null or empty.
        /// </summary>
        /// <param name="icollection"><see cref="System.Collections.IList"/>
        ///     to check for.</param>
        /// <returns>Return true if Count is 0 or it is null</returns>
        public static bool IsNullOrEmpty(this IList icollection)
        {
            return icollection == null || icollection.Count == 0;
        }

        /// <summary>
        /// Indicates whether the specified ICollectionT
        /// is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="list">The list instance</param>
        /// <returns>
        ///     <c>true</c> if the collection is null or empty,
        /// otherwise <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>
        /// Randomizes the specified source.
        /// </summary>
        /// <typeparam name="T">The generic list object types</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The randomized list</returns>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(item => Guid.NewGuid());
        }

        /// <summary>
        /// Shuffles the specified list.
        /// </summary>
        /// <typeparam name="T">The list type</typeparam>
        /// <param name="list">The list to shuffle.</param>
        /// <see href="http://stackoverflow.com/questions/273313/randomize-a-listt-in-c"/>
        public static void Shuffle<T>(this IList<T> list)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                int n = list.Count;
                while (n > 1)
                {
                    var box = new byte[1];
                    do
                    {
                        provider.GetBytes(box);
                    }
                    while (!(box[0] < n * (byte.MaxValue / n)));

                    int k = box[0] % n;
                    n--;
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }
    }
}
