//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.DataTable.Caching.cs" company="genuine">
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
    ///     Contains Sql operation where return result is DataTable (in-memory data) with ASP.NET runtime caching.
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataTable.Caching.cs" title="SqlQuery.DataTable.Caching.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Select all records from the SQL table. CACHING INVOLVED.
        /// </summary>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="cachingtime">The caching time.</param>
        /// <param name="parameters">Sql Parameter array.</param>
        /// <returns>The returns SQL  DataTable.</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataTable.Caching.cs" title="SqlQuery.DataTable.Caching.cs" lang="C#" />
        /// </example>
        public static DataTable ExecuteReaderAsDataTable(string sqlprocedure, VCacheTime cachingtime, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsDataTable(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, cachingtime, parameters);
        }

        /// <summary>
        /// Select all records from the SQL table. CACHING INVOLVED.
        /// </summary>
        /// <param name="commandtype">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachingtime">The caching time.</param>
        /// <param name="parameters">Sql Parameter array.</param>
        /// <returns>The returns SQL DataTable.</returns>
        /// <example>View code: <br />
        /// <code lang="xml" title="web.config">
        /// <![CDATA[
        /// /* Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library! */
        /// <connectionStrings>
        ///     <remove name="LocalSqlServer"/>
        ///     <add name="SqlQueryConnection" connectionString="Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Persist Security Info=True;User ID=USER_NAME;Password=USER_PASSWORD" providerName="System.Data.SqlClient"/>
        /// </connectionStrings> 
        /// ]]>
        /// </code>
        /// <code title="ASP.NET C# Page" lang="C#">
        /// const string Sql = @"
        /// SELECT
        ///     [ProductID]
        ///     ,[ProductName]
        /// FROM
        ///     [Current Product List]
        /// ";
        ///     // Get DataTable
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(CommandType.Text, Sql, CacheTime.Normal);
        ///     // Do something
        /// }
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataTableDemo()
        /// {
        ///     const string Sql = @"
        ///         SELECT
        ///             [ProductID]
        ///             ,[ProductName]
        ///         FROM
        ///             [Current Product List]
        ///     ";
        ///     // Get DataTable
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(CommandType.Text, Sql, CacheTime.Normal);
        ///     // In the real life always check if datatable is not null
        ///     return datatable == null ? string.Empty : datatable.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataTable.Caching.cs" title="SqlQuery.DataTable.Caching.cs" lang="C#" />
        /// </example>
        public static DataTable ExecuteReaderAsDataTable(CommandType commandtype, string sql, VCacheTime cachingtime, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsDataTable(SqlQueryConnection.DefaultConnectionString, commandtype, sql, cachingtime, parameters);
        }

        /// <summary>
        /// Select all records from the SQL table. CACHING INVOLVED.
        /// </summary>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachingtime">The caching time.</param>
        /// <param name="parameters">Sql Parameter array.</param>
        /// <returns>The returns SQL DataTable.</returns>
        /// <example>View code: <br />
        /// <code lang="xml" title="web.config">
        /// <![CDATA[
        /// /* Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library! */
        /// <connectionStrings>
        ///     <remove name="LocalSqlServer"/>
        ///     <add name="SqlQueryConnection" connectionString="Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Persist Security Info=True;User ID=USER_NAME;Password=USER_PASSWORD" providerName="System.Data.SqlClient"/>
        /// </connectionStrings> 
        /// ]]>
        /// </code>
        /// <code title="ASP.NET C# Page" lang="C#">
        /// const string Sql = @"
        ///     SELECT
        ///          [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///         [Current Product List]
        ///     ";
        ///     // Get DataTable
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(connectionstring, CommandType.Text, Sql, CacheTime.Normal);
        ///     // Do something
        /// </code>
        /// <code title="C# source code in System.Web.Services.WebService file" lang="C#">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataTableDemo()
        /// {
        ///     const string Sql = @"
        ///         SELECT
        ///              [ProductID]
        ///             ,[ProductName]
        ///         FROM
        ///              [Current Product List]
        ///     ";
        ///     // Get DataTable
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(connectionstring, CommandType.Text, Sql, CacheTime.Normal);
        ///     // In the real life always check if datatable is not null
        ///     return datatable == null ? string.Empty : datatable.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataTable.Caching.cs" title="SqlQuery.DataTable.Caching.cs" lang="C#" />
        /// </example>
        public static DataTable ExecuteReaderAsDataTable(string connectionstring, CommandType commandtype, string sql, VCacheTime cachingtime, params SqlParameter[] parameters)
        {
            string cachekey = Cache.GenerateCacheKey<DataTable>("SqlQuery.ExecuteReaderAsDataTable", sql, parameters);

            var datatable = HttpRuntime.Cache[cachekey] as DataTable;

            if (datatable == null)
            {
                datatable = ExecuteReaderAsDataTable(SqlQueryConnection.DefaultConnectionString, commandtype, sql, parameters);

                if (datatable != null && cachingtime > VCacheTime.None)
                {
                    HttpRuntime.Cache.Insert(cachekey, datatable, null, DateTime.Now.AddMinutes((double)cachingtime), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }

            return datatable;
        }
    }
}
