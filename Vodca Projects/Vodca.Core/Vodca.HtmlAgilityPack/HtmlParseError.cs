//-----------------------------------------------------------------------------
// <copyright file="HtmlParseError.cs" company="genuine">
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
    /// Represents a parsing error found during document parsing.
    /// </summary>
    public sealed partial class HtmlParseError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlParseError"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="line">The line.</param>
        /// <param name="linePosition">The line position.</param>
        /// <param name="streamPosition">The stream position.</param>
        /// <param name="sourceText">The source text.</param>
        /// <param name="reason">The reason.</param>
        internal HtmlParseError(HtmlParseErrorCode code, int line, int linePosition, int streamPosition, string sourceText, string reason)
        {
            this.Code = code;
            this.Line = line;
            this.LinePosition = linePosition;
            this.StreamPosition = streamPosition;
            this.SourceText = sourceText;
            this.Reason = reason;
        }

        /// <summary>
        ///   Gets the type of error.
        /// </summary>
        public HtmlParseErrorCode Code { get; private set; }

        /// <summary>
        ///   Gets the line number of this error in the document.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        ///   Gets the column number of this error in the document.
        /// </summary>
        public int LinePosition { get; private set; }

        /// <summary>
        ///   Gets a description for the error.
        /// </summary>
        public string Reason { get; private set; }

        /// <summary>
        ///   Gets the the full text of the line containing the error.
        /// </summary>
        public string SourceText { get; private set; }

        /// <summary>
        ///   Gets the absolute stream position of this error in the document, relative to the start of the document.
        /// </summary>
        public int StreamPosition { get; private set; }
    }
}