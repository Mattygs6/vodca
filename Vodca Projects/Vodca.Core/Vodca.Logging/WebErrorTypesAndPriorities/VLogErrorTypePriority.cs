//-----------------------------------------------------------------------------
// <copyright file="VLogErrorTypePriority.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Occurred error priority levels 
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "No need for None enum in this case")]
    [Serializable]
    public enum VLogErrorTypePriority : byte
    {
        /// <summary>
        ///     Normal Priority
        /// </summary>
        Normal = 0,

        /// <summary>
        ///     Low Priority
        /// </summary>
        Low = 1,

        /// <summary>
        ///     High Priority
        /// </summary>
        High = 2,

        /// <summary>
        ///     Critical Priority
        /// </summary>
        Critical = 4
    }
}
