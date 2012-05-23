//-----------------------------------------------------------------------------
// <copyright file="VTag.Attributes.Class.cs" company="genuine">
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
        /// Adds the class.
        /// </summary>
        /// <param name="cssclass">The css class.</param>
        /// <returns>The VTag instance</returns>
        public VTag AddClassAttr(string cssclass)
        {
            // surround with spaces so we can look for cssclass with spaces around it
            var classNames = string.Format(" {0} ", this.GetAttribute(WellKnownXNames.Class));

            cssclass = string.Format(" {0} ", cssclass);

            if (!classNames.Contains(cssclass))
            {
                classNames = string.Format("{0} {1}", classNames.Trim(), cssclass);
            }

            if (!string.IsNullOrWhiteSpace(classNames))
            {
                return this.AddAttribute(WellKnownXNames.Class, classNames);
            }

            return this.RemoveAttribute(WellKnownXNames.Class);
        }

        /// <summary>
        /// Gets the class.
        /// </summary>
        /// <returns>The class attribute</returns>
        public string GetClassAttr()
        {
            return this.GetAttribute(WellKnownXNames.Class);
        }

        /// <summary>
        /// Removes the class.
        /// </summary>
        /// <param name="cssclass">The css class.</param>
        /// <returns>The VTag instance</returns>
        public VTag RemoveClassAttr(string cssclass)
        {
            // surround with spaces so we can look for cssclass with spaces around it
            var classNames = string.Format(" {0} ", this.GetAttribute(WellKnownXNames.Class));

            cssclass = string.Format(" {0} ", cssclass);

            var newClassNames = classNames.Replace(cssclass, " ");

            if (!string.IsNullOrWhiteSpace(newClassNames))
            {
                return this.AddAttribute(WellKnownXNames.Class, newClassNames);
            }

            return this.RemoveAttribute(WellKnownXNames.Class);
        }

        /// <summary>
        /// Sets the class.
        /// </summary>
        /// <param name="cssclass">The cssclass.</param>
        /// <returns>The VTag instance.</returns>
        public VTag SetClassAttr(string cssclass)
        {
            if (!string.IsNullOrWhiteSpace(cssclass))
            {
                return this.AddAttribute(WellKnownXNames.Class, cssclass);
            }

            return this.RemoveAttribute(WellKnownXNames.Class);
        }
    }
}