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
    using System;
    using System.Data;

    /// <summary>
    /// Vodca Data Entities for Entity Framework Code First
    /// </summary>
    /// <example>
    /// http://devpress.gilinux.dev/gi/net/net-vodca-data-entities-how-to
    /// </example>
    public abstract partial class VDataEntity
    {
        /// <summary>
        /// Occurs when [before insert].
        /// </summary>
        public event EventHandler<EntityEventArgs> BeforeInsert;

        /// <summary>
        /// Occurs when [after insert].
        /// </summary>
        public event EventHandler AfterInsert;

        /// <summary>
        /// Occurs when [before update].
        /// </summary>
        public event EventHandler<EntityEventArgs> BeforeUpdate;

        /// <summary>
        /// Occurs when [after update].
        /// </summary>
        public event EventHandler AfterUpdate;

        /// <summary>
        /// Occurs when [before delete].
        /// </summary>
        public event EventHandler<EntityEventArgs> BeforeDelete;

        /// <summary>
        /// Occurs when [after delete].
        /// </summary>
        public event EventHandler AfterDelete;

        /// <summary>
        /// Gets or sets the inserting.
        /// </summary>
        /// <value>
        /// The inserting.
        /// </value>
        public Action<EntityEventArgs> Inserting { get; internal protected set; }

        /// <summary>
        /// Gets or sets the updating.
        /// </summary>
        /// <value>
        /// The updating.
        /// </value>
        public Action<EntityEventArgs> Updating { get; internal protected set; }

        /// <summary>
        /// Gets or sets the deleting.
        /// </summary>
        /// <value>
        /// The deleting.
        /// </value>
        public Action<EntityEventArgs> Deleting { get; internal protected set; }

        /// <summary>
        /// Invokes the OnAfterInsert EventHandler.
        /// </summary>
        public void Inserted()
        {
            OnAfterInsert();
        }

        /// <summary>
        /// Invokes the OnAfterUpdate EventHandler.
        /// </summary>
        public void Updated()
        {
            OnAfterUpdate();
        }

        /// <summary>
        /// Invokes the OnAfterDelete EventHandler.
        /// </summary>
        public void Deleted()
        {
            OnAfterDelete();
        }

        /// <summary>
        /// Raises the <see cref="E:BeforeInsert"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Vodca.EntityEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBeforeInsert(EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> handler = BeforeInsert;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Called when [after insert].
        /// </summary>
        protected virtual void OnAfterInsert()
        {
            EventHandler handler = AfterInsert;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:BeforeUpdate"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Vodca.EntityEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBeforeUpdate(EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> handler = BeforeUpdate;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Called when [after update].
        /// </summary>
        protected virtual void OnAfterUpdate()
        {
            EventHandler handler = AfterUpdate;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:BeforeDelete"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Vodca.EntityEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBeforeDelete(EntityEventArgs e)
        {
            EventHandler<EntityEventArgs> handler = BeforeDelete;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Called when [after delete].
        /// </summary>
        protected virtual void OnAfterDelete()
        {
            EventHandler handler = AfterDelete;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// If the event is cancelled, detach the entity to prevent inserting.
        /// </summary>
        /// <param name="e">The <see cref="Vodca.EntityEventArgs"/> instance containing the event data.</param>
        private void Insert(EntityEventArgs e)
        {
            if (e.Cancel)
            {
                e.ChangedEntry.State = EntityState.Detached;
                return;
            }

            this.DateCreated = DateTime.Now;
        }

        /// <summary>
        /// If the event is cancelled, reload data from database.
        /// </summary>
        /// <param name="e">The <see cref="Vodca.EntityEventArgs"/> instance containing the event data.</param>
        private void Update(EntityEventArgs e)
        {
            if (e.Cancel)
            {
                e.ChangedEntry.Reload();
                return;
            }

            this.DateModified = DateTime.Now;
        }

        /// <summary>
        /// If the event is cancelled, reload data from database.
        /// </summary>
        /// <param name="e">The <see cref="Vodca.EntityEventArgs"/> instance containing the event data.</param>
        private void Delete(EntityEventArgs e)
        {
            if (e.Cancel)
            {
                e.ChangedEntry.Reload();
                return;
            }
        }
    }
}
