//-----------------------------------------------------------------------------
// <copyright file="VTag.HtmlTags.DtoH.cs" company="genuine">
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
        #region D

        /// <summary>
        /// Gets the html dd tag.
        /// </summary>
        public static VTag Dd
        {
            get
            {
                return new VTag(WellKnownXNames.Dd, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html del tag.
        /// </summary>
        public static VTag Del
        {
            get
            {
                return new VTag(WellKnownXNames.Del, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html dfn tag.
        /// </summary>
        public static VTag Dfn
        {
            get
            {
                return new VTag(WellKnownXNames.Dfn, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html div tag.
        /// </summary>
        public static VTag Div
        {
            get
            {
                return new VTag(WellKnownXNames.Div, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html dl tag.
        /// </summary>
        public static VTag Dl
        {
            get
            {
                return new VTag(WellKnownXNames.Dl, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html dt tag.
        /// </summary>
        public static VTag Dt
        {
            get
            {
                return new VTag(WellKnownXNames.Dt, string.Empty);
            }
        }

        #endregion

        #region E to F

        /// <summary>
        /// Gets the html em tag.
        /// </summary>
        public static VTag Em
        {
            get
            {
                return new VTag(WellKnownXNames.Em, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html fieldset tag.
        /// </summary>
        public static VTag FieldSet
        {
            get
            {
                return new VTag(WellKnownXNames.FieldSet, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html figcaption tag.
        /// </summary>
        public static VTag FigCaption
        {
            get
            {
                return new VTag(WellKnownXNames.FigCaption, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html figure tag.
        /// </summary>
        public static VTag Figure
        {
            get
            {
                return new VTag(WellKnownXNames.Figure, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html footer tag.
        /// </summary>
        public static VTag Footer
        {
            get
            {
                return new VTag(WellKnownXNames.Footer, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html form tag.
        /// </summary>
        public static VTag Form
        {
            get
            {
                return new VTag(WellKnownXNames.Form, string.Empty);
            }
        }

        #endregion

        #region H

        /// <summary>
        /// Gets the html h1 tag.
        /// </summary>
        public static VTag H1
        {
            get
            {
                return new VTag(WellKnownXNames.H1, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html h2 tag.
        /// </summary>
        public static VTag H2
        {
            get
            {
                return new VTag(WellKnownXNames.H2, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html h3 tag.
        /// </summary>
        public static VTag H3
        {
            get
            {
                return new VTag(WellKnownXNames.H3, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html h4 tag.
        /// </summary>
        public static VTag H4
        {
            get
            {
                return new VTag(WellKnownXNames.H4, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html h5 tag.
        /// </summary>
        public static VTag H5
        {
            get
            {
                return new VTag(WellKnownXNames.H5, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html h6 tag.
        /// </summary>
        public static VTag H6
        {
            get
            {
                return new VTag(WellKnownXNames.H6, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html head tag.
        /// </summary>
        public static VTag Head
        {
            get
            {
                return new VTag(WellKnownXNames.Head, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html header tag.
        /// </summary>
        public static VTag Header
        {
            get
            {
                return new VTag(WellKnownXNames.Header, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html hgroup tag.
        /// </summary>
        public static VTag Hgroup
        {
            get
            {
                return new VTag(WellKnownXNames.Hgroup, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html 'html' tag.
        /// </summary>
        public static VTag Html
        {
            get
            {
                return new VTag(WellKnownXNames.Html, string.Empty);
            }
        }

        #endregion
    }
}
