//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.AccessKey.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       12/09/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class VTag
    {
        /// <summary>
        /// Gets the access key.
        /// </summary>
        /// <returns>The access key value</returns>
        public string GetAccessKey()
        {
            return this.GetAttribute(WellKnownXNames.AccessKey);
        }

        /// <summary>
        /// Sets the access key.
        /// </summary>
        /// <param name="accesskey">The access key.</param>
        /// <returns>The VTag instance</returns>
        public VTag SetAccessKey(string accesskey)
        {
            if (!string.IsNullOrWhiteSpace(accesskey))
            {
                return this.AddAttribute(WellKnownXNames.AccessKey, accesskey);
            }

            return this.RemoveAttribute(WellKnownXNames.AccessKey);
        }
    }
}