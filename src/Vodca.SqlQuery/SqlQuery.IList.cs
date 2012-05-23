//-----------------------------------------------------------------------
// <copyright file="SqlQuery.IList.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/03/2008
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
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.cs" title="SqlQuery.IList.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        /// Find All records from the selected table. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns array of TObject's from selected SQL table.
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
        /// <code title="C# Class" lang="C#">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// </code>
        /// <code lang="C#" title="ASPX Page C# with sqlParameters">
        /// // Retrieve all collection where ProductID less then 10
        /// var collection = SqlQuery.IEnumerable<![CDATA[<Product>]]>("Sql-StoredProcedure-Name", new SqlParameter("@ProductID", 10));
        /// // Do some binding with Controls having DataSource
        /// Repeater control = new Repeater();
        /// control.ItemDataBound += new RepeaterItemEventHandler(DemoRepeaterControl_ItemDataBound);
        /// control.DataSource = collection;
        /// control.DataBind();
        /// this.Page.Form.Controls.Add(control);
        /// ........
        /// // Repeater Item Data Bind demo
        /// void DemoRepeaterControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        /// {
        ///     switch (e.Item.ItemType)
        ///     {
        ///         case ListItemType.AlternatingItem:
        ///         case ListItemType.Item:
        ///             TObjectProductList item = (Product)e.Item.DataItem;
        ///             Literal literal = new Literal();
        ///             literal.Text = string.Format("ProductID - {0}, ProductName - {1}<br/>", item.ProductID, item.ProductName);
        ///             e.Item.Controls.Add(literal);
        ///             break;
        ///         default:
        ///         break;
        ///     }
        /// }
        /// </code>
        /// <code title="C# File Without SqlParameters" lang="C#">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// .....
        /// // Retrieve all collection
        /// var collection = SqlQuery.IList<![CDATA[<Product>]]>("Sql-StoredProcedure-Name");
        /// // Do some binding with Controls having DataSource
        /// Repeater control = new Repeater();
        /// control.ItemDataBound += new RepeaterItemEventHandler(DemoRepeaterControl_ItemDataBound);
        /// control.DataSource = collection;
        /// control.DataBind();
        /// this.Page.Form.Controls.Add(control);
        /// .....
        /// // Repeater Item Data Bind demo
        /// void DemoRepeaterControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        /// {
        ///     switch (e.Item.ItemType)
        ///     {
        ///         case ListItemType.AlternatingItem:
        ///         case ListItemType.Item:
        ///             TObjectProductList item = (Product)e.Item.DataItem;
        ///             Literal literal = new Literal();
        ///             literal.Text = string.Format("ProductID - {0}, ProductName - {1}<br/>", item.ProductID, item.ProductName);
        ///             e.Item.Controls.Add(literal);
        ///             break;
        ///         default:
        ///             break;
        ///     }
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.cs" title="SqlQuery.IList.cs" lang="C#" />
        /// </example>
        public static List<TObject> IList<TObject>(string sqlprocedure, params SqlParameter[] parameters) where TObject : class
        {
            return IList<TObject>(SqlQueryConnection.DefaultConnectionString, CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Find All records from the selected table.
        /// </summary>
        /// <typeparam name="TObject">The generic object</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns array of TObject's from selected SQL table.</returns>
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
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// .........
        ///     const string Sql = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                         WHERE 
        ///                             ProductID <![CDATA[<]]> @ProductID
        ///                     ";
        ///     // Retrieve all collection where ProductID less then 10
        ///     var collection = SqlQuery.IEnumerable<![CDATA[<Product>]]>(CommandType.Text, Sql, new SqlParameter("@ProductID", 10));
        ///     // Do some binding with Controls having DataSource
        ///     Repeater control = new Repeater();
        ///     control.ItemDataBound += new RepeaterItemEventHandler(DemoRepeaterControl_ItemDataBound);
        ///     control.DataSource = collection;
        ///     control.DataBind();
        ///     this.Page.Form.Controls.Add(control);
        /// .........
        /// // Repeater Item Data Bind demo
        /// void DemoRepeaterControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        /// {
        ///     switch (e.Item.ItemType)
        ///     {
        ///         case ListItemType.AlternatingItem:
        ///         case ListItemType.Item:
        ///             TObjectProductList item = (Product)e.Item.DataItem;
        ///             Literal literal = new Literal();
        ///             literal.Text = string.Format("ProductID - {0}, ProductName - {1}<br />", item.ProductID, item.ProductName);
        ///             e.Item.Controls.Add(literal);
        ///             break;
        ///         default:
        ///             break;
        ///     }
        /// }
        /// </code>
        /// <code title="C# File Without SqlParameters" lang="C#">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// .........................
        ///     const string Sql = @"
        ///                         SELECT 
        ///                              [ProductID]
        ///                             ,[ProductName]
        ///                         FROM 
        ///                             [Current Product List]
        ///                     ";
        ///     // Retrieve all collection
        ///     var collection = SqlQuery.IList<![CDATA[<Product>]]>(CommandType.Text, Sql);
        ///     // Do some binding with Controls having DataSource
        ///     Repeater control = new Repeater();
        ///     control.ItemDataBound += new RepeaterItemEventHandler(DemoRepeaterControl_ItemDataBound);
        ///     control.DataSource = collection;
        ///     control.DataBind();
        ///     this.Page.Form.Controls.Add(control);
        /// .........................
        /// // Repeater Item Data Bind demo
        /// void DemoRepeaterControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        /// {
        ///     switch (e.Item.ItemType)
        ///     {
        ///         case ListItemType.AlternatingItem:
        ///         case ListItemType.Item:
        ///             TObjectProductList item = (Product)e.Item.DataItem;
        ///             Literal literal = new Literal();
        ///             literal.Text = string.Format("ProductID - {0}, ProductName - {1}<br />", item.ProductID, item.ProductName);
        ///             e.Item.Controls.Add(literal);
        ///             break;
        ///         default:
        ///             break;
        ///     }
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.cs" title="SqlQuery.IList.cs" lang="C#" />
        /// </example>
        /// <remarks>
        /// <pre>
        /// <b>Important:</b>
        ///     For ListControls only use specially designed XListItem<![CDATA[<TObject>]]>
        ///     // OnLoad method
        ///     ListControl control = (ListControl)sender;
        ///     // Id is type of int in SqlTable and Country is typeof( varchar)
        ///     var items = SqlQuery.IEnumerable<![CDATA[<XListItem<int>>]]>(CommandType.Text, "SELECT Id AS Value, Country as Text FROM Countries");
        ///     control.Items.AddRange(items);
        /// </pre>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static List<TObject> IList<TObject>(CommandType commandtype, string sql, params SqlParameter[] parameters) where TObject : class
        {
            return IList<TObject>(SqlQueryConnection.DefaultConnectionString, commandtype, sql, parameters);
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
        /// The returns array of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File With SqlParameters" lang="C#">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// ...................................
        /// const string Sql = @"
        ///     SELECT
        ///         [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///         [Current Product List]
        ///     WHERE
        ///         ProductID <![CDATA[<]]> @ProductID
        /// ";
        /// // Retrieve all collection where ProductID less then 10
        /// var collection = SqlQuery.IEnumerable<![CDATA[<Product>]]>(connectionstring, CommandType.Text, Sql, new SqlParameter("@ProductID", 10));
        /// // Do some binding with Controls having DataSource
        /// Repeater control = new Repeater();
        /// control.ItemDataBound += new RepeaterItemEventHandler(DemoRepeaterControl_ItemDataBound);
        /// control.DataSource = collection;
        /// control.DataBind();
        /// this.Page.Form.Controls.Add(control);
        /// ..................................
        /// // Repeater Item Data Bind demo
        /// void DemoRepeaterControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        /// {
        ///     switch (e.Item.ItemType)
        ///     {
        ///         case ListItemType.AlternatingItem:
        ///         case ListItemType.Item:
        ///             TObjectProductList item = (Product)e.Item.DataItem;
        ///             Literal literal = new Literal();
        ///             literal.Text = string.Format("ProductID - {0}, ProductName - {1}<br/>", item.ProductID, item.ProductName);
        ///             e.Item.Controls.Add(literal);
        ///             break;
        ///         default:
        ///             break;
        ///     }
        /// }
        /// </code>
        /// <code title="C# File Without SqlParameters" lang="C#">
        /// public class Product
        /// {
        ///     public int ProductID { get; set; }
        ///     public string ProductName { get; set; }
        /// }
        /// .................................
        /// const string Sql = @"
        ///     SELECT
        ///         [ProductID]
        ///         ,[ProductName]
        ///     FROM
        ///         [Current Product List]
        /// ";
        /// // Retrieve all collection
        /// var collection = SqlQuery.IList<![CDATA[<Product>]]>(connectionstring, CommandType.Text, Sql);
        /// // Do some binding with Controls having DataSource
        /// Repeater control = new Repeater();
        /// control.ItemDataBound += new RepeaterItemEventHandler(DemoRepeaterControl_ItemDataBound);
        /// control.DataSource = collection;
        /// control.DataBind();
        /// this.Page.Form.Controls.Add(control);
        /// .........................................
        /// // Repeater Item Data Bind demo
        /// void DemoRepeaterControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        /// {
        ///     switch (e.Item.ItemType)
        ///     {
        ///         case ListItemType.AlternatingItem:
        ///         case ListItemType.Item:
        ///             TObjectProductList item = (Product)e.Item.DataItem;
        ///             Literal literal = new Literal();
        ///             literal.Text = string.Format("ProductID - {0}, ProductName - {1}<br/>", item.ProductID, item.ProductName);
        ///             e.Item.Controls.Add(literal);
        ///             break;
        ///         default:
        ///             break;
        ///     }
        /// }
        /// </code>
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IList.cs" title="SqlQuery.IList.cs" lang="C#" />
        /// </example>
        /// <remarks>
        /// <pre>
        /// <b>Important:</b>
        ///     For ListControls only use specially designed XListItem<![CDATA[<TObject>]]>
        ///     // OnLoad method
        ///     ListControl control = (ListControl)sender;
        ///     // Id is type of int in SqlTable and Country is typeof( varchar)
        ///     var items = SqlQuery.IEnumerable<![CDATA[<XListItem<int>>]]>(CommandType.Text, "SELECT Id AS Value, Country as Text FROM Countries");
        ///     control.Items.AddRange(items);
        /// </pre>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]
        public static List<TObject> IList<TObject>(string connectionstring, CommandType commandtype, string sql, params SqlParameter[] parameters) where TObject : class
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
                        var builder = DynamicSqlDataReader<TObject>.CreateDynamicMethod(reader);

                        var list = new List<TObject>();
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
