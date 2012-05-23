//-----------------------------------------------------------------------------
// <copyright file="VApiContentType.cs" company="genuine">
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
    /// Vodca Web API Content Types
    /// </summary>
    [Serializable, Flags]
    public enum VApiContentType : byte
    {
        /// <summary>
        /// The header content type
        /// </summary>
        Html = 0,

        /// <summary>
        /// The header content type
        /// </summary>
        Json = 1,

        /// <summary>
        /// The header content type
        /// </summary>
        Xml = 2,

        /// <summary>
        /// The header content type
        /// </summary>
        Text = 4,

        /// <summary>
        /// The header content type
        /// </summary>
        Custom = 8
    }
}
