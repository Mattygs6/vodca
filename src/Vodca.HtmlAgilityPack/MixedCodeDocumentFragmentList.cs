//-----------------------------------------------------------------------------
// <copyright file="MixedCodeDocumentFragmentList.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of mixed code fragments.
    /// </summary>
    public partial class MixedCodeDocumentFragmentList : IEnumerable
    {
        /// <summary>
        /// The _items.
        /// </summary>
        private readonly IList<MixedCodeDocumentFragment> codeDocumentFragment = new List<MixedCodeDocumentFragment>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MixedCodeDocumentFragmentList"/> class.
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        internal MixedCodeDocumentFragmentList(MixedCodeDocument doc)
        {
            this.CodeDocument = doc;
        }

        /// <summary>
        ///   Gets the number of fragments contained in the list.
        /// </summary>
        public int Count
        {
            get
            {
                return this.codeDocumentFragment.Count;
            }
        }

        /// <summary>
        /// Gets the Document
        /// </summary>
        public MixedCodeDocument CodeDocument { get; private set; }

        /// <summary>
        /// Gets a fragment from the list using its index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The document fragment</returns>
        public MixedCodeDocumentFragment this[int index]
        {
            get
            {
                return this.codeDocumentFragment[index];
            }
        }

        /// <summary>
        /// Appends a fragment to the list of fragments.
        /// </summary>
        /// <param name="newFragment">
        /// The fragment to append. May not be null. 
        /// </param>
        public void Append(MixedCodeDocumentFragment newFragment)
        {
            Ensure.IsNotNull(newFragment, "newFragment");

            this.codeDocumentFragment.Add(newFragment);
        }

        /// <summary>
        /// Gets an enumerator that can iterate through the fragment list.
        /// </summary>
        /// <returns>The Fragment Enumerator</returns>
        public MixedCodeDocumentFragmentEnumerator GetEnumerator()
        {
            return new MixedCodeDocumentFragmentEnumerator(this.codeDocumentFragment);
        }

        /// <summary>
        /// Prepends a fragment to the list of fragments.
        /// </summary>
        /// <param name="newFragment">The fragment to append. May not be null.</param>
        public void Prepend(MixedCodeDocumentFragment newFragment)
        {
            Ensure.IsNotNull(newFragment, "newFragment");

            this.codeDocumentFragment.Insert(0, newFragment);
        }

        /// <summary>
        /// Remove a fragment from the list of fragments. If this fragment was not in the list, an exception will be raised.
        /// </summary>
        /// <param name="fragment">
        /// The fragment to remove. May not be null. 
        /// </param>
        public void Remove(MixedCodeDocumentFragment fragment)
        {
            Ensure.IsNotNull(fragment, "fragment");

            int index = this.GetFragmentIndex(fragment);
            if (index == -1)
            {
                throw new IndexOutOfRangeException();
            }

            this.RemoveAt(index);
        }

        /// <summary>
        /// Remove all fragments from the list.
        /// </summary>
        public void RemoveAll()
        {
            this.codeDocumentFragment.Clear();
        }

        /// <summary>
        /// Remove a fragment from the list of fragments, using its index in the list.
        /// </summary>
        /// <param name="index">
        /// The index of the fragment to remove. 
        /// </param>
        public void RemoveAt(int index)
        {
            // MixedCodeDocumentFragment frag = (MixedCodeDocumentFragment) _items[index];
            this.codeDocumentFragment.RemoveAt(index);
        }

        /// <summary>
        /// Gets an enumerator that can iterate through the fragment list.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// The clear.
        /// </summary>
        internal void Clear()
        {
            this.codeDocumentFragment.Clear();
        }

        /// <summary>
        /// The get fragment index.
        /// </summary>
        /// <param name="fragment">The fragment.</param>
        /// <returns>
        /// The fragment index.
        /// </returns>
        internal int GetFragmentIndex(MixedCodeDocumentFragment fragment)
        {
            Ensure.IsNotNull(fragment, "fragment");

            for (int i = 0; i < this.codeDocumentFragment.Count; i++)
            {
                if (this.codeDocumentFragment[i] == fragment)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Represents a fragment enumerator.
        /// </summary>
        public class MixedCodeDocumentFragmentEnumerator : IEnumerator
        {
            /// <summary>
            /// The _items.
            /// </summary>
            private readonly IList<MixedCodeDocumentFragment> fragments;

            /// <summary>
            /// The _index.
            /// </summary>
            private int currentindex;

            /// <summary>
            /// Initializes a new instance of the <see cref="MixedCodeDocumentFragmentEnumerator"/> class.
            /// </summary>
            /// <param name="items">
            /// The items.
            /// </param>
            internal MixedCodeDocumentFragmentEnumerator(IList<MixedCodeDocumentFragment> items)
            {
                this.fragments = items;
                this.currentindex = -1;
            }

            /// <summary>
            ///   Gets the current element in the collection.
            /// </summary>
            public MixedCodeDocumentFragment Current
            {
                get
                {
                    return this.fragments[this.currentindex];
                }
            }

            /// <summary>
            ///   Gets the current element in the collection.
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection. 
            /// </returns>
            public bool MoveNext()
            {
                this.currentindex++;
                return this.currentindex < this.fragments.Count;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                this.currentindex = -1;
            }
        }
    }
}