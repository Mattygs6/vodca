//-----------------------------------------------------------------------------
// <copyright file="Extensions.ThirdParty.IList.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Date:       07/24/2010
//  Source:     .NET Extensions - Extensions Methods Library
//  Url:        http://dnpextensions.codeplex.com/
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Inserts an item uniquely to to a list and returns a value whether the item was inserted or not.
        /// </summary>
        /// <typeparam name="T">The generic list item type.</typeparam>
        /// <param name="list">The list to be inserted into.</param>
        /// <param name="index">The index to insert the item at.</param>
        /// <param name="item">The item to be added.</param>
        /// <returns>Indicates whether the item was inserted or not</returns>
        public static bool InsertUnqiue<T>(this IList<T> list, int index, T item)
        {
            if (!list.Contains(item))
            {
                list.Insert(index, item);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Inserts a range of items uniquely to a list starting at a given index and returns the amount of items inserted.
        /// </summary>
        /// <typeparam name="T">The generic list item type.</typeparam>
        /// <param name="list">The list to be inserted into.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="items">The items to be inserted.</param>
        /// <returns>The amount if items that were inserted.</returns>
        public static int InsertRangeUnique<T>(this IList<T> list, int startIndex, IEnumerable<T> items)
        {
            var index = startIndex + items.Count(item => list.InsertUnqiue(startIndex, item));

            return index - startIndex;
        }

        /// <summary>
        ///     Return the index of the first matching item or -1.
        /// </summary>
        /// <typeparam name="T">The generic object type</typeparam>
        /// <param name="list">The list instance.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The item index</returns>
        public static int IndexOf<T>(this IList<T> list, Func<T, bool> comparison)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (comparison(list[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
