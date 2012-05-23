//-----------------------------------------------------------------------------
// <copyright file="VEqualityComparer.cs" company="genuine">
//     Copyright (c) Genuine Interactive. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
// Author:  M.Gramolini
//   Date:  05/04/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Equality Comparer
    /// </summary>
    /// <typeparam name="TComparable">The type of the compare.</typeparam>
    public sealed class VEqualityComparer<TComparable> : IEqualityComparer<TComparable> where TComparable : IVComparable
    {
        /// <summary>
        /// Gets the comparer.
        /// </summary>
        public static VEqualityComparer<TComparable> Comparer
        {
            get
            {
                return new VEqualityComparer<TComparable>();
            }
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type TComparable to compare.</param>
        /// <param name="y">The second object of type TComparable to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(TComparable x, TComparable y)
        {
            // ReSharper disable CSharpWarnings::CS0183
            if (x is IVComparable && y is IVComparable)
            // ReSharper restore CSharpWarnings::CS0183
            {
                return x.GetUniqueJsonString().Equals(y.GetUniqueJsonString(), StringComparison.InvariantCulture);
            }

            return object.Equals(x, y);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.
        /// </exception>
        public int GetHashCode(TComparable obj)
        {
            Ensure.IsNotNull(obj, "obj");
            return obj.GetComparableHashCode();
        }
    }
}