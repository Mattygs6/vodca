//-----------------------------------------------------------------------------
// <copyright file="VApiHttpMethod.cs" company="genuine">
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
    /// The Web API http request methods 
    /// </summary>
    [Serializable, Flags]
    public enum VApiHttpMethod
    {
        /// <summary>
        /// http request method
        /// </summary>
        Get = 0,

        /// <summary>
        /// http request method
        /// </summary>
        Post = 1,

        /// <summary>
        /// http request method
        /// </summary>
        Put = 2,

        /// <summary>
        /// http request method
        /// </summary>
        Delete = 4,
    }
}
