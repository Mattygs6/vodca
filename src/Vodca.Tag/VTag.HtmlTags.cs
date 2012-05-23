//-----------------------------------------------------------------------------
// <copyright file="VTag.HtmlTags.cs" company="genuine">
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
        /// Adds the HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>The VTag instance</returns>
        public VTag AddHtml(string html)
        {
            Extensions.AddHtml(this, html);

            return this;
        }

        /// <summary>
        /// Adds the WYSIWYG.
        /// </summary>
        /// <param name="wysiwyg">The WYSIWYG.</param>
        /// <param name="roottagattributes">The attributes for the enclosing div</param>
        /// <returns>The VTag instance</returns>
        public VTag AddWysiwyg(string wysiwyg, params System.Xml.Linq.XAttribute[] roottagattributes)
        {
            Extensions.AddWysiwyg(this, wysiwyg, roottagattributes);

            return this;
        }
    }
}
