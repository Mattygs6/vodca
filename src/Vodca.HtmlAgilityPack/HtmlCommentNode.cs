//-----------------------------------------------------------------------------
// <copyright file="HtmlCommentNode.cs" company="genuine">
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
    /// Represents an HTML comment.
    /// </summary>
    public partial class HtmlCommentNode : HtmlNode
    {
        /// <summary>
        /// The _comment.
        /// </summary>
        private string comment;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlCommentNode"/> class.
        /// </summary>
        /// <param name="ownerdocument">
        /// The owner document.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        internal HtmlCommentNode(HtmlDocument ownerdocument, int index)
            : base(HtmlNodeType.Comment, ownerdocument, index)
        {
        }

        /// <summary>
        ///   Gets or sets the comment text of the node.
        /// </summary>
        public string Comment
        {
            get
            {
                if (this.comment == null)
                {
                    return base.InnerHtml;
                }

                return this.comment;
            }

            set
            {
                this.comment = value;
            }
        }

        /// <summary>
        ///   Gets or Sets the HTML between the start and end tags of the object. In the case of a text node, it is equals to OuterHtml.
        /// </summary>
        public override string InnerHtml
        {
            get
            {
                if (this.comment == null)
                {
                    return base.InnerHtml;
                }

                return this.comment;
            }

            set
            {
                this.comment = value;
            }
        }

        /// <summary>
        ///   Gets or Sets the object and its content in HTML.
        /// </summary>
        public override string OuterHtml
        {
            get
            {
                if (this.comment == null)
                {
                    return base.OuterHtml;
                }

                return "<!--" + this.comment + "-->";
            }
        }
    }
}