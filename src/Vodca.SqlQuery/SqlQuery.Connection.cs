//-----------------------------------------------------------------------------
// <copyright file="SqlQuery.Connection.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/31/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Data.SqlClient;
    using System.Web.Configuration;

    /// <summary>
    /// Web Site Default Sql Server Connections wrapper<br/>
    ///     <b>IMPORTANT:</b>
    ///     <br/>
    /// Add connection string named <b>"SqlQueryConnection"</b> to the web.config file in order use Data Access Library.
    /// </summary>
    /// <remarks>
    /// The library uses Reflection.Emit and required ReflectionPermissions enabled. Some host disabled that functionality like GoDaddy for some hosting plans
    /// </remarks>
    /// <example>View code: <br />
    /// <code title="Web.config" lang="xml">
    /// <![CDATA[
    /// <connectionStrings>
    /// <remove name="LocalSqlServer"/>
    /// <add name="SqlQueryConnection" connectionString="Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Persist Security Info=True;User ID=USER_NAME;Password=USER_PASSWORD" providerName="System.Data.SqlClient"/>
    /// </connectionStrings>
    /// ]]>
    /// </code>
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Connection.cs" title="SqlQuery.Connection.cs" lang="C#" />
    /// </example> 
    public static partial class SqlQueryConnection
    {
        /// <summary>
        /// The Default Connection String Name
        /// </summary>
        public const string DefaultConnectionStringName = "SqlQueryConnection";

        /// <summary>
        ///     Improve performance by keeping connection string for other calls
        /// </summary>
        private static string defaultConnectionString;

        /// <summary>
        ///     Gets or sets a default connection string 
        /// </summary>
        /// <remarks>
        ///     This property is added primarily for Unit Testing 
        /// where WebConfigurationManager is NULL or other special cases only
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        ///     var connection =  SqlQueryConnection.DefaultConnectionString;
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Connection.cs" title="SqlQuery.Connection.cs" lang="C#" />
        /// </example> 
        public static string DefaultConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(defaultConnectionString))
                {
                    defaultConnectionString = SetSqlQueryConnection(DefaultConnectionStringName);
                }

                return defaultConnectionString;
            }

            set
            {
                Ensure.IsNotNullOrEmpty(value, DefaultConnectionStringName);

                defaultConnectionString = value;
            }
        }

        /// <summary>
        /// Sets the SQL query connection.
        /// </summary>
        /// <param name="connectionname">The connection name.</param>
        /// <returns>The new Sql connection string</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.Connection.cs" title="SqlQuery.Connection.cs" lang="C#" />
        /// </example> 
        public static string SetSqlQueryConnection(string connectionname)
        {
            Ensure.IsNotNullOrEmpty(connectionname, "connection name");

            var connection = WebConfigurationManager.ConnectionStrings[connectionname];

            Ensure.IsNotNull(connection, string.Format(@"Web.config connectionStrings section is  missing <add name=""{0}"" connectionString=""Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Persist Security Info=True;User ID=USER_NAME;Password=USER_PASSWORD"" providerName=""System.Data.SqlClient""/>", connectionname));

            Ensure.IsNotNullOrEmpty(connection.ConnectionString, string.Format("The '{0}' connection string is missing in the web.config!", connectionname));

            defaultConnectionString = connection.ConnectionString;

            return connection.ConnectionString;
        }

        /// <summary>
        /// Gets the default SQL connection.
        /// </summary>
        /// <returns>Default sql connection</returns>
        public static SqlConnection GetDefaultSqlConnection()
        {
            return new SqlConnection(DefaultConnectionString);
        }
    }
}
