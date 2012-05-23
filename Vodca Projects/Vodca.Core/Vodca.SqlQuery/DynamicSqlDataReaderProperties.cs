//-----------------------------------------------------------------------------
// <copyright file="DynamicSqlDataReaderProperties.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:      J.Baltikauskas
//  Date:           12/18/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data.SqlClient;
    using System.Reflection;

    /// <summary>
    /// Dynamic SqlData Reader properties
    /// </summary>
    internal static class DynamicSqlDataReaderProperties
    {
        /// <summary>
        ///     The attributes and meta-data of a method 'get_Item'.
        /// </summary>
        internal static readonly MethodInfo GetValueMethod = typeof(SqlDataReader).GetMethod("get_Item", new[] { typeof(int) });

        /// <summary>
        ///     The attributes and meta-data of a method 'IsDBNull'.
        /// </summary>
        internal static readonly MethodInfo IsDbNullMethod = typeof(SqlDataReader).GetMethod("IsDBNull", new[] { typeof(int) });

        /// <summary>
        ///     Represents type declarations for SqlDataReader
        /// </summary>
        internal static readonly Type DataRecordType = typeof(SqlDataReader);
    }
}