//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Style.cs" company="genuine">
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
        /// Gets the style attr.
        /// </summary>
        /// <returns>
        /// The style attribute
        /// </returns>
        public string GetStyleAttr()
        {
            return this.GetAttribute(WellKnownXNames.Style);
        }

        /// <summary>
        /// Sets the style attr.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetStyleAttr(string style)
        {
            if (!string.IsNullOrWhiteSpace(style))
            {
                return this.AddAttribute(WellKnownXNames.Style, style);
            }

            return this.RemoveAttribute(WellKnownXNames.Style);
        }
    }
}