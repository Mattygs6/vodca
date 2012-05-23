//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.TabIndex.cs" company="genuine">
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
        /// Gets the tab index attr.
        /// </summary>
        /// <returns>
        /// The tab index attribute
        /// </returns>
        public string GetTabIndexAttr()
        {
            return this.GetAttribute(WellKnownXNames.TabIndex);
        }

        /// <summary>
        /// Sets the tab index attr removing any non digit characters.
        /// </summary>
        /// <param name="tabIndex">Index of the tab.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetTabIndexAttr(string tabIndex)
        {
            tabIndex = tabIndex.RemoveNonDigitsChars();

            if (!string.IsNullOrWhiteSpace(tabIndex))
            {
                return this.AddAttribute(WellKnownXNames.TabIndex, tabIndex);
            }

            return this.RemoveAttribute(WellKnownXNames.TabIndex);
        }

        /// <summary>
        /// Sets the tab index attr removing any non digit characters.
        /// </summary>
        /// <param name="tabIndex">Index of the tab.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetTabIndexAttr(int tabIndex)
        {
            if (tabIndex.IsNotNull())
            {
                return this.AddAttribute(WellKnownXNames.TabIndex, tabIndex);
            }

            return this.RemoveAttribute(WellKnownXNames.TabIndex);
        }
    }
}