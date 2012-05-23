//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.ExecuteNonQuery.Generic.cs" company="genuine">
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

    /// <content>
    ///     Contains Data Access Layer Utilities to executes a SQL statement against a connection object. Use to change the data in a database 
    /// by executing UPDATE, INSERT, or DELETE statements.
    ///  </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ExecuteNonQuery.Generic.cs" title="SqlQuery.ExecuteNonQuery.Generic.cs" lang="C#" />
    /// </example> 
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Executes a Transact-SQL statement against the connection and returns the number of rows affected. 
        /// Use to change the data in a database by executing UPDATE, INSERT, or DELETE statements. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TReturnObject">The type of the return object. Recommended the Nullable type.</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents a parameter array to a SqlCommand</param>
        /// <returns>The return value</returns>
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
        /// <code title="C# example one" lang="C#">
        /// <![CDATA[
        /// SqlQuery.ExecuteNonQuery<long>(
        ///                   "[Extranet].[CountArchiveEvents]
        ///                 , new SqlParameter() { ParameterName = "@Count", DbType= DbType.Int, Direction = ParameterDirection.Output });    
        /// // Recommended way to avoid bad cases then parameter @Count isn't set in Sql procedure               
        /// SqlQuery.ExecuteNonQuery<long?>(
        ///                 , "[Extranet].[CountArchiveEvents]
        ///                 , new SqlParameter() { ParameterName = "@Count", DbType= DbType.Int, Direction = ParameterDirection.Output });
        /// </code>
        /// <code title="C# example two" lang="C#">
        /// SqlQuery.ExecuteNonQuery<guid?>(
        ///                   "[Extranet].[EventsInsertUpdateByKey]"
        ///                 , new SqlParameter() { ParameterName = "@EventKey", DbType= DbType.Guid, Direction = ParameterDirection.Output }
        ///                 , new SqlParameter("@Title", calendarevent.Title)
        ///                 , new SqlParameter("@DateFrom", calendarevent.DateFrom)
        ///                 , new SqlParameter("@DateTo", calendarevent.DateTo)
        ///                 , new SqlParameter("@Summary", calendarevent.Summary)
        ///                 , new SqlParameter("@Story", calendarevent.Story)
        ///                 , new SqlParameter("@EventCategoryKey", calendarevent.EventCategoryKey));
        /// ]]>
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ExecuteNonQuery.Generic.cs" title="SqlQuery.ExecuteNonQuery.Generic.cs" lang="C#" />
        /// </example> 
        public static TReturnObject ExecuteNonQuery<TReturnObject>(string sqlprocedure, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery<TReturnObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Executes a Transact-SQL statement against the connection and returns the number of rows affected. 
        /// Use to change the data in a database by executing UPDATE, INSERT, or DELETE statements.
        /// </summary>
        /// <typeparam name="TReturnObject">The type of the return object. Recommended the Nullable type.</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents a parameter array to a SqlCommand</param>
        /// <returns>The return value</returns>
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
        /// <code title="C# Example one" lang="C#">
        /// <![CDATA[
        /// SqlQuery.ExecuteNonQuery<long>(
        ///                 CommandType.StoredProcedure
        ///                 , "[Extranet].[CountArchiveEvents]"
        ///                 , new SqlParameter() { ParameterName = "@Count", DbType= DbType.Int, Direction = ParameterDirection.Output });    
        /// // Recommended way to avoid bad cases then parameter @Count isn't set in Sql procedure               
        /// SqlQuery.ExecuteNonQuery<long?>(
        ///                 CommandType.StoredProcedure
        ///                 , "[Extranet].[CountArchiveEvents]"
        ///                 , new SqlParameter() { ParameterName = "@Count", DbType= DbType.Int, Direction = ParameterDirection.Output });
        /// </code>
        /// <code lang="C#" title="C# Example two">
        /// SqlQuery.ExecuteNonQuery<guid?>(
        ///                 CommandType.StoredProcedure
        ///                 , "[Extranet].[EventsInsertUpdateByKey]"
        ///                 , new SqlParameter() { ParameterName = "@EventKey", DbType= DbType.Guid, Direction = ParameterDirection.Output }
        ///                 , new SqlParameter("@Title", calendarevent.Title)
        ///                 , new SqlParameter("@DateFrom", calendarevent.DateFrom)
        ///                 , new SqlParameter("@DateTo", calendarevent.DateTo)
        ///                 , new SqlParameter("@Summary", calendarevent.Summary)
        ///                 , new SqlParameter("@Story", calendarevent.Story)
        ///                 , new SqlParameter("@EventCategoryKey", calendarevent.EventCategoryKey));
        /// ]]>
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ExecuteNonQuery.Generic.cs" title="SqlQuery.ExecuteNonQuery.Generic.cs" lang="C#" />
        /// </example> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static TReturnObject ExecuteNonQuery<TReturnObject>(CommandType commandtype, string sql, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery<TReturnObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, parameters);
        }

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// Use to change the data in a database by executing UPDATE, INSERT, or DELETE statements.
        /// </summary>
        /// <typeparam name="TReturnObject">The type of the return object. Recommended the Nullable type.</typeparam>
        /// <param name="connectionstring">The sql connection string.</param>
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents a parameter array to a SqlCommand</param>
        /// <returns>The return value</returns>
        /// <example>View code: <br />
        /// <code title="C#  Example one" lang="C#">
        /// <![CDATA[
        /// SqlQuery.ExecuteNonQuery<long>(
        /// connectionstring,
        /// CommandType.StoredProcedure
        /// , "[Extranet].[CountArchiveEvents]"
        /// , new SqlParameter() { ParameterName = "@Count", DbType= DbType.Int, Direction = ParameterDirection.Output });
        /// // Recommended way to avoid bad cases then parameter @Count isn't set in Sql procedure
        /// SqlQuery.ExecuteNonQuery<long?>(
        /// connectionstring,
        /// CommandType.StoredProcedure
        /// , "[Extranet].[CountArchiveEvents]"
        /// , new SqlParameter() { ParameterName = "@Count", DbType= DbType.Int, Direction = ParameterDirection.Output });
        /// </code>
        /// <code lang="C#" title="C# Example two">
        /// SqlQuery.ExecuteNonQuery<guid?>(
        /// connectionstring,
        /// CommandType.StoredProcedure
        /// , "[Extranet].[EventsInsertUpdateByKey]"
        /// , new SqlParameter() { ParameterName = "@EventKey", DbType= DbType.Guid, Direction = ParameterDirection.Output }
        /// , new SqlParameter("@Title", calendarevent.Title)
        /// , new SqlParameter("@DateFrom", calendarevent.DateFrom)
        /// , new SqlParameter("@DateTo", calendarevent.DateTo)
        /// , new SqlParameter("@Summary", calendarevent.Summary)
        /// , new SqlParameter("@Story", calendarevent.Story)
        /// , new SqlParameter("@EventCategoryKey", calendarevent.EventCategoryKey));
        /// ]]>
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.ExecuteNonQuery.Generic.cs" title="SqlQuery.ExecuteNonQuery.Generic.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static TReturnObject ExecuteNonQuery<TReturnObject>(string connectionstring, CommandType type, string sql, params SqlParameter[] parameters)
        {
            TReturnObject output = default(TReturnObject);

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

                    sqlcommand.ExecuteNonQuery();

                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            if ((parameter.Direction != ParameterDirection.Input) && (parameter.Value is TReturnObject))
                            {
                                output = (TReturnObject)parameter.Value;
                            }
                        }
                    }
                }
            }

            return output;
        }
    }
}
