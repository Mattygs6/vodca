//-----------------------------------------------------------------------------
// <copyright file="VApiController.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//   Date:      03/29/2012
//-----------------------------------------------------------------------------
namespace Vodca.WebApi
{
    using System;
    using Vodca;

    /// <summary>
    /// The VApi action Controller
    /// </summary>
    internal sealed partial class VApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VApiController"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="json">The constructor json.</param>
        private VApiController(Type type, string json)
        {
            this.ActionController = type.GetInstance(json) as IVApiActionController;

            Ensure.IsNotNull(this.ActionController, "The instance don't implement IApiActionControllerName interface");

            this.Key = this.TryResolveKey();
        }

        /// <summary>
        /// Gets or sets the action controller.
        /// </summary>
        /// <value>
        /// The action controller.
        /// </value>
        public IVApiActionController ActionController { get; set; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// News the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="json">The constructor json.</param>
        /// <returns>
        /// The new instance VApiController
        /// </returns>
        public static VApiController New(Type type, string json = null)
        {
            Ensure.IsNotNull(type, "type");
            return new VApiController(type, json);
        }

        /// <summary>
        /// Tries the resolve dictionary key.
        /// </summary>
        /// <returns>
        /// The URL dictionary key
        /// </returns>
        private string TryResolveKey()
        {
            var key = string.Concat(
                       this.ActionController.FileAccessPermission == VApiAccessPermission.Public
                        ? VApiManagerModule.UrlPrefixPublicAccessTrigger
                        : VApiManagerModule.UrlPrefixSecuredAccessTrigger,
                    this.ActionController.FileName,
                    this.ActionController.FileExtension);

            return key;
        }
    }
}
