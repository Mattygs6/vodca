//-----------------------------------------------------------------------------
// <copyright file="Extensions.ThirdParty.IO.File.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Date:       07/24/2010
//  Source:     .NET Extensions - Extensions Methods Library
//  Url:        http://dnpextensions.codeplex.com/
// Modified:    J.Baltikauskas
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.IO;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        #region Thirh party extensions

        /// <summary>
        ///     Gets all files in the directory matching one of the several (!) supplied patterns (instead of just one in the regular implementation).
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="patterns">The patterns.</param>
        /// <returns>The matching files.</returns>
        /// <remarks>This methods is quite perfect to be used in conjunction with the newly created FileInfo-Array extension methods.</remarks>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var files = directory.GetFiles("*.txt", "*.xml");
        /// </code>
        /// </example>
        public static IEnumerable<FileInfo> GetFiles(this DirectoryInfo directory, params string[] patterns)
        {
            var files = new List<FileInfo>();
            if (directory != null && patterns != null)
            {
                foreach (var pattern in patterns)
                {
                    files.AddRange(directory.GetFiles(pattern));
                }
            }

            return files;
        }

        /// <summary>
        ///     Renames a file.
        /// </summary>
        /// <param name="file">The file info.</param>
        /// <param name="newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var file = new FileInfo(@"c:\test.txt");
        /// file.Rename("test2.txt");
        /// </code></example>
        public static FileInfo Rename(this FileInfo file, string newName)
        {
            if (file != null && !string.IsNullOrWhiteSpace(newName))
            {
                var path = Path.GetDirectoryName(file.FullName);
                // ReSharper disable AssignNullToNotNullAttribute
                Ensure.IsNotNullOrEmpty(path, "path != null");

                var filePath = Path.Combine(path, newName);
                // ReSharper restore AssignNullToNotNullAttribute
                file.MoveTo(filePath);
            }

            return file;
        }

        /// <summary>
        /// Renames a without changing its extension.
        /// </summary>
        /// <param name="file">The file path.</param>
        /// <param name="newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var file = new FileInfo(@"c:\test.txt");
        /// file.RenameFileWithoutExtension("test3");
        /// </code></example>
        public static FileInfo RenameFileWithoutExtension(this FileInfo file, string newName)
        {
            if (file != null && !string.IsNullOrWhiteSpace(newName))
            {
                var fileName = string.Concat(newName, file.Extension);
                file.Rename(fileName);
            }

            return file;
        }

        /// <summary>
        /// Changes the files extension.
        /// </summary>
        /// <param name="file">The file path.</param>
        /// <param name="newExtension">The new extension.</param>
        /// <returns>The renamed file</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var file = new FileInfo(@"c:\test.txt");
        /// file.ChangeExtension("xml");
        /// </code></example>
        public static FileInfo ChangeExtension(this FileInfo file, string newExtension)
        {
            if (file != null && !string.IsNullOrWhiteSpace(newExtension))
            {
                newExtension = newExtension.EnsureStartsWith(".");

                var fileName = string.Concat(Path.GetFileNameWithoutExtension(file.FullName), newExtension);
                file.Rename(fileName);
            }

            return file;
        }

        /// <summary>
        ///     Deletes several files at once.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var files = directory.GetFiles("*.txt", "*.xml");
        /// files.Delete()
        /// </code></example>
        public static void Delete(this IEnumerable<FileInfo> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    file.Delete();
                }
            }
        }

        /// <summary>
        ///     Copies several files to a new folder at once.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns>The newly created file copies</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var files = directory.GetFiles("*.txt", "*.xml");
        /// var copiedFiles = files.CopyTo(@"c:\temp\");
        /// </code></example>
        public static IEnumerable<FileInfo> CopyTo(this IEnumerable<FileInfo> files, string targetPath)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    yield return file.CopyTo(fileName);
                }
            }
        }

        /// <summary>
        ///     Movies several files to a new folder at once.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns>The moved files</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var files = directory.GetFiles("*.txt", "*.xml");
        /// files.MoveTo(@"c:\temp\");
        /// </code></example>
        public static IEnumerable<FileInfo> MoveTo(this IEnumerable<FileInfo> files, string targetPath)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    file.MoveTo(fileName);
                    yield return file;
                }
            }
        }

        /// <summary>
        /// Sets file attributes for several files at once
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var files = directory.GetFiles("*.txt", "*.xml");
        /// files.SetAttributes(FileAttributes.Archive);
        /// </code></example>
        public static IEnumerable<FileInfo> SetAttributes(this IEnumerable<FileInfo> files, FileAttributes attributes)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    file.Attributes = attributes;
                    yield return file;
                }
            }
        }

        /// <summary>
        /// Appends file attributes for several files at once (additive to any existing attributes)
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// var files = directory.GetFiles("*.txt", "*.xml");
        /// files.SetAttributesAdditive(FileAttributes.Archive);
        /// </code></example>
        public static IEnumerable<FileInfo> SetAttributesAdditive(this IEnumerable<FileInfo> files, FileAttributes attributes)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    file.Attributes = file.Attributes | attributes;
                    yield return file;
                }
            }
        }

        #endregion
    }
}