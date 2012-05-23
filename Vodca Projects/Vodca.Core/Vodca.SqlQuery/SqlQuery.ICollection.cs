//-----------------------------------------------------------------------
// <copyright file="SqlQuery.ICollection.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/03/2010
//-----------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <content>
    ///     Contains generics Sql operation like Select All or ByKey(s) where return result is single column record list. The only difference between SqlQuery.IList and SqlQuery.IEnumerable is returning types.
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.cs" title="SqlQuery.ICollection.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Find All records from the selected table. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns HashSet of TObject's from selected SQL table.</returns>
        /// <remarks>Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library!</remarks>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.cs" title="SqlQuery.ICollection.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code sample")]
        public static HashSet<TObject> ICollection<TObject>(string sqlprocedure, params SqlParameter[] parameters) where TObject : class
        {
            return ICollection<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Find All records from the selected table.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns HashSet of TObject's from selected SQL table.</returns>
        /// <remarks>Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library!</remarks>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.cs" title="SqlQuery.ICollection.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code sample")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static HashSet<TObject> ICollection<TObject>(CommandType commandtype, string sql, params SqlParameter[] parameters) where TObject : class
        {
            return ICollection<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, parameters);
        }

        /// <summary>
        /// Find All records from the selected table.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns HashSet of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ICollection.cs" title="SqlQuery.ICollection.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code sample")]
        public static HashSet<TObject> ICollection<TObject>(string connectionstring, CommandType commandtype, string sql, params SqlParameter[] parameters) where TObject : class
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

                    using (SqlDataReader reader = sqlcommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        DynamicSqlDataReader<TObject> builder = DynamicSqlDataReader<TObject>.CreateDynamicMethod(reader);

                        var list = new HashSet<TObject>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TObject entity = builder.Build(reader);
                                list.Add(entity);
                            }
                        }

                        return list;
                    }
                }
            }
        }

        /* ReSharper restore InconsistentNaming */
    }
}
