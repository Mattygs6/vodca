//-----------------------------------------------------------------------
// <copyright file="SqlQuery.IList.Caching.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/03/2010
//-----------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;

    /// <content>
    ///     Contains generics Sql operation like Select All or ByKey(s) where return result is single column record list. The only difference between SqlQuery.IList and SqlQuery.IEnumerable is returning types.
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.Caching.cs" title="SqlQuery.IList.Caching.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Find All records from the selected table. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="sqlstoredprocedure">The name of a stored procedure</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns array of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.Caching.cs" title="SqlQuery.IList.Caching.cs" lang="C#" />
        /// </example>
        public static List<TObject> IList<TObject>(string sqlstoredprocedure, VCacheTime cachetime, params SqlParameter[] parameters) where TObject : class
        {
            return IList<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlstoredprocedure, cachetime, parameters);
        }

        /// <summary>
        /// Find All records from the selected table.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns array of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.Caching.cs" title="SqlQuery.IList.Caching.cs" lang="C#" />
        /// </example>
        public static List<TObject> IList<TObject>(CommandType commandtype, string sql, VCacheTime cachetime, params SqlParameter[] parameters) where TObject : class
        {
            return IList<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, cachetime, parameters);
        }

        /// <summary>
        /// Find All records from the selected table.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns array of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.Caching.cs" title="SqlQuery.IList.Caching.cs" lang="C#" />
        /// </example>
        public static List<TObject> IList<TObject>(string connectionstring, CommandType commandtype, string sql, VCacheTime cachetime, params SqlParameter[] parameters) where TObject : class
        {
            string cachekey = Cache.GenerateCacheKey<TObject>("SqlQuery.IList<TObject>", sql, parameters);

            var list = HttpRuntime.Cache[cachekey] as List<TObject>;

            if (list == null)
            {
                list = IList<TObject>(connectionstring, commandtype, sql, parameters);

                // Ensure cache conditions
                if (list != null && cachetime > VCacheTime.None)
                {
                    // Adding SqlCacheDependency has no effect if ASP.NET SqlCacheDependencyAdmin notification not enabled
                    HttpRuntime.Cache.Insert(cachekey, list, null, DateTime.Now.AddMinutes((double)cachetime), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }

            return list;
        }

        /* ReSharper restore InconsistentNaming */
    }
}
