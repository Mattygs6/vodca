//-----------------------------------------------------------------------------
// <copyright file="DictionaryDebugView.cs" company="GenuineInteractive">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/19/2011
//-----------------------------------------------------------------------------
namespace Vodca.Diagnostics
{
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The dictionary debug view
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class DictionaryDebugView<TKey, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryDebugView&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public DictionaryDebugView(IDictionary<TKey, TValue> dictionary)
        {
            this.Dictionary = dictionary ?? new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "VS debug view"), DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<TKey, TValue>[] Items
        {
            get
            {
                var array = new KeyValuePair<TKey, TValue>[this.Dictionary.Count];
                this.Dictionary.CopyTo(array, 0);

                return array;
            }
        }

        /// <summary>
        /// Gets or sets the dictionary.
        /// </summary>
        /// <value>
        /// The dictionary.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<TKey, TValue> Dictionary { get; set; }
    }
}