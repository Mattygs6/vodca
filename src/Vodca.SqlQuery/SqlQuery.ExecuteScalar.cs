//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.ExecuteScalar.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;

    /// <content>
    /// Contains Data Access Layer Utilities to executes the query 
    /// and returns the first column of the first row in the result set returned by the query.
    /// </content>
    /// <example>
    /// View code: <br/>
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ExecuteScalar.cs" title="SqlQuery.ExecuteScalar.cs" lang="C#" />
    /// </example> 
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TObject">The .NET generic type to return Sql value as</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>The first column of the first row in the result set returned by the query</returns>
        /// <remarks>
        /// <pre>
        /// <b>IMPORTANT:</b>
        /// <![CDATA[
        /// Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library! 
        /// <connectionStrings>
        ///     <remove name="LocalSqlServer"/>
        ///     <add name="SqlQueryConnection" connectionString="Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Persist Security Info=True;User ID=USER_NAME;Password=USER_PASSWORD" providerName="System.Data.SqlClient"/>
        /// </connectionStrings> 
        /// ]]>
        /// </pre>
        /// </remarks>
        /// <example>
        /// <code title="C# File" lang="C#">
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// Without SqlParameters:
        /// For Sql statements as calculation use value type variables like int, long, double and etc.
        /// /* Sql Procedure: "SELECT COUNT (UserId) FROM dbo.aspnet_Membership")" */
        /// int counter = SqlQuery.ExecuteScalar<![CDATA[<int>]]>("Sql-StoredProcedure-Name");
        /// For Sql statements to retrieve values (get value from column) use Nullable value types like int?, double? and ect. if Sql column can be Nullable.
        /// <br />
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// With SqlParameters:
        /// For Sql statements as calculation use value type variables like int, long, double and etc.
        ///  /* Sql Procedure: SELECT COUNT (UserId) FROM dbo.aspnet_Membership WHERE IsApproved = @IsApproved */
        ///     int counter = SqlQuery.ExecuteScalar<![CDATA[<int>]]>("Sql-StoredProcedure-Name", new SqlParameter("@IsApproved", true)");
        /// For Sql statements to retrieve values (get value from column) use Nullable types.
        /// Like example it could be a user with particular email also it couldn't.
        /// Guid? userid = SqlQuery.ExecuteScalar<![CDATA[<Guid?>]]>(CommandType.Text, "SELECT UserId FROM dbo.aspnet_Membership WHERE Email = @Email", new SqlParameter("@Email", "Baltika@gmail.com"));
        /// if (userid.HasValue)
        /// {
        ///     // Found a user do something
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ExecuteScalar.cs" title="C# Source File" lang="C#" />
        /// </example>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        public static TObject ExecuteScalar<TObject>(string sqlprocedure, params SqlParameter[] parameters)
        {
            return ExecuteScalar<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.
        /// </summary>
        /// <typeparam name="TObject">The .NET generic type to return Sql value as</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>The first column of the first row in the result set returned by the query</returns>
        /// <remarks>
        /// <pre>
        /// <b>IMPORTANT:</b>
        /// <![CDATA[
        /// Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library! 
        /// <connectionStrings>
        ///     <remove name="LocalSqlServer"/>
        ///     <add name="SqlQueryConnection" connectionString="Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Persist Security Info=True;User ID=USER_NAME;Password=USER_PASSWORD" providerName="System.Data.SqlClient"/>
        /// </connectionStrings> 
        /// ]]>
        /// </pre>
        /// </remarks>
        /// <example>
        /// <code title="C# File" lang="C#">
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// Without SqlParameters:
        /// For Sql statements as calculation use value type variables like int, long, double and etc.
        /// int counter = SqlQuery.ExecuteScalar<![CDATA[<int>]]>(CommandType.Text, "SELECT COUNT (UserId) FROM dbo.aspnet_Membership")");
        /// For Sql statements to retrieve values (get value from column) use Nullable value types like int?, double? and ect. if Sql column can be Nullable.
        /// <br />
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// With SqlParameters:
        /// For Sql statements as calculation use value type variables like int, long, double and etc.
        ///     int counter = SqlQuery.ExecuteScalar<![CDATA[<int>]]>(CommandType.Text, "SELECT COUNT (UserId) FROM dbo.aspnet_Membership WHERE IsApproved = @IsApproved", new SqlParameter("@IsApproved", true)");
        /// For Sql statements to retrieve values (get value from column) use Nullable types.
        /// Like example it could be a user with particular email also it couldn't.
        /// Guid? userid = SqlQuery.ExecuteScalar<![CDATA[<Guid?>]]>(CommandType.Text, "SELECT UserId FROM dbo.aspnet_Membership WHERE Email = @Email", new SqlParameter("@Email", "Baltika@gmail.com"));
        /// if (userid.HasValue)
        /// {
        ///     // Found a user do something
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ExecuteScalar.cs" title="C# Source File" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        public static TObject ExecuteScalar<TObject>(CommandType commandtype, string sql, params SqlParameter[] parameters)
        {
            return ExecuteScalar<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, parameters);
        }

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.
        /// </summary>
        /// <typeparam name="TObject">The .NET generic type to return Sql value as</typeparam>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="commandtype">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>
        /// The first column of the first row in the result set returned by the query
        /// </returns>
        /// <example>
        ///     <code title="C# File" lang="C#">
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// Without SqlParameters:
        /// For Sql statements as calculation use value type variables like int, long, double and etc.
        /// int counter = SqlQuery.ExecuteScalar<![CDATA[<int>]]>(connectionstring, CommandType.Text, "SELECT COUNT (UserId) FROM dbo.aspnet_Membership")");
        /// For Sql statements to retrieve values (get value from column) use Nullable value types like int?, double? and ect. if Sql column can be Nullable.
        /// <br/>
        /// ----------------------------------------------------------------------------------------------------------------------------------------------------
        /// With SqlParameters:
        /// For Sql statements as calculation use value type variables like int, long, double and etc.
        /// int counter = SqlQuery.ExecuteScalar<![CDATA[<int>]]>(connectionstring, CommandType.Text, "SELECT COUNT (UserId) FROM dbo.aspnet_Membership WHERE IsApproved = @IsApproved", new SqlParameter("@IsApproved", true)");
        /// For Sql statements to retrieve values (get value from column) use Nullable types.
        /// Like example it could be a user with particular email also it couldn't.
        /// Guid? userid = SqlQuery.ExecuteScalar<![CDATA[<Guid?>]]>(connectionstring, CommandType.Text, "SELECT UserId FROM dbo.aspnet_Membership WHERE Email = @Email", new SqlParameter("@Email", "Baltika@gmail.com"));
        /// if (userid.HasValue)
        /// {
        /// // Found a user do something
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ExecuteScalar.cs" title="C# Source File" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static TObject ExecuteScalar<TObject>(string connectionstring, CommandType commandtype, string sql, params SqlParameter[] parameters)
        {
            TObject result = default(TObject);

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
                    object obj = sqlcommand.ExecuteScalar();
                    if (obj is TObject)
                    {
                        return (TObject)obj;
                    }

                    return result;
                }
            }
        }
    }
}