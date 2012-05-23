//-----------------------------------------------------------------------------
// <copyright file="VTag.HtmlTags.AtoC.cs" company="genuine">
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
        #region A

        /// <summary>
        /// Gets the html a tag.
        /// </summary>
        public static VTag A
        {
            get
            {
                return new VTag(WellKnownXNames.A, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html abbr tag.
        /// </summary>
        public static VTag Abbr
        {
            get
            {
                return new VTag(WellKnownXNames.Abbr, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html address tag.
        /// </summary>
        public static VTag Address
        {
            get
            {
                return new VTag(WellKnownXNames.Address, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html article tag.
        /// </summary>
        public static VTag Article
        {
            get
            {
                return new VTag(WellKnownXNames.Article, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html aside tag.
        /// </summary>
        public static VTag Aside
        {
            get
            {
                return new VTag(WellKnownXNames.Aside, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html audio tag.
        /// </summary>
        public static VTag Audio
        {
            get
            {
                return new VTag(WellKnownXNames.Audio, string.Empty);
            }
        }

        #endregion

        #region B

        /// <summary>
        /// Gets the html b tag.
        /// </summary>
        public static VTag B
        {
            get
            {
                return new VTag(WellKnownXNames.B, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html bdo tag.
        /// </summary>
        public static VTag Bdo
        {
            get
            {
                return new VTag(WellKnownXNames.Bdo, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html blockquote tag.
        /// </summary>
        public static VTag BlockQuote
        {
            get
            {
                return new VTag(WellKnownXNames.BlockQuote, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html body tag.
        /// </summary>
        public static VTag Body
        {
            get
            {
                return new VTag(WellKnownXNames.Body, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html button tag.
        /// </summary>
        public static VTag Button
        {
            get
            {
                return new VTag(WellKnownXNames.Button, string.Empty);
            }
        }

        #endregion

        #region C

        /// <summary>
        /// Gets the html canvas tag.
        /// </summary>
        public static VTag Canvas
        {
            get
            {
                return new VTag(WellKnownXNames.Canvas, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html caption tag.
        /// </summary>
        public static VTag Caption
        {
            get
            {
                return new VTag(WellKnownXNames.Caption, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html cite tag.
        /// </summary>
        public static VTag Cite
        {
            get
            {
                return new VTag(WellKnownXNames.Cite, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html code tag.
        /// </summary>
        public static VTag Code
        {
            get
            {
                return new VTag(WellKnownXNames.Code, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html colgroup tag.
        /// </summary>
        public static VTag ColGroup
        {
            get
            {
                return new VTag(WellKnownXNames.ColGroup, string.Empty);
            }
        }

        #endregion
    }
}
