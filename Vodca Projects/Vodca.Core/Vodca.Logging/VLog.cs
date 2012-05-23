//-----------------------------------------------------------------------------
// <copyright file="VLog.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web;
    using Vodca.Logging;
    using Vodca.SDK.NLog;

    /// <summary>
    ///     HTTP module implementation that logs unhandled exceptions in an
    /// ASP.NET Web application to an error log.
    /// </summary>
    /// <remarks>
    /// The module added by default as far as included the Vodca.Core AssemblyInfo.Registration.cs, [assembly: VRegisterHttpModule(typeof(VLog), Order = 100)]
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "VLog", Justification = "Project Name")]
    // ReSharper disable ClassNeverInstantiated.Global
    public sealed partial class VLog : IHttpModule
    // ReSharper restore ClassNeverInstantiated.Global
    {
        /// <summary>
        ///     The generic NLog logger
        /// </summary>
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     The WebError Code Dictionary for runtime access of settings
        /// </summary>
        public static readonly ConcurrentDictionary<int, VLogErrorCode> WebErrorCodes = new ConcurrentDictionary<int, VLogErrorCode>();

        /// <summary>
        /// Initializes static members of the <see cref="VLog"/> class.  
        /// </summary>
        static VLog()
        {
            try
            {
                /* Init default NLOG logger configuration */
                InitLogManagerConfiguration();
            }
            catch (Exception exception)
            {
                Debug.Print(exception.ToString());
            }
        }

        /// <summary>
        /// Adds the specified collection to error logger.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public static void AddErrorCodes(IEnumerable<VLogErrorCode> collection)
        {
            if (collection != null)
            {
                // Add LookUp Table
                foreach (var item in collection.ToArray())
                {
                    WebErrorCodes[item.Code] = item;
                }
            }
        }

        #region IHttpModule Members

        /// <summary>
        ///     Initializes the module and prepares it to handle requests.
        /// </summary>
        /// <param name="application">An HttpApplication that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        void IHttpModule.Init(HttpApplication application)
        {
            if (application != null)
            {
                application.Error += this.OnError;

                try
                {
                    /* Add default error codes */
                    /* Fix this */
                    AddErrorCodes(GetVLogErrorCodes());
                }
                catch (Exception exception)
                {
                    VLog.Logger.Error(exception.ToString());
                }
            }
        }

        /// <summary>
        ///     Disposes of the resources (other than memory) used by the module that implements IHttpModule.
        /// </summary>
        void IHttpModule.Dispose()
        {
        }

        #endregion

        /// <summary>
        ///     Gets the web error code or default.
        /// </summary>
        /// <param name="errornumber">The error number.</param>
        /// <param name="errortype">The error type.</param>
        /// <returns>The instance of the Web Error Code</returns>
        private static VLogErrorCode GetWebErrorCodeOrDefault(int errornumber, VLogErrorTypes errortype)
        {
            VLogErrorCode error;
            if (VLog.WebErrorCodes.TryGetValue(errornumber, out error))
            {
                return error;
            }

            return new VLogErrorCode(errortype);
        }

        /// <summary>
        /// Initializes the log manager configuration.
        /// </summary>
        private static void InitLogManagerConfiguration()
        {
            try
            {
                // Step 1. Create configuration object 
                var config = new Vodca.SDK.NLog.Config.LoggingConfiguration();

                // Step 2. Create targets and add them to the configuration 
                var fileTarget = new Vodca.SDK.NLog.Targets.FileTarget
                {
                    CreateDirs = true,
                    FileName = "${basedir}/App_Logs/${shortdate}-${level}.log",
                    Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}"
                };

                config.AddTarget("file", fileTarget);

                // Step 3. Define rules
#if DEBUG
                var rule1 = new Vodca.SDK.NLog.Config.LoggingRule("*", LogLevel.Debug, fileTarget);
#else
            var rule1 = new Vodca.SDK.NLog.Config.LoggingRule("*", LogLevel.Info, fileTarget);
#endif
                config.LoggingRules.Add(rule1);

                Logger.Factory.Configuration = config;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Write(exception.ToString());
                System.Diagnostics.Debug.Fail(exception.ToString());
            }
        }

        /// <summary>
        ///     Called when an unhandled exception occurs in the execution of a page request.
        /// Even if you have exception handlers in your code, an exception generated in the ASP .NET
        /// code can still cause errors to be displayed to users.  It' text a good idea to clear these 
        /// errors here, or use the customErrors section of the web.config file to display a friendly
        /// error page
        /// </summary>
        /// <param name="sender">Http Application as sender</param>
        /// <param name="args">EventArgs is the base class for classes containing event private data.</param>
        private void OnError(object sender, EventArgs args)
        {
            var application = (HttpApplication)sender;
            var exception = application.Server.GetLastError() as HttpException;
            if (exception != null)
            {
                /* The filter will use httpapplication.Context.Server.ClearError(); to clear error. Must be rechecked.*/
                VLog.ApplicationOnErrorFilter(application, exception);
                exception = application.Server.GetLastError() as HttpException;

                if (exception != null)
                {
                    VLog.LogException(exception);
                }
            }
        }
    }
}
