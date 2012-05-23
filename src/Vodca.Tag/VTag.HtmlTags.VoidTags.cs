//-----------------------------------------------------------------------------
// <copyright file="VTag.HtmlTags.VoidTags.cs" company="genuine">
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
        /// Gets the html area tag.
        /// </summary>
        public static VTag Area
        {
            get
            {
                return new VTag(WellKnownXNames.Area);
            }
        }

        /// <summary>
        /// Gets the html base tag.
        /// </summary>
        public static VTag Base
        {
            get
            {
                return new VTag(WellKnownXNames.Base);
            }
        }

        /// <summary>
        /// Gets the html br tag.
        /// </summary>
        public static VTag Br
        {
            get
            {
                return new VTag(WellKnownXNames.Br);
            }
        }

        /// <summary>
        /// Gets the html col tag.
        /// </summary>
        public static VTag Col
        {
            get
            {
                return new VTag(WellKnownXNames.Col);
            }
        }

        /// <summary>
        /// Gets the html embed tag.
        /// </summary>
        public static VTag Embed
        {
            get
            {
                return new VTag(WellKnownXNames.Embed);
            }
        }

        /// <summary>
        /// Gets the html hr tag.
        /// </summary>
        public static VTag Hr
        {
            get
            {
                return new VTag(WellKnownXNames.Hr);
            }
        }

        /// <summary>
        /// Gets the html img tag.
        /// </summary>
        public static VTag Img
        {
            get
            {
                return new VTag(WellKnownXNames.Img);
            }
        }

        /// <summary>
        /// Gets the html input tag.
        /// </summary>
        public static VTag Input
        {
            get
            {
                return new VTag(WellKnownXNames.Input);
            }
        }

        /// <summary>
        /// Gets the html link tag.
        /// </summary>
        public static VTag Link
        {
            get
            {
                return new VTag(WellKnownXNames.Link);
            }
        }

        /// <summary>
        /// Gets the html meta tag.
        /// </summary>
        public static VTag Meta
        {
            get
            {
                return new VTag(WellKnownXNames.Meta);
            }
        }

        /// <summary>
        /// Gets the html param tag.
        /// </summary>
        public static VTag Param
        {
            get
            {
                return new VTag(WellKnownXNames.Param);
            }
        }

        /// <summary>
        /// Gets the html source tag.
        /// </summary>
        public static VTag Source
        {
            get
            {
                return new VTag(WellKnownXNames.Source);
            }
        }
    }
}
