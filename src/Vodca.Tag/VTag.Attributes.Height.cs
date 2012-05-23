//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Height.cs" company="genuine">
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
        /// Gets the height attr.
        /// </summary>
        /// <returns>
        /// The height attr
        /// </returns>
        public string GetHeightAttr()
        {
            return this.GetAttribute(WellKnownXNames.Height);
        }

        /// <summary>
        /// Sets the height attr.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetHeightAttr(string height)
        {
            height = height.RemoveNonDigitsChars();

            if (!string.IsNullOrWhiteSpace(height))
            {
                return this.AddAttribute(WellKnownXNames.Height, height);
            }

            return this.RemoveAttribute(WellKnownXNames.Height);
        }

        /// <summary>
        /// Sets the height attr.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetHeightAttr(int height)
        {
            if (height.IsNotNull())
            {
                return this.AddAttribute(WellKnownXNames.Height, height);
            }
            
            return this.RemoveAttribute(WellKnownXNames.Height);
        }
    }
}