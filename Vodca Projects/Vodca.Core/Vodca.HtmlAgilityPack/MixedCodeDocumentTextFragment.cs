//-----------------------------------------------------------------------------
// <copyright file="MixedCodeDocumentTextFragment.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    /// <summary>
    /// Represents a fragment of text in a mixed code document.
    /// </summary>
    public partial class MixedCodeDocumentTextFragment : MixedCodeDocumentFragment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MixedCodeDocumentTextFragment"/> class.
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        internal MixedCodeDocumentTextFragment(MixedCodeDocument doc)
            : base(doc, MixedCodeDocumentFragmentType.Text)
        {
        }

        /// <summary>
        /// Gets or sets the fragment text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get
            {
                return this.FragmentText;
            }

            set
            {
                this.FragmentText = value;
            }
        }
    }
}