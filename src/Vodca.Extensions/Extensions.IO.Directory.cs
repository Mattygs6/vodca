//-----------------------------------------------------------------------------
// <copyright file="Extensions.IO.Directory.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Date:       02/04/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.IO;
    using System.Web.Hosting;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Ensures the settings folder exists.
        /// </summary>
        /// <param name="folderpath">The folder path to check if exists or create</param>
        /// <param name="isvirtualpath">if set to <c>true</c> is virtual path.</param>
        public static void EnsureFolderExistsOrCreate(this string folderpath, bool isvirtualpath = true)
        {
            if (!string.IsNullOrWhiteSpace(folderpath))
            {
                string settingsfolder = isvirtualpath ? HostingEnvironment.MapPath(folderpath) : folderpath;

                // ReSharper disable AssignNullToNotNullAttribute
                Directory.CreateDirectory(settingsfolder);
                // ReSharper restore AssignNullToNotNullAttribute
            }
        }

        /// <summary>
        ///     Modified the directory files.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectory">The target directory.</param>
        /// <param name="lastmodification">The last modification.</param>
        public static void ModifiedDirectoryFiles(string sourceDirectory, string targetDirectory, DateTime lastmodification)
        {
            if (!string.IsNullOrWhiteSpace(sourceDirectory) && !string.IsNullOrWhiteSpace(targetDirectory))
            {
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                string[] files = Directory.GetFiles(sourceDirectory);
                foreach (string file in files)
                {
                    FileAttributes attributes = File.GetAttributes(file);
                    if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        string name = Path.GetFileName(file);

                        /* ReSharper disable AssignNullToNotNullAttribute */
                        string dest = Path.Combine(targetDirectory, name);
                        /* ReSharper restore AssignNullToNotNullAttribute */

                        if (File.GetLastWriteTime(file) >= lastmodification)
                        {
                            File.Copy(file, dest, false); // overwrite any existing files.
                        }
                    }
                }

                string[] directories = Directory.GetDirectories(sourceDirectory);
                foreach (string directory in directories)
                {
                    FileAttributes attributes = File.GetAttributes(directory);
                    if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        string name = Path.GetFileName(directory);

                        // ReSharper disable AssignNullToNotNullAttribute
                        string dest = Path.Combine(targetDirectory, name);
                        // ReSharper restore AssignNullToNotNullAttribute

                        // Recursive call
                        ModifiedDirectoryFiles(directory, dest, lastmodification);
                    }
                }
            }
        }

        /// <summary>
        ///     Recursively copies a directory to given location. Overwrites any existing files. Skips hidden files and directories.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectory">The target directory.</param>
        /// <remarks>
        ///     Modified version of the http://github.com/cuyahogaproject/cuyahoga/blob/master/src/Cuyahoga.Core/Util/IOUtil.cs
        /// </remarks>
        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            if (!string.IsNullOrWhiteSpace(sourceDirectory) && !string.IsNullOrWhiteSpace(targetDirectory))
            {
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                string[] files = Directory.GetFiles(sourceDirectory);
                foreach (string file in files)
                {
                    FileAttributes attributes = File.GetAttributes(file);
                    if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        string name = Path.GetFileName(file);

                        // ReSharper disable AssignNullToNotNullAttribute
                        string dest = Path.Combine(targetDirectory, name);
                        // ReSharper restore AssignNullToNotNullAttribute
                        File.Copy(file, dest, false); // overwrite any existing files.
                    }
                }

                string[] directories = Directory.GetDirectories(sourceDirectory);
                foreach (string directory in directories)
                {
                    FileAttributes attributes = File.GetAttributes(directory);
                    if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        string name = Path.GetFileName(directory);

                        // ReSharper disable AssignNullToNotNullAttribute
                        string dest = Path.Combine(targetDirectory, name);
                        // ReSharper restore AssignNullToNotNullAttribute

                        // Recursive call
                        CopyDirectory(directory, dest);
                    }
                }
            }
        }

        /// <summary>
        ///     Checks if the given directory is writable.
        /// </summary>
        /// <param name="physicalDirectory">The physical directory.</param>
        /// <returns>
        ///     <c>true</c> if is the specified physical directory writable; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDirectoryWritable(string physicalDirectory)
        {
            try
            {
                // Check if the given directory is writable by creating a dummy file.
                string fileName = Path.Combine(physicalDirectory, "empty.txt");
                File.WriteAllText(fileName, "Test");

                File.Delete(fileName);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }

            return false;
        }

        /// <summary>
        ///   Copies all files from one directory to another.
        /// </summary>      
        /// <remarks>
        /// Based on the Christian Liensberger http://www.liensberger.it/ code
        /// </remarks>
        /// <param name="source">The source directory</param>
        /// <param name="target">The target directory</param>
        /// <param name="recursive">The flag indicating to copy subdirectories</param>
        public static void CopyTo(this DirectoryInfo source, DirectoryInfo target, bool recursive = false)
        {
            if (source != null && target != null)
            {
                if (!target.Exists)
                {
                    target.Create();
                }

                foreach (var file in source.GetFiles())
                {
                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                }

                if (recursive)
                {
                    foreach (var directory in source.GetDirectories())
                    {
                        directory.CopyTo(new DirectoryInfo(Path.Combine(target.FullName, directory.Name)), true);
                    }
                }
            }
        }
    }
}
