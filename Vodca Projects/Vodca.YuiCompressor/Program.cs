//-----------------------------------------------------------------------------
// <copyright file="Program.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.YuiCompressor
{
    using System;
    using System.IO;
    using Vodca.SDK.NLog;
    using Vodca.SDK.NLog.Targets;

    /// <summary>
    /// The YuiCompressor job
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
                FileName = folder + @"yuicompressor-${level}.log",
                KeepFileOpen = false
            };

            Vodca.SDK.NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
        }

        /// <summary>
        ///     Mains the specified args.
        /// </summary>
        /// <param name="args">The optional args.</param>
        public static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                var settimgsdirectory = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
                                            ? args[1]
                                            : Environment.CurrentDirectory;

                var settingspath = Path.Combine(settimgsdirectory, args[0]);
                try
                {
                    var manager = new CompressManager(settingspath, isvirtualpath: false);
                    manager.Run();
                }
                catch (Exception ex)
                {
                    Logger.Fatal(ex.ToString());
                }
            }
            else
            {
                Logger.Fatal("The configuration file name isn't passed!");
            }
        }
    }
}
