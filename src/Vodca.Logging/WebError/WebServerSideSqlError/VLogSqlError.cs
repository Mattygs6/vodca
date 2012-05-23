//-----------------------------------------------------------------------------
// <copyright file="VLogSqlError.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Data.SqlClient;

    /// <summary>
    ///     The Microsoft SqlError class can't be  XML serialized.
    ///  This class is serializable clone of SqlError
    /// </summary>
    [Serializable]
    public sealed partial class VLogSqlError
    {
        /// <summary>
        ///     Initializes a new instance of the VLogSqlError class.
        /// </summary>
        public VLogSqlError()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the VLogSqlError class.
        /// </summary>
        /// <param name="error">Sql Server error</param>
        public VLogSqlError(SqlError error)
            : this()
        {
            Ensure.IsNotNull(error, "VLogSqlError.VLogSqlError()-error");

            this.Message = error.Message;
            this.Procedure = error.Procedure;
            this.Severity = error.Class;
            this.LineNumber = error.LineNumber;
            this.Number = error.Number;
            this.Server = error.Server;
            this.Source = error.Source;
            this.State = error.State;
        }

        /// <summary>
        ///     Gets or sets a text describing the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets a name of the stored procedure or remote procedure call (RPC) that generated the error.
        /// </summary>
        public string Procedure { get; set; }

        /// <summary>
        ///     Gets or sets a the severity level of the error returned from SQL Server.
        /// </summary>
        public byte Severity { get; set; }

        /// <summary>
        ///     Gets or sets a line number within the Transact-SQL command batch or stored procedure that contains the error.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        ///     Gets or sets a number that identifies the command of error.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///     Gets or sets a name of SQL Server that generated the error.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        ///     Gets or sets a source that generated the error.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        ///     Gets or sets a numeric error code from SQL Server that represents an error, warning or "no data found" message.
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        ///     Convert from SqlError object to VLogSqlError
        /// </summary>
        /// <param name="error">SqlError instance</param>
        /// <returns>The VLogSqlError instance from SqlError</returns>
        public static VLogSqlError ConvertFrom(SqlError error)
        {
            return new VLogSqlError(error);
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current Object(VLogSqlError).</returns>
        public override int GetHashCode()
        {
            return string.Concat("VLogSqlError", this.Number).ToHashCode();
        }
    }
}
