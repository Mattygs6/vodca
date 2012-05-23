//-----------------------------------------------------------------------------
// <copyright file="VTag.HtmlTags.ItoR.cs" company="genuine">
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
        #region I to M

        /// <summary>
        /// Gets the html i tag.
        /// </summary>
        public static VTag I
        {
            get
            {
                return new VTag(WellKnownXNames.I, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html iframe tag.
        /// </summary>
        public static VTag Iframe
        {
            get
            {
                return new VTag(WellKnownXNames.Iframe, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html ins tag.
        /// </summary>
        public static VTag Ins
        {
            get
            {
                return new VTag(WellKnownXNames.Ins, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html label tag.
        /// </summary>
        public static VTag Label
        {
            get
            {
                return new VTag(WellKnownXNames.Label, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html legend tag.
        /// </summary>
        public static VTag Legend
        {
            get
            {
                return new VTag(WellKnownXNames.Legend, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html li tag.
        /// </summary>
        public static VTag Li
        {
            get
            {
                return new VTag(WellKnownXNames.Li, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html map tag.
        /// </summary>
        public static VTag Map
        {
            get
            {
                return new VTag(WellKnownXNames.Map, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html mark tag.
        /// </summary>
        public static VTag Mark
        {
            get
            {
                return new VTag(WellKnownXNames.Mark, string.Empty);
            }
        }

        #endregion

        #region N to R

        /// <summary>
        /// Gets the html nav tag.
        /// </summary>
        public static VTag Nav
        {
            get
            {
                return new VTag(WellKnownXNames.Nav, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html noscript tag.
        /// </summary>
        public static VTag NoScript
        {
            get
            {
                return new VTag(WellKnownXNames.NoScript, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html object tag.
        /// </summary>
        public static VTag Object
        {
            get
            {
                return new VTag(WellKnownXNames.Object, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html ol tag.
        /// </summary>
        public static VTag Ol
        {
            get
            {
                return new VTag(WellKnownXNames.Ol, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html optgroup tag.
        /// </summary>
        public static VTag OptGroup
        {
            get
            {
                return new VTag(WellKnownXNames.OptGroup, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html option tag.
        /// </summary>
        public static VTag Option
        {
            get
            {
                return new VTag(WellKnownXNames.Option, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html p tag.
        /// </summary>
        public static VTag P
        {
            get
            {
                return new VTag(WellKnownXNames.P, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html pre tag.
        /// </summary>
        public static VTag Pre
        {
            get
            {
                return new VTag(WellKnownXNames.Pre, string.Empty);
            }
        }

        /// <summary>
        /// Gets the html q tag.
        /// </summary>
        public static VTag Q
        {
            get
            {
                return new VTag(WellKnownXNames.Q, string.Empty);
            }
        }

        #endregion
    }
}
