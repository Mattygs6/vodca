//-----------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void LogException(this Exception exception)
        {
            VLog.LogException(exception);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void LogException(this HttpException exception)
        {
            VLog.LogException(exception);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void LogException(this SqlException exception)
        {
            VLog.LogException(exception);
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The Exception Message text</returns>
        public static string GetErrorMessage(this Exception exception)
        {
            if (exception != null)
            {
                if (exception.InnerException != null)
                {
                    return exception.InnerException.Message;
                }

                return exception.Message;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the error source.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>the exception source text</returns>
        public static string GetErrorSource(this Exception exception)
        {
            if (exception != null)
            {
                if (exception.InnerException != null)
                {
                    return exception.InnerException.Source;
                }

                return exception.Source;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the name of the error type.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>the exception type name text</returns>
        public static string GetErrorTypeName(this Exception exception)
        {
            if (exception != null)
            {
                if (exception.InnerException != null)
                {
                    return exception.InnerException.GetType().FullName;
                }

                return exception.GetType().FullName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the error error details.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>the exception detail text</returns>
        public static string GetErrorErrorDetails(this Exception exception)
        {
            if (exception != null)
            {
                if (exception.InnerException != null)
                {
                    return exception.InnerException.ToString();
                }

                return exception.ToString();
            }

            return string.Empty;
        }
    }
}