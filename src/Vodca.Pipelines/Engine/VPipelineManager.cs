//-----------------------------------------------------------------------------
// <copyright file="VPipelineManager.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca.Pipelines
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Web;
    using System.Web.Configuration;
    using Vodca.SDK.NLog;

    /// <summary>
    /// VPipelines Manager
    /// </summary>
    public sealed partial class VPipelineManager : IHttpModule
    {
        /// <summary>
        /// The manager logger
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The Pipeline internal manager
        /// </summary>
        private static readonly VInternalPipelineManager Manager = new VInternalPipelineManager();

        /// <summary>
        /// The Collection of Assemblies to scan for attributes
        /// </summary>
        private static readonly ConcurrentBag<Assembly> Assemblies;

        /// <summary>
        ///  Initializes static members of the <see cref="VPipelineManager"/> class.
        /// </summary>
        static VPipelineManager()
        {
            AssemblyNameSearchRegex = WebConfigurationManager.AppSettings["VRegistrationManager.AssemblyNameSearchRegex"];
            if (string.IsNullOrWhiteSpace(AssemblyNameSearchRegex))
            {
                AssemblyNameSearchRegex = @"Vodca|Framework|Library|WebApplication";
            }

            PipelineFolder = WebConfigurationManager.AppSettings["VRegistrationManager.PipelineFolder"];
            if (string.IsNullOrWhiteSpace(PipelineFolder))
            {
                PipelineFolder = @"/App_Config/Vodca.Pipelines/";
            }

            // Cache the list of relevant assemblies, since we need it for many things
            Assemblies = new ConcurrentBag<Assembly>();
            foreach (var assemblyFile in InternalExtensions.GetAssemblyFiles(AssemblyNameSearchRegex))
            {
                try
                {
                    // Ignore assemblies we can't load. They could be native, etc...
                    Assemblies.Add(Assembly.LoadFrom(assemblyFile));
                }
                catch (Exception exception)
                {
                    /* Log to specific log file */
                    Logger.Fatal(exception.Message, exception.ToString());

                    /* Error application logging */
                    exception.LogException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VPipelineManager"/> class.
        /// </summary>
        public VPipelineManager()
        {
            this.OnInit += this.InitWebConfigPipelines;
            this.OnInit += this.InitAssemblyEmbeddedPipelines;
        }

        /// <summary>
        /// Gets or sets the assembly name search REGEX.
        /// </summary>
        /// <value>
        /// The assembly name search REGEX.
        /// </value>
        private static string AssemblyNameSearchRegex { get; set; }

        /// <summary>
        /// Gets or sets the on init actions.
        /// </summary>
        /// <value>
        /// The on initialize.
        /// </value>
        private Action<HttpApplication> OnInit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        private bool IsInitialized { get; set; }

        /// <summary>
        /// Runs the specified unique name.
        /// </summary>
        /// <param name="uniquename">The unique name.</param>
        /// <param name="args">The args.</param>
        /// <returns>The result args</returns>
        public static IPipelineArgs Run(string uniquename, IPipelineArgs args)
        {
            VInternalPiplineMethodCollectionManager collectionmanager;
            if (!Manager.Collection.TryGetValue(uniquename, out collectionmanager))
            {
                throw new VHttpArgumentException("Pipeline not found!");
            }

            collectionmanager.Execute(args);

            return args;
        }

        /// <summary>
        /// Determines whether [is pipeline exist] [the specified unique name].
        /// </summary>
        /// <param name="uniquename">The unique name.</param>
        /// <returns>
        ///     <c>true</c> if [is pipeline exist] [the specified unique name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPipelineExist(string uniquename)
        {
            return Manager.Collection.ContainsKey(uniquename);
        }

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
            if (!this.IsInitialized)
            {
                this.OnInit(context);

                this.IsInitialized = true;
            }
        }
    }
}
