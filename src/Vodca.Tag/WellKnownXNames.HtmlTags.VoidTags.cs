//-----------------------------------------------------------------------------
// <copyright file="WellKnownXNames.HtmlTags.VoidTags.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       12/09/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Xml.Linq;

    /// <summary>
    /// Well known Html void tag names
    /// </summary>
    internal static partial class WellKnownXNames
    {
        /// <summary>
        /// Strongly typed Html tag area
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Area = XName.Get("area");

        /// <summary>
        /// Strongly typed Html tag base
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Base = XName.Get("base");

        /// <summary>
        /// Strongly typed Html tag br
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Br = XName.Get("br");

        /// <summary>
        /// Strongly typed Html tag col
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Col = XName.Get("col");

        /// <summary>
        /// Strongly typed Html tag embed
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Embed = XName.Get("embed");

        /// <summary>
        /// Strongly typed Html tag hr
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Hr = XName.Get("hr");

        /// <summary>
        /// Strongly typed Html tag img
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Img = XName.Get("img");

        /// <summary>
        /// Strongly typed Html tag input
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Input = XName.Get("input");

        /// <summary>
        /// Strongly typed Html tag link
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Link = XName.Get("link");

        /// <summary>
        /// Strongly typed Html tag meta
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Meta = XName.Get("meta");

        /// <summary>
        /// Strongly typed Html tag param
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Param = XName.Get("param");

        /// <summary>
        /// Strongly typed Html tag source
        /// <para>This tag cannot contain children</para>
        /// </summary>
        public static readonly XName Source = XName.Get("source");
    }
}
