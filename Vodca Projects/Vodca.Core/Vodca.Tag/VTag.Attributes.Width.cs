//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Width.cs" company="genuine">
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
        /// Gets the width attr.
        /// </summary>
        /// <returns>
        /// The width attr
        /// </returns>
        public string GetWidthAttr()
        {
            return this.GetAttribute(WellKnownXNames.Width);
        }

        /// <summary>
        /// Sets the width attr.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetWidthAttr(string width)
        {
            width = width.RemoveNonDigitsChars();

            if (!string.IsNullOrWhiteSpace(width))
            {
                return this.AddAttribute(WellKnownXNames.Width, width);
            }

            return this.RemoveAttribute(WellKnownXNames.Width);
        }

        /// <summary>
        /// Sets the width attr.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetWidthAttr(int width)
        {
            if (width.IsNotNull())
            {
                return this.AddAttribute(WellKnownXNames.Width, width);
            }
            
            return this.RemoveAttribute(WellKnownXNames.Width);
        }
    }
}