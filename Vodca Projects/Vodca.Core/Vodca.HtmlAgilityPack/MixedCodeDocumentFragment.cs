//-----------------------------------------------------------------------------
// <copyright file="MixedCodeDocumentFragment.cs" company="genuine">
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
    /// Represents a base class for fragments in a mixed code document.
    /// </summary>
    public abstract partial class MixedCodeDocumentFragment
    {
        /// <summary>
        /// The _fragment text.
        /// </summary>
        private string fragmentText;

        /// <summary>
        /// Initializes a new instance of the <see cref="MixedCodeDocumentFragment"/> class.
        /// </summary>
        /// <param name="mixedcodedocument">The mixed code document.</param>
        /// <param name="type">The type.</param>
        internal MixedCodeDocumentFragment(MixedCodeDocument mixedcodedocument, MixedCodeDocumentFragmentType type)
        {
            this.MixedCodeDocument = mixedcodedocument;
            this.FragmentType = type;
            switch (type)
            {
                case MixedCodeDocumentFragmentType.Text:
                    this.MixedCodeDocument.TextFragments.Append(this);
                    break;

                case MixedCodeDocumentFragmentType.Code:
                    this.MixedCodeDocument.CodeFragments.Append(this);
                    break;
            }

            this.MixedCodeDocument.Fragments.Append(this);
        }

        /// <summary>
        ///   Gets the type of fragment.
        /// </summary>
        public MixedCodeDocumentFragmentType FragmentType { get; internal set; }

        /// <summary>
        ///   Gets the line number of the fragment.
        /// </summary>
        public int Line { get; internal set; }

        /// <summary>
        ///   Gets the line position (column) of the fragment.
        /// </summary>
        public int LinePosition { get; internal set; }

        /// <summary>
        ///   Gets the fragment position in the document's stream.
        /// </summary>
        public int StreamPosition { get; internal set; }

        /// <summary>
        /// Gets the fragment text.
        /// </summary>
        public string FragmentText
        {
            get
            {
                return this.fragmentText ?? (this.fragmentText = this.MixedCodeDocument.Text.Substring(this.StreamPosition, this.Length));
            }

            internal set
            {
                this.fragmentText = value;
            }
        }

        /// <summary>
        /// Gets or sets the mixed code document.
        /// </summary>
        /// <value>
        /// The mixed code document.
        /// </value>
        internal MixedCodeDocument MixedCodeDocument { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        internal int Length { get; set; }
    }
}