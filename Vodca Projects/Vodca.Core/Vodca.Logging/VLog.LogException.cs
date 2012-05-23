//-----------------------------------------------------------------------------
// <copyright file="VLog.LogException.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;
    using Vodca.Logging;

    // ReSharper disable ClassNeverInstantiated.Global

    /// <summary>
    ///     HTTP module implementation that logs unhandled exceptions in an
    /// ASP.NET Web application to an error log.
    /// </summary>
    public sealed partial class VLog
    // ReSharper restore ClassNeverInstantiated.Global
    {
        /// <summary>
        ///     Log ASP.NET Server Error to the media
        /// </summary>
        /// <param name="exception">ASP.NET Server exception</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "IMPORTANT! We swallow any exception raised during the logging")]
        public static void LogException(Exception exception)
        {
            if (exception != null)
            {
                Debug.Print(exception.ToString());
                Logger.Error(exception.ToString());

                try
                {
                    var error = new VLogServerSideError(exception);

                    VLogErrorCode weberrorcode = VLog.GetWebErrorCodeOrDefault(error.ErrorCode, VLogErrorTypes.ServerSideIIS);

                    if (weberrorcode != null && !weberrorcode.ExcludeFromLogging)
                    {
                        if (VLog.OnCommitExceptionToServerRepository != null)
                        {
                            VLog.OnCommitExceptionToServerRepository(error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // IMPORTANT! We swallow any exception raised during the 
                    // logging and send them out to the trace . The idea 
                    // here is that logging of exceptions by itself should not 
                    // be  critical to the overall operation of the application.
                    // The bad thing is that we catch ANY kind of exception, 
                    // even system ones and potentially let them slip by.
                    Logger.Error(ex.ToString());
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Log ASP.NET Server Error to the media
        /// </summary>
        /// <param name="exception">ASP.NET Server exception</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "IMPORTANT! We swallow any exception raised during the logging")]
        public static void LogException(HttpException exception)
        {
            if (exception != null)
            {
                Debug.Print(exception.ToString());
                Logger.Error(exception.ToString());

                try
                {
                    var error = new VLogServerSideError(exception);

                    VLogErrorCode weberrorcode = VLog.GetWebErrorCodeOrDefault(error.ErrorCode, VLogErrorTypes.ServerSideIIS);
                    if (weberrorcode != null && !weberrorcode.ExcludeFromLogging)
                    {
                        if (VLog.OnCommitExceptionToServerRepository != null)
                        {
                            VLog.OnCommitExceptionToServerRepository(error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // IMPORTANT! We swallow any exception raised during the 
                    // logging and send them out to the trace . The idea 
                    // here is that logging of exceptions by itself should not 
                    // be  critical to the overall operation of the application.
                    // The bad thing is that we catch ANY kind of exception, 
                    // even system ones and potentially let them slip by.
                    Logger.Error(ex.ToString());
                    Debug.Print(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Log Sql Server Error to the media
        /// </summary>
        /// <param name="exception">SQL Server exception</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "IMPORTANT! We swallow any exception raised during the logging")]
        public static void LogException(SqlException exception)
        {
            if (exception != null)
            {
                Debug.Print(exception.ToString());
                Logger.Error(exception.ToString());

                try
                {
                    /* All SQL errors must be logged */
                    var error = new VLogServerSideSqlError(exception);

                    if (VLog.OnCommitExceptionToServerRepository != null)
                    {
                        VLog.OnCommitExceptionToServerRepository(error);
                    }
                }
                catch (Exception ex)
                {
                    // IMPORTANT! We swallow any exception raised during the 
                    // logging and send them out to the trace . The idea 
                    // here is that logging of exceptions by itself should not 
                    // be  critical to the overall operation of the application.
                    // The bad thing is that we catch ANY kind of exception, 
                    // even system ones and potentially let them slip by.
                    Logger.Error(ex.ToString());
                    Debug.Print(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Log Clients Browser Error to the media
        /// </summary>
        /// <param name="exception"> Clients Browser Error</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "IMPORTANT! We swallow any exception raised during the logging")]
        public static void LogException(JsException exception)
        {
            if (exception != null)
            {
                try
                {
                    Debug.Print(exception.ToString());
                    Logger.Warn(exception.Message);

                    var error = new VLogClientSideError(exception);

                    VLogErrorCode weberrorcode = VLog.GetWebErrorCodeOrDefault(exception.ErrorNumber.GetValueOrDefault(JsException.DefaultExceptionStatusCode), VLogErrorTypes.ClientSide);
                    if (weberrorcode != null && !weberrorcode.ExcludeFromLogging)
                    {
                        if (VLog.OnCommitExceptionToServerRepository != null)
                        {
                            VLog.OnCommitExceptionToServerRepository(error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // IMPORTANT! We swallow any exception raised during the 
                    // logging and send them out to the trace . The idea 
                    // here is that logging of exceptions by itself should not 
                    // be  critical to the overall operation of the application.
                    // The bad thing is that we catch ANY kind of exception, 
                    // even system ones and potentially let them slip by.
                    Logger.Error(ex.ToString());
                    Debug.Print(ex.Message);
                }
            }
        }
    }
}
