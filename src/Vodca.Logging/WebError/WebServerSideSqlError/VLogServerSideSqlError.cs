//-----------------------------------------------------------------------------
// <copyright file="VLogServerSideSqlError.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Web;

    /// <summary>
    ///     Represents a logical SQl application errors.
    /// </summary>
    [Serializable]
    public sealed partial class VLogServerSideSqlError : VLogError
    {
        /// <summary>
        ///     Initializes a new instance of the VLogServerSideSqlError class.
        /// </summary>
        public VLogServerSideSqlError()
        {
            this.SqlErrors = new HashSet<VLogSqlError>();
            this.ErrorType = VLogErrorTypes.ServerSideSql;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VLogServerSideSqlError"/> class
        /// from a given <see cref="SqlException"/> instance and 
        /// <see cref="HttpContext"/> instance representing the HTTP 
        /// context during the exception.
        /// </summary>
        /// <param name="exception">The exception occurred in the Sql Server</param>
        public VLogServerSideSqlError(SqlException exception)
            : this()
        {
            HttpContext context = HttpContext.Current;
            if (exception != null && context != null)
            {
                // Sets an object of a uniform resource identifier properties
                this.SetAdditionalHttpContextInfo(context);
                this.SetAdditionalExceptionInfo(exception);

                foreach (SqlError error in exception.Errors)
                {
                    // Get Sql Error Info
                    VLogSqlError sqlerror = VLogSqlError.ConvertFrom(error);
                    this.SqlErrors.Add(sqlerror);
                }

                this.ErrorMessage = exception.Message;

                // Gets a string representation of the frames on the call stack at the time the current exception was thrown.
                this.ErrorDetails = exception.StackTrace;
            }
        }

        /// <summary>
        ///     Gets or sets a Sql Exceptions
        /// </summary>
        public HashSet<VLogSqlError> SqlErrors { get; set; }
    }
}
