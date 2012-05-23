//-----------------------------------------------------------------------------
// <copyright file="HtmlAttributeCollection.cs" company="genuine">
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
    public partial class HtmlAttributeCollection : IList<HtmlAttribute>
    {
        /// <summary>
        /// The _ownernode.
        /// </summary>
        private readonly HtmlNode ownernode;

        /// <summary>
        /// The items.
        /// </summary>
        private readonly List<HtmlAttribute> items = new List<HtmlAttribute>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttributeCollection"/> class.
        /// </summary>
        /// <param name="ownernode">
        /// The owner node.
        /// </param>
        internal HtmlAttributeCollection(HtmlNode ownernode)
            : this()
        {
            this.ownernode = ownernode;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="HtmlAttributeCollection"/> class from being created.
        /// </summary>
        private HtmlAttributeCollection()
        {
            this.Hashitems = new Dictionary<string, HtmlAttribute>();
        }

        /// <summary>
        /// Gets the hash items.
        /// </summary>
        public IDictionary<string, HtmlAttribute> Hashitems { get; private set; }

        /// <summary>
        ///   Gets the number of elements actually contained in the list.
        /// </summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the collection status read only 
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
        /// Gets a given attribute from the list using its name.
        /// </summary>
        /// <param name="name">The html attribute name</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        public HtmlAttribute this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException("name");
                }

                return this.Hashitems.ContainsKey(name.ToLower()) ? this.Hashitems[name.ToLower()] : null;
            }

            set
            {
                this.Append(value);
            }
        }

        /// <summary>
        /// Gets the attribute at the specified index.
        /// </summary>
        /// <param name="index">The Html attribute index</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        public HtmlAttribute this[int index]
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
        /// Adds supplied item to collection
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        public void Add(HtmlAttribute item)
        {
            this.Append(item);
        }

        /// <summary>
        /// Adds a new attribute to the collection with the given values
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, string value)
        {
            this.Append(name, value);
        }

        /// <summary>
        /// Inserts the specified attribute as the last attribute in the collection.
        /// </summary>
        /// <param name="newAttribute">
        /// The attribute to insert. May not be null. 
        /// </param>
        /// <returns>
        /// The appended attribute. 
        /// </returns>
        public HtmlAttribute Append(HtmlAttribute newAttribute)
        {
            if (newAttribute == null)
            {
                throw new ArgumentNullException("newAttribute");
            }

            this.Hashitems[newAttribute.Name] = newAttribute;
            newAttribute.OwnerNode = this.ownernode;
            this.items.Add(newAttribute);

            this.ownernode.InnerChanged = true;
            this.ownernode.OuterChanged = true;
            return newAttribute;
        }

        /// <summary>
        /// Creates and inserts a new attribute as the last attribute in the collection.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute to insert. 
        /// </param>
        /// <returns>
        /// The appended attribute. 
        /// </returns>
        public HtmlAttribute Append(string name)
        {
            HtmlAttribute att = this.ownernode.OwnerDocument.CreateAttribute(name);
            return this.Append(att);
        }

        /// <summary>
        /// Creates and inserts a new attribute as the last attribute in the collection.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute to insert. 
        /// </param>
        /// <param name="value">
        /// The value of the attribute to insert. 
        /// </param>
        /// <returns>
        /// The appended attribute. 
        /// </returns>
        public HtmlAttribute Append(string name, string value)
        {
            HtmlAttribute att = this.ownernode.OwnerDocument.CreateAttribute(name, value);
            return this.Append(att);
        }

        /// <summary>
        /// Returns all attributes with specified name. Handles case in-sentivity
        /// </summary>
        /// <param name="attributeName">Name of the attribute</param>
        /// <returns>The attributes with name</returns>
        public IEnumerable<HtmlAttribute> AttributesWithName(string attributeName)
        {
            attributeName = attributeName.ToLower();
            return this.items.Where(t => string.Equals(t.Name, attributeName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Retrieves existence of supplied item
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// The contains.
        /// </returns>
        public bool Contains(HtmlAttribute item)
        {
            return this.items.Contains(item);
        }

        /// <summary>
        /// Checks for existence of attribute with given name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The contains.
        /// </returns>
        public bool Contains(string name)
        {
            return this.items.Any(t => string.Equals(t.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Copies collection to array
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(HtmlAttribute[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Retrieves the index for the supplied item, -1 if not found
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <returns>
        /// The index of.
        /// </returns>
        public int IndexOf(HtmlAttribute item)
        {
            return this.items.IndexOf(item);
        }

        /// <summary>
        /// Inserts given item into collection at supplied index
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        public void Insert(int index, HtmlAttribute item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this.Hashitems[item.Name] = item;
            item.OwnerNode = this.ownernode;
            this.items.Insert(index, item);

            this.ownernode.InnerChanged = true;
            this.ownernode.OuterChanged = true;
        }

        /// <summary>
        /// Inserts the specified attribute as the first node in the collection.
        /// </summary>
        /// <param name="newAttribute">
        /// The attribute to insert. May not be null. 
        /// </param>
        /// <returns>
        /// The prepended attribute. 
        /// </returns>
        public HtmlAttribute Prepend(HtmlAttribute newAttribute)
        {
            this.Insert(0, newAttribute);
            return newAttribute;
        }

        /// <summary>
        /// Removes a given attribute from the list.
        /// </summary>
        /// <param name="attribute">
        /// The attribute to remove. May not be null. 
        /// </param>
        public void Remove(HtmlAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }

            int index = this.GetAttributeIndex(attribute);
            if (index == -1)
            {
                throw new IndexOutOfRangeException();
            }

            this.RemoveAt(index);
        }

        /// <summary>
        /// Removes an attribute from the list, using its name. If there are more than one attributes with this name, they will all be removed.
        /// </summary>
        /// <param name="name">
        /// The attribute's name. May not be null. 
        /// </param>
        public void Remove(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            string lname = name.ToLower();
            for (int i = 0; i < this.items.Count; i++)
            {
                HtmlAttribute att = this.items[i];
                if (att.Name == lname)
                {
                    this.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Removes all attributes from the collection
        /// </summary>
        public void Remove()
        {
            foreach (HtmlAttribute item in this.items)
            {
                item.Remove();
            }
        }

        /// <summary>
        /// Remove all attributes in the list.
        /// </summary>
        public void RemoveAll()
        {
            this.Hashitems.Clear();
            this.items.Clear();

            this.ownernode.InnerChanged = true;
            this.ownernode.OuterChanged = true;
        }

        /// <summary>
        /// Removes the attribute at the specified index.
        /// </summary>
        /// <param name="index">
        /// The index of the attribute to remove. 
        /// </param>
        public void RemoveAt(int index)
        {
            HtmlAttribute att = this.items[index];
            this.Hashitems.Remove(att.Name);
            this.items.RemoveAt(index);

            this.ownernode.InnerChanged = true;
            this.ownernode.OuterChanged = true;
        }

        /// <summary>
        /// Explicit clear
        /// </summary>
        void ICollection<HtmlAttribute>.Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// Get Explicit enumerator
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<HtmlAttribute> IEnumerable<HtmlAttribute>.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Explicit non-generic enumerator
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Explicit collection remove
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// The remove.
        /// </returns>
        bool ICollection<HtmlAttribute>.Remove(HtmlAttribute item)
        {
            return this.items.Remove(item);
        }

        /// <summary>
        /// Clears the attribute collection
        /// </summary>
        internal void Clear()
        {
            this.Hashitems.Clear();
            this.items.Clear();
        }

        /// <summary>
        /// The get attribute index.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>
        /// The attribute index.
        /// </returns>
        internal int GetAttributeIndex(HtmlAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }

            for (int i = 0; i < this.items.Count; i++)
            {
                if (this.items[i] == attribute)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// The get attribute index.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The attribute index.
        /// </returns>
        internal int GetAttributeIndex(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            string lname = name.ToLower();
            for (int i = 0; i < this.items.Count; i++)
            {
                if (this.items[i].Name == lname)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}