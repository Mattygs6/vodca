//-----------------------------------------------------------------------
// <copyright file="SqlQuery.IList.ByFirstColumn.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/03/2008
//-----------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <content>
    ///     Contains generics Sql operation like Select All or ByKey(s) where return result is single column record list. The only difference between SqlQuery.IList and SqlQuery.IEnumerable is returning types.
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.ByFirstColumn.cs" title="SqlQuery.IList.ByFirstColumn.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Find All records from the selected Sql table and add all first column  NOT NULL values to the list. 
        /// </summary>
        /// <typeparam name="TObject">The generic and primitive object types like int, string and etc</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns list of TObject's from selected SQL table.</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.ByFirstColumn.cs" title="SqlQuery.IList.ByFirstColumn.cs" lang="C#" />
        /// </example>
        public static IEnumerable<TObject> IListByFirstColumn<TObject>(string sqlprocedure, params SqlParameter[] parameters)
        {
            return IListByFirstColumn<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Find All records from the selected Sql table and add all first column  NOT NULL values to the list. 
        /// </summary>
        /// <typeparam name="TObject">The generic and primitive object types like int, string and etc</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns list of TObject's from selected SQL table.</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.ByFirstColumn.cs" title="SqlQuery.IList.ByFirstColumn.cs" lang="C#" />
        /// </example> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static IEnumerable<TObject> IListByFirstColumn<TObject>(CommandType commandtype, string sql, params SqlParameter[] parameters)
        {
            return IListByFirstColumn<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, parameters);
        }

        /// <summary>
        /// Find All records from the selected Sql table and add all first column  NOT NULL values to the list.
        /// </summary>
        /// <typeparam name="TObject">The generic and primitive object types like int, string and etc</typeparam>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns list of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# With SqlParameters" lang="C#">
        /// const string Sql = @"
        ///     SELECT
        ///         [EmployeeID]
        ///     FROM
        ///         [Employees]
        ///     WHERE
        ///         EmployeeID <![CDATA[<=]]> @EmployeeID
        ///         ORDER BY EmployeeID ASC
        /// ";
        /// // Get list of employees with id less then 6
        /// var emploeeIdList = SqlQuery.IListByFirstColumn<![CDATA[<int>]]>(connectionstring, CommandType.Text, Sql, new SqlParameter("@EmployeeID", 5));
        /// </code>
        /// <code title="C# File Without SqlParameters" lang="C#">
        /// const string Sql = @"
        ///     SELECT
        ///         [EmployeeID]
        ///     FROM
        ///         [Employees]
        ///         ORDER BY EmployeeID ASC
        /// ";
        /// var emploeeIdList = SqlQuery.IListByFirstColumn<![CDATA[<int>]]>(connectionstring, CommandType.Text, Sql);
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.ByFirstColumn.cs" title="SqlQuery.IList.ByFirstColumn.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static IEnumerable<TObject> IListByFirstColumn<TObject>(string connectionstring, CommandType commandtype, string sql, params SqlParameter[] parameters)
        {
            // Initialize SQL connection
            using (var sqlconnection = new SqlConnection(connectionstring))
            {
                using (var sqlcommand = new SqlCommand(sql, sqlconnection))
                {
                    sqlcommand.CommandType = commandtype;

                    if (parameters != null && parameters.Length > 0)
                    {
                        sqlcommand.Parameters.AddRange(parameters);
                    }

                    // Execute Sql statement
                    sqlconnection.Open();

                    using (var reader = sqlcommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Add only first column
                                if (reader[0] != DBNull.Value)
                                {
                                    yield return (TObject)reader[0];
                                }
                            }
                        }
                    }
                }
            }
        }

        /* ReSharper restore InconsistentNaming */
    }
}
