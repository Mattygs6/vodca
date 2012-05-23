//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Href.cs" company="genuine">
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
        /// Gets the href attr.
        /// </summary>
        /// <returns>
        /// The href attribute
        /// </returns>
        public string GetHrefAttr()
        {
            return this.GetAttribute(WellKnownXNames.Href);
        }

        /// <summary>
        /// Sets the href attr.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetHrefAttr(string href)
        {
            if (!string.IsNullOrWhiteSpace(href))
            {
                return this.AddAttribute(WellKnownXNames.Href, href);
            }

            return this.RemoveAttribute(WellKnownXNames.Href);
        }
    }
}