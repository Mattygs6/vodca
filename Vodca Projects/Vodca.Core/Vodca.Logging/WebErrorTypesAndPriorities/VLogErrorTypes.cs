//-----------------------------------------------------------------------------
// <copyright file="VLogErrorTypes.cs" company="genuine">
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
    ///     Error types
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "No need for None enum in this case")]
    [Serializable]
    public enum VLogErrorTypes
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Internal server error 500
        /// </summary>
        ServerSideIIS = 0,
        /* ReSharper restore InconsistentNaming */

        /// <summary>
        ///     Sql Server error
        /// </summary>
        ServerSideSql = 1,

        /// <summary>
        ///     ClientSide Java Script Error
        /// </summary>
        ClientSide = 2,

        /// <summary>
        ///     The External Web service related error
        /// </summary>
        WebServices = 4
    }
}
