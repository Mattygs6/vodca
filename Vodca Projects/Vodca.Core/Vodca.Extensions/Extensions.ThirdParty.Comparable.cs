//-----------------------------------------------------------------------------
// <copyright file="Extensions.ThirdParty.Comparable.cs" company="genuine">
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Determines whether the specified value is between the the defined minimum and maximum range (including those values).
        /// </summary>
        /// <typeparam name="T">The generic comparable object instance</typeparam>
        /// <param name="value">The value to compare.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>
        ///     <c>true</c> if the specified value is between min and max; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// var value = 5;
        /// if(value.IsBetween(1, 10)) { 
        ///     // ... 
        /// }
        /// </example>
        public static bool IsBetween<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            return value.IsBetween(minValue, maxValue, null);
        }

        /// <summary>
        ///     Determines whether the specified value is between the the defined minimum and maximum range (including those values).
        /// </summary>
        /// <typeparam name="T">The generic comparable object instance</typeparam>
        /// <param name="value">The value to compare.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="comparer">An optional comparer to be used instead of the types default comparer.</param>
        /// <returns>
        ///     <c>true</c> if the specified value is between min and max; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// var value = 5;
        /// if(value.IsBetween(1, 10)) {
        /// // ...
        /// }
        /// </example>
        public static bool IsBetween<T>(this T value, T minValue, T maxValue, IComparer<T> comparer) where T : IComparable<T>
        {
            comparer = comparer ?? Comparer<T>.Default;

            var minMaxCompare = comparer.Compare(minValue, maxValue);
            if (minMaxCompare < 0)
            {
                return (comparer.Compare(value, minValue) >= 0) && (comparer.Compare(value, maxValue) <= 0);
            }

            if (minMaxCompare == 0)
            {
                return comparer.Compare(value, minValue) == 0;
            }

            return (comparer.Compare(value, maxValue) >= 0) && (comparer.Compare(value, minValue) <= 0);
        }
    }
}