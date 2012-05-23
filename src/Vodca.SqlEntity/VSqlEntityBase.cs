//-----------------------------------------------------------------------------
// <copyright file="VSqlEntityBase.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       05/22/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Vodca.SDK.Newtonsoft.Json;

    /// <summary>
    /// Vodca SQl Entity base
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public abstract partial class VSqlEntity<TKey> : IToXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VSqlEntity&lt;TKey&gt;"/> class.
        /// </summary>
        protected VSqlEntity()
        {
        }

        /// <summary>
        /// Gets or sets the uid.
        /// </summary>
        /// <value>
        /// The uid.
        /// </value>
        public virtual TKey Uid { get; set; }

        /// <summary>
        /// Gets or sets the content string.
        /// </summary>
        /// <value>
        /// The content string.
        /// </value>
        [JsonIgnore, XmlIgnore]
        public abstract string Xml { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        public virtual DateTime DateModified { get; set; }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public virtual XElement ToXElement(string rootname = "DataEntity")
        {
            return new XElement(
                rootname,
                new XElement("Uid", this.Uid),
                new XElement("DateCreated", this.DateCreated),
                new XElement("DateModified", this.DateModified),
                XElement.Parse(this.Xml));
        }
    }
}
