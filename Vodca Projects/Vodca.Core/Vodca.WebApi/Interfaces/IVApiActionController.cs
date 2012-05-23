//-----------------------------------------------------------------------------
// <copyright file="IVApiActionController.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using Vodca.WebApi;

    /// <summary>
    /// The base interface for the Vodca Web API
    /// </summary>
    public interface IVApiActionController
    {
        /// <summary>
        /// Gets the handler name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets the type of the file content.
        /// </summary>
        /// <value>
        /// The type of the file content.
        /// </value>
        string FileContentType { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is compression enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is compression disabled; otherwise, <c>false</c>.
        /// </value>
        bool IsCompressionDisabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is secured.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is secured; otherwise, <c>false</c>.
        /// </value>
        bool IsSecured { get; }

        /// <summary>
        /// Gets the file access permission.
        /// </summary>
        VApiAccessPermission FileAccessPermission { get; }
    }
}