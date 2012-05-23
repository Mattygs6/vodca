//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.VDataEntityList.cs" company="genuine">
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
    ///     Contains Sql operation where return result is VDataEntityList (single row from table or view).
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.VDataEntityList.cs" title="SqlQuery.VDataEntityList.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Select all records from the Sql table. NO caching involved. Command Type equals CommandType.StoredProcedure.
        /// </summary>  
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        ///     The returns VDataEntityList or Null in case of No records.
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
        /// <code title="C# source code in ASP.NET page file" lang="C#">
        /// <![CDATA[
        ///  /* StoredProcedure = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// */
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList("Sql-StoredProcedure-Name", new SqlParameter("@EmployeeID", 1));
        /// string user =  string.Concat("Employee: ", row.GetValueAsInt("EmployeeID"), " - " ,row.GetValueAsString("FirstName"), ' ' , row.GetValueAsString("LastName"));
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string DataEntityListDemo()
        /// {
        ///     /* StoredProcedure = @"
        ///         SELECT [EmployeeID]
        ///               ,[LastName]
        ///               ,[FirstName]
        ///           FROM [Employees]
        ///           WHERE EmployeeID = @EmployeeID
        ///               ";
        ///     */
        ///     // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        ///     // Get Product By ID 1
        ///     VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList("Sql-StoredProcedure-Name", new SqlParameter("@EmployeeID", 1));
        ///     // In the real life always check if row is not null then using some sqlparameter(s)
        ///     // Null if Sql Record doesn't return any results
        ///     return row == null ? string.Empty : row.SerializeToJson();
        /// }
        /// ]]>
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.VDataEntityList.cs" title="SqlQuery.VDataEntityList.cs" lang="C#" />
        /// </example>
        public static VDataEntityList ExecuteReaderAsDataEntityList(string sqlprocedure, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsDataEntityList(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Select all records from the Sql table. NO caching involved.
        /// </summary>  
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="storedprocedure">Specifies how a command string is interpreted. Stored Procedure Name or Sql Query</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        ///     The returns VDataEntityList or Null in case of No records.
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
        /// <code title=" C# source code in ASP.NET page file" lang="C#">
        /// <![CDATA[
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// string user =  string.Concat("Employee: ", row.GetValueAsInt("EmployeeID"), " - " ,row.GetValueAsString("FirstName"), ' ' , row.GetValueAsString("LastName"));
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string DataEntityListDemo()
        /// {
        ///     const string SqlByKey = @"
        ///         SELECT [EmployeeID]
        ///               ,[LastName]
        ///               ,[FirstName]
        ///           FROM [Employees]
        ///           WHERE EmployeeID = @EmployeeID
        ///               ";
        ///     // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        ///     // Get Product By ID 1
        ///     VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        ///     // In the real life always check if row is not null then using some sqlparameter(s)
        ///     // Null if Sql Record doesn't return any results
        ///     return row == null ? string.Empty : row.SerializeToJson();
        /// }
        /// ]]>
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.VDataEntityList.cs" title="SqlQuery.VDataEntityList.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static VDataEntityList ExecuteReaderAsDataEntityList(CommandType type, string storedprocedure, params SqlParameter[] parameters)
        {
            return ExecuteReaderAsDataEntityList(SqlQueryConnection.DefaultConnectionString, type, storedprocedure, parameters);
        }

        /// <summary>
        /// Select all records from the Sql table. NO caching involved.
        /// </summary>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="storedprocedure">Specifies how a command string is interpreted. Stored Procedure Name or Sql Query</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns VDataEntityList or Null in case of No records.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# source code in ASP.NET page file" lang="C#">
        /// <![CDATA[
        /// const string SqlByKey = @"
        /// SELECT [EmployeeID]
        /// ,[LastName]
        /// ,[FirstName]
        /// FROM [Employees]
        /// WHERE EmployeeID = @EmployeeID
        /// ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(connectionstring, CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// string user =  string.Concat("Employee: ", row.GetValueAsInt("EmployeeID"), " - " ,row.GetValueAsString("FirstName"), ' ' , row.GetValueAsString("LastName"));
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string DataEntityListDemo()
        /// {
        /// const string SqlByKey = @"
        /// SELECT [EmployeeID]
        /// ,[LastName]
        /// ,[FirstName]
        /// FROM [Employees]
        /// WHERE EmployeeID = @EmployeeID
        /// ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Product By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(connectionstring, CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// // In the real life always check if row is not null then using some sqlparameter(s)
        /// // Null if Sql Record doesn't return any results
        /// return row == null ? string.Empty : row.SerializeToJson();
        /// }
        /// ]]>
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.VDataEntityList.cs" title="SqlQuery.VDataEntityList.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static VDataEntityList ExecuteReaderAsDataEntityList(string connectionstring, CommandType type, string storedprocedure, params SqlParameter[] parameters)
        {
            // Initialize SQL connection
            using (var sqlconnection = new SqlConnection(connectionstring))
            {
                using (var sqlcommand = new SqlCommand(storedprocedure, sqlconnection))
                {
                    sqlcommand.CommandType = type;

                    if (parameters != null && parameters.Length > 0)
                    {
                        sqlcommand.Parameters.AddRange(parameters);
                    }

                    // Execute Sql statement
                    sqlconnection.Open();
                    using (var reader = sqlcommand.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        return VDataEntityList.Load(reader);
                    }
                }
            }
        }
    }
}
