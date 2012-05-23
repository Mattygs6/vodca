//-----------------------------------------------------------------------------
// <copyright file="MixedCodeDocumentCodeFragment.cs" company="genuine">
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
    /// Represents a fragment of code in a mixed code document.
    /// </summary>
    public partial class MixedCodeDocumentCodeFragment : MixedCodeDocumentFragment
    {
        /// <summary>
        /// The _code.
        /// </summary>
        private string code;

        /// <summary>
        /// Initializes a new instance of the <see cref="MixedCodeDocumentCodeFragment"/> class.
        /// </summary>
        /// <param name="doc">
        /// The doc.
        /// </param>
        internal MixedCodeDocumentCodeFragment(MixedCodeDocument doc)
            : base(doc, MixedCodeDocumentFragmentType.Code)
        {
        }

        /// <summary>
        /// Gets or sets the fragment code text.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code
        {
            get
            {
                if (this.code == null)
                {
                    this.code =
                        this.FragmentText.Substring(
                            MixedCodeDocument.TokenCodeStart.Length,
                            this.FragmentText.Length - MixedCodeDocument.TokenCodeEnd.Length - MixedCodeDocument.TokenCodeStart.Length - 1)
                            .Trim();

                    if (this.code.StartsWith("="))
                    {
                        this.code = MixedCodeDocument.TokenResponseWrite + this.code.Substring(1, this.code.Length - 1);
                    }
                }

                return this.code;
            }

            set
            {
                this.code = value;
            }
        }
    }
}