//-----------------------------------------------------------------------------
// <copyright file="VEnumerable.cs" company="genuine">
//     Copyright (c) genuine. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Skeet
//  Modified:   M.Gramolini
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Type chaining an IEnumerable&lt;T&gt; to allow the iterating code
    /// to detect the first and last entries simply.
    /// </summary>
    /// <typeparam name="T">Type to iterate over</typeparam>
    public class VEnumerable<T> : IEnumerable<VEnumerable<T>.Entry>
    {
        /// <summary>
        /// Enumerable we proxy to
        /// </summary>
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        /// Initializes a new instance of the <see cref="VEnumerable&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="enumerable">Collection to enumerate. Must not be null.</param>
        public VEnumerable(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            this.enumerable = enumerable;
        }

        /// <summary>
        /// Returns an enumeration of Entry objects, each of which knows
        /// whether it is the first/last of the enumeration, as well as the
        /// current value.
        /// </summary>
        /// <returns>
        /// An collection of the entry object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Entry> GetEnumerator()
        {
            using (var enumerator = this.enumerable.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                bool isFirst = true;
                bool isLast = false;
                int index = 0;

                while (!isLast)
                {
                    T current = enumerator.Current;
                    isLast = !enumerator.MoveNext();
                    yield return new Entry(isFirst, isLast, current, index++);
                    isFirst = false;
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Represents each entry returned within a collection,
        /// containing the value and whether it is the first and/or
        /// the last entry in the collection's. enumeration
        /// </summary>
        public class Entry
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="VEnumerable&lt;T&gt;.Entry"/> class.
            /// </summary>
            /// <param name="isFirst">if set to <c>true</c> [is first].</param>
            /// <param name="isLast">if set to <c>true</c> [is last].</param>
            /// <param name="value">The value.</param>
            /// <param name="index">The index.</param>
            internal Entry(bool isFirst, bool isLast, T value, int index)
            {
                this.IsFirst = isFirst;
                this.IsLast = isLast;
                this.Value = value;
                this.Index = index;
            }

            /// <summary>
            /// Gets the value of the current instance
            /// </summary>
            public T Value { get; private set; }

            /// <summary>
            /// Gets a value indicating whether this is the first instance in the collection
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance is first; otherwise, <c>false</c>.
            /// </value>
            public bool IsFirst { get; private set; }

            /// <summary>
            /// Gets a value indicating whether this is the last instance in the collection
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is last; otherwise, <c>false</c>.
            /// </value>
            public bool IsLast { get; private set; }

            /// <summary>
            /// Gets the index of the current instance
            /// </summary>
            public int Index { get; private set; }
        }
    }
}