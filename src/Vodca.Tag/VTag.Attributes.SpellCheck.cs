//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.SpellCheck.cs" company="genuine">
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
        /// Gets the SpellCheck attr.
        /// </summary>
        /// <returns>
        /// The SpellCheck attribute
        /// </returns>
        public string GetSpellCheckAttr()
        {
            return this.GetAttribute(WellKnownXNames.SpellCheck);
        }

        /// <summary>
        /// Sets the SpellCheck attr.
        /// </summary>
        /// <param name="spellCheck">The spellCheck.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetSpellCheckAttr(SpellCheck spellCheck)
        {
            if (!string.IsNullOrWhiteSpace(spellCheck.ToString()))
            {
                return this.AddAttribute(WellKnownXNames.SpellCheck, spellCheck.ToString());
            }

            return this.RemoveAttribute(WellKnownXNames.SpellCheck);
        }

        /// <summary>
        /// Possible values for spellcheck attribute
        /// </summary>
        public class SpellCheck
        {
            /// <summary>
            /// Gets the true.
            /// </summary>
            public static SpellCheck True
            {
                get
                {
                    return new SpellCheck { Value = "true" };
                }
            }

            /// <summary>
            /// Gets the false.
            /// </summary>
            public static SpellCheck False
            {
                get
                {
                    return new SpellCheck { Value = "false" };
                }
            }

            /// <summary>
            /// Gets the remove.
            /// </summary>
            public static SpellCheck Remove
            {
                get
                {
                    return new SpellCheck { Value = string.Empty };
                }
            }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            private string Value { get; set; }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return this.Value;
            }
        }
    }
}