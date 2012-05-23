//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.ContentEditable.cs" company="genuine">
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
        /// Gets the content editable attr.
        /// </summary>
        /// <returns>
        /// The content editable attribute
        /// </returns>
        public string GetContentEditableAttr()
        {
            return this.GetAttribute(WellKnownXNames.ContentEditable);
        }

        /// <summary>
        /// Sets the content editable attr.
        /// </summary>
        /// <param name="contentEditable">The content editable.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetContentEditableAttr(ContentEditable contentEditable)
        {
            if (!string.IsNullOrWhiteSpace(contentEditable.ToString()))
            {
                return this.AddAttribute(WellKnownXNames.ContentEditable, contentEditable.ToString());
            }

            return this.RemoveAttribute(WellKnownXNames.ContentEditable);
        }

        /// <summary>
        /// Possible values for content editable attribute
        /// </summary>
        public class ContentEditable
        {
            /// <summary>
            /// Gets the true.
            /// </summary>
            public static ContentEditable True
            {
                get
                {
                    return new ContentEditable { Value = "true" };
                }
            }

            /// <summary>
            /// Gets the false.
            /// </summary>
            public static ContentEditable False
            {
                get
                {
                    return new ContentEditable { Value = "false" };
                }
            }

            /// <summary>
            /// Gets the inherit.
            /// </summary>
            public static ContentEditable Inherit
            {
                get
                {
                    return new ContentEditable { Value = "inherit" };
                }
            }

            /// <summary>
            /// Gets the remove.
            /// </summary>
            public static ContentEditable Remove
            {
                get
                {
                    return new ContentEditable { Value = string.Empty };
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