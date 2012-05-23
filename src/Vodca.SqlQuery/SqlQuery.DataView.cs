//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.DataView.cs" company="genuine">
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
    ///     Contains Sql operation where return result is DataView (in-memory data read-only)
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataView.cs" title="SqlQuery.DataView.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /// <summary>
        ///     Select all records from the Sql table. NO caching involved. Command Type equals CommandType.StoredProcedure.
        /// </summary>  
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>
        ///     The returns SQL DataView.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="ASP.NET C# Page" lang="C#">
        ///     // Get DataView                
        ///     DataView dataview = SqlQuery.ExecuteReader("Sql-StoredProcedure-Name");
        ///     // Do something 
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataViewDemo()
        /// {
        ///     // Get DataView                
        ///     DataView dataview = SqlQuery.ExecuteReader("Sql-StoredProcedure-Name");
        ///     // In the real life always check if dataview is not null 
        ///     return dataview == null ? string.Empty : dataview.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataView.cs" title="SqlQuery.DataView.cs" lang="C#" />
        /// </example>
        public static DataView ExecuteReader(string sqlprocedure, params SqlParameter[] parameters)
        {
            return ExecuteReader(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Select all records from the Sql table. NO caching involved.
        /// </summary>  
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>
        ///     The returns SQL DataView.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="ASP.NET C# Page" lang="C#">
        ///     const string Sql = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                     ";
        ///     // Get DataView                
        ///     DataView dataview = SqlQuery.ExecuteReader(CommandType.Text, Sql);
        ///     // Do something 
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataViewDemo()
        /// {
        ///     const string Sql = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                     ";
        ///     // Get DataView                
        ///     DataView dataview = SqlQuery.ExecuteReader(CommandType.Text, Sql);
        ///     // In the real life always check if dataview is not null 
        ///     return dataview == null ? string.Empty : dataview.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataView.cs" title="SqlQuery.DataView.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This is impossible in this scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static DataView ExecuteReader(CommandType type, string sql, params SqlParameter[] parameters)
        {
            return ExecuteReader(SqlQueryConnection.DefaultConnectionString, type, sql, parameters);
        }

        /// <summary>
        /// Select all records from the Sql table. NO caching involved.
        /// </summary>
        /// <param name="connectionstring">The connection string.</param>
        /// <param name="type">Specifies how a command string is interpreted. Command Type (CommandType.StoredProcedure OR CommandType.Text )</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Represents parameters to a SqlCommand</param>
        /// <returns>The returns SQL DataView.</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// const string Sql = @"
        /// SELECT
        /// [ProductID]
        /// ,[ProductName]
        /// FROM
        /// [Current Product List]
        /// ";
        /// // Get DataView
        /// DataView dataview = SqlQuery.ExecuteReader(connectionstring, CommandType.Text, Sql);
        /// // Do something
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string AjaxDataViewDemo()
        /// {
        ///     const string Sql = @"
        ///                         SELECT
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM
        ///                             [Current Product List]
        ///                         ";
        ///     // Get DataView
        ///     DataView dataview = SqlQuery.ExecuteReader(connectionstring, CommandType.Text, Sql);
        ///     // In the real life always check if dataview is not null
        ///     return dataview == null ? string.Empty : dataview.SerializeToJson();
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.DataView.cs" title="SqlQuery.DataView.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This is impossible in this scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static DataView ExecuteReader(string connectionstring, CommandType type, string sql, params SqlParameter[] parameters)
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

                        return datatable.DefaultView;
                    }
                }
            }
        }
    }
}
