//-----------------------------------------------------------------------------
// <copyright file="HtmlNode.Internal.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class HtmlNode
    {
        /// <summary>
        /// Gets or sets a value indicating whether [inner changed].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [inner changed]; otherwise, <c>false</c>.
        /// </value>
        internal bool InnerChanged { get; set; }

        /// <summary>
        /// Gets or sets the length of the inner.
        /// </summary>
        /// <value>
        /// The length of the inner.
        /// </value>
        internal int InnerLength { get; set; }

        /// <summary>
        /// Gets or sets the start index of the inner.
        /// </summary>
        /// <value>
        /// The start index of the inner.
        /// </value>
        internal int InnerStartIndex { get; set; }

        /// <summary>
        /// Gets or sets the length of the name.
        /// </summary>
        /// <value>
        /// The length of the name.
        /// </value>
        internal int NameLength { get; set; }

        /// <summary>
        /// Gets or sets the start index of the name.
        /// </summary>
        /// <value>
        /// The start index of the name.
        /// </value>
        internal int NameStartIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [outer changed].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [outer changed]; otherwise, <c>false</c>.
        /// </value>
        internal bool OuterChanged { get; set; }

        /// <summary>
        /// Gets or sets the length of the outer.
        /// </summary>
        /// <value>
        /// The length of the outer.
        /// </value>
        internal int OuterLength { get; set; }

        /// <summary>
        /// Gets or sets the start index of the outer.
        /// </summary>
        /// <value>
        /// The start index of the outer.
        /// </value>
        internal int OuterStartIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [start tag].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [start tag]; otherwise, <c>false</c>.
        /// </value>
        internal bool StartTag { get; set; }

        /// <summary>
        /// Gets or sets the name of the previous with same.
        /// </summary>
        /// <value>
        /// The name of the previous with same.
        /// </value>
        internal HtmlNode PreviousWithSameName { get; set; }

        /// <summary>
        /// Gets or sets the end node.
        /// </summary>
        /// <value>
        /// The end node.
        /// </value>
        internal HtmlNode EndNode { get; set; }

        /// <summary>
        /// Gets or sets the name of the HTML node.
        /// </summary>
        /// <value>
        /// The name of the HTML node.
        /// </value>
        private string HtmlNodeName { get; set; }
    }
}