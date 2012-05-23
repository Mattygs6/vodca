//-----------------------------------------------------------------------------
// <copyright file="VDataEntity.EntityEventArgs.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/31/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data.Entity.Infrastructure;

    /// <summary>
    /// VDataEntity Event Args
    /// </summary>
    public partial class EntityEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityEventArgs"/> class.
        /// </summary>
        /// <param name="changedEntry">The changed entry.</param>
        public EntityEventArgs(DbEntityEntry changedEntry)
        {
            this.ChangedEntry = changedEntry;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EntityEventArgs"/> is cancel.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancel; otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets the changed entry.
        /// </summary>
        /// <value>
        /// The changed entry.
        /// </value>
        public DbEntityEntry ChangedEntry { get; protected set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Data.Entity.Infrastructure.DbEntityEntry"/> to <see cref="Vodca.EntityEventArgs"/>.
        /// </summary>
        /// <param name="changedEntry">The changed entry.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator EntityEventArgs(DbEntityEntry changedEntry)
        {
            if (changedEntry != null)
            {
                return new EntityEventArgs(changedEntry);
            }

            return null;
        }
    }
}
