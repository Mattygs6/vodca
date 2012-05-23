//-----------------------------------------------------------------------------
// <copyright file="HtmlNodeCollection.cs" company="genuine">
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
    using System.Linq;

    /// <summary>
    /// Represents a combined list and collection of HTML nodes.
    /// </summary>
    public partial class HtmlNodeCollection : IList<HtmlNode>
    {
        /// <summary>
        /// The _items.
        /// </summary>
        private readonly IList<HtmlNode> items = new List<HtmlNode>();

        /// <summary>
        /// The _parentnode.
        /// </summary>
        private readonly HtmlNode parentnode;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeCollection"/> class. 
        /// Initialize the HtmlNodeCollection with the base parent node
        /// </summary>
        /// <param name="parentnode">
        /// The base node of the collection 
        /// </param>
        public HtmlNodeCollection(HtmlNode parentnode)
        {
            this.parentnode = parentnode; // may be null
        }

        /// <summary>
        ///  Gets the number of elements actually contained in the list.
        /// </summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a given node from the list.
        /// </summary>
        /// <param name="node">The html node</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        public int this[HtmlNode node]
        {
            get
            {
                int index = this.GetNodeIndex(node);
                if (index == -1)
                {
                    throw new ArgumentOutOfRangeException("node", "Node \"" + node.CloneNode(false).OuterHtml + "\" was not found in the collection");
                }

                return index;
            }
        }

        /// <summary>
        /// Get node with tag name
        /// </summary>
        /// <param name="nodeName">The node name</param>
        /// <returns>
        /// The Html node
        /// </returns>
        public HtmlNode this[string nodeName]
        {
            get
            {
                return this.items.FirstOrDefault(t => t.NodeName.Equals(nodeName, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        /// <summary>
        /// Gets the node at the specified index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        public HtmlNode this[int index]
        {
            get
            {
                return this.items[index];
            }

            set
            {
                this.items[index] = value;
            }
        }

        /// <summary>
        /// Get first instance of node in supplied collection
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="name">The name.</param>
        /// <returns>The Html Node</returns>
        public static HtmlNode FindFirst(IEnumerable<HtmlNode> items, string name)
        {
            foreach (HtmlNode node in items)
            {
                if (node.NodeName.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return node;
                }

                if (!node.HasChildNodes)
                {
                    continue;
                }

                HtmlNode returnNode = FindFirst(node.ChildNodes, name);
                if (returnNode != null)
                {
                    return returnNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Add node to the collection
        /// </summary>
        /// <param name="node">The node.</param>
        public void Add(HtmlNode node)
        {
            this.items.Add(node);
        }

        /// <summary>
        /// Add node to the end of the collection
        /// </summary>
        /// <param name="node">The node.</param>
        public void Append(HtmlNode node)
        {
            HtmlNode last = null;
            if (this.items.Count > 0)
            {
                last = this.items[this.items.Count - 1];
            }

            this.items.Add(node);
            node.PreviousSibling = last;
            node.NextSibling = null;
            node.ParentNode = this.parentnode;
            if (last == null)
            {
                return;
            }

            if (last == node)
            {
                throw new InvalidProgramException("Unexpected error.");
            }

            last.NextSibling = node;
        }

        /// <summary>
        /// Clears out the collection of HtmlNodes. Removes each nodes reference to parent node, next node and previous node
        /// </summary>
        public void Clear()
        {
            foreach (HtmlNode node in this.items)
            {
                node.ParentNode = null;
                node.NextSibling = null;
                node.PreviousSibling = null;
            }

            this.items.Clear();
        }

        /// <summary>
        /// Gets existence of node in collection
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// The contains.
        /// </returns>
        public bool Contains(HtmlNode item)
        {
            return this.items.Contains(item);
        }

        /// <summary>
        /// Copy collection to array
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(HtmlNode[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get all node descended from this collection
        /// </summary>
        /// <returns>The collection of HtmlNodes</returns>
        public IEnumerable<HtmlNode> Descendants()
        {
            return this.items.SelectMany(item => item.Descendants());
        }

        /// <summary>
        /// Get all node descended from this collection with matching name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The collection of HtmlNodes</returns>
        public IEnumerable<HtmlNode> Descendants(string name)
        {
            return this.items.SelectMany(item => item.Descendants(name));
        }

        /// <summary>
        /// Gets all first generation elements in collection
        /// </summary>
        /// <returns>The collection of HtmlNodes</returns>
        public IEnumerable<HtmlNode> Elements()
        {
            return this.items.SelectMany(item => item.ChildNodes);
        }

        /// <summary>
        /// Gets all first generation elements matching name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The collection of HtmlNodes</returns>
        public IEnumerable<HtmlNode> Elements(string name)
        {
            return this.items.SelectMany(item => item.Elements(name));
        }

        /// <summary>
        /// Get first instance of node with name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The first instance of Html Node by name</returns>
        public HtmlNode FindFirst(string name)
        {
            return FindFirst(this, name);
        }

        /// <summary>
        /// Get index of node
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>
        /// The get node index.
        /// </returns>
        public int GetNodeIndex(HtmlNode node)
        {
            // TODO: should we rewrite this? what would be the key of a node?
            for (int i = 0; i < this.items.Count; i++)
            {
                if (node == this.items[i])
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get index of node
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <returns>
        /// The index of.
        /// </returns>
        public int IndexOf(HtmlNode item)
        {
            return this.items.IndexOf(item);
        }

        /// <summary>
        /// Insert node at index
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="node">The node.</param>
        public void Insert(int index, HtmlNode node)
        {
            HtmlNode next = null;
            HtmlNode prev = null;

            if (index > 0)
            {
                prev = this.items[index - 1];
            }

            if (index < this.items.Count)
            {
                next = this.items[index];
            }

            this.items.Insert(index, node);

            if (prev != null)
            {
                if (node == prev)
                {
                    throw new InvalidProgramException("Unexpected error.");
                }

                prev.NextSibling = node;
            }

            if (next != null)
            {
                next.PreviousSibling = node;
            }

            node.PreviousSibling = prev;
            if (next == node)
            {
                throw new InvalidProgramException("Unexpected error.");
            }

            node.NextSibling = next;
            node.ParentNode = this.parentnode;
        }

        /// <summary>
        /// All first generation nodes in collection
        /// </summary>
        /// <returns>The child nodes</returns>
        public IEnumerable<HtmlNode> Nodes()
        {
            return this.items.SelectMany(item => item.ChildNodes);
        }

        /// <summary>
        /// Add node to the beginning of the collection
        /// </summary>
        /// <param name="node">The node.</param>
        public void Prepend(HtmlNode node)
        {
            HtmlNode first = null;
            if (this.items.Count > 0)
            {
                first = this.items[0];
            }

            this.items.Insert(0, node);

            if (node == first)
            {
                throw new InvalidProgramException("Unexpected error.");
            }

            node.NextSibling = first;
            node.PreviousSibling = null;
            node.ParentNode = this.parentnode;

            if (first != null)
            {
                first.PreviousSibling = node;
            }
        }

        /// <summary>
        /// Remove node
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// The remove.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public bool Remove(HtmlNode item)
        {
            int i = this.items.IndexOf(item);
            this.RemoveAt(i);
            return true;
        }

        /// <summary>
        /// Remove node at index
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>
        /// The remove.
        /// </returns>
        public bool Remove(int index)
        {
            this.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Remove <see cref="HtmlNode"/> at index
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        /// </exception>
        public void RemoveAt(int index)
        {
            HtmlNode next = null;
            HtmlNode prev = null;
            HtmlNode oldnode = this.items[index];

            if (index > 0)
            {
                prev = this.items[index - 1];
            }

            if (index < (this.items.Count - 1))
            {
                next = this.items[index + 1];
            }

            this.items.RemoveAt(index);

            if (prev != null)
            {
                if (next == prev)
                {
                    throw new InvalidProgramException("Unexpected error.");
                }

                prev.NextSibling = next;
            }

            if (next != null)
            {
                next.PreviousSibling = prev;
            }

            oldnode.PreviousSibling = null;
            oldnode.NextSibling = null;
            oldnode.ParentNode = null;
        }

        /// <summary>
        /// Replace node at index
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="node">The node.</param>
        public void Replace(int index, HtmlNode node)
        {
            HtmlNode next = null;
            HtmlNode prev = null;
            HtmlNode oldnode = this.items[index];

            if (index > 0)
            {
                prev = this.items[index - 1];
            }

            if (index < (this.items.Count - 1))
            {
                next = this.items[index + 1];
            }

            this.items[index] = node;

            if (prev != null)
            {
                if (node == prev)
                {
                    throw new InvalidProgramException("Unexpected error.");
                }

                prev.NextSibling = node;
            }

            if (next != null)
            {
                next.PreviousSibling = node;
            }

            node.PreviousSibling = prev;

            if (next == node)
            {
                throw new InvalidProgramException("Unexpected error.");
            }

            node.NextSibling = next;
            node.ParentNode = this.parentnode;

            oldnode.PreviousSibling = null;
            oldnode.NextSibling = null;
            oldnode.ParentNode = null;
        }

        #region Explicit Interface Methods

        /// <summary>
        /// Get Enumerator
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<HtmlNode> IEnumerable<HtmlNode>.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Get Explicit Enumerator
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        #endregion
    }
}