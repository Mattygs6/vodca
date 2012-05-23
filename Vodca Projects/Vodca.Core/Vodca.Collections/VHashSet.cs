//-----------------------------------------------------------------------------
// <copyright file="VHashSet.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/09/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Linq;

    /// <summary>
    /// The HashSet with IToXelement interface
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    [SuppressMessage("Microsoft.Usage", "CA2229:ImplementSerializationConstructors", Justification = "No additional properties or functionality changes are added")]
    [SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly", Justification = "No additional properties or functionality changes are added")]
    [Serializable]
    public partial class VHashSet<TObject> : HashSet<TObject> where TObject : class,  IToXElement, IValidate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VHashSet{TObject}"/> class.
        /// </summary>
        public VHashSet()
        {
            this.XElementItemName = typeof(TObject).Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VHashSet&lt;TObject&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public VHashSet(IEqualityComparer<TObject> comparer)
            : base(comparer)
        {
            this.XElementItemName = typeof(TObject).Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VHashSet&lt;TObject&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public VHashSet(IEnumerable<TObject> collection)
            : base(collection)
        {
            this.XElementItemName = typeof(TObject).Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VHashSet&lt;TObject&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public VHashSet(IEnumerable<TObject> collection, IEqualityComparer<TObject> comparer)
            : base(collection, comparer)
        {
            this.XElementItemName = typeof(TObject).Name;
        }

        /// <summary>
        /// Gets or sets the name of the XElement item.
        /// </summary>
        /// <value>
        /// The name of the XElement item.
        /// </value>
        public string XElementItemName { get; set; }

        /// <summary>
        /// Adds the specified object.
        /// </summary>
        /// <param name="tobject">The object.</param>
        public new void Add(TObject tobject)
        {
            if (tobject != null && tobject.Validate())
            {
                base.Add(tobject);
            }
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement()
        {
            string name = typeof(TObject).Name + "s";
            return this.ToXElement(name);
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement(string rootname)
        {
            var root = new XElement(rootname);

            foreach (var item in this)
            {
                root.Add(item.ToXElement(this.XElementItemName));
            }

            return root;
        }
    }
}
