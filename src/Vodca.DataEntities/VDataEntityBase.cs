//-----------------------------------------------------------------------------
// <copyright file="VDataEntityBase.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/31/2012
//-----------------------------------------------------------------------------
extern alias SDK;

namespace Vodca
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using SDK::Newtonsoft.Json;

    public abstract partial class VDataEntity : IToXElement, INotifyCRUD
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VDataEntity"/> class.
        /// </summary>
        protected VDataEntity()
        {
            this.Inserting += OnBeforeInsert;
            this.Inserting += Insert;
            this.Updating += OnBeforeUpdate;
            this.Updating += Update;
            this.Deleting += OnBeforeDelete;
            this.Deleting += Delete;
#if DEBUG

#endif
        }

        /// <summary>
        /// Gets or sets the uid.
        /// </summary>
        /// <value>
        /// The uid.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Uid { get; set; }

        /// <summary>
        /// Gets or sets the content string.
        /// </summary>
        /// <value>
        /// The content string.
        /// </value>
        [Column(TypeName = "Xml")]
        [JsonIgnore, XmlIgnore]
        public abstract string ContentString { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        [Column(TypeName = "datetime2")]
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        [Column(TypeName = "datetime2")]
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
            return new XElement(rootname,
                new XElement("Uid", this.Uid.ToShortId()),
                new XElement("DateCreated", this.DateCreated),
                new XElement("DateModified", this.DateModified),
                XElement.Parse(this.ContentString));
        }
    }
}
