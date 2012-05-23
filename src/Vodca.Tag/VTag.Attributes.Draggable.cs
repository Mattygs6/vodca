//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Draggable.cs" company="genuine">
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
        /// Gets the draggable attr.
        /// </summary>
        /// <returns>
        /// The draggable attribute
        /// </returns>
        public string GetDraggableAttr()
        {
            return this.GetAttribute(WellKnownXNames.Draggable);
        }

        /// <summary>
        /// Sets the draggable attr.
        /// </summary>
        /// <param name="draggable">The draggable.</param>
        /// <returns>
        /// The VTag instance
        /// </returns>
        public VTag SetDraggableAttr(Draggable draggable)
        {
            if (!string.IsNullOrWhiteSpace(draggable.ToString()))
            {
                return this.AddAttribute(WellKnownXNames.Draggable, draggable.ToString());
            }

            return this.RemoveAttribute(WellKnownXNames.Draggable);
        }

        /// <summary>
        /// Possible values for draggable attribute
        /// </summary>
        public class Draggable
        {
            /// <summary>
            /// Gets the true.
            /// </summary>
            public static Draggable True
            {
                get
                {
                    return new Draggable { Value = "true" };
                }
            }

            /// <summary>
            /// Gets the false.
            /// </summary>
            public static Draggable False
            {
                get
                {
                    return new Draggable { Value = "false" };
                }
            }

            /// <summary>
            /// Gets the auto.
            /// </summary>
            public static Draggable Auto
            {
                get
                {
                    return new Draggable { Value = "auto" };
                }
            }

            /// <summary>
            /// Gets the remove.
            /// </summary>
            public static Draggable Remove
            {
                get
                {
                    return new Draggable { Value = string.Empty };
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