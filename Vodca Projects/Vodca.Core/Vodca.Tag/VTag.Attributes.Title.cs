//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Title.cs" company="genuine">
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
        /// Gets the title attr.
        /// </summary>
        /// <returns>
        /// The title attribute
        /// </returns>
        public string GetTitleAttr()
        {
            return this.GetAttribute(WellKnownXNames.Title);
        }

        /// <summary>
        /// Sets the title attr.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetTitleAttr(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                return this.AddAttribute(WellKnownXNames.Title, title);
            }

            return this.RemoveAttribute(WellKnownXNames.Title);
        }
    }
}