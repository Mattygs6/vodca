//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Name.cs" company="genuine">
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
        /// Gets the name attr.
        /// </summary>
        /// <returns>
        /// The name attribute
        /// </returns>
        public string GetNameAttr()
        {
            return this.GetAttribute(WellKnownXNames.Name);
        }

        /// <summary>
        /// Sets the name attr.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetNameAttr(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return this.AddAttribute(WellKnownXNames.Name, name);
            }

            return this.RemoveAttribute(WellKnownXNames.Name);
        }
    }
}