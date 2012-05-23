//-----------------------------------------------------------------------------
// <copyright file="Program.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using Vodca.SDK.NLog;
    using Vodca.SDK.NLog.Config;
    using Vodca.SDK.NLog.Targets;

    /// <summary>
    /// The IIS register job
    /// </summary>
    public sealed partial class Program
    {
        /// <summary>
        ///     The generic NLog logger
        /// </summary>
        internal static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes static members of the <see cref="Program"/> class.
        /// </summary>
        static Program()
        {
            var folder = Environment.CurrentDirectory.EnsureEndsWith(@"\");

            var target = new FileTarget
            {
                Layout = "${longdate} ${message}",
                FileName = folder + @"IISConfiguration-${level}.log",
                KeepFileOpen = false
            };

            SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
        }

        /// <summary>
        ///     Mains the specified args.
        /// </summary>
        /// <param name="args">The optional args.</param>
        public static void Main(string[] args)
        {
            if (args != null && args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                var hostname = args[0];
                string physicalDirectory;
                if (args.Length > 1)
                {
                    physicalDirectory = args[1];
                }
                else
                {
                    var path = Environment.CurrentDirectory.ToLowerInvariant();
                    int index = path.LastIndexOf('\\');
                    if (index > 1)
                    {
                        physicalDirectory = path.Substring(0, index);
                    }
                    else
                    {
                        throw new ArgumentException("Couldn't resolve path!");
                    }
                }

                try
                {
                    IISAdministration.CreateSiteWithEntryInHostFile(hostname, physicalDirectory);
                }
                catch (Exception ex)
                {
                    Logger.Fatal(ex.ToString());
                }
            }
            else
            {
                Logger.Fatal("The configuration settings isn't passed!");
            }
        }
    }
}
