//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Hidden.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       12/30/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class VTag
    {
        /// <summary>
        /// Gets the hidden attr.
        /// </summary>
        /// <returns>
        /// The hidden attribute
        /// </returns>
        public string GetHiddenAttr()
        {
            return this.GetAttribute(WellKnownXNames.Hidden);
        }

        /// <summary>
        /// Sets the hidden attr.
        /// </summary>
        /// <param name="hidden">if set to <c>true</c> [hidden].</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetHiddenAttr(bool hidden = true)
        {
            if (hidden)
            {
                return this.AddAttribute(WellKnownXNames.Hidden, WellKnownXNames.Hidden.ToString());
            }

            return this.RemoveAttribute(WellKnownXNames.Hidden);
        }
    }
}