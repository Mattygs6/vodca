//-----------------------------------------------------------------------------
// <copyright file="VRegistrationStart.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using Vodca.SDK.NLog;

    /// <summary>
    /// The Web Infrastructure manager
    /// </summary>
    public static partial class VRegistrationStart
    {
        /// <summary>
        /// The manager logger
        /// </summary>
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The synch root
        /// </summary>
        private static readonly object SynchRoot = new object();

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        private static bool IsInitialized { get; set; }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            lock (SynchRoot)
            {
                if (!IsInitialized)
                {
                    try
                    {
                        /*  Assembly Microsoft.Web.Infrastructure.dll */
                        Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(VRegistrationManager));

                        /*
                         * This 'RegisterModule' method can only be called during the application's pre-start initialization stage.
                         * and rest of attributes will be called on HttpModule Init so HttpContext will be available.
                         */
                        VRegistrationManager.InitOnApplicationStartup(runonapplicationstartup: true);
                    }
                    catch (Exception exception)
                    {
                        exception.LogException();
                    }

                    IsInitialized = true;
                }
            }
        }
    }
}
