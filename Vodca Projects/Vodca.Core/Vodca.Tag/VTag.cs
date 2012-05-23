//-----------------------------------------------------------------------------
// <copyright file="VTag.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/03/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics;
    using System.Web;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    // ReSharper disable ClassWithVirtualMembersNeverInherited.Global

    /// <summary>
    /// The Sitecore Custom type for complex elements like links or images .
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Tag\VTag.cs" title="VTag.cs" lang="C#" />
    /// </example>
    [Serializable, XmlRoot("VTag")]
    [DebuggerDisplay("{ToString()}")]
    public partial class VTag : XElement, IToXElement, IHtmlString
    {
        // ReSharper restore ClassWithVirtualMembersNeverInherited.Global

        /// <summary>
        /// Initializes a new instance of the <see cref="VTag"/> class.
        /// </summary>
        /// <param name="tagname">The tag name.</param>
        protected VTag(string tagname)
            : base(XName.Get(tagname))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VTag"/> class.
        /// </summary>
        /// <param name="tagname">The tag name.</param>
        /// <param name="value">The value.</param>
        protected VTag(string tagname, string value)
            : base(XName.Get(tagname))
        {
            this.SetValue(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VTag"/> class.
        /// </summary>
        /// <param name="tagname">The tag name.</param>
        /// <param name="value">The value.</param>
        protected VTag(string tagname, object value)
            : base(XName.Get(tagname))
        {
            this.SetValue(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VTag"/> class.
        /// </summary>
        /// <param name="xname">The XName of the tag.</param>
        protected VTag(XName xname)
            : base(xname)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VTag"/> class.
        /// </summary>
        /// <param name="xname">The XName.</param>
        /// <param name="value">The value.</param>
        protected VTag(XName xname, string value)
            : base(xname)
        {
            this.SetValue(value);
        }

        #region IToXElement Members

        /// <summary>
        /// News the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The VTag instance</returns>
        public static VTag New(string name)
        {
            return new VTag(name);
        }

        /// <summary>
        /// News the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The VTag instance</returns>
        public static VTag New(string name, object value)
        {
            return new VTag(name, value);
        }

        /// <summary>
        /// News the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The VTag instance</returns>
        public static VTag New(string name, string value)
        {
            return new VTag(name, value);
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <returns>The object instance data as XElement</returns>
        public virtual XElement ToXElement()
        {
            return this;
        }

        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootelementname">The root element name.</param>
        /// <returns>The object instance data as XElement</returns>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Tag\VTag.cs" title="VTag.cs" lang="C#" />
        /// </example>
        public virtual XElement ToXElement(string rootelementname)
        {
            Ensure.IsNotNullOrEmpty(rootelementname, "root element name");

            var root = new XElement(rootelementname);

            root.Add(this);

            return root;
        }

        #endregion

        /// <summary>
        /// Adds the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The tag instance</returns>
        public new VTag Add(params object[] obj)
        {
            if (obj != null)
            {
                base.Add(obj);
            }

            return this;
        }

        /// <summary>
        /// Adds the new.
        /// </summary>
        /// <param name="tagname">The tag name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The current instance</returns>
        public VTag AddNewTag(string tagname, object value)
        {
            if (!string.IsNullOrWhiteSpace(tagname))
            {
                this.Add(new XElement(tagname, value));
            }

            return this;
        }

        /// <summary>
        /// Returns an HTML-encoded string.
        /// </summary>
        /// <returns>
        /// An HTML-encoded string.
        /// </returns>
        public string ToHtmlString()
        {
            return this.ToString();
        }
    }
}
