//-----------------------------------------------------------------------------
// <copyright file="Extensions.ICollection.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections;
    using System.Collections.Generic;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Indicates whether the specified <see cref="System.Collections.ICollection"/> is null or empty.
        /// </summary>
        /// <param name="collection"><see cref="System.Collections.ICollection"/> to check for.</param>
        /// <returns>Return true if Count is 0 or it is null</returns>
        public static bool IsNullOrEmpty(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        ///     Indicates whether the specified ICollection is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection instance</param>
        /// <returns>
        ///     <c>true</c> if the collection is null or empty, otherwise <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}
