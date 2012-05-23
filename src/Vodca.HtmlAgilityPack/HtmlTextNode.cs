//-----------------------------------------------------------------------------
// <copyright file="HtmlTextNode.cs" company="genuine">
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
    /// Represents an HTML text node.
    /// </summary>
    public partial class HtmlTextNode : HtmlNode
    {
        /// <summary>
        /// The _text.
        /// </summary>
        private string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlTextNode"/> class.
        /// </summary>
        /// <param name="ownerdocument">
        /// The owner document.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        internal HtmlTextNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Text, ownerdocument, index)
        {
        }

        /// <summary>
        ///   Gets or Sets the HTML between the start and end tags of the object. In the case of a text node, it is equals to OuterHtml.
        /// </summary>
        public override string InnerHtml
        {
            get
            {
                return this.OuterHtml;
            }

            set
            {
                this.text = value;
            }
        }

        /// <summary>
        ///   Gets or Sets the object and its content in HTML.
        /// </summary>
        public override string OuterHtml
        {
            get
            {
                if (this.text == null)
                {
                    return base.OuterHtml;
                }

                return this.text;
            }
        }

        /// <summary>
        /// Gets or sets the text of the node.
        /// </summary>
        public string Text
        {
            get
            {
                if (this.text == null)
                {
                    return base.OuterHtml;
                }

                return this.text;
            }

            set
            {
                this.text = value;
            }
        }
    }
}