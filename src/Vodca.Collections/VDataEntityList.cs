//-----------------------------------------------------------------------------
// <copyright file="VDataEntityList.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       08/31/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Specialized;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Linq;

    /// <summary>
    ///     Single Sql Table/View Data row implemented for fast keyed retrieval and designed for <b>Web Service with Ajax and JSON</b> mainly. <br />
    /// It is smaller and faster than a Hash Table if the number of elements is around 10. 
    /// This should not be used if performance is important for large numbers of elements > 100.
    /// Items in a VDataEntityList are not in any guaranteed order; code should not depend on the current order. 
    /// </summary>
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
    /// 
    /// <code title="C# source code in ASP.NET page file" lang="C#">
    /// const string SqlByKey = @"
    ///                             SELECT [EmployeeID]
    ///                                 ,[LastName]
    ///                                 ,[FirstName]
    ///                             FROM [Employees]
    ///                             WHERE EmployeeID = @EmployeeID
    ///                          ";
    ///                          
    /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
    /// // Get Employee By ID 1
    /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
    /// string user =  string.Concat("Employee: ", row.GetValueAsInt("EmployeeID"), " - " ,row.GetValueAsString("FirstName"), ' ' , row.GetValueAsString("LastName"));
    /// </code>
    /// 
    /// <code title="C# source code in  System.Web.Services.WebService page file" lang="C#">
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
    ///               
    ///     // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
    ///     // Get Product By ID 1
    ///     VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
    ///     // In the real life always check if row is not null then using some sql parameter(s)
    ///     // Null if Sql Record doesn't return any results
    ///     return row == null ? string.Empty : row.SerializeToJson();
    /// }
    /// </code>
    /// <code source="..\Vodca.Core\Vodca.Collections\VDataEntityList.cs" title="VDataEntityList.cs" lang="C#" />
    /// </example>
    [SuppressMessage("Microsoft.Design", "CA1035:ICollectionImplementationsHaveStronglyTypedMembers", Justification = "No need for this functionality"), Serializable]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code example")]
    public sealed partial class VDataEntityList : ListDictionary, IToXElement
    {
        /// <summary>
        ///     Gets a value indicating whether VDataEntityList Has Records
        /// </summary>
        public bool HasRecords
        {
            get { return this.Count > 0; }
        }

        /// <summary>
        ///     Initializes a new instance of the VDataEntityList class.
        /// VDataEntityList implemented as case insensitive ListDictionary under hood.
        /// </summary>
        /// <param name="reader">Provides a way of reading a forward-only stream of rows from a SQL Server  database</param>
        /// <returns>Initialized a new instance of the VDataEntityList class</returns>
        public static VDataEntityList Load(DbDataReader reader)
        {
            var list = new VDataEntityList();
            if (reader != null && reader.HasRows && reader.Read())
            {
                list = new VDataEntityList();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    list.Add(reader.GetName(i), reader.GetValue(i));
                }
            }

            return list;
        }

        /// <summary>
        ///     Initializes a new instance of the VDataEntityList class using data from DataRowView.
        /// </summary>
        /// <param name="row">DataRowView containing data</param>
        /// <returns>Initialized a new instance of the VDataEntityList class</returns>
        public static VDataEntityList Load(DataRowView row)
        {
            var list = new VDataEntityList();
            if (row != null)
            {
                DataColumnCollection columnCollection = row.DataView.Table.Columns;
                if (columnCollection.Count > 0)
                {
                    object[] rowitems = row.Row.ItemArray;

                    for (int i = 0; i < columnCollection.Count; i++)
                    {
                        list.Add(columnCollection[i].Caption, rowitems[i]);
                    }
                }
            }

            return list;
        }

        /// <summary>
        ///     Initializes a new instance of the VDataEntityList class using data from DataRow.
        /// </summary>
        /// <param name="row">DataRow containing data</param>  
        /// <returns>Initialized a new instance of the VDataEntityList class</returns>
        public static VDataEntityList Load(DataRow row)
        {
            var list = new VDataEntityList();
            if (row != null)
            {
                DataColumnCollection columnCollection = row.Table.Columns;
                if (columnCollection.Count > 0)
                {
                    object[] rowitems = row.ItemArray;
                    for (int i = 0; i < columnCollection.Count; i++)
                    {
                        list.Add(columnCollection[i].Caption, rowitems[i]);
                    }
                }
            }

            return list;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of Object.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a object.</returns>
        ///  <example>View code: <br />
        /// <code lang="xml" title="web.config">
        /// <![CDATA[
        /// /* Add connection string named "SqlQueryConnection" to the web.config file in order use Data Access Library! */
        /// <connectionStrings>
        ///     <remove name="LocalSqlServer"/>
        ///     <add name="SqlQueryConnection" connectionString="Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Persist Security Info=True;User ID=USER_NAME;Password=USER_PASSWORD" providerName="System.Data.SqlClient"/>
        /// </connectionStrings> 
        /// ]]>
        /// </code>
        /// <code title="C# File" lang="C# source code in ASP.NET page file">
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        ///                          
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// 
        /// // For Non Nullable columns only
        /// int employeeid =  (int) row.GetValue("EmployeeID"); 
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string DataEntityListDemo()
        /// {
        ///     /* The same as in ASPX */
        ///     // In the real life always check if row is not null then using sql parameter(s)
        ///     // Null if Sql Record doesn't return any results
        ///     return row == null ? string.Empty : row.SerializeToJson();
        /// }
        /// </code>
        /// </example>
        public object GetValue(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column];
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of Object ToString().
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a object.</returns>
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
        /// 
        /// // For Non Nullable columns only
        /// string employee = row.GetValueToString("LastName"); 
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string DataEntityListDemo()
        /// {
        ///     /* Same as ASPX */
        ///     // In the real life always check if row is not null then using sql parameter(s)
        ///     // Null if Sql Record doesn't return any results
        ///     return row == null ? string.Empty : row.SerializeToJson();
        /// }
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code example")]
        public string GetValueToString(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            object value = this[column];
            if (value != null)
            {
                return value.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of String.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a string.</returns>
        public string GetValueAsString(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return string.Concat(this[column]);
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable 16-bit signed integer.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable 16-bit signed integer.</returns>
        public short? GetValueAsSmallInt(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as short?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable integer.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable integer.</returns>
        /// <example>View code: <br />
        /// <code title="C# source code in ASP.NET page file" lang="C#">
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                                 ,1 AS RegionID
        ///                                 ,Null AS DistrictID
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// // For Nullable columns only. In the Northwind.dbo.Employees table hasn't Nullable Sql Int Columns. This is for demo purposes only.
        /// int? regionid =  row.GetValueAsInt("RegionID"); 
        /// int? districtid =  row.GetValueAsInt("DistrictID"); 
        /// if(districtid.HasValue)
        /// {
        /// }
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string DataEntityListDemo()
        /// {
        ///     .............................
        ///     // In the real life always check if row is not null then using some sql parameter(s)
        ///     // Null if Sql Record doesn't return any results
        ///     return row == null ? string.Empty : row.SerializeToJson();
        /// }
        /// </code>
        /// </example>
        public int? GetValueAsInt(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as int?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable long.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable long.</returns>
        /// <example>View code: <br />
        /// <code title=" C# source code in ASP.NET page file" lang="C#">
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                                 , 1 As ID
        ///                                 , NULL as ReferralID
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// // For Nullable columns only. In the Northwind.dbo.Employees table hasn't Nullable Sql long Columns. This is for demo purposes only.
        /// long? referralid =  row.GetValueAsLong("ReferralID"); 
        /// if(referralid.HasValue)
        /// {
        /// }
        /// long? id =  row.GetValueAsLong("ID"); 
        /// if(id.HasValue)
        /// {
        /// }
        /// </code>
        /// <code lang="C#" title="C# source code in System.Web.Services.WebService file">
        /// [WebMethod]
        /// [ScriptMethod(UseHttpGet = true)]
        /// public string DataEntityListDemo()
        /// {
        ///     .............................
        ///     // In the real life always check if row is not null then using some sql parameter(s)
        ///     // Null if Sql Record doesn't return any results
        ///     return row == null ? string.Empty : row.SerializeToJson();
        /// }
        /// </code>
        /// </example>
        public long? GetValueAsLong(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as long?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of double.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a double.</returns>
        /// <example>View code: <br />
        /// <code title="C# source code in ASP.NET page file" lang="C#"> 
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                                 , 35.00 AS Salary
        ///                                 , NULL AS Premium
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// // For Nullable columns only. In the Northwind.dbo.Employees table hasn't Nullable Sql double Columns. This is for demo purposes only.
        /// double? salary =  row.GetValueAsDouble("Salary"); 
        /// if(salary.HasValue)
        /// {
        /// }
        /// double? premium =  row.GetValueAsDouble("Premium"); 
        /// if(premium.HasValue)
        /// {
        /// }
        /// </code>
        /// </example>
        public double? GetValueAsDouble(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as double?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable float.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable float.</returns>
        /// <example>View code: <br />
        /// <code title="C# source code in ASP.NET page file" lang="C#">
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                                 , 35.00 AS Salary
        ///                                 , NULL AS Premium
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// // For Nullable columns only. In the Northwind.dbo.Employees table hasn't Nullable Sql double Columns. This is for demo purposes only.
        /// float? salary =  row.GetValueAsFloat("Salary"); 
        /// if(salary.HasValue)
        /// {
        /// }
        /// float? premium =  row.GetValueAsFloat("Premium"); 
        /// if(premium.HasValue)
        /// {
        /// }
        /// </code>
        /// </example>
        public float? GetValueAsFloat(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as float?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable Boolean.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable Boolean.</returns>
        /// <example>View code: <br />
        /// <code title="C# source code in ASP.NET page file" lang="C#">
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                                 , true As IsProduction
        ///                                 , NULL as IsReferral
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// // For Nullable columns only. In the Northwind.dbo.Employees table hasn't Nullable Sql long Columns. This is for demo purposes only.
        /// bool? isreferral =  row.GetValueAsLong("IsReferral"); 
        /// if(isreferral.HasValue)
        /// {
        /// }
        /// bool? isproduction =  row.GetValueAsBoolean("ID"); 
        /// if(id.HasValue)
        /// {
        /// }
        /// </code>
        /// </example>
        public bool? GetValueAsBoolean(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as bool?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable date time.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable date time.</returns>
        /// <example>View code: <br />
        /// <code title="C# source code in ASP.NET page file" lang="C#">
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                                 , '01/01/2009' AS Created
        ///                                 , NULL as LastModify
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// // For Nullable columns only. In the Northwind.dbo.Employees table hasn't Nullable Sql long Columns. This is for demo purposes only.
        /// DateTime? created =  row.GetValueAsLong("Created"); 
        /// if(created.HasValue)
        /// {
        /// }
        /// bool? lastmodify =  row.GetValueAsBoolean("LastModify"); 
        /// if(lastmodify.HasValue)
        /// {
        /// }
        /// </code>
        /// </example>
        public DateTime? GetValueAsDateTime(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as DateTime?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable decimal.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable decimal.</returns>
        /// <example>View code: <br />
        /// <code title="C# source code in ASP.NET page file" lang="C#">
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                                 , 35 AS Salary
        ///                                 , NULL AS Premium
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// // For Nullable columns only. In the Northwind.dbo.Employees table hasn't Nullable Sql double Columns. This is for demo purposes only.
        /// decimal? salary =  row.GetValueAsDecimal("Salary"); 
        /// if(salary.HasValue)
        /// {
        /// }
        /// decimal? premium =  row.GetValueAsDecimal("Premium"); 
        /// if(premium.HasValue)
        /// {
        /// }
        /// </code>
        /// </example>
        public decimal? GetValueAsDecimal(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as decimal?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable unique identifier.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable unique identifier.</returns>
        /// <example>View code: <br />
        /// <code title="C# source code in ASP.NET page file File" lang="C#">
        /// const string SqlByKey = @"
        ///                             SELECT [EmployeeID]
        ///                                 ,[LastName]
        ///                                 ,[FirstName]
        ///                                 , '141264DA-0A28-470d-BA6A-E4523B6474BA' AS MembersID
        ///                                 , NULL AS CrmMembersID
        ///                             FROM [Employees]
        ///                             WHERE EmployeeID = @EmployeeID
        ///                          ";
        /// // Design to get data for Sql Views or partial sql tables without strong type object as DataEntity
        /// // Get Employee By ID 1
        /// VDataEntityList row = SqlQuery.ExecuteReaderAsDataEntityList(CommandType.Text, SqlByKey, new SqlParameter("@EmployeeID", 1));
        /// 
        /// // For Nullable columns only. In the Northwind.dbo.Employees table hasn't Nullable Sql double Columns. This is for demo purposes only.
        /// Guid? membersid =  row.GetValueAsGuid("MembersID"); 
        /// if(membersid.HasValue)
        /// {
        /// }
        /// 
        /// Guid? crmmembersid =  row.GetValueAsGuid("CrmMembersID"); 
        /// if(crmmembersid.HasValue)
        /// {
        /// }
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code example")]
        public Guid? GetValueAsGuid(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as Guid?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of nullable byte.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a nullable byte.</returns>
        public byte? GetValueAsByte(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as byte?;
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of byte array.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a byte array.</returns>
        public byte[] GetValueAsBytes(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as byte[];
        }

        /// <summary>
        ///     Gets the value of the specified column as an instance of char array.
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        /// <returns>Gets the value of the specified column as a char array.</returns>
        public char[] GetValueAsChars(string column)
        {
            // Design By Contract Preconditions
            this.EnsureColumnExists(column);

            return this[column] as char[];
        }

        #region IToXElement Members

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootelementname">The root element name.</param>
        /// <returns>he object instance data as XElement</returns>
        public XElement ToXElement(string rootelementname = "VDataEntityList")
        {
            Ensure.IsNotNullOrEmpty(rootelementname, "rootelementname");

            var root = new XElement(rootelementname);
            foreach (var item in this.Keys)
            {
                root.Add(new XElement(string.Concat(item), this[item]));
            }

            return root;
        }

        #endregion

        /// <summary>
        ///     Ensures pre conditions
        /// </summary>
        /// <param name="column">The name of the specified column</param>
        private void EnsureColumnExists(string column)
        {
            Ensure.IsNotNullOrEmpty(column, "The column name could not be null or empty!");

            if (!this.Contains(column))
            {
                throw new ArgumentException(string.Format("The column {0} is not part of VDataEntityList", column));
            }
        }
    }
}
