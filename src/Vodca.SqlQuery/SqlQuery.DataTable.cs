//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.DataTable.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/11/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Data;
    using System.Data.SqlClient;

    /// <content>
    ///     Contains Sql operation where return result is DataTable (in-memory data).
    ///  </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataTable.cs" title="SqlQuery.DataTable.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Select all records from the Sql table. NO caching involved. Command Type equals CommandType.StoredProcedure.
        /// </summary>  
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        ///     The returns SQL DataTable.
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
        /// <code title="C# File With SqlParameters" lang="C#">
        ///    /* Stored Procedure = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                         WHERE
        ///                             ProductID <![CDATA[<=]]> @ProductID
        ///                     ";
        ///     */
        ///     // Get DataTable where ID less then 11                
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable("Sql-StoredProcedure-Name", new SqlParameter("@ProductID", 10));
        ///     // Do something 
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataTableDemo()
        /// {
        ///     /* Stored Procedure = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                         WHERE
        ///                             ProductID <![CDATA[<=]]> @ProductID
        ///                     ";
        ///     */
        ///     // Get DataTable where ID less then 11                
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable("Sql-StoredProcedure-Name", new SqlParameter("@ProductID", 10));
        ///     // In the real life always check if datatable is not null 
        ///     return datatable == null ? string.Empty : datatable.SerializeToJson();
        /// }
        /// </code>
        /// <code title="C# File Without SqlParameters" lang="C#">
        ///     /* Stored Procedure = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                     ";
        ///     */
        ///     // Get DataTable                
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable("Sql-StoredProcedure-Name");
        ///     // Do something 
        /// }
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataTableDemo()
        /// {
        ///     /* Stored Procedure = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                     ";
        ///     */
        ///     // Get DataTable                
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(CommandType.Text, Sql);
        ///     // In the real life always check if datatable is not null 
        ///     return datatable == null ? string.Empty : datatable.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataTable.cs" title="SqlQuery.DataTable.cs" lang="C#" />
        /// </example>
        public static DataTable ExecuteReaderAsDataTable(string sqlprocedure, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsDataTable(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Select all records from the Sql table. NO caching involved.
        /// </summary>  
        /// <param name="commandtype">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        ///     The returns SQL DataTable.
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
        /// <code title="C# File With SqlParameters" lang="C#">
        ///     const string Sql = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                         WHERE
        ///                             ProductID <![CDATA[<=]]> @ProductID
        ///                     ";
        ///     // Get DataTable where ID less then 11                
        ///     using(DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(CommandType.Text, SqlSql, new SqlParameter("@ProductID", 10)))
        ///     {
        ///         // Do something
        ///     }
        /// }
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataTableDemo()
        /// {
        ///     const string Sql = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                         WHERE
        ///                             ProductID <![CDATA[<=]]> @ProductID
        ///                     ";
        ///     // Get DataTable where ID less then 11                
        ///     using(DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(CommandType.Text, SqlSql, new SqlParameter("@ProductID", 10)))
        ///     {
        ///         // In the real life always check if datatable is not null 
        ///         return datatable == null ? string.Empty : datatable.SerializeToJson();
        ///     }
        /// }
        /// </code>
        /// <code title="C# File Without SqlParameters" lang="C#">
        ///     const string Sql = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                     ";
        ///     // Get DataTable                
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(CommandType.Text, Sql);
        ///     // Do something 
        /// }
        /// </code>
        /// <code title="C# source code in System.Web.Services.WebService file" lang="C#">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataTableDemo()
        /// {
        ///     const string Sql = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                     ";
        ///     // Get DataTable                
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(CommandType.Text, Sql);
        ///     // In the real life always check if datatable is not null 
        ///     return datatable == null ? string.Empty : datatable.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataTable.cs" title="SqlQuery.DataTable.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This is impossible in this scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static DataTable ExecuteReaderAsDataTable(CommandType commandtype, string sql, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsDataTable(SqlQueryConnection.DefaultConnectionString, commandtype, sql, parameters);
        }

        /// <summary>
        /// Select all records from the Sql table. NO caching involved.
        /// </summary>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns SQL DataTable.</returns>
        /// <example>View code: <br />
        /// <code title="C# file With SqlParameters" lang="C#">
        /// const string Sql = @"
        ///     SELECT
        ///         [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///         [Current Product List]
        ///          WHERE
        ///          ProductID <![CDATA[<=]]> @ProductID
        /// ";
        /// // Get DataTable where ID less then 11
        /// using(DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(connectionstring, CommandType.Text, SqlSql, new SqlParameter("@ProductID", 10)))
        /// {
        ///     // Do something
        /// }
        /// </code>
        /// <code title="C# source code in System.Web.Services.WebService file" lang="C#">
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
        ///         WHERE
        ///             ProductID <![CDATA[<=]]> @ProductID
        /// ";
        /// // Get DataTable where ID less then 11
        /// using(DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(connectionstring, CommandType.Text, SqlSql, new SqlParameter("@ProductID", 10)))
        /// {
        ///     // In the real life always check if datatable is not null
        ///     return datatable == null ? string.Empty : datatable.SerializeToJson();
        /// }
        /// </code>
        /// <code title="C# File Without SqlParameters" lang="C#">
        /// const string Sql = @"
        /// SELECT
        /// [ProductID]
        /// ,[ProductName]
        /// FROM
        /// [Current Product List]
        /// ";
        /// // Get DataTable
        /// DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(connectionstring, CommandType.Text, Sql);
        /// // Do something
        /// }
        /// -------------------------------------------------------------------
        /// C# source code in System.Web.Services.WebService file
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
        ///     DataTable datatable = SqlQuery.ExecuteReaderAsDataTable(connectionstring, CommandType.Text, Sql);
        ///     // In the real life always check if datatable is not null
        ///     return datatable == null ? string.Empty : datatable.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataTable.cs" title="SqlQuery.DataTable.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This is impossible in this scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static DataTable ExecuteReaderAsDataTable(string connectionstring, CommandType type, string sql, params SqlParameter[] parameters)
        {
            // Initialize SQL connection
            using (var sqlconnection = new SqlConnection(connectionstring))
            {
                using (var sqlcommand = new SqlCommand(sql, sqlconnection))
                {
                    sqlcommand.CommandType = type;

                    if (parameters != null && parameters.Length > 0)
                    {
                        sqlcommand.Parameters.AddRange(parameters);
                    }

                    // Execute Sql statement
                    sqlconnection.Open();
                    using (SqlDataReader reader = sqlcommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        var datatable = new DataTable("SqlDataTable");
                        datatable.Load(reader);

                        return datatable;
                    }
                }
            }
        }
    }
}
