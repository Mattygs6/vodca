//-----------------------------------------------------------------------------
// <copyright file="WellKnownXNames.Attributes.Global.cs" company="genuine">
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
    /// Well known Html global attributes
    /// </summary>
    internal static partial class WellKnownXNames
    {
        /// <summary>
        /// Strong typed Html global attribute accesskey
        /// </summary>
        public static readonly XName AccessKey = XName.Get("accesskey");

        /// <summary>
        /// Strong typed Html global attribute class
        /// </summary>
        public static readonly XName Class = XName.Get("class");

        /// <summary>
        /// Strong typed Html global attribute contenteditable
        /// </summary>
        public static readonly XName ContentEditable = XName.Get("contenteditable");

        /// <summary>
        /// Strong typed Html global attribute contextmenu
        /// </summary>
        public static readonly XName ContextMenu = XName.Get("contextmenu");

        /// <summary>
        /// Strong typed Html global attribute dir
        /// </summary>
        public static readonly XName Dir = XName.Get("dir");

        /// <summary>
        /// Strong typed Html global attribute draggable
        /// </summary>
        public static readonly XName Draggable = XName.Get("draggable");

        /// <summary>
        /// Strong typed Html global attribute dropzone
        /// </summary>
        public static readonly XName DropZone = XName.Get("dropzone");

        /// <summary>
        /// Strong typed Html global attribute hidden
        /// </summary>
        public static readonly XName Hidden = XName.Get("hidden");

        /// <summary>
        /// Strongly typed Html global attribute id
        /// </summary>
        public static readonly XName Id = XName.Get("id");

        /// <summary>
        /// Strongly typed Html global attribute lang
        /// </summary>
        public static readonly XName Lang = XName.Get("lang");

        /// <summary>
        /// Strongly typed Html global attribute spellcheck
        /// </summary>
        public static readonly XName SpellCheck = XName.Get("spellcheck");

        /// <summary>
        /// Strongly typed Html global attribute style
        /// </summary>
        public static readonly XName Style = XName.Get("style");

        /// <summary>
        /// Strongly typed Html global attribute tabindex
        /// </summary>
        public static readonly XName TabIndex = XName.Get("tabindex");

        /// <summary>
        /// Strongly typed Html global attribute title
        /// </summary>
        public static readonly XName Title = XName.Get("title");
    }
}
