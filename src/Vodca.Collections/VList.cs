//-----------------------------------------------------------------------------
// <copyright file="VList.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;

    /// <summary>
    /// Serializable and XElement List
    /// </summary>
    /// <typeparam name="TXObject">The type of the object.</typeparam>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Collections\VList.cs" title="VList.cs" lang="C#" />
    /// </example>
    [Serializable, GuidAttribute("50030600-8BF2-4C42-9460-EE922B809252")]
    public partial class VList<TXObject> : List<TXObject>, IToXElement where TXObject : IToXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VList{TXObject}"/> class.
        /// </summary>
        public VList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VList{TXObject}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public VList(IEnumerable<TXObject> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VList{TXObject}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public VList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public XElement ToXElement(string rootname = "VList")
        {
            Ensure.IsNotNullOrEmpty(rootname, "root name");

            var root = new XElement(rootname);

            for (int i = 0; i < this.Count; i++)
            {
                root.Add(this[i].ToXElement());
            }

            return root;
        }
    }
}
