//-----------------------------------------------------------------------------
// <copyright file="EncodingFoundException.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    using System.Text;
    using System.Web;

    /// <summary>
    /// The encoding found exception.
    /// </summary>
    internal class EncodingFoundException : HttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncodingFoundException"/> class.
        /// </summary>
        /// <param name="encoding">
        /// The encoding.
        /// </param>
        internal EncodingFoundException(Encoding encoding)
        {
            this.Encoding = encoding;
        }

        /// <summary>
        /// Gets Encoding.
        /// </summary>
        internal Encoding Encoding { get; private set; }
    }
}