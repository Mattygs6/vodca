//-----------------------------------------------------------------------
// <copyright file="SqlQuery.ICollection.Cache.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/03/2010
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
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.Cache.cs" title="SqlQuery.ICollection.Cache.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Find All records from the selected Sql Object.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure.</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns HashSet of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.Cache.cs" title="SqlQuery.ICollection.Cache.cs" lang="C#" />
        /// </example>
        public static HashSet<TObject> ICollection<TObject>(string sqlprocedure, VCacheTime cachetime, params SqlParameter[] parameters) where TObject : class
        {
            return ICollection<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, cachetime, parameters);
        }

        /// <summary>
        /// Find All records from the selected Sql Object.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns HashSet of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.Cache.cs" title="SqlQuery.ICollection.Cache.cs" lang="C#" />
        /// </example>
        public static HashSet<TObject> ICollection<TObject>(CommandType commandtype, string sql, VCacheTime cachetime, params SqlParameter[] parameters) where TObject : class
        {
            return ICollection<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, cachetime, parameters);
        }

        /// <summary>
        /// Find All records from the selected Sql Object.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns HashSet of TObject's from selected SQL table.
        /// </returns>
        /// <remarks>Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library!</remarks>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.cs" title="C# Source File" lang="C#" />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.Cache.cs" title="Dependency C# Source File" lang="C#" />
        /// </example>
        public static HashSet<TObject> ICollection<TObject>(string connectionstring, CommandType commandtype, string sql, VCacheTime cachetime, params SqlParameter[] parameters) where TObject : class
        {
            string cachekey = Cache.GenerateCacheKey<TObject>("SqlQuery.ICollection<TObject>", sql, parameters);

            var hashset = HttpRuntime.Cache[cachekey] as HashSet<TObject>;

            if (hashset == null)
            {
                hashset = ICollection<TObject>(connectionstring, commandtype, sql, parameters);

                // Ensure cache conditions
                if (hashset != null && hashset.Count > 0 && cachetime > VCacheTime.None)
                {
                    HttpRuntime.Cache.Insert(cachekey, hashset, null, DateTime.Now.AddMinutes((double)cachetime), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }

            return hashset;
        }

        /* ReSharper restore InconsistentNaming */
    }
}
