//-----------------------------------------------------------------------------
// <copyright file="INotifyInserting.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/31/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    public interface INotifyInserting
    {
        Action<EntityEventArgs> Inserting{ get; }
        void Inserted();
    }
}
