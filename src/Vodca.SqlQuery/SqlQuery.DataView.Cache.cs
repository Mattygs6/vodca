//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.DataView.Cache.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/11/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;

    /// <content>
    ///     Contains Sql operation where return result is DataView (in-memory data read-only)
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataView.Cache.cs" title="SqlQuery.DataView.Cache.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Select all records from the SQL table. CACHING INVOLVED.
        /// </summary>
        /// <param name="sqlprocedurename">The sql procedure name.</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>
        ///     The returns SQL  DataView or throws Sql Exception in case of SQL failure.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataView.Cache.cs" title="SqlQuery.DataView.Cache.cs" lang="C#" />
        /// </example>
        public static DataView ExecuteReader(string sqlprocedurename, VCacheTime cachetime, params SqlParameter[] parameters)
        {
            return ExecuteReader(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedurename, cachetime, parameters);
        }

        /// <summary>
        /// Select all records from the SQL table. CACHING INVOLVED.
        /// </summary>
        /// <param name="commandtype">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachetime">The caching time</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>
        /// The returns SQL  DataView or throws Sql Exception in case of SQL failure.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File With SqlParameters" lang="C#">
        ///     const string Sql = @"
        ///         SELECT
        ///              [ProductID]
        ///             ,[ProductName]
        ///         FROM
        ///             [Current Product List]
        ///         WHERE
        ///             ProductID <![CDATA[<=]]> @ProductID
        ///     ";
        ///     // Get DataView
        ///     DataView dataview = SqlQuery.ExecuteReader(CommandType.Text, Sql, CacheTime.Normal, new SqlParameter("@ProductID" , 1));
        ///     // Do something
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataViewDemo()
        /// {
        ///     const string Sql = @"
        ///         SELECT
        ///             [ProductID]
        ///             ,[ProductName]
        ///         FROM
        ///             [Current Product List]
        ///         WHERE
        ///             ProductID <![CDATA[<=]]> @ProductID
        ///     ";
        ///     // Get DataView
        ///     DataView dataview = SqlQuery.ExecuteReader(CommandType.Text, Sql, CacheTime.Normal, new SqlParameter("@ProductID" , 1));
        ///     // In the real life always check if dataview is not null
        ///     return dataview == null ? string.Empty : dataview.SerializeToJson();
        /// }
        /// </code>
        /// <code lang="C#" title="Without SqlParameters">
        /// const string Sql = @"
        ///     SELECT
        ///         [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///     [Current Product List]
        ///                    ";
        /// // Get DataView
        /// DataView dataview = SqlQuery.ExecuteReader(CommandType.Text, Sql, CacheTime.Normal, new SqlParameter("@ProductID", 10));
        /// // Do something
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataViewDemo()
        /// {
        /// const string Sql = @"
        ///     SELECT
        ///          [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///         [Current Product List]
        /// ";
        ///     // Get DataView where ID less then 11
        ///     DataView dataview = SqlQuery.ExecuteReader(CommandType.Text, Sql, CacheTime.Normal, new SqlParameter("@ProductID", 10));
        ///     // In the real life always check if dataview is not null
        ///     return dataview == null ? string.Empty : dataview.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataView.Cache.cs" title="SqlQuery.DataView.Cache.cs" lang="C#" />
        /// </example>
        public static DataView ExecuteReader(CommandType commandtype, string sql, VCacheTime cachetime, params SqlParameter[] parameters)
        {
            return ExecuteReader(SqlQueryConnection.DefaultConnectionString, commandtype, sql, cachetime, parameters);
        }

        /// <summary>
        /// Select all records from the SQL table. CACHING INVOLVED.
        /// </summary>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachingtime">The caching time</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>The returns SQL  DataView or throws Sql Exception in case of SQL failure.</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataView.Cache.cs" title="SqlQuery.DataView.Cache.cs" lang="C#" />
        /// </example>
        public static DataView ExecuteReader(string connectionstring, CommandType type, string sql, VCacheTime cachingtime, params SqlParameter[] parameters)
        {
            string cachekey = Cache.GenerateCacheKey<DataView>("SqlQuery.ExecuteReader", sql, parameters);

            var datatable = HttpRuntime.Cache[cachekey] as DataView;

            if (datatable == null)
            {
                datatable = ExecuteReader(connectionstring, type, sql, parameters);

                if (datatable != null && cachingtime > VCacheTime.None)
                {
                    HttpRuntime.Cache.Insert(cachekey, datatable, null, DateTime.Now.AddMinutes((double)cachingtime), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }

            return datatable;
        }
    }
}
