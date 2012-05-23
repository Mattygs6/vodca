//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Value.cs" company="genuine">
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
        /// Gets the value attr.
        /// </summary>
        /// <returns>
        /// The value attr
        /// </returns>
        public string GetValueAttr()
        {
            return this.GetAttribute(WellKnownXNames.Value);
        }

        /// <summary>
        /// Sets the value attr.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetValueAttr(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return this.AddAttribute(WellKnownXNames.Value, value);
            }

            return this.RemoveAttribute(WellKnownXNames.Value);
        }
    }
}