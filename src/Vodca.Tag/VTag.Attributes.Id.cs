//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Id.cs" company="genuine">
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
        /// Gets the id attr.
        /// </summary>
        /// <returns>The id attribute</returns>
        public string GetIdAttr()
        {
            return this.GetAttribute(WellKnownXNames.Id);
        }

        /// <summary>
        /// Sets the id attr.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetIdAttr(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                return this.AddAttribute(WellKnownXNames.Id, id);
            }

            return this.RemoveAttribute(WellKnownXNames.Id);
        }
    }
}