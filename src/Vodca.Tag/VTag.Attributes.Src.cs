//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Src.cs" company="genuine">
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
        /// Gets the SRC attr.
        /// </summary>
        /// <returns>
        /// The src attribute
        /// </returns>
        public string GetSrcAttr()
        {
            return this.GetAttribute(WellKnownXNames.Src);
        }

        /// <summary>
        /// Sets the SRC attr.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetSrcAttr(string src)
        {
            if (!string.IsNullOrWhiteSpace(src))
            {
                return this.AddAttribute(WellKnownXNames.Src, src);
            }

            return this.RemoveAttribute(WellKnownXNames.Src);
        }
    }
}