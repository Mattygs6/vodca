//-----------------------------------------------------------------------
// <copyright file="SqlQuery.TObject.Cache.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/11/2010
//-----------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.Caching;

    /// <content>
    ///     Contains generics Sql operation with cache mechanism on the web server
    /// </content>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Find all Sql record by primary key
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="sqlprocedure">The sql procedure name.</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="primarykey">The primary and/or foreign key(s)</param>
        /// <returns>Returns a instance of the generic object</returns>
        /// <remarks>Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library!</remarks>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.Cache.cs" title="SqlQuery.TObject.Cache.cs" lang="C#" />
        /// </example>
        public static TObject ItemByKey<TObject>(string sqlprocedure, VCacheTime cachetime, params SqlParameter[] primarykey) where TObject : class
        {
            return ItemByKey<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, cachetime, null, primarykey);
        }

        /// <summary>
        ///     Find all Sql record by primary key
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="primarykey">The primary or/and foreign key(s)</param>
        /// <returns>Returns a instance of the generic object</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.Cache.cs" title="SqlQuery.TObject.Cache.cs" lang="C#" />
        /// </example>
        public static TObject ItemByKey<TObject>(CommandType commandtype, string sql, VCacheTime cachetime, params SqlParameter[] primarykey) where TObject : class
        {
            return ItemByKey<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, cachetime, null, primarykey);
        }

        /// <summary>
        /// Find all Sql record by primary key
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="cachetime">The cache time.</param>
        /// <param name="cachedependency">The cache dependency.</param>
        /// <param name="primarykey">The primary or/and foreign key(s)</param>
        /// <returns>
        /// Returns a instance of the generic object
        /// </returns>
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
        /// <code title="C# class file">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// </code>
        /// <code title="ASP.NET C# file">
        /// const string Sql = @"
        ///     SELECT
        ///         [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///         [Current Product List]
        ///     WHERE
        ///         ProductID <![CDATA[=]]> @ProductID";
        /// // Retrieve object where ProductID = 1
        /// var item = SqlQuery.ItemByKey<![CDATA[<Product>]]>(connectionstring, CommandType.Text, Sql, CacheTime.Normal, null, new SqlParameter("@ProductID", 1));
        /// // Do something
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.Cache.cs" title="SqlQuery.TObject.Cache.cs" lang="C#"/>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.cs" title="Dependency C# Source File" lang="C#"/> 
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code sample")]
        public static TObject ItemByKey<TObject>(string connectionstring, CommandType commandtype, string sql, VCacheTime cachetime, CacheDependency cachedependency, params SqlParameter[] primarykey) where TObject : class
        {
            string cachekey = Cache.GenerateCacheKey<TObject>("SqlQuery.ItemByKey<TObject>", sql, primarykey);

            var entity = HttpRuntime.Cache[cachekey] as TObject;

            if (entity == null)
            {
                entity = ItemByKey<TObject>(connectionstring, commandtype, sql, primarykey);

                // Ensure cache conditions
                if (entity != null && cachetime > VCacheTime.None)
                {
                    HttpRuntime.Cache.Insert(cachekey, entity, cachedependency, DateTime.Now.AddMinutes((double)cachetime), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }

            return entity;
        }
    }
}
