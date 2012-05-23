//-----------------------------------------------------------------------------
// <copyright file="MixedCodeDocument.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Represents a document with mixed code and text. ASP, ASPX, JSP, are good example of such documents.
    /// </summary>
    public partial class MixedCodeDocument
    {
        /// <summary>
        ///   Gets or sets the token representing code end.
        /// </summary>
        public const string TokenCodeEnd = "%>";

        /// <summary>
        ///   Gets or sets the token representing code start.
        /// </summary>
        public const string TokenCodeStart = "<%";

        /// <summary>
        ///   Gets or sets the token representing code directive.
        /// </summary>
        public const string TokenDirective = "@";

        /// <summary>
        ///   Gets or sets the token representing response write directive.
        /// </summary>
        public const string TokenResponseWrite = "Response.Write ";

        /// <summary>
        /// The token text block.
        /// </summary>
        private const string TokenTextBlock = "TextBlock({0})";

        /// <summary>
        /// The current character
        /// </summary>
        private int character;

        /// <summary>
        /// The _index.
        /// </summary>
        private int index;

        /// <summary>
        /// The _currentfragment.
        /// </summary>
        private MixedCodeDocumentFragment currentfragment;

        /// <summary>
        /// Initializes a new instance of the <see cref="MixedCodeDocument"/> class. 
        ///   Creates a mixed code document instance.
        /// </summary>
        public MixedCodeDocument()
        {
            this.CodeFragments = new MixedCodeDocumentFragmentList(this);
            this.TextFragments = new MixedCodeDocumentFragmentList(this);
            this.Fragments = new MixedCodeDocumentFragmentList(this);
        }

        /// <summary>
        /// The parse state.
        /// </summary>
        private enum ParseState
        {
            /// <summary>
            /// The text.
            /// </summary>
            Text,

            /// <summary>
            /// The code.
            /// </summary>
            Code
        }

        /// <summary>
        ///   Gets the code represented by the mixed code document seen as a template.
        /// </summary>
        public string Code
        {
            get
            {
                string s = string.Empty;
                int i = 0;
                foreach (MixedCodeDocumentFragment frag in this.Fragments)
                {
                    switch (frag.FragmentType)
                    {
                        case MixedCodeDocumentFragmentType.Text:
                            s += TokenResponseWrite + string.Format(TokenTextBlock, i) + Environment.NewLine;
                            i++;
                            break;

                        case MixedCodeDocumentFragmentType.Code:
                            s += ((MixedCodeDocumentCodeFragment)frag).Code + Environment.NewLine;
                            break;
                    }
                }

                return s;
            }
        }

        /// <summary>
        ///   Gets the list of code fragments in the document.
        /// </summary>
        public MixedCodeDocumentFragmentList CodeFragments { get; internal set; }

        /// <summary>
        ///   Gets the list of all fragments in the document.
        /// </summary>
        public MixedCodeDocumentFragmentList Fragments { get; internal set; }

        /// <summary>
        ///   Gets the encoding of the stream used to read the document.
        /// </summary>
        public Encoding StreamEncoding { get; private set; }

        /// <summary>
        /// Gets the list of text fragments in the document.
        /// </summary>
        public MixedCodeDocumentFragmentList TextFragments { get; internal set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        internal string Text { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        private int Line { get; set; }

        /// <summary>
        /// Gets or sets the line position.
        /// </summary>
        /// <value>
        /// The line position.
        /// </value>
        private int Lineposition { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        private ParseState State { get; set; }

        /// <summary>
        /// Create a code fragment instances.
        /// </summary>
        /// <returns>
        /// The newly created code fragment instance. 
        /// </returns>
        public MixedCodeDocumentCodeFragment CreateCodeFragment()
        {
            return (MixedCodeDocumentCodeFragment)this.CreateFragment(MixedCodeDocumentFragmentType.Code);
        }

        /// <summary>
        /// Create a text fragment instances.
        /// </summary>
        /// <returns>
        /// The newly created text fragment instance. 
        /// </returns>
        public MixedCodeDocumentTextFragment CreateTextFragment()
        {
            return (MixedCodeDocumentTextFragment)this.CreateFragment(MixedCodeDocumentFragmentType.Text);
        }

        /// <summary>
        /// Loads a mixed code document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        public void Load(Stream stream)
        {
            this.Load(new StreamReader(stream));
        }

        /// <summary>
        /// Loads a mixed code document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        public void Load(Stream stream, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(stream, detectEncodingFromByteOrderMarks));
        }

        /// <summary>
        /// Loads a mixed code document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        public void Load(Stream stream, Encoding encoding)
        {
            this.Load(new StreamReader(stream, encoding));
        }

        /// <summary>
        /// Loads a mixed code document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks));
        }

        /// <summary>
        /// Loads a mixed code document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        /// <param name="buffersize">
        /// The minimum buffer size. 
        /// </param>
        public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
        {
            this.Load(new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, buffersize));
        }

        /// <summary>
        /// Loads a mixed code document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. 
        /// </param>
        public void Load(string path)
        {
            this.Load(new StreamReader(path));
        }

        /// <summary>
        /// Loads a mixed code document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        public void Load(string path, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(path, detectEncodingFromByteOrderMarks));
        }

        /// <summary>
        /// Loads a mixed code document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        public void Load(string path, Encoding encoding)
        {
            this.Load(new StreamReader(path, encoding));
        }

        /// <summary>
        /// Loads a mixed code document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(path, encoding, detectEncodingFromByteOrderMarks));
        }

        /// <summary>
        /// Loads a mixed code document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        /// <param name="buffersize">
        /// The minimum buffer size. 
        /// </param>
        public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
        {
            this.Load(new StreamReader(path, encoding, detectEncodingFromByteOrderMarks, buffersize));
        }

        /// <summary>
        /// Loads the mixed code document from the specified TextReader.
        /// </summary>
        /// <param name="reader">
        /// The TextReader used to feed the HTML data into the document. 
        /// </param>
        public void Load(TextReader reader)
        {
            this.CodeFragments.Clear();
            this.TextFragments.Clear();

            // all pseudo constructors get down to this one
            using (var sr = reader as StreamReader)
            {
                if (sr != null)
                {
                    this.StreamEncoding = sr.CurrentEncoding;
                }

                this.Text = reader.ReadToEnd();
                reader.Close();
            }

            this.Parse();
        }

        /// <summary>
        /// Loads a mixed document from a text
        /// </summary>
        /// <param name="html">
        /// The text to load. 
        /// </param>
        public void LoadHtml(string html)
        {
            this.Load(new StringReader(html));
        }

        /// <summary>
        /// Saves the mixed document to the specified stream.
        /// </summary>
        /// <param name="outStream">
        /// The stream to which you want to save. 
        /// </param>
        public void Save(Stream outStream)
        {
            var sw = new StreamWriter(outStream, this.GetOutEncoding());
            this.Save(sw);
        }

        /// <summary>
        /// Saves the mixed document to the specified stream.
        /// </summary>
        /// <param name="outStream">
        /// The stream to which you want to save. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        public void Save(Stream outStream, Encoding encoding)
        {
            var sw = new StreamWriter(outStream, encoding);
            this.Save(sw);
        }

        /// <summary>
        /// Saves the mixed document to the specified file.
        /// </summary>
        /// <param name="filename">
        /// The location of the file where you want to save the document. 
        /// </param>
        public void Save(string filename)
        {
            var sw = new StreamWriter(filename, false, this.GetOutEncoding());
            this.Save(sw);
        }

        /// <summary>
        /// Saves the mixed document to the specified file.
        /// </summary>
        /// <param name="filename">
        /// The location of the file where you want to save the document. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        public void Save(string filename, Encoding encoding)
        {
            var sw = new StreamWriter(filename, false, encoding);
            this.Save(sw);
        }

        /// <summary>
        /// Saves the mixed document to the specified StreamWriter.
        /// </summary>
        /// <param name="writer">
        /// The StreamWriter to which you want to save. 
        /// </param>
        public void Save(StreamWriter writer)
        {
            this.Save((TextWriter)writer);
        }

        /// <summary>
        /// Saves the mixed document to the specified TextWriter.
        /// </summary>
        /// <param name="writer">
        /// The TextWriter to which you want to save. 
        /// </param>
        public void Save(TextWriter writer)
        {
            writer.Flush();
        }

        /// <summary>
        /// The create fragment.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The mixed document fragment</returns>
        internal MixedCodeDocumentFragment CreateFragment(MixedCodeDocumentFragmentType type)
        {
            switch (type)
            {
                case MixedCodeDocumentFragmentType.Text:
                    return new MixedCodeDocumentTextFragment(this);

                case MixedCodeDocumentFragmentType.Code:
                    return new MixedCodeDocumentCodeFragment(this);

                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// The get out encoding.
        /// </summary>
        /// <returns>The encoding</returns>
        internal Encoding GetOutEncoding()
        {
            if (this.StreamEncoding != null)
            {
                return this.StreamEncoding;
            }

            return Encoding.UTF8;
        }

        /// <summary>
        /// The increment position.
        /// </summary>
        private void IncrementPosition()
        {
            this.index++;
            if (this.character == 10)
            {
                this.Lineposition = 1;
                this.Line++;
            }
            else
            {
                this.Lineposition++;
            }
        }

        /// <summary>
        /// The parse.
        /// </summary>
        private void Parse()
        {
            this.State = ParseState.Text;
            this.index = 0;
            this.currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Text);

            while (this.index < this.Text.Length)
            {
                this.character = this.Text[this.index];
                this.IncrementPosition();

                switch (this.State)
                {
                    case ParseState.Text:
                        if (this.index + TokenCodeStart.Length < this.Text.Length)
                        {
                            if (this.Text.Substring(this.index - 1, TokenCodeStart.Length) == TokenCodeStart)
                            {
                                this.State = ParseState.Code;
                                this.currentfragment.Length = this.index - 1 - this.currentfragment.StreamPosition;
                                this.currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Code);
                                this.SetPosition();
                                continue;
                            }
                        }

                        break;

                    case ParseState.Code:
                        if (this.index + TokenCodeEnd.Length < this.Text.Length)
                        {
                            if (this.Text.Substring(this.index - 1, TokenCodeEnd.Length) == TokenCodeEnd)
                            {
                                this.State = ParseState.Text;
                                this.currentfragment.Length = this.index + TokenCodeEnd.Length
                                                               - this.currentfragment.StreamPosition;
                                this.index += TokenCodeEnd.Length;
                                this.Lineposition += TokenCodeEnd.Length;
                                this.currentfragment = this.CreateFragment(MixedCodeDocumentFragmentType.Text);
                                this.SetPosition();
                                continue;
                            }
                        }

                        break;
                }
            }

            this.currentfragment.Length = this.index - this.currentfragment.StreamPosition;
        }

        /// <summary>
        /// The set position.
        /// </summary>
        private void SetPosition()
        {
            this.currentfragment.Line = this.Line;
            this.currentfragment.LinePosition = this.Lineposition;
            this.currentfragment.StreamPosition = this.index - 1;
            this.currentfragment.Length = 0;
        }
    }
}