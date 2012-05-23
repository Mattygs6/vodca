//-----------------------------------------------------------------------------
// <copyright file="VComparableHashSet.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       05/04/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;

    /// <summary>
    /// The HashSet with IVComparable interface
    /// </summary>
    /// <typeparam name="TComparable">The type of the object.</typeparam>
    public partial class VComparableHashSet<TComparable> : HashSet<TComparable> where TComparable : class, IVComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VComparableHashSet{TComparable}"/> class.
        /// </summary>
        public VComparableHashSet()
            : base(VEqualityComparer<TComparable>.Comparer)
        {
            this.Ctor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VComparableHashSet&lt;TComparable&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public VComparableHashSet(IEnumerable<TComparable> collection)
            : base(collection, VEqualityComparer<TComparable>.Comparer)
        {
            this.Ctor();
        }

        /// <summary>
        /// Adds the specified object.
        /// </summary>
        /// <param name="tcomparable">The object.</param>
        public new void Add(TComparable tcomparable)
        {
            if (tcomparable != null)
            {
                base.Add(tcomparable);
            }
        }

        /// <summary>
        /// Ctors this instance.
        /// </summary>
        private void Ctor()
        {
        }
    }
}
