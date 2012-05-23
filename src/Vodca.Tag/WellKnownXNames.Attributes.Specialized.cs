//-----------------------------------------------------------------------------
// <copyright file="WellKnownXNames.Attributes.Specialized.cs" company="genuine">
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
    /// Well known Html attribute names
    /// </summary>
    internal static partial class WellKnownXNames
    {
        /// <summary>
        /// Strongly Typed XName
        /// </summary>
        public static readonly XName Alt = XName.Get("alt");

        /// <summary>
        /// Strongly Typed Attribute Name
        /// </summary>
        public static readonly XName Height = XName.Get("height");

        /// <summary>
        /// Strongly Typed Attribute Name
        /// </summary>
        public static readonly XName Href = XName.Get("href");

        /// <summary>
        /// Strongly Typed Attribute Name
        /// </summary>
        public static readonly XName Key = XName.Get("key");

        /// <summary>
        /// Strongly Typed Attribute Name
        /// </summary>
        public static readonly XName Name = XName.Get("name");

        /// <summary>
        /// Strongly Typed Attribute Name
        /// </summary>
        public static readonly XName Rel = XName.Get("rel");

        /// <summary>
        /// Strongly typed html attribute selected
        /// </summary>
        public static readonly XName Selected = XName.Get("selected");

        /// <summary>
        /// Strongly typed html attribute src
        /// </summary>
        public static readonly XName Src = XName.Get("src");

        /// <summary>
        /// Strongly Typed Attribute Name
        /// </summary>
        public static readonly XName Target = XName.Get("target");

        /// <summary>
        /// Strongly Typed Attribute Name
        /// </summary>
        public static readonly XName Text = XName.Get("text");

        /// <summary>
        /// Strongly typed html attribute type
        /// </summary>
        public static readonly XName Type = XName.Get("type");

        /// <summary>
        /// Strongly typed html attribute value
        /// </summary>
        public static readonly XName Value = XName.Get("value");

        /// <summary>
        /// Strongly Typed Attribute Name
        /// </summary>
        public static readonly XName Width = XName.Get("width");
    }
}
