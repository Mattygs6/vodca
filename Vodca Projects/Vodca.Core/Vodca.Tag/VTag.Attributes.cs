//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/12/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.Linq;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class VTag
    {
        /// <summary>
        /// Adds the attribute.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The tag instance</returns>
        public VTag AddAttribute(string name, string value)
        {
            return this.AddAttribute(XName.Get(name), value);
        }

        /// <summary>
        /// Adds the attribute.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The tag instance</returns>
        public VTag AddAttribute(string name, object value)
        {
            return this.AddAttribute(XName.Get(name), string.Concat(value));
        }

        /// <summary>
        /// Mimics the jQuery Data functionality, Sets the Data attribute
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The VTag instance</returns>
        public VTag Data(string name, string value)
        {
            Extensions.Data(this, name, value);

            return this;
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The VTag instance</returns>
        public VTag RemoveAttribute(string name)
        {
            Extensions.RemoveAttribute(this, name);

            return this;
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="xname">The xname.</param>
        /// <returns>The VTag instance</returns>
        public VTag RemoveAttribute(XName xname)
        {
            Extensions.RemoveAttribute(this, xname);

            return this;
        }

        /// <summary>
        /// Adds the attribute.
        /// </summary>
        /// <param name="xname">The xname.</param>
        /// <param name="value">The value.</param>
        /// <returns>The VTag instance</returns>
        private VTag AddAttribute(XName xname, string value)
        {
            this.SetOrAddAttributeValue(xname, value);
            return this;
        }

        /// <summary>
        /// Adds the attribute.
        /// </summary>
        /// <param name="xname">The xname.</param>
        /// <param name="value">The value.</param>
        /// <returns>The VTag instance</returns>
        private VTag AddAttribute(XName xname, object value)
        {
            this.SetOrAddAttributeValue(xname, string.Concat(value));
            return this;
        }
    }
}