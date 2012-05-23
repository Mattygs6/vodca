//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Type.cs" company="genuine">
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
        /// Gets the type attr.
        /// </summary>
        /// <returns>
        /// The type attribute
        /// </returns>
        public string GetTypeAttr()
        {
            return this.GetAttribute(WellKnownXNames.Type);
        }

        /// <summary>
        /// Sets the type attr.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetTypeAttr(string type)
        {
            if (!string.IsNullOrWhiteSpace(type))
            {
                return this.AddAttribute(WellKnownXNames.Type, type);
            }

            return this.RemoveAttribute(WellKnownXNames.Type);
        }
    }
}