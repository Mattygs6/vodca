//-----------------------------------------------------------------------------
// <copyright file="IVSortable.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// The sort entity
    /// </summary>
    public interface IVSortable
    {
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        int Order { get; set; }
    }
}