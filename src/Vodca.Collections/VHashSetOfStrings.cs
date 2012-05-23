//-----------------------------------------------------------------------------
// <copyright file="VHashSetOfStrings.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/09/2012
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
    [SuppressMessage("Microsoft.Usage", "CA2229:ImplementSerializationConstructors", Justification = "No additional properties or changes  used for serialization")]
    [SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly", Justification = "No additional properties or changes  used for serialization")]
    [Serializable]
    public sealed partial class VHashSetOfStrings : HashSet<string>, IToXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VHashSetOfStrings"/> class.
        /// </summary>
        public VHashSetOfStrings()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VHashSetOfStrings"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public VHashSetOfStrings(IEnumerable<string> collection)
            : this()
        {
            this.AddRange(collection);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cdata.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is cdata; otherwise, <c>false</c>.
        /// </value>
        public bool IsCData { get; set; }

        /// <summary>
        /// Adds non-empty the specified string value.
        /// </summary>
        /// <param name="value">The value.</param>
        public new void Add(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                base.Add(value);
            }
        }

        /// <summary>
        /// Adds the range of strings.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AddRange(IEnumerable<string> collection)
        {
            if (collection != null)
            {
                foreach (var value in collection)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        base.Add(value);
                    }
                }
            }
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement(string rootname = "VHashSetOfStrings")
        {
            return this.ToXElement(rootname, "Item");
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <param name="itemname">The itemname.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement(string rootname, string itemname)
        {
            var root = new XElement(rootname);

            if (this.IsCData)
            {
                foreach (var item in this)
                {
                    root.Add(itemname, item.ToXCData());
                }
            }
            else
            {
                foreach (var item in this)
                {
                    root.Add(itemname, item);
                }
            }

            return root;
        }
    }
}
