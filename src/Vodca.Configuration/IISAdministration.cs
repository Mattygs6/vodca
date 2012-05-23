//-----------------------------------------------------------------------------
// <copyright file="IISAdministration.cs" company="HIBERNATING RHINOS">
//     Copyright (c) Raven DB. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     Raven DB project
//  Modified:   J.Baltikauskas
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    /// <summary>
    /// The IIS configuration
    /// </summary>
    // ReSharper disable InconsistentNaming
    public static partial class IISAdministration
    // ReSharper restore InconsistentNaming
    {
        /// <summary>
        /// The Host file
        /// </summary>
        private const string HostFile = @"C:\Windows\System32\drivers\etc\hosts";

        /// <summary>
        /// The IIS Administration assembly path
        /// </summary>
        private const string IisAdministrationAssembly = @"C:\Windows\System32\InetSrv\Microsoft.Web.Administration.dll";

        /// <summary>
        /// The main IIS assembly instance
        /// </summary>
        private static Assembly assembly;

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        public static Assembly Assembly
        {
            get
            {
                if (assembly != null)
                {
                    return assembly;
                }

                if (File.Exists(IisAdministrationAssembly))
                {
                    assembly = Assembly.LoadFile(IisAdministrationAssembly);
                }

                if (assembly == null)
                {
                    throw new HttpException(500, string.Format("Couldn't load assembly: '{0}' !", IisAdministrationAssembly));
                }

                return assembly;
            }
        }

        /// <summary>
        /// Removes the by application pool.
        /// </summary>
        /// <param name="applicationPoolName">Name of the application pool.</param>
        public static void RemoveByApplicationPool(string applicationPoolName)
        {
            using (dynamic manager = Assembly.CreateInstance("Microsoft.Web.Administration.ServerManager"))
            {
                if (manager != null)
                {
                    foreach (var site in manager.Sites)
                    {
                        var applications = (IEnumerable<dynamic>)site.Applications;
                        if (applications.All(s => s.ApplicationPoolName.Contains(applicationPoolName)) && applications.Any())
                        {
                            site.Delete();
                        }
                    }

                    manager.CommitChanges();
                }
            }

            using (dynamic manager = Assembly.CreateInstance("Microsoft.Web.Administration.ServerManager"))
            {
                if (manager != null)
                {
                    var applicationPool = manager.ApplicationPools[applicationPoolName];
                    if (applicationPool != null)
                    {
                        applicationPool.Delete();
                        manager.CommitChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Creates the application pool.
        /// </summary>
        /// <param name="applicationPoolName">Name of the application pool.</param>
        /// <param name="managedRuntimeVersion">managed Runtime Version</param>
        /// <param name="isIntegrated">Managed Pipeline Mode</param>
        /// <returns>The flag if pool created</returns>
        public static bool CreateApplicationPool(string applicationPoolName, string managedRuntimeVersion = "v4.0", bool isIntegrated = true)
        {
            using (dynamic manager = Assembly.CreateInstance("Microsoft.Web.Administration.ServerManager"))
            {
                if (manager != null)
                {
                    var pool = manager.ApplicationPools[applicationPoolName];
                    if (pool == null)
                    {
                        pool = manager.ApplicationPools.Add(applicationPoolName);
                        pool.ManagedRuntimeVersion = managedRuntimeVersion;
                        pool.AutoStart = true;

                        // ReSharper disable ConvertIfStatementToConditionalTernaryExpression
                        if (isIntegrated)
                        // ReSharper restore ConvertIfStatementToConditionalTernaryExpression
                        {
                            pool.ManagedPipelineMode = 0;
                        }
                        else
                        {
                            pool.ManagedPipelineMode = 1;
                        }

                        manager.CommitChanges();
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="physicalDirectory">The physical directory.</param>
        /// <param name="applicationPoolName">Name of the application pool.</param>
        /// <param name="port">The port.</param>
        public static void CreateSite(string hostName, string physicalDirectory, string applicationPoolName = null, int port = 80)
        {
            if (string.IsNullOrWhiteSpace(applicationPoolName))
            {
                applicationPoolName = hostName;
            }

            CreateApplicationPool(applicationPoolName);

            using (dynamic manager = Assembly.CreateInstance("Microsoft.Web.Administration.ServerManager"))
            {
                if (manager != null)
                {
                    var pool = manager.ApplicationPools[applicationPoolName];
                    if (manager.Sites[hostName] == null && pool != null)
                    {
                        var site = manager.Sites.Add(hostName, "http", "*:" + port + ":" + hostName, physicalDirectory);
                        site.ApplicationDefaults.ApplicationPoolName = applicationPoolName;
                        manager.CommitChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Creates the site with entry in host file.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="physicalDirectory">The physical directory.</param>
        /// <param name="applicationPoolName">Name of the application pool.</param>
        /// <param name="port">The port.</param>
        public static void CreateSiteWithEntryInHostFile(string hostName, string physicalDirectory, string applicationPoolName = null, int port = 80)
        {
            CreateSite(hostName, physicalDirectory, applicationPoolName, port);
            try
            {
                var lines = File.ReadAllLines(HostFile);
                bool isregistred = (from line in lines where !string.IsNullOrWhiteSpace(line) select line.Trim())
                    .Any(hostline => (hostline.StartsWith("127.0.0.1") && hostline.EndsWith(hostName, StringComparison.InvariantCultureIgnoreCase)));

                if (!isregistred)
                {
                    var newhostfilelines = new[] { Environment.NewLine, "# Vodca Configuration entry " + DateTime.Today.ToString("MM/dd/yyyy"), "127.0.0.1    " + hostName, Environment.NewLine };
                    File.AppendAllLines(HostFile, newhostfilelines);
                }
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.Write(exception);
                /* Log? */
            }
        }
    }
}
