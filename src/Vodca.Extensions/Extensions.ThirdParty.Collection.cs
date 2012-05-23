//-----------------------------------------------------------------------------
// <copyright file="Extensions.ThirdParty.Collection.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Date:       07/24/2010
//  Source:     .NET Extensions - Extensions Methods Library
//  Url:        http://dnpextensions.codeplex.com/
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Adds a value uniquely to to a collection and returns a value whether the value was added or not.
        /// </summary>
        /// <typeparam name="T">The generic collection value type</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="value">The value to be added.</param>
        /// <returns>Indicates whether the value was added or not</returns>
        /// <example>View code: <br /><code title="C# File" lang="C#">
        /// list.AddUnique(1); // returns true;
        /// list.AddUnique(1); // returns false the second time;
        /// </code></example>
        public static bool AddUnique<T>(this ICollection<T> collection, T value)
        {
            if (!collection.Contains(value))
            {
                collection.Add(value);

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Adds a range of value uniquely to a collection and returns the amount of values added.
        /// </summary>
        /// <typeparam name="T">The generic collection value type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="values">The values to be added.</param>
        /// <returns>The amount if values that were added.</returns>
        public static int AddRangeUnique<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            return values.Count(collection.AddUnique);
        }
    }
}
