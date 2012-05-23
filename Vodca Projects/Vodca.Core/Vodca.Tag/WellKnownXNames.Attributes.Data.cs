//-----------------------------------------------------------------------------
// <copyright file="WellKnownXNames.Attributes.Data.cs" company="genuine">
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
    /// Well known Html (jQuery) data attributes
    /// </summary>
    internal static partial class WellKnownXNames
    {
        /// <summary>
        /// Strongly typed attribute data-id
        /// </summary>
        public static readonly XName DataId = XName.Get("data-id");

        /// <summary>
        /// Strongly typed attribute data-ga used for Google Analytics
        /// </summary>
        public static readonly XName DataGaAction = XName.Get("data-ga");

        /// <summary>
        /// Strongly typed attribute data-gaeventcategory used for Google Analytics
        /// </summary>
        public static readonly XName DataGaEventCategory = XName.Get("data-gaeventcategory");

        /// <summary>
        /// Strongly typed attribute data-gaeventaction used for Google Analytics
        /// </summary>
        public static readonly XName DataGaEventAction = XName.Get("data-gaeventaction");

        /// <summary>
        /// Strongly typed attribute data-gaeventlabel used for Google Analytics
        /// </summary>
        public static readonly XName DataGaEventLabel = XName.Get("data-gaeventlabel");

        /// <summary>
        /// Strongly typed attribute data-gaeventvalue used for Google Analytics
        /// </summary>
        public static readonly XName DataGaEventValue = XName.Get("data-gaeventvalue");

        /// <summary>
        /// Strongly typed attribute data-gapageview used for Google Analytics
        /// </summary>
        public static readonly XName DataGaPageview = XName.Get("data-gapageview");
    }
}
