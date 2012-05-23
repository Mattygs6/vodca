//-----------------------------------------------------------------------
// <copyright file="SqlQuery.Caching.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/03/2010
//-----------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;

    /// <summary>
    ///     Contains  the cache generators and extension logic
    /// </summary>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Http Runtime cache operations
        /// </summary>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Caching.cs" title="SqlQuery.Caching.cs" lang="C#" />
        /// </example>
        public static partial class Cache
        {
            /// <summary>
            ///     Clears Http Runtime cache containing any SqlQuery cached object.
            /// </summary>
            /// <example>View code: <br />
            /// <code title="C# File" lang="C#">
            /// /* Clear all SqlQuery caches */
            /// SqlQuery.Cache.Clear();    
            /// </code>
            /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Caching.cs" title="SqlQuery.Caching.cs" lang="C#" />source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Caching.cs" title="C# Source File" lang="C#" />
            /// </example>
            public static void Clear()
            {
                HttpRuntime.Cache.RemoveKeysContaining("SqlQuery");
            }

            /// <summary>
            ///     Removes the specified type from cache.
            /// </summary>
            /// <param name="type">The type of the object.</param>
            /// <example>View code: <br />
            /// <code title="C# File" lang="C#">
            /// /* Executed somewhere in code previously */
            /// var collection = SqlQuery.IEnumerable<![CDATA[<Product>]]>("Sql-StoredProcedure-Name", CacheTime.Normal);
            /// /* Remove from cache */
            /// SqlQuery.Cache.Remove(typeof(Product));
            /// </code>
            /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Caching.cs" title="SqlQuery.Caching.cs" lang="C#" />
            /// </example>
            public static void Remove(Type type)
            {
                Ensure.IsNotNull(type, "SqlQuery.Remove-type");

                var hash = type.FullName.ToHashCode().ToString(CultureInfo.InvariantCulture);
                HttpRuntime.Cache.RemoveKeysContaining(hash);
            }

            /// <summary>
            /// Removes the specified type from cache using Sql command.
            /// </summary>
            /// <param name="sql">The SQL statement or stored procedure.</param>
            /// <example>View code: <br />
            /// <code title="C# File" lang="C#">
            /// /* Executed somewhere in code previously */
            /// var collection = SqlQuery.IEnumerable<![CDATA[<Product>]]>("Sql-StoredProcedure-Name", CacheTime.Normal);
            /// /* Remove from cache */
            /// SqlQuery.Cache.Remove("Sql-StoredProcedure-Name");
            /// </code>
            /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Caching.cs" title="SqlQuery.Caching.cs" lang="C#" />
            /// </example>
            public static void Remove(string sql)
            {
                Ensure.IsNotNullOrEmpty(sql, "SqlQuery.Remove-type");
                var hash = sql.ToHashCode().ToString(CultureInfo.InvariantCulture);

                HttpRuntime.Cache.RemoveKeysContaining(hash);
            }

            /// <summary>
            ///     Generates the cache key.
            /// </summary>
            /// <typeparam name="TObject">The type of the object.</typeparam>
            /// <param name="executingmethodprefix">The executing method prefix.</param>
            /// <param name="sql">The SQL statement or stored procedure</param>
            /// <param name="parameters">The parameters.</param>
            /// <returns>
            /// The Cache key based on Sql command and parameters
            /// </returns>     
            /// <example>View code: <br />
            /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Caching.cs" title="SqlQuery.Caching.cs" lang="C#" />
            /// </example>
            internal static string GenerateCacheKey<TObject>(string executingmethodprefix, string sql, params SqlParameter[] parameters)
            {
                Type type = typeof(TObject);
                var builder = new StringBuilder(64);

                if (parameters != null && parameters.Length > 0)
                {
                    // Ensure that sequence of SqlParameters would not have influence
                    var sorted = parameters.OrderBy(n => n.ParameterName, StringComparer.OrdinalIgnoreCase);

                    foreach (SqlParameter item in sorted)
                    {
                        builder.Append(item.ParameterName).Append(':').Append(item.Value).Append('|');
                    }
                }

                // Hash code will make key shorter
                return string.Concat(executingmethodprefix, '#', type.FullName.ToHashCode(), '#', sql.ToHashCode(), '#', builder.ToString().ToHashCode());
            }
        }
    }
}
