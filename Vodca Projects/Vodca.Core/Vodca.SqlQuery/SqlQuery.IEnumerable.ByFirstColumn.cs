//-----------------------------------------------------------------------
// <copyright file="SqlQuery.IEnumerable.ByFirstColumn.cs" company="genuine">
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
    /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IEnumerable.ByFirstColumn.cs" title="SqlQuery.IEnumerable.ByFirstColumn.cs" lang="C#" />
    /// </example>
    public static partial class SqlQuery
    {
        /* ReSharper disable InconsistentNaming */

        /// <summary>
        ///     Find All records from the selected Sql table and add all first column  NOT NULL values to the list. Command Type equals CommandType.StoredProcedure.
        /// </summary>
        /// <typeparam name="TObject">The generic and primitive object types like int, string and etc</typeparam>
        /// <param name="sqlprocedure">The name of a stored procedure</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>
        /// The returns array of TObject's from selected SQL table.
        /// </returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IEnumerable.ByFirstColumn.cs" title="SqlQuery.IEnumerable.ByFirstColumn.cs" lang="C#" />
        /// </example>
        public static IEnumerable<TObject> IEnumerableByFirstColumn<TObject>(string sqlprocedure, params SqlParameter[] parameters)
        {
            return IListByFirstColumn<TObject>(CommandType.StoredProcedure, sqlprocedure, parameters);
        }

        /// <summary>
        ///     Find All records from the selected Sql table and add all first column  NOT NULL values to the list. 
        /// </summary>
        /// <typeparam name="TObject">The generic and primitive object types like int, string and etc</typeparam>
        /// <param name="commandtype">Specifies how a command string is interpreted.</param>
        /// <param name="sql">The name of a stored procedure or an SQL text command</param>
        /// <param name="parameters">Sql Parameter array</param>
        /// <returns>The returns array of TObject's from selected SQL table.</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.SqlQuery\SqlQuery.IEnumerable.ByFirstColumn.cs" title="SqlQuery.IEnumerable.ByFirstColumn.cs" lang="C#" />
        /// </example>
        public static IEnumerable<TObject> IEnumerableByFirstColumn<TObject>(CommandType commandtype, string sql, params SqlParameter[] parameters)
        {
            return IListByFirstColumn<TObject>(commandtype, sql, parameters);
        }

        /* ReSharper restore InconsistentNaming */
    }
}
