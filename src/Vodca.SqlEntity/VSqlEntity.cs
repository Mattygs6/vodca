//-----------------------------------------------------------------------------
// <copyright file="VSqlEntity.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       05/22/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Vodca.SDK.Newtonsoft.Json;

    /// <summary>
    /// Vodca Sql Entity
    /// </summary>
    /// <typeparam name="TInner">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public abstract partial class VSqlEntity<TEntity, TInner, TKey> : VSqlEntity<TKey>
        where TEntity : VSqlEntity<TEntity, TInner, TKey>
        where TInner : class, IToXElement, new()
    {
        /// <summary>
        /// Gets or sets the name of the resolve type.
        /// </summary>
        /// <value>
        /// The name of the resolve type.
        /// </value>
        [JsonIgnore, XmlIgnore]
        public static Func<string> ResolveTypeName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VSqlEntity&lt;TInner, TKey&gt;"/> class.
        /// </summary>
        protected VSqlEntity()
            : this(new TInner())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VSqlEntity&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="entity">The data entity.</param>
        protected VSqlEntity(TInner entity)
            : base()
        {
            this.DataEntity = entity;
        }

        /// <summary>
        /// Gets or sets the content string.
        /// </summary>
        /// <value>
        /// The content string.
        /// </value>
        public override string Xml
        {
            get
            {
                return this.DataEntity.SerializeToSqlXml();
            }

            set
            {
                this.DataEntity = value.DeserializeFromXml<TInner>();
            }
        }

        /// <summary>
        /// Gets or sets the inner entity.
        /// </summary>
        /// <value>
        /// The inner entity.
        /// </value>
        public virtual TInner DataEntity { get; set; }

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>Array of entities</returns>
        public static IList<TEntity> GetAll()
        {
            return SqlQuery.TryGetIList<TEntity>(string.Concat(GetSchemaString(), ".[GetAll]"));
        }

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The entity</returns>
        public static TEntity GetById(TKey id)
        {
            return SqlQuery.TryGetItemByKey<TEntity>(
                string.Concat(GetSchemaString(), ".[GetById]"),
                new SqlParameter("@Uid", id));
        }

        /// <summary>
        /// Deletes the entity by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>True if successful</returns>
        public static bool DeleteById(TKey id)
        {
            return SqlQuery.TryExecuteNonQuery(
                string.Concat(GetSchemaString(), ".[DeleteById]"),
                new SqlParameter("@Uid", id));
        }

        /// <summary>
        /// Creates or updates the entity
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if successful</returns>
        public static bool CreateOrUpdate(TEntity entity)
        {
            return SqlQuery.TryExecuteNonQuery(
                string.Concat(GetSchemaString(), ".[CreateOrUpdate]"),
                new SqlParameter("@Uid", entity.Uid),
                new SqlParameter("@Xml", entity.Xml));
        }

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

            return new XElement(
                rootname,
                new XElement("Uid", this.Uid),
                new XElement("DateCreated", this.DateCreated),
                new XElement("DateModified", this.DateModified),
                this.DataEntity.ToXElement());
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <returns>The name of the type</returns>
        protected static string GetTypeName()
        {
            return ResolveTypeName != null
                ? ResolveTypeName()
                : typeof(TInner).Name;
        }

        /// <summary>
        /// Gets the schema string.
        /// </summary>
        /// <returns>The schema string</returns>
        protected static string GetSchemaString()
        {
            return string.Concat('[', GetTypeName(), ']');
        }
    }
}
