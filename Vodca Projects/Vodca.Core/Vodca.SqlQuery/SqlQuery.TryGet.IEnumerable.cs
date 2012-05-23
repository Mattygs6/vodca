//-----------------------------------------------------------------------
// <copyright file="SqlQuery.TryGet.IEnumerable.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/10/2008
//-----------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <content>
    ///     Contains generics Sql operation like Select All or ByKey(s) where return result is single column record list.
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.TryGetIEnumerable.cs" title="SqlQuery.TObject.TryGetIEnumerable.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Find All records from MULTIPLE COLUMNS AND ROWS in the selected table. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns array of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.TryGetIEnumerable.cs" title="SqlQuery.TObject.TryGetIEnumerable.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code sample")]
        public static IEnumerable<TObject> TryGetIEnumerable<TObject>(string sqlprocedure, params SqlParameter[] parameters) where TObject : class
        {
            return TryGetIEnumerable<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Find All records from MULTIPLE COLUMNS AND ROWS in the selected table.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns array of TObject's from selected SQL table.</returns>
        /// <remarks>Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library!</remarks>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.TryGetIEnumerable.cs" title="SqlQuery.TObject.TryGetIEnumerable.cs" lang="C#" />
        /// </example> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static IEnumerable<TObject> TryGetIEnumerable<TObject>(CommandType commandtype, string sql, params SqlParameter[] parameters) where TObject : class
        {
            return TryGetIEnumerable<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, parameters);
        }

        /// <summary>
        /// Find All records from MULTIPLE COLUMNS AND ROWS in the selected table.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns array of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File With SqlParameters" lang="C#">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// .........................................................
        /// const string Sql = @"
        ///     SELECT
        ///         [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///         [Current Product List]
        ///     WHERE
        ///         ProductID <![CDATA[<]]> @ProductID
        /// ";
        /// // Retrieve all collection where ProductID less then 10
        /// var collection = SqlQuery.TryGetIEnumerable<![CDATA[<Product>]]>(connectionstring, CommandType.Text, Sql, new SqlParameter("@ProductID", 10));
        /// // Do some binding with Controls having DataSource
        /// }
        /// </code>
        /// <code title="C# File Without SqlParameters" lang="C#">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// ...............................................................
        /// const string Sql = @"
        ///     SELECT
        ///         [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///         [Current Product List]
        /// ";
        /// // Retrieve all collection
        /// var collection = SqlQuery.TryGetIEnumerable<![CDATA[<Product>]]>(connectionstring, CommandType.Text, Sql);
        /// // Do some binding with Controls having DataSource
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.TryGetIEnumerable.cs" title="SqlQuery.TObject.TryGetIEnumerable.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static IEnumerable<TObject> TryGetIEnumerable<TObject>(string connectionstring, CommandType commandtype, string sql, params SqlParameter[] parameters) where TObject : class
        {
            try
            {
                return IEnumerable<TObject>(connectionstring, commandtype, sql, parameters);
            }
            catch (SqlException sqlex)
            {
                VLog.LogException(sqlex);
            }
            catch (Exception ex)
            {
                VLog.LogException(ex);
            }

            return null;
        }

        /* ReSharper restore InconsistentNaming */
    }
}
