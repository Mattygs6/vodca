//-----------------------------------------------------------------------------
// <copyright file="VRegistrationManager.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Configuration;
    using Vodca.SDK.NLog;

    /// <summary>
    /// The Web Infrastructure manager
    /// </summary>
    public partial class VRegistrationManager : IHttpModule
    {
        /// <summary>
        /// The manager logger
        /// </summary>
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The Collection of Assemblies to scan for attributes
        /// </summary>
        private static readonly ConcurrentBag<Assembly> AssemblyCollection;

        /// <summary>
        /// The collection holding the action attribute
        /// </summary>
        private static readonly ConcurrentBag<VRegistrationManagerActionAttribute> ActionsCollection;

        /// <summary>
        /// The collection holding the VRegister attributes
        /// </summary>
        private static readonly ConcurrentBag<VRegisterAttribute> AttributeCollection;

        /// <summary>
        /// The synch root
        /// </summary>
        private static readonly object SynchRoot = new object();

        /// <summary>
        ///  Initializes static members of the <see cref="VRegistrationManager"/> class.
        /// </summary>
        static VRegistrationManager()
        {
            // Cache the list of relevant assemblies, since we need it for many things
            AssemblyCollection = new ConcurrentBag<Assembly>();

            ActionsCollection = new ConcurrentBag<VRegistrationManagerActionAttribute>();
            AttributeCollection = new ConcurrentBag<VRegisterAttribute>();

            string setting = WebConfigurationManager.AppSettings["VRegistrationManager.AssemblyNameSearchRegex"];
            AssemblyNameSearchRegex = string.IsNullOrWhiteSpace(setting) ? @"Vodca|Framework|Library" : setting;

            foreach (var assemblyFile in InternalExtensions.GetAssemblyFiles(AssemblyNameSearchRegex))
            {
                try
                {
                    // Ignore assemblies we can't load. They could be native, etc...
                    AssemblyCollection.Add(Assembly.LoadFrom(assemblyFile));
                }
                catch (Exception exception)
                {
                    /* Error system logging */
                    exception.LogException();

                    Logger.Fatal(exception.Message, exception.ToString());

                    /* DEBUG ONLY */
                    System.Diagnostics.Debug.Fail(exception.Message);
                }
            }
#if !DEBUG
            try
            {
#endif
            foreach (var assembly in AssemblyCollection)
            {
                var attributes = assembly.GetCustomAttributes(typeof(VRegistrationManagerActionAttribute), inherit: true).OfType<VRegistrationManagerActionAttribute>().Where(x => x.Method != null).OrderBy(x => x.Order);
                foreach (var attr in attributes)
                {
                    ActionsCollection.Add(attr);
                }

                var vattributes = assembly.GetCustomAttributes(typeof(VRegisterAttribute), inherit: true).OfType<VRegisterAttribute>().OrderBy(x => x.Order);
                foreach (var vattribute in vattributes)
                {
                    AttributeCollection.Add(vattribute);
                }
            }
#if !DEBUG
            }
            catch (Exception exception)
            {
                /* Error system logging */
                exception.LogException();

                Logger.Fatal(exception.Message, exception.ToString());

                /* DEBUG ONLY */
                System.Diagnostics.Debug.Fail(exception.Message);
            }
#endif
        }

        /// <summary>
        /// Gets the assemblies to scan for attributes
        /// </summary>
        public static IEnumerable<Assembly> AttributeAssemblies
        {
            get
            {
                return AssemblyCollection;
            }
        }

        /// <summary>
        /// Gets the get attribute collection.
        /// </summary>
        public static IEnumerable<VRegistrationManagerActionAttribute> GetAttributeCollection
        {
            get
            {
                /* Give a copies only */
                return ActionsCollection.ToArray();
            }
        }

        /// <summary>
        /// Gets the get actions collection.
        /// </summary>
        public static IEnumerable<VRegisterAttribute> GetActionsCollection
        {
            get
            {
                /* Give a copies only */
                return ActionsCollection.ToArray();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        private static bool IsInitialized { get; set; }

        /// <summary>
        /// Gets or sets the assembly name search REGEX.
        /// </summary>
        /// <value>
        /// The assembly name search REGEX.
        /// </value>
        private static string AssemblyNameSearchRegex { get; set; }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            lock (SynchRoot)
            {
#if DEBUG
                IsInitialized = false;
#endif
                if (!IsInitialized)
                {
                    InitOnApplicationStartup(runonapplicationstartup: false);

                    /* Run once */
                    IsInitialized = true;
                }
            }
        }

        /// <summary>
        /// Initialize the on application startup functionality.
        /// </summary>
        /// <param name="runonapplicationstartup">if set to <c>true</c> run on application startup.</param>
        internal static void InitOnApplicationStartup(bool runonapplicationstartup)
        {
            var collection = ActionsCollection.Where(x => x.RunOnApplicationStartup() == runonapplicationstartup).OrderBy(x => x.Order).ToArray();
            foreach (var actionattr in collection)
            {
                try
                {
                    actionattr.Method.Run(AttributeCollection.Where(x => x.RunOnApplicationStartup() == runonapplicationstartup).ToArray());
                }
                catch (Exception exception)
                {
                    exception.LogException();

                    /* DEBUG ONLY */
                    System.Diagnostics.Debug.Fail(exception.Message);
                }
            }
        }
    }
}
