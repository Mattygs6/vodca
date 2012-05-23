//-----------------------------------------------------------------------------
// <copyright file="IVAction.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    /// <summary>
    /// The interfaces for action attributes and entities
    /// </summary>
    public interface IVAction
    {
        /// <summary>
        /// Gets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        /// <remarks>
        /// Used in context of Activator.CreateInstance(type) or JsonConvert.DeserializeObject(json, type)
        /// </remarks>
        Type ActionType { get; }

        /// <summary>
        /// Gets the constructor JSON.
        /// </summary>
        /// <value>
        /// The JSON.
        /// </value>
        /// <remarks>Used by JsonConvert.DeserializeObject(json, type)</remarks>
        string ActionTypeJson { get; }
    }
}