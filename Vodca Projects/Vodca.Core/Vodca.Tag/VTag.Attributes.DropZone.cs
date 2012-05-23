//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.DropZone.cs" company="genuine">
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
        /// Gets the dropzone attr.
        /// </summary>
        /// <returns>
        /// The dropzone attribute
        /// </returns>
        public string GetDropZoneAttr()
        {
            return this.GetAttribute(WellKnownXNames.DropZone);
        }

        /// <summary>
        /// Sets the dropzone attr.
        /// </summary>
        /// <param name="dropZone">The dropZone.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetDropZoneAttr(DropZone dropZone)
        {
            if (!string.IsNullOrWhiteSpace(dropZone.ToString()))
            {
                return this.AddAttribute(WellKnownXNames.DropZone, dropZone.ToString());
            }

            return this.RemoveAttribute(WellKnownXNames.DropZone);
        }

        /// <summary>
        /// Possible values for drop zone attribute
        /// </summary>
        public class DropZone
        {
            /// <summary>
            /// Gets the copy.
            /// </summary>
            public static DropZone CopyAttr
            {
                get
                {
                    return new DropZone { Value = "copy" };
                }
            }

            /// <summary>
            /// Gets the move.
            /// </summary>
            public static DropZone MoveAttr
            {
                get
                {
                    return new DropZone { Value = "move" };
                }
            }

            /// <summary>
            /// Gets the link.
            /// </summary>
            public static DropZone LinkAttr
            {
                get
                {
                    return new DropZone { Value = "link" };
                }
            }

            /// <summary>
            /// Gets the remove.
            /// </summary>
            public static DropZone Remove
            {
                get
                {
                    return new DropZone { Value = string.Empty };
                }
            }

            /// <summary>
            /// Gets the value.
            /// </summary>
            public string Value { get; private set; }

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