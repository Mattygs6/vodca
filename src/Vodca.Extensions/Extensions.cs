//-----------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Common WebApplication extension methods
    /// </summary>
    [GuidAttribute("115425A7-80DB-4CEC-A702-CED9D6C733D7")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Synch root object
        /// </summary>
        public static readonly object SynchRoot = new object();

        /// <summary>
        ///     The method determines if object is Null/DBNull
        /// </summary>
        /// <param name="value">The object to check against it</param>
        /// <returns>True in the case object is not Null/DBNull and false otherwise</returns>
        [DebuggerHidden]
        public static bool IsNotNull(this object value)
        {
            return !(value == null || value is DBNull);
        }

        /// <summary>
        ///     The method determines if object is Null 
        /// </summary>
        /// <param name="value">The string to check against it</param>
        /// <returns>True in the case string is not Null and false otherwise</returns>
        [DebuggerHidden]
        public static bool IsNotNull(this string value)
        {
            return value != null;
        }

        /// <summary>
        ///     The method determines if object is Null/DBNull
        /// </summary>
        /// <param name="value">The object to check against it</param>
        /// <returns>True in the case object is Null/DBNull and false otherwise</returns>
        [DebuggerHidden]
        public static bool IsNull(this object value)
        {
            return value == null || value is DBNull;
        }

        /// <summary>
        ///     The method determines if string is Null 
        /// </summary>
        /// <param name="value">The string to check against it</param>
        /// <returns>True in the case object is Null and false otherwise</returns>
        [DebuggerHidden]
        public static bool IsNull(this string value)
        {
            return value == null;
        }
    }
}
