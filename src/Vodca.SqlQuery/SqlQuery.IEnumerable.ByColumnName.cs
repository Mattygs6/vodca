//-----------------------------------------------------------------------
// <copyright file="SqlQuery.IEnumerable.ByColumnName.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/10/2008
//-----------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <content>
    ///     Contains generics Sql operation like Select All or ByKey(s) where return result is single column record list.
    /// </content>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IEnumerable.ByColumnName.cs" title="SqlQuery.IEnumerable.ByColumnName.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        /// Find All records from the selected Sql table and add all first column  NOT NULL values to the list.
        /// Sometimes the returned results must be serialized and exported or stored. The IList isn't serializable. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TObject">The generic and primitive object types like int, string and etc</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="columnname">Sql Column Name</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns array of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IEnumerable.ByColumnName.cs" title="SqlQuery.IEnumerable.ByColumnName.cs" lang="C#" />
        /// </example>
        public static IEnumerable<TObject> IEnumerableByColumnName<TObject>(string sqlprocedure, string columnname, params SqlParameter[] parameters)
        {
            return IListByColumnName<TObject>(CommandType.StoredProcedure, sqlprocedure, columnname, parameters);
        }

        /// <summary>
        ///     Find All records from the selected Sql table and add all first column  NOT NULL values to the list. 
        /// Sometimes the returned results must be serialized and exported or stored. The IList isn't serializable.
        /// </summary>
        /// <typeparam name="TObject">The generic and primitive object types like int, string and etc</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="columnname">Sql Column Name</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns array of TObject's from selected SQL table.</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IEnumerable.ByColumnName.cs" title="SqlQuery.IEnumerable.ByColumnName.cs" lang="C#" />
        /// </example>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code sample")]
        public static IEnumerable<TObject> IEnumerableByColumnName<TObject>(CommandType commandtype, string sql, string columnname, params SqlParameter[] parameters)
        {
            return IListByColumnName<TObject>(commandtype, sql, columnname, parameters);
        }

        /* ReSharper restore InconsistentNaming */
    }
}
