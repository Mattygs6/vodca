//-----------------------------------------------------------------------------
// <copyright file="VApiActionController.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Concurrent;
    using Vodca.WebApi;

    /// <summary>
    /// The base class for Web API Handlers
    /// </summary>
    public abstract class VApiActionController : IVApiActionController
    {
        /// <summary>
        /// The Static cache wrapper
        /// </summary>
        private static readonly ConcurrentDictionary<string, object> StaticCache = new ConcurrentDictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="VApiActionController"/> class.
        /// </summary>
        protected VApiActionController()
        {
            this.FileName = this.GetType().Name;
            this.FileExtension = VApiArgs.FileExtensions.Json;
            this.FileContentType = VApiArgs.FileContentTypes.Json;
        }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the type of the file content.
        /// </summary>
        /// <value>
        /// The type of the file content.
        /// </value>
        public string FileContentType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is compression enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is compression disabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompressionDisabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is secured.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is secured; otherwise, <c>false</c>.
        /// </value>
        public bool IsSecured { get; set; }

        /// <summary>
        /// Gets or sets the file access permission.
        /// </summary>
        public VApiAccessPermission FileAccessPermission { get; set; }

        /// <summary>
        /// Gets or sets the cache object with the specified key.
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <returns>The cached object</returns>
        protected object this[string key]
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    object obj;
                    if (StaticCache.TryGetValue(key, out obj))
                    {
                        return obj;
                    }
                }

                return null;
            }

            set
            {
                if (value != null && !string.IsNullOrWhiteSpace(key))
                {
                    StaticCache[key] = value;
                }
            }
        }
    }
}