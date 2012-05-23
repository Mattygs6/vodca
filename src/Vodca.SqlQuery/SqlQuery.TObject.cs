//-----------------------------------------------------------------------
// <copyright file="SqlQuery.TObject.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/10/2008
//-----------------------------------------------------------------------
namespace Vodca
{
    using System.Data;
    using System.Data.SqlClient;

    /// <content>
    ///     Contains generics Sql operation like Select All or ByKey(s).
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.cs" title="SqlQuery.TObject.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Find all Sql record by primary key. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="primarykey">The primary key</param>
        /// <returns>Returns a instance of The generic object</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.cs" title="SqlQuery.TObject.cs" lang="C#" />
        /// </example>
        public static TObject ItemByKey<TObject>(string sqlprocedure, params SqlParameter[] primarykey) where TObject : class
        {
            return ItemByKey<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, primarykey);
        }

        /// <summary>
        ///     Find all Sql record by primary key
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="primarykey">The primary key</param>
        /// <returns>Returns a instance of The generic object</returns>
        /// <remarks>Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library!</remarks>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.cs" title="SqlQuery.TObject.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static TObject ItemByKey<TObject>(CommandType commandtype, string sql, params SqlParameter[] primarykey) where TObject : class
        {
            return ItemByKey<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, primarykey);
        }

        /// <summary>
        /// Find all Sql record by primary key
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="primarykey">The primary key</param>
        /// <returns>Returns a instance of The generic object</returns>
        /// <example>View code: <br />
        /// <code title="C# class file">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     
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
        ///         ProductID <![CDATA[=]]> @ProductID
        ///     ";
        ///     
        /// // Retrieve object where ProductID = 1
        /// var item = SqlQuery.ItemByKey<![CDATA[<Product>]]>(connectionstring, CommandType.Text, Sql, new SqlParameter("@ProductID", 1));
        /// 
        /// // Do something
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TObject.cs" title="SqlQuery.TObject.cs" lang="C#" />
        /// </example> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code sample")]
        public static TObject ItemByKey<TObject>(string connectionstring, CommandType commandtype, string sql, params SqlParameter[] primarykey) where TObject : class
        {
            TObject entity = default(TObject);

            // Initialize SQL connection
            using (var sqlconnection = new SqlConnection(connectionstring))
            {
                using (var sqlcommand = new SqlCommand(sql, sqlconnection))
                {
                    sqlcommand.CommandType = commandtype;

                    if (primarykey.Length > 0)
                    {
                        // Add SQL Parameters
                        sqlcommand.Parameters.AddRange(primarykey);
                    }

                    // Execute Sql statement
                    sqlconnection.Open();

                    using (SqlDataReader reader = sqlcommand.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            DynamicSqlDataReader<TObject> builder = DynamicSqlDataReader<TObject>.CreateDynamicMethod(reader);
                            entity = builder.Build(reader);
                        }
                    }
                }
            }

            return entity;
        }
    }
}
