//-----------------------------------------------------------------------------
// <copyright file="VDbContext.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/31/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Data;
    using System.Data.Entity;

    /// <summary>
    /// 
    /// </summary>
    public partial class VDbContext : DbContext
    {
        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public override int SaveChanges()
        {
            int ret;

            foreach (var chEntity in this.ChangeTracker.Entries())
            {
                if(chEntity.Entity is VDataEntity)
                {
                    switch (chEntity.State)
                    {
                        case EntityState.Added:
                            var notifyInsert = chEntity.Entity as INotifyInserting;
                            if (notifyInsert != null)
                            {
                                notifyInsert.Inserting(chEntity);
                            }
                            break;
                        case EntityState.Modified:
                            var notifyUpdate = chEntity.Entity as INotifyUpdating;
                            if (notifyUpdate != null)
                            {
                                notifyUpdate.Updating(chEntity);
                            }
                            break;
                        case EntityState.Deleted:
                            var notifyDelete = chEntity.Entity as INotifyDeleting;
                            if (notifyDelete != null)
                            {
                                notifyDelete.Deleting(chEntity);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            ret = base.SaveChanges();

            foreach (var chEntity in this.ChangeTracker.Entries())
            {
                if (chEntity.Entity is VDataEntity)
                {
                    switch (chEntity.State)
                    {
                        case EntityState.Added:
                            var notifyInsert = chEntity.Entity as INotifyInserting;
                            if (notifyInsert != null)
                            {
                                notifyInsert.Inserted();
                            }
                            break;
                        case EntityState.Modified:
                            var notifyUpdate = chEntity.Entity as INotifyUpdating;
                            if (notifyUpdate != null)
                            {
                                notifyUpdate.Updated();
                            }
                            break;
                        case EntityState.Deleted:
                            var notifyDelete = chEntity.Entity as INotifyDeleting;
                            if (notifyDelete != null)
                            {
                                notifyDelete.Deleted();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return ret;
        }
    }
}
