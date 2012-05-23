//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Alt.cs" company="genuine">
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
        /// Gets the alt attr.
        /// </summary>
        /// <returns>
        /// The alt attribute
        /// </returns>
        public string GetAltAttr()
        {
            return this.GetAttribute(WellKnownXNames.Alt);
        }

        /// <summary>
        /// Sets the alt attr.
        /// </summary>
        /// <param name="alt">The alt.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetAltAttr(string alt)
        {
            if (!string.IsNullOrWhiteSpace(alt))
            {
                return this.AddAttribute(WellKnownXNames.Alt, alt);
            }

            return this.RemoveAttribute(WellKnownXNames.Alt);
        }
    }
}