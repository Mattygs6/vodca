//-----------------------------------------------------------------------------
// <copyright file="VDataEntity.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/31/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    /// <summary>
    /// Vodca Data Entities for Entity Framework Code First
    /// </summary>
    /// <example>
    /// http://devpress.gilinux.dev/gi/net/net-vodca-data-entities-how-to
    /// </example>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract partial class VDataEntity<TEntity> : VDataEntity where TEntity : class, IToXElement, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VDataEntity&lt;TEntity&gt;"/> class.
        /// </summary>
        protected VDataEntity() : this(new TEntity()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VDataEntity&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="TDataEntity">The T data entity.</param>
        protected VDataEntity(TEntity TDataEntity)
            :base()
        {
            this.DataEntity = TDataEntity; 
        }

        /// <summary>
        /// Gets or sets the content string.
        /// </summary>
        /// <value>
        /// The content string.
        /// </value>
        public override string ContentString
        {
            get
            {
                return this.DataEntity.SerializeToSqlXml();
            }

            set
            {
                this.DataEntity = value.DeserializeFromXml<TEntity>();
            }
        }

        /// <summary>
        /// Gets or sets the inner entity.
        /// </summary>
        /// <value>
        /// The inner entity.
        /// </value>
        [NotMapped]
        public virtual TEntity DataEntity { get; set; }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        public override XElement ToXElement(string rootname = null)
        {
            rootname = string.IsNullOrWhiteSpace(rootname)
                ? string.Format("{0}Outer", this.DataEntity.GetType().Name)
                : rootname;

            return new XElement(rootname,
                new XElement("Uid", this.Uid.ToShortId()),
                new XElement("DateCreated", this.DateCreated),
                new XElement("DateModified", this.DateModified),
                this.DataEntity.ToXElement());
        }
    }
}
