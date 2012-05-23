//-----------------------------------------------------------------------------
// <copyright file="VApiAccessPermission.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca.WebApi
{
    using System;

    /// <summary>
    /// Vodca Web API access
    /// </summary>
    [Serializable, Flags]
    public enum VApiAccessPermission : byte
    {
        /// <summary>
        /// The access flag
        /// </summary>
        Public,

        /// <summary>
        /// The access flag
        /// </summary>
        Secured
    }
}
