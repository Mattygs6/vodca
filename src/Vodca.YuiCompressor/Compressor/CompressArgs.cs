//-----------------------------------------------------------------------------
// <copyright file="CompressArgs.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.YuiCompressor
{
    using System.Collections.Generic;

    /// <summary>
    /// The Js Compress Args
    /// </summary>
    public partial class CompressFileGroupArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressFileGroupArgs"/> class.
        /// </summary>
        /// <param name="fileGroup">The file group.</param>
        /// <param name="currentdirectory">The current directory.</param>
        /// <param name="isrelease">if set to <c>true</c> [isrelease].</param>
        public CompressFileGroupArgs(XmlFileGroup fileGroup, string currentdirectory, bool isrelease = true)
        {
            Ensure.IsNotNull(fileGroup, "XmlFileGroup");
            Ensure.IsNotNullOrEmpty(currentdirectory, "currentdirectory");

            this.XmlFileGroup = fileGroup;
            this.CurrentDirectory = currentdirectory;

            this.Files = new HashSet<ProcessFile>();
            foreach (var filename in this.XmlFileGroup.Files)
            {
                var file = new ProcessFile(filename, this.CurrentDirectory);
                if (file.Validate())
                {
                    this.Files.Add(file);
                }
            }

            this.IsRelease = isrelease;
        }

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        public string CurrentDirectory { get; private set; }

        /// <summary>
        /// Gets the XML file group.
        /// </summary>
        public XmlFileGroup XmlFileGroup { get; private set; }

        /// <summary>
        /// Gets the files.
        /// </summary>
        public HashSet<ProcessFile> Files { get; private set; }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>
        /// The output.
        /// </value>
        public string Output { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is debug.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is debug; otherwise, <c>false</c>.
        /// </value>
        public bool IsRelease { get; private set; }
    }
}
