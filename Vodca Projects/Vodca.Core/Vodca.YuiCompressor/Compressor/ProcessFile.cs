//-----------------------------------------------------------------------------
// <copyright file="ProcessFile.cs" company="genuine">
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

    /// <summary>
    /// The Process file 
    /// </summary>
    [Serializable]
    public sealed partial class ProcessFile : IValidate
    {
        /// <summary>
        /// The file name and path.
        /// </summary>
        private string fileNameAndPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessFile"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="directory">The directory.</param>
        public ProcessFile(string filename, string directory)
        {
            Ensure.IsNotNullOrEmpty(filename, "filename");
            Ensure.IsNotNullOrEmpty(directory, "directory");

            this.FileName = filename;
            this.FileNameAndPath = directory.EnsureEndsWith(@"\") + filename;
            this.IsFileExists = File.Exists(this.FileNameAndPath);

            if (this.IsFileExists)
            {
                this.FileInfo = new FileInfo(this.FileNameAndPath);
                this.Content = File.ReadAllText(this.fileNameAndPath);
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ProcessFile"/> class from being created.
        /// </summary>
        private ProcessFile()
        {
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the file info.
        /// </summary>
        public FileInfo FileInfo { get; private set; }

        /// <summary>
        /// Gets the file name and path.
        /// </summary>
        /// <value>
        /// The file name and path.
        /// </value>
        public string FileNameAndPath
        {
            get
            {
                return this.fileNameAndPath;
            }

            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.fileNameAndPath = value;
                    this.CompressorOutputStreamAction = ResolveCompressorAction(value);
                }
            }
        }

        /// <summary>
        /// Gets the compressor output stream action.
        /// </summary>
        /// <value>
        /// The compressor output stream action.
        /// </value>
        public CompressorOutputStreamAction CompressorOutputStreamAction { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is file exists.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is file exists; otherwise, <c>false</c>.
        /// </value>
        public bool IsFileExists { get; private set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets a flag indicating whether object is valid or not
        /// </summary>
        /// <returns>
        /// True if valid otherwise false
        /// </returns>
        public bool Validate()
        {
            return this.IsFileExists;
        }

        /// <summary>
        /// Resolves the compressor action.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The Compress Action</returns>
        private static CompressorOutputStreamAction ResolveCompressorAction(string filename)
        {
            return filename.Contains(".min.") ? CompressorOutputStreamAction.Append : CompressorOutputStreamAction.Compress;
        }
    }
}
