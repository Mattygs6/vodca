//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.TryExecuteNonQuery.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/16/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;

    /// <content>
    ///     Contains Data Access Layer Utilities to executes a SQL statement against a connection object. Use to change the data in a database 
    /// by executing UPDATE, INSERT, or DELETE statements.
    ///  </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TryExecuteNonQuery.cs" title="SqlQuery.TryExecuteNonQuery.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Executes a Transact-SQL statement against the connection and returns the number of rows affected. 
        /// Use to change the data in a database by executing UPDATE, INSERT, or DELETE statements.
        /// Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Represents a parameter array to a SqlCommand</param>
        /// <returns>The true if sucess and false otherwise</returns>
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
        /// SqlQuery.TryExecuteNonQuery("[Extranet].[ArchiveEvents]");
        /// </code>
        /// <code title="C# Example two" lang="C#">                  
        /// SqlQuery.TryExecuteNonQuery(
        ///                   "[Extranet].[EventsInsertUpdateByKey]"
        ///                 , new SqlParameter("@EventKey", calendarevent.EventKey)
        ///                 , new SqlParameter("@Title", calendarevent.Title)
        ///                 , new SqlParameter("@DateFrom", calendarevent.DateFrom)
        ///                 , new SqlParameter("@DateTo", calendarevent.DateTo)
        ///                 , new SqlParameter("@Summary", calendarevent.Summary)
        ///                 , new SqlParameter("@Story", calendarevent.Story)
        ///                 , new SqlParameter("@EventCategoryKey", calendarevent.EventCategoryKey));
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TryExecuteNonQuery.cs" title="SqlQuery.TryExecuteNonQuery.cs" lang="C#" />
        /// </example>
        /// <remarks>Use this method instead of 'ExecuteNonQuery' for important cases only</remarks>
        public static bool TryExecuteNonQuery(string sqlprocedure, params SqlParameter[] parameters)
        {
            return TryExecuteNonQuery(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Executes a Transact-SQL statement against the connection and returns the number of rows affected. 
        /// Use to change the data in a database by executing UPDATE, INSERT, or DELETE statements.
        /// </summary>
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents a parameter array to a SqlCommand</param>
        /// <returns>The true if sucess and false otherwise</returns>
        /// <example>View code: <br />
        /// <code title="C# Example one" lang="C#">
        /// SqlQuery.TryExecuteNonQuery(CommandType.StoredProcedure , "[Extranet].[ArchiveEvents]");
        /// </code>
        /// <code lang="C#" title="C# Example two">
        /// SqlQuery.TryExecuteNonQuery(
        ///                 CommandType.StoredProcedure
        ///                 , "[Extranet].[EventsInsertUpdateByKey]"
        ///                 , new SqlParameter("@EventKey", calendarevent.EventKey)
        ///                 , new SqlParameter("@Title", calendarevent.Title)
        ///                 , new SqlParameter("@DateFrom", calendarevent.DateFrom)
        ///                 , new SqlParameter("@DateTo", calendarevent.DateTo)
        ///                 , new SqlParameter("@Summary", calendarevent.Summary)
        ///                 , new SqlParameter("@Story", calendarevent.Story)
        ///                 , new SqlParameter("@EventCategoryKey", calendarevent.EventCategoryKey));
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TryExecuteNonQuery.cs" title="SqlQuery.TryExecuteNonQuery.cs" lang="C#" />
        /// </example>
        /// <remarks>Use this method instead of 'ExecuteNonQuery' for important cases only</remarks>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static bool TryExecuteNonQuery(CommandType type, string sql, params SqlParameter[] parameters)
        {
            return TryExecuteNonQuery(SqlQueryConnection.DefaultConnectionString, type, sql, parameters);
        }

        /// <summary>
        /// Executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// Use to change the data in a database by executing UPDATE, INSERT, or DELETE statements.
        /// </summary>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents a parameter array to a SqlCommand</param>
        /// <returns>The true if sucess and false otherwise</returns>
        /// <example>View code: <br />
        /// <code title="C# Example one" lang="C#">
        /// SqlQuery.TryExecuteNonQuery(connectionstring, CommandType.StoredProcedure, "[Extranet].[ArchiveEvents]");
        /// </code>
        /// <code lang="C#" title="Example two">
        /// SqlQuery.TryExecuteNonQuery(
        ///   connectionstring
        /// , CommandType.StoredProcedure
        /// , "[Extranet].[EventsInsertUpdateByKey]"
        /// , new SqlParameter("@EventKey", calendarevent.EventKey)
        /// , new SqlParameter("@Title", calendarevent.Title)
        /// , new SqlParameter("@DateFrom", calendarevent.DateFrom)
        /// , new SqlParameter("@DateTo", calendarevent.DateTo)
        /// , new SqlParameter("@Summary", calendarevent.Summary)
        /// , new SqlParameter("@Story", calendarevent.Story)
        /// , new SqlParameter("@EventCategoryKey", calendarevent.EventCategoryKey));
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.TryExecuteNonQuery.cs" title="SqlQuery.TryExecuteNonQuery.cs" lang="C#" />
        /// </example>
        /// <remarks>Use this method instead of 'ExecuteNonQuery' for important cases only</remarks>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static bool TryExecuteNonQuery(string connectionstring, CommandType type, string sql, params SqlParameter[] parameters)
        {
            try
            {
                ExecuteNonQuery(connectionstring, type, sql, parameters);

                return true;
            }
            catch (SqlException sqlexception)
            {
                VLog.LogException(sqlexception);
            }
            catch (Exception exception)
            {
                VLog.LogException(exception);
            }

            return false;
        }
    }
}
