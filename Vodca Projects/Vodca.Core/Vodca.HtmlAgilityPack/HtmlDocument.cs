//-----------------------------------------------------------------------------
// <copyright file="HtmlDocument.cs" company="genuine">
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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    /// <summary>
    /// Represents a complete HTML document.
    /// </summary>
    public partial class HtmlDocument
    {
        /// <summary>
        /// The html exception ref not child.
        /// </summary>
        internal const string HtmlExceptionRefNotChild = "Reference node must be a child of this node";

        /// <summary>
        /// The html exception use id attribute false.
        /// </summary>
        internal const string HtmlExceptionUseIdAttributeFalse = "You need to set UseIdAttribute property to true to enable this feature";

        /// <summary>
        /// The _parseerrors.
        /// </summary>
        private IList<HtmlParseError> parseerrors = new List<HtmlParseError>();

        /// <summary>
        /// The _state.
        /// </summary>
        private ParseState state;

        /// <summary>
        /// The _fullcomment.
        /// </summary>
        private bool fullcomment;

        /// <summary>
        /// The _index.
        /// </summary>
        private int cuurrentindex;

        /// <summary>
        /// The current char
        /// </summary>
        private int currentchar;

        /// <summary>
        /// The _currentattribute.
        /// </summary>
        private HtmlAttribute currentattribute;

        /// <summary>
        /// The _currentnode.
        /// </summary>
        private HtmlNode currentnode;

        /// <summary>
        /// The _documentnode.
        /// </summary>
        private HtmlNode documentnode;

        /// <summary>
        /// The _lastparentnode.
        /// </summary>
        private HtmlNode lastparentnode;

        /// <summary>
        /// The _line.
        /// </summary>
        private int line;

        /// <summary>
        /// The _lineposition.
        /// </summary>
        private int lineposition;

        /// <summary>
        /// The _maxlineposition.
        /// </summary>
        private int maxlineposition;

        /// <summary>
        /// The _oldstate.
        /// </summary>
        private ParseState oldstate;

        /// <summary>
        /// The _only detect encoding.
        /// </summary>
        private bool onlyDetectEncoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlDocument"/> class.
        /// Creates an instance of an HTML document.
        /// </summary>
        public HtmlDocument()
        {
            this.documentnode = this.CreateNode(HtmlNodeType.Document, 0);

            this.OptionDefaultStreamEncoding = Encoding.Default;
            this.OptionExtractErrorSourceTextMaxLength = 100;
            this.OptionCheckSyntax = true;
            this.OptionReadEncoding = true;
            this.OptionUseIdAttribute = true;

            this.LastNodes = new Dictionary<string, HtmlNode>();
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
            /// The which tag.
            /// </summary>
            WhichTag,

            /// <summary>
            /// The tag.
            /// </summary>
            Tag,

            /// <summary>
            /// The between attributes.
            /// </summary>
            BetweenAttributes,

            /// <summary>
            /// The empty tag.
            /// </summary>
            EmptyTag,

            /// <summary>
            /// The attribute name.
            /// </summary>
            AttributeName,

            /// <summary>
            /// The attribute before equals.
            /// </summary>
            AttributeBeforeEquals,

            /// <summary>
            /// The attribute after equals.
            /// </summary>
            AttributeAfterEquals,

            /// <summary>
            /// The attribute value.
            /// </summary>
            AttributeValue,

            /// <summary>
            /// The comment.
            /// </summary>
            Comment,

            /// <summary>
            /// The quoted attribute value.
            /// </summary>
            QuotedAttributeValue,

            /// <summary>
            /// The server side code.
            /// </summary>
            ServerSideCode,

            /// <summary>
            /// The pc data.
            /// </summary>
            PcData
        }

        /// <summary>
        /// Gets or sets a value indicating whether add debugging attributes.
        /// </summary>
        /// <value>
        /// <c>true</c> if add debugging attributes; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>Adds Debugging attributes to node. Default is false.</remarks>
        public bool OptionAddDebuggingAttributes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [option auto close on end].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [option auto close on end]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if closing for non closed nodes must be done at the end or directly in the document. Setting this to true can actually change how browsers render the page. Default is false.
        /// </remarks>
        public bool OptionAutoCloseOnEnd { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [option check syntax].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [option check syntax]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if non closed nodes will be checked at the end of parsing. Default is true.
        /// </remarks>
        public bool OptionCheckSyntax { get; set; }

        /// <summary>
        /// Gets or sets the option default stream encoding.
        /// </summary>
        /// <value>
        /// The option default stream encoding.
        /// </value>
        /// <remarks>
        /// Defines the default stream encoding to use. Default is System.Text.Encoding.Default.
        /// </remarks>
        public Encoding OptionDefaultStreamEncoding { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [option extract error source text].
        /// </summary>
        /// <value>
        /// <c>true</c> if [option extract error source text]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if source text must be extracted while parsing errors. If the document has a lot of errors, or cascading errors, parsing performance can be dramatically affected if set to true. Default is false.
        /// </remarks>
        public bool OptionExtractErrorSourceText { get; set; }

        /// <summary>
        /// Gets or sets the length of the option extract error source text max.
        /// </summary>
        /// <value>
        /// The length of the option extract error source text max.
        /// </value>
        /// <remarks>
        /// Defines the maximum length of source text or parse errors. Default is 100.
        /// </remarks>
        public int OptionExtractErrorSourceTextMaxLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether fix nested tags.
        /// </summary>
        /// <value>
        /// <c>true</c> if fix nested tags; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if LI, TR, TH, TD tags must be partially fixed when nesting errors are detected. Default is false.
        /// </remarks>
        public bool OptionFixNestedTags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether output as XML.
        /// </summary>
        /// <value>
        ///   <c>true</c> if output as XML; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if output must conform to XML, instead of HTML.
        /// </remarks>
        public bool OptionOutputAsXml { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [option output optimize attribute values].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [option output optimize attribute values]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if attribute value output must be optimized (not bound with double quotes if it is possible). Default is false.
        /// </remarks>
        public bool OptionOutputOptimizeAttributeValues { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether output original case.
        /// </summary>
        /// <value>
        /// <c>true</c> if output original case; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if name must be output with it's original case. Useful for asp.net tags and attributes
        /// </remarks>
        public bool OptionOutputOriginalCase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [option output upper case].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [option output upper case]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if name must be output in uppercase. Default is false.
        /// </remarks>
        public bool OptionOutputUpperCase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [option read encoding].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [option read encoding]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if declared encoding must be read from the document. Declared encoding is determined using the meta http-equiv="content-type" content="text/html;charset=XXXXX" html node. Default is true.
        /// </remarks>
        public bool OptionReadEncoding { get; set; }

        /// <summary>
        /// Gets or sets the name of the option stopper node.
        /// </summary>
        /// <value>
        /// The name of the option stopper node.
        /// </value>
        /// <remarks>
        /// Defines the name of a node that will throw the StopperNodeException when found as an end node. Default is null.
        /// </remarks>
        public string OptionStopperNodeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [option use id attribute].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [option use id attribute]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if the 'id' attribute must be specifically used. Default is true.
        /// </remarks>
        public bool OptionUseIdAttribute { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [option write empty nodes].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [option write empty nodes]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// Defines if empty nodes must be written as closed during output. Default is false.
        /// </remarks>
        public bool OptionWriteEmptyNodes { get; set; }

        /// <summary>
        ///   Gets the root node of the document.
        /// </summary>
        public HtmlNode DocumentNode
        {
            get
            {
                return this.documentnode;
            }
        }

        /// <summary>
        ///   Gets the document's output encoding.
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                return this.GetOutEncoding();
            }
        }

        /// <summary>
        ///   Gets a list of parse errors found in the document.
        /// </summary>
        public IEnumerable<HtmlParseError> ParseErrors
        {
            get
            {
                return this.parseerrors;
            }
        }

        /// <summary>
        /// Gets the remaining text. Will always be null if OptionStopperNodeName is null.
        /// </summary>
        public string Remainder { get; internal set; }

        /// <summary>
        ///     Gets the offset of Remainder in the original Html text. If OptionStopperNodeName is null, this will return the length of the original Html text.
        /// </summary>
        public int RemainderOffset { get; internal set; }

        /// <summary>
        ///   Gets the document's stream encoding.
        /// </summary>
        public Encoding StreamEncoding { get; internal set; }

        /// <summary>
        ///   Gets the document's declared encoding. Declared encoding is determined using the meta http-equiv="content-type" content="text/html;charset=XXXXX" html node.
        /// </summary>
        public Encoding DeclaredEncoding { get; internal set; }

        /// <summary>
        /// Gets the last nodes.
        /// </summary>
        public IDictionary<string, HtmlNode> LastNodes { get; private set; }

        /// <summary>
        /// Gets or sets the nodes id.
        /// </summary>
        /// <value>
        /// The nodes id.
        /// </value>
        internal IDictionary<string, HtmlNode> Nodesid { get; set; }

        /// <summary>
        /// Gets or sets the opened nodes.
        /// </summary>
        /// <value>
        /// The opened nodes.
        /// </value>
        internal IDictionary<int, HtmlNode> Openednodes { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        internal string Text { get; set; }

        /// <summary>
        /// Gets a valid XML name.
        /// </summary>
        /// <param name="name">
        /// Any text. 
        /// </param>
        /// <returns>
        /// A string that is a valid XML name. 
        /// </returns>
        public static string GetXmlName(string name)
        {
            string xmlname = string.Empty;
            bool nameisok = true;
            foreach (char t in name)
            {
                // names are lcase
                // note: we are very limited here, too much?
                if (((t >= 'a') && (t <= 'z')) || ((t >= '0') && (t <= '9'))
                    ||
                    /* (name[i]==':') || (name[i]=='_') || (name[i]=='-') || (name[i]=='.')) // these are bads in fact */
                    (t == '_') || (t == '-') || (t == '.'))
                {
                    xmlname += t;
                }
                else
                {
                    nameisok = false;
                    byte[] bytes = Encoding.UTF8.GetBytes(new[] { t });
                    for (int j = 0; j < bytes.Length; j++)
                    {
                        xmlname += bytes[j].ToString("x2");
                    }

                    xmlname += "_";
                }
            }

            if (nameisok)
            {
                return xmlname;
            }

            return "_" + xmlname;
        }

        /// <summary>
        /// Applies HTML encoding to a specified string.
        /// </summary>
        /// <param name="html">
        /// The input string to encode. May not be null. 
        /// </param>
        /// <returns>
        /// The encoded string. 
        /// </returns>
        public static string HtmlEncode(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            // replace & by &amp; but only once!
            var rx = new Regex("&(?!(amp;)|(lt;)|(gt;)|(quot;))", RegexOptions.IgnoreCase);
            return rx.Replace(html, "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
        }

        /// <summary>
        /// Determines if the specified character is considered as a whitespace character.
        /// </summary>
        /// <param name="c">
        /// The character to check. 
        /// </param>
        /// <returns>
        /// true if if the specified character is considered as a whitespace character. 
        /// </returns>
        public static bool IsWhiteSpace(int c)
        {
            if ((c == 10) || (c == 13) || (c == 32) || (c == 9))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates an HTML attribute with the specified name.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute. May not be null. 
        /// </param>
        /// <returns>
        /// The new HTML attribute. 
        /// </returns>
        public HtmlAttribute CreateAttribute(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            HtmlAttribute att = this.CreateAttribute();
            att.Name = name;
            return att;
        }

        /// <summary>
        /// Creates an HTML attribute with the specified name.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute. May not be null. 
        /// </param>
        /// <param name="value">
        /// The value of the attribute. 
        /// </param>
        /// <returns>
        /// The new HTML attribute. 
        /// </returns>
        public HtmlAttribute CreateAttribute(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            HtmlAttribute att = this.CreateAttribute(name);
            att.Value = value;
            return att;
        }

        /// <summary>
        /// Creates an HTML comment node.
        /// </summary>
        /// <returns>
        /// The new HTML comment node. 
        /// </returns>
        public HtmlCommentNode CreateComment()
        {
            return (HtmlCommentNode)this.CreateNode(HtmlNodeType.Comment);
        }

        /// <summary>
        /// Creates an HTML comment node with the specified comment text.
        /// </summary>
        /// <param name="comment">
        /// The comment text. May not be null. 
        /// </param>
        /// <returns>
        /// The new HTML comment node. 
        /// </returns>
        public HtmlCommentNode CreateComment(string comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            HtmlCommentNode c = this.CreateComment();
            c.Comment = comment;
            return c;
        }

        /// <summary>
        /// Creates an HTML element node with the specified name.
        /// </summary>
        /// <param name="name">
        /// The qualified name of the element. May not be null. 
        /// </param>
        /// <returns>
        /// The new HTML node. 
        /// </returns>
        public HtmlNode CreateElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            HtmlNode node = this.CreateNode(HtmlNodeType.Element);
            node.NodeName = name;
            return node;
        }

        /// <summary>
        /// Creates an HTML text node.
        /// </summary>
        /// <returns>
        /// The new HTML text node. 
        /// </returns>
        public HtmlTextNode CreateTextNode()
        {
            return (HtmlTextNode)this.CreateNode(HtmlNodeType.Text);
        }

        /// <summary>
        /// Creates an HTML text node with the specified text.
        /// </summary>
        /// <param name="text">
        /// The text of the node. May not be null. 
        /// </param>
        /// <returns>
        /// The new HTML text node. 
        /// </returns>
        public HtmlTextNode CreateTextNode(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            HtmlTextNode t = this.CreateTextNode();
            t.Text = text;
            return t;
        }

        /// <summary>
        /// Detects the encoding of an HTML stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. May not be null. 
        /// </param>
        /// <returns>
        /// The detected encoding. 
        /// </returns>
        public Encoding DetectEncoding(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            return this.DetectEncoding(new StreamReader(stream));
        }

        /// <summary>
        /// Detects the encoding of an HTML file.
        /// </summary>
        /// <param name="path">
        /// Path for the file containing the HTML document to detect. May not be null. 
        /// </param>
        /// <returns>
        /// The detected encoding. 
        /// </returns>
        public Encoding DetectEncoding(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            using (var sr = new StreamReader(path, this.OptionDefaultStreamEncoding))
            {
                Encoding encoding = this.DetectEncoding(sr);
                return encoding;
            }
        }

        /// <summary>
        /// Detects the encoding of an HTML text provided on a TextReader.
        /// </summary>
        /// <param name="reader">
        /// The TextReader used to feed the HTML. May not be null. 
        /// </param>
        /// <returns>
        /// The detected encoding. 
        /// </returns>
        public Encoding DetectEncoding(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            this.onlyDetectEncoding = true;
            this.Openednodes = this.OptionCheckSyntax ? new Dictionary<int, HtmlNode>() : null;

            this.Nodesid = this.OptionUseIdAttribute ? new Dictionary<string, HtmlNode>() : null;

            var sr = reader as StreamReader;
            this.StreamEncoding = sr != null ? sr.CurrentEncoding : null;

            this.DeclaredEncoding = null;

            this.Text = reader.ReadToEnd();
            this.documentnode = this.CreateNode(HtmlNodeType.Document, 0);

            // this is almost a hack, but it allows us not to muck with the original parsing code
            try
            {
                this.Parse();
            }
            catch (EncodingFoundException ex)
            {
                return ex.Encoding;
            }

            return null;
        }

        /// <summary>
        /// Detects the encoding of an HTML document from a file first, and then loads the file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. May not be null. 
        /// </param>
        /// <param name="detectEncoding">
        /// true to detect encoding, false otherwise. 
        /// </param>
        public void DetectEncodingAndLoad(string path, bool detectEncoding = true)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            Encoding enc = detectEncoding ? this.DetectEncoding(path) : null;

            if (enc == null)
            {
                this.Load(path);
            }
            else
            {
                this.Load(path, enc);
            }
        }

        /// <summary>
        /// Detects the encoding of an HTML text.
        /// </summary>
        /// <param name="html">
        /// The input html text. May not be null. 
        /// </param>
        /// <returns>
        /// The detected encoding. 
        /// </returns>
        public Encoding DetectEncodingHtml(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            var sr = new StringReader(html);
            Encoding encoding = this.DetectEncoding(sr);
            sr.Close();
            return encoding;
        }

        /// <summary>
        /// Gets the HTML node with the specified 'id' attribute value.
        /// </summary>
        /// <param name="id">
        /// The attribute id to match. May not be null. 
        /// </param>
        /// <returns>
        /// The HTML node with the matching id or null if not found. 
        /// </returns>
        public HtmlNode GetElementbyId(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (this.Nodesid == null)
            {
                throw new Exception(HtmlExceptionUseIdAttributeFalse);
            }

            return this.Nodesid.ContainsKey(id.ToLower()) ? this.Nodesid[id.ToLower()] : null;
        }

        /// <summary>
        /// Loads an HTML document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        public void Load(Stream stream)
        {
            this.Load(new StreamReader(stream, this.OptionDefaultStreamEncoding));
        }

        /// <summary>
        /// Loads an HTML document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the stream. 
        /// </param>
        public void Load(Stream stream, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(stream, detectEncodingFromByteOrderMarks));
        }

        /// <summary>
        /// Loads an HTML document from a stream.
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
        /// Loads an HTML document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the stream. 
        /// </param>
        public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
        {
            this.Load(new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks));
        }

        /// <summary>
        /// Loads an HTML document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the stream. 
        /// </param>
        /// <param name="buffersize">
        /// The minimum buffer size. 
        /// </param>
        public void Load(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
        {
            this.Load(new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, buffersize));
        }

        /// <summary>
        /// Loads an HTML document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. May not be null. 
        /// </param>
        public void Load(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            var sr = new StreamReader(path, this.OptionDefaultStreamEncoding);
            this.Load(sr);
            sr.Close();
        }

        /// <summary>
        /// Loads an HTML document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. May not be null. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        public void Load(string path, bool detectEncodingFromByteOrderMarks)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            var sr = new StreamReader(path, detectEncodingFromByteOrderMarks);
            this.Load(sr);
            sr.Close();
        }

        /// <summary>
        /// Loads an HTML document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. May not be null. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. May not be null. 
        /// </param>
        public void Load(string path, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            var sr = new StreamReader(path, encoding);
            this.Load(sr);
            sr.Close();
        }

        /// <summary>
        /// Loads an HTML document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. May not be null. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. May not be null. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            var sr = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks);
            this.Load(sr);
            sr.Close();
        }

        /// <summary>
        /// Loads an HTML document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. May not be null. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. May not be null. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        /// <param name="buffersize">
        /// The minimum buffer size. 
        /// </param>
        public void Load(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            var sr = new StreamReader(path, encoding, detectEncodingFromByteOrderMarks, buffersize);
            this.Load(sr);
            sr.Close();
        }

        /// <summary>
        /// Loads the HTML document from the specified TextReader.
        /// </summary>
        /// <param name="reader">
        /// The TextReader used to feed the HTML data into the document. May not be null. 
        /// </param>
        public void Load(TextReader reader)
        {
            // all Load methods pass down to this one
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            this.onlyDetectEncoding = false;

            this.Openednodes = this.OptionCheckSyntax ? new Dictionary<int, HtmlNode>() : null;

            this.Nodesid = this.OptionUseIdAttribute ? new Dictionary<string, HtmlNode>() : null;

            var sr = reader as StreamReader;
            if (sr != null)
            {
                try
                {
                    // trigger bom read if needed
                    // ReSharper disable UnusedVariable
                    int p = sr.Peek();
                    // ReSharper restore UnusedVariable
                }

                    // ReSharper disable EmptyGeneralCatchClause
                catch (Exception)
                {
                    // ReSharper restore EmptyGeneralCatchClause
                    // void on purpose
                }

                this.StreamEncoding = sr.CurrentEncoding;
            }
            else
            {
                this.StreamEncoding = null;
            }

            this.DeclaredEncoding = null;

            this.Text = reader.ReadToEnd();
            this.documentnode = this.CreateNode(HtmlNodeType.Document, 0);
            this.Parse();

            if (!this.OptionCheckSyntax || this.Openednodes == null)
            {
                return;
            }

            foreach (HtmlNode node in this.Openednodes.Values)
            {
                if (!node.StartTag)
                {
                    // already reported
                    continue;
                }

                string html;
                if (this.OptionExtractErrorSourceText)
                {
                    html = node.OuterHtml;
                    if (html.Length > this.OptionExtractErrorSourceTextMaxLength)
                    {
                        html = html.Substring(0, this.OptionExtractErrorSourceTextMaxLength);
                    }
                }
                else
                {
                    html = string.Empty;
                }

                this.AddError(
                    HtmlParseErrorCode.TagNotClosed,
                    node.Line,
                    node.LinePosition,
                    node.StreamPosition,
                    html,
                    "End tag </" + node.NodeName + "> was not found");
            }

            // we don't need this anymore
            this.Openednodes.Clear();
        }

        /// <summary>
        /// Loads the HTML document from the specified string.
        /// </summary>
        /// <param name="html">
        /// String containing the HTML document to load. May not be null. 
        /// </param>
        public void LoadHtml(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            using (var sr = new StringReader(html))
            {
                this.Load(sr);
            }
        }

        /// <summary>
        /// Saves the HTML document to the specified stream.
        /// </summary>
        /// <param name="outStream">
        /// The stream to which you want to save. 
        /// </param>
        public void Save(Stream outStream)
        {
            using (var sw = new StreamWriter(outStream, this.GetOutEncoding()))
            {
                this.Save(sw);
            }
        }

        /// <summary>
        /// Saves the HTML document to the specified stream.
        /// </summary>
        /// <param name="outStream">
        /// The stream to which you want to save. May not be null. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. May not be null. 
        /// </param>
        public void Save(Stream outStream, Encoding encoding)
        {
            if (outStream == null)
            {
                throw new ArgumentNullException("outStream");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            using (var sw = new StreamWriter(outStream, encoding))
            {
                this.Save(sw);
            }
        }

        /// <summary>
        /// Saves the mixed document to the specified file.
        /// </summary>
        /// <param name="filename">
        /// The location of the file where you want to save the document. 
        /// </param>
        public void Save(string filename)
        {
            using (var sw = new StreamWriter(filename, false, this.GetOutEncoding()))
            {
                this.Save(sw);
            }
        }

        /// <summary>
        /// Saves the mixed document to the specified file.
        /// </summary>
        /// <param name="filename">
        /// The location of the file where you want to save the document. May not be null. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. May not be null. 
        /// </param>
        public void Save(string filename, Encoding encoding)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            using (var sw = new StreamWriter(filename, false, encoding))
            {
                this.Save(sw);
            }
        }

        /// <summary>
        /// Saves the HTML document to the specified StreamWriter.
        /// </summary>
        /// <param name="writer">
        /// The StreamWriter to which you want to save. 
        /// </param>
        public void Save(StreamWriter writer)
        {
            this.Save((TextWriter)writer);
        }

        /// <summary>
        /// Saves the HTML document to the specified TextWriter.
        /// </summary>
        /// <param name="writer">
        /// The TextWriter to which you want to save. May not be null. 
        /// </param>
        public void Save(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            this.DocumentNode.WriteTo(writer);
        }

        /// <summary>
        /// Saves the HTML document to the specified XmlWriter.
        /// </summary>
        /// <param name="writer">
        /// The XmlWriter to which you want to save. 
        /// </param>
        public void Save(XmlWriter writer)
        {
            this.DocumentNode.WriteTo(writer);
            writer.Flush();
        }

        /// <summary>
        /// The create attribute.
        /// </summary>
        /// <returns>The html attribute</returns>
        internal HtmlAttribute CreateAttribute()
        {
            return new HtmlAttribute(this);
        }

        /// <summary>
        /// The create node.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="index">The index.</param>
        /// <returns>The new html node</returns>
        internal HtmlNode CreateNode(HtmlNodeType type, int index = -1)
        {
            switch (type)
            {
                case HtmlNodeType.Comment:
                    return new HtmlCommentNode(this, index);

                case HtmlNodeType.Text:
                    return new HtmlTextNode(this, index);

                default:
                    return new HtmlNode(type, this, index);
            }
        }

        /// <summary>
        /// The get out encoding.
        /// </summary>
        /// <returns>The encoding format</returns>
        internal Encoding GetOutEncoding()
        {
            // when unspecified, use the stream encoding first
            return this.DeclaredEncoding ?? (this.StreamEncoding ?? this.OptionDefaultStreamEncoding);
        }

        /// <summary>
        /// The get xml declaration.
        /// </summary>
        /// <returns>The Xml declaration</returns>
        internal HtmlNode GetXmlDeclaration()
        {
            if (!this.documentnode.HasChildNodes)
            {
                return null;
            }

            return this.documentnode.ChildNodes.FirstOrDefault(node => string.Equals(node.NodeName, "?xml"));
        }

        /// <summary>
        /// The set id for node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        internal void SetIdForNode(HtmlNode node, string id)
        {
            if (!this.OptionUseIdAttribute)
            {
                return;
            }

            if ((this.Nodesid == null) || (id == null))
            {
                return;
            }

            if (node == null)
            {
                this.Nodesid.Remove(id.ToLower());
            }
            else
            {
                this.Nodesid[id.ToLower()] = node;
            }
        }

        /// <summary>
        /// The update last parent node.
        /// </summary>
        internal void UpdateLastParentNode()
        {
            do
            {
                if (this.lastparentnode.Closed)
                {
                    this.lastparentnode = this.lastparentnode.ParentNode;
                }
            }
            while ((this.lastparentnode != null) && this.lastparentnode.Closed);

            if (this.lastparentnode == null)
            {
                this.lastparentnode = this.documentnode;
            }
        }

        /// <summary>
        /// The add error.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="errorline">The error line.</param>
        /// <param name="linePosition">The line position.</param>
        /// <param name="streamPosition">The stream position.</param>
        /// <param name="sourceText">The source text.</param>
        /// <param name="reason">The reason.</param>
        private void AddError(HtmlParseErrorCode code, int errorline, int linePosition, int streamPosition, string sourceText, string reason)
        {
            var err = new HtmlParseError(code, errorline, linePosition, streamPosition, sourceText, reason);
            this.parseerrors.Add(err);
        }

        /// <summary>
        /// The close current node.
        /// </summary>
        private void CloseCurrentNode()
        {
            if (this.currentnode.Closed)
            {
                // text or document are by def closed
                return;
            }

            bool error = false;
            HtmlNode prev = this.LastNodes.GetDictionaryValueOrNull(this.currentnode.NodeName);

            // find last node of this kind
            if (prev == null)
            {
                if (HtmlNode.IsClosedElement(this.currentnode.NodeName))
                {
                    // </br> will be seen as <br>
                    this.currentnode.CloseNode(this.currentnode);

                    // add to parent node
                    if (this.lastparentnode != null)
                    {
                        HtmlNode foundNode = null;
                        var futureChild = new Stack<HtmlNode>();
                        for (HtmlNode node = this.lastparentnode.LastChild; node != null; node = node.PreviousSibling)
                        {
                            if ((node.NodeName == this.currentnode.NodeName) && (!node.HasChildNodes))
                            {
                                foundNode = node;
                                break;
                            }

                            futureChild.Push(node);
                        }

                        if (foundNode != null)
                        {
                            while (futureChild.Count != 0)
                            {
                                HtmlNode node = futureChild.Pop();
                                this.lastparentnode.RemoveChild(node);
                                foundNode.AppendChild(node);
                            }
                        }
                        else
                        {
                            this.lastparentnode.AppendChild(this.currentnode);
                        }
                    }
                }
                else
                {
                    // node has no parent
                    // node is not a closed node
                    if (HtmlNode.CanOverlapElement(this.currentnode.NodeName))
                    {
                        // this is a hack: add it as a text node
                        HtmlNode closenode = this.CreateNode(HtmlNodeType.Text, this.currentnode.OuterStartIndex);
                        closenode.OuterLength = this.currentnode.OuterLength;
                        ((HtmlTextNode)closenode).Text = ((HtmlTextNode)closenode).Text.ToLower();
                        if (this.lastparentnode != null)
                        {
                            this.lastparentnode.AppendChild(closenode);
                        }
                    }
                    else
                    {
                        if (HtmlNode.IsEmptyElement(this.currentnode.NodeName))
                        {
                            this.AddError(
                                HtmlParseErrorCode.EndTagNotRequired,
                                this.currentnode.Line,
                                this.currentnode.LinePosition,
                                this.currentnode.StreamPosition,
                                this.currentnode.OuterHtml,
                                "End tag </" + this.currentnode.NodeName + "> is not required");
                        }
                        else
                        {
                            // node cannot overlap, node is not empty
                            this.AddError(
                                HtmlParseErrorCode.TagNotOpened,
                                this.currentnode.Line,
                                this.currentnode.LinePosition,
                                this.currentnode.StreamPosition,
                                this.currentnode.OuterHtml,
                                "Start tag <" + this.currentnode.NodeName + "> was not found");
                            error = true;
                        }
                    }
                }
            }
            else
            {
                if (this.OptionFixNestedTags)
                {
                    if (this.FindResetterNodes(prev, this.GetResetters(this.currentnode.NodeName)))
                    {
                        this.AddError(
                            HtmlParseErrorCode.EndTagInvalidHere,
                            this.currentnode.Line,
                            this.currentnode.LinePosition,
                            this.currentnode.StreamPosition,
                            this.currentnode.OuterHtml,
                            "End tag </" + this.currentnode.NodeName + "> invalid here");
                        error = true;
                    }
                }

                if (!error)
                {
                    this.LastNodes[this.currentnode.NodeName] = prev.PreviousWithSameName;
                    prev.CloseNode(this.currentnode);
                }
            }

            // we close this node, get grandparent
            if (!error)
            {
                if ((this.lastparentnode != null)
                    && ((!HtmlNode.IsClosedElement(this.currentnode.NodeName)) || this.currentnode.StartTag))
                {
                    this.UpdateLastParentNode();
                }
            }
        }

        /// <summary>
        /// Gets the current node name.
        /// </summary>
        /// <returns>
        /// The current node name.
        /// </returns>
        private string CurrentNodeName()
        {
            return this.Text.Substring(this.currentnode.NameStartIndex, this.currentnode.NameLength);
        }

        /// <summary>
        /// The decrement position.
        /// </summary>
        private void DecrementPosition()
        {
            this.cuurrentindex--;
            if (this.lineposition == 1)
            {
                this.lineposition = this.maxlineposition;
                this.line--;
            }
            else
            {
                this.lineposition--;
            }
        }

        /// <summary>
        /// The find resetter node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The html node</returns>
        private HtmlNode FindResetterNode(HtmlNode node)
        {
            HtmlNode resetter = this.LastNodes.GetDictionaryValueOrNull(this.currentnode.NodeName);
            if (resetter == null)
            {
                return null;
            }

            if (resetter.Closed)
            {
                return null;
            }

            return resetter.StreamPosition < node.StreamPosition ? null : resetter;
        }

        /// <summary>
        /// The find resetter nodes.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="names">The names.</param>
        /// <returns>
        /// The resetter nodes.
        /// </returns>
        private bool FindResetterNodes(HtmlNode node, IEnumerable<string> names)
        {
            if (names == null)
            {
                return false;
            }

            return names.Any(t => this.FindResetterNode(node) != null);
        }

        /// <summary>
        /// The fix nested tag.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="resetters">The resetters.</param>
        private void FixNestedTag(string name, IEnumerable<string> resetters)
        {
            if (resetters == null)
            {
                return;
            }

            HtmlNode prev = this.LastNodes.GetDictionaryValueOrNull(this.currentnode.NodeName);

            // if we find a previous unclosed same name node, without a resetter node between, we must close it
            if (prev == null || this.LastNodes[name].Closed)
            {
                return;
            }

            // try to find a resetter node, if found, we do nothing
            if (this.FindResetterNodes(prev, resetters))
            {
                return;
            }

            // ok we need to close the prev now
            // create a fake closer node
            var close = new HtmlNode(prev.NodeType, this, -1);
            close.EndNode = close;
            prev.CloseNode(close);
        }

        /// <summary>
        /// The fix nested tags.
        /// </summary>
        private void FixNestedTags()
        {
            // we are only interested by start tags, not closing tags
            if (!this.currentnode.StartTag)
            {
                return;
            }

            string name = this.CurrentNodeName();
            this.FixNestedTag(name, this.GetResetters(name));
        }

        /// <summary>
        /// The get resetters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The resetters</returns>
        private IEnumerable<string> GetResetters(string name)
        {
            switch (name)
            {
                case "li":
                    return new[] { "ul" };

                case "tr":
                    return new[] { "table" };

                case "th":
                case "td":
                    return new[] { "tr", "table" };

                default:
                    return null;
            }
        }

        /// <summary>
        /// The increment position.
        /// </summary>
        private void IncrementPosition()
        {
            this.cuurrentindex++;
            this.maxlineposition = this.lineposition;
            if (this.currentchar == 10)
            {
                this.lineposition = 1;
                this.line++;
            }
            else
            {
                this.lineposition++;
            }
        }

        /// <summary>
        /// The new check.
        /// </summary>
        /// <returns>
        /// The true if new check
        /// </returns>
        private bool NewCheck()
        {
            if (this.currentchar != '<')
            {
                return false;
            }

            if (this.cuurrentindex < this.Text.Length)
            {
                if (this.Text[this.cuurrentindex] == '%')
                {
                    switch (this.state)
                    {
                        case ParseState.AttributeAfterEquals:
                            this.PushAttributeValueStart(this.cuurrentindex - 1);
                            break;

                        case ParseState.BetweenAttributes:
                            this.PushAttributeNameStart(this.cuurrentindex - 1);
                            break;

                        case ParseState.WhichTag:
                            this.PushNodeNameStart(true, this.cuurrentindex - 1);
                            this.state = ParseState.Tag;
                            break;
                    }

                    this.oldstate = this.state;
                    this.state = ParseState.ServerSideCode;
                    return true;
                }
            }

            if (!this.PushNodeEnd(this.cuurrentindex - 1, true))
            {
                // stop parsing
                this.cuurrentindex = this.Text.Length;
                return true;
            }

            this.state = ParseState.WhichTag;
            if ((this.cuurrentindex - 1) <= (this.Text.Length - 2))
            {
                if (this.Text[this.cuurrentindex] == '!')
                {
                    this.PushNodeStart(HtmlNodeType.Comment, this.cuurrentindex - 1);
                    this.PushNodeNameStart(true, this.cuurrentindex);
                    this.PushNodeNameEnd(this.cuurrentindex + 1);
                    this.state = ParseState.Comment;
                    if (this.cuurrentindex < (this.Text.Length - 2))
                    {
                        if ((this.Text[this.cuurrentindex + 1] == '-') && (this.Text[this.cuurrentindex + 2] == '-'))
                        {
                            this.fullcomment = true;
                        }
                        else
                        {
                            this.fullcomment = false;
                        }
                    }

                    return true;
                }
            }

            this.PushNodeStart(HtmlNodeType.Element, this.cuurrentindex - 1);
            return true;
        }

        /// <summary>
        /// The parse.
        /// </summary>
        private void Parse()
        {
            int lastquote = 0;

            this.LastNodes = new Dictionary<string, HtmlNode>();
            this.currentchar = 0;
            this.fullcomment = false;
            this.parseerrors = new List<HtmlParseError>();
            this.line = 1;
            this.lineposition = 1;
            this.maxlineposition = 1;

            this.state = ParseState.Text;
            this.oldstate = this.state;
            this.documentnode.InnerLength = this.Text.Length;
            this.documentnode.OuterLength = this.Text.Length;
            this.RemainderOffset = this.Text.Length;

            this.lastparentnode = this.documentnode;
            this.currentnode = this.CreateNode(HtmlNodeType.Text, 0);
            this.currentattribute = null;

            this.cuurrentindex = 0;
            this.PushNodeStart(HtmlNodeType.Text, 0);
            while (this.cuurrentindex < this.Text.Length)
            {
                this.currentchar = this.Text[this.cuurrentindex];
                this.IncrementPosition();

                switch (this.state)
                {
                    case ParseState.Text:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        break;

                    case ParseState.WhichTag:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        if (this.currentchar == '/')
                        {
                            this.PushNodeNameStart(false, this.cuurrentindex);
                        }
                        else
                        {
                            this.PushNodeNameStart(true, this.cuurrentindex - 1);
                            this.DecrementPosition();
                        }

                        this.state = ParseState.Tag;
                        break;

                    case ParseState.Tag:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        if (IsWhiteSpace(this.currentchar))
                        {
                            this.PushNodeNameEnd(this.cuurrentindex - 1);
                            if (this.state != ParseState.Tag)
                            {
                                continue;
                            }

                            this.state = ParseState.BetweenAttributes;
                            continue;
                        }

                        if (this.currentchar == '/')
                        {
                            this.PushNodeNameEnd(this.cuurrentindex - 1);
                            if (this.state != ParseState.Tag)
                            {
                                continue;
                            }

                            this.state = ParseState.EmptyTag;
                            continue;
                        }

                        if (this.currentchar == '>')
                        {
                            this.PushNodeNameEnd(this.cuurrentindex - 1);
                            if (this.state != ParseState.Tag)
                            {
                                continue;
                            }

                            if (!this.PushNodeEnd(this.cuurrentindex, false))
                            {
                                // stop parsing
                                this.cuurrentindex = this.Text.Length;
                                break;
                            }

                            if (this.state != ParseState.Tag)
                            {
                                continue;
                            }

                            this.state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this.cuurrentindex);
                        }

                        break;

                    case ParseState.BetweenAttributes:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        if (IsWhiteSpace(this.currentchar))
                        {
                            continue;
                        }

                        if ((this.currentchar == '/') || (this.currentchar == '?'))
                        {
                            this.state = ParseState.EmptyTag;
                            continue;
                        }

                        if (this.currentchar == '>')
                        {
                            if (!this.PushNodeEnd(this.cuurrentindex, false))
                            {
                                // stop parsing
                                this.cuurrentindex = this.Text.Length;
                                break;
                            }

                            if (this.state != ParseState.BetweenAttributes)
                            {
                                continue;
                            }

                            this.state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this.cuurrentindex);
                            continue;
                        }

                        this.PushAttributeNameStart(this.cuurrentindex - 1);
                        this.state = ParseState.AttributeName;
                        break;

                    case ParseState.EmptyTag:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        if (this.currentchar == '>')
                        {
                            if (!this.PushNodeEnd(this.cuurrentindex, true))
                            {
                                // stop parsing
                                this.cuurrentindex = this.Text.Length;
                                break;
                            }

                            if (this.state != ParseState.EmptyTag)
                            {
                                continue;
                            }

                            this.state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this.cuurrentindex);
                            continue;
                        }

                        this.state = ParseState.BetweenAttributes;
                        break;

                    case ParseState.AttributeName:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        if (IsWhiteSpace(this.currentchar))
                        {
                            this.PushAttributeNameEnd(this.cuurrentindex - 1);
                            this.state = ParseState.AttributeBeforeEquals;
                            continue;
                        }

                        if (this.currentchar == '=')
                        {
                            this.PushAttributeNameEnd(this.cuurrentindex - 1);
                            this.state = ParseState.AttributeAfterEquals;
                            continue;
                        }

                        if (this.currentchar == '>')
                        {
                            this.PushAttributeNameEnd(this.cuurrentindex - 1);
                            if (!this.PushNodeEnd(this.cuurrentindex, false))
                            {
                                // stop parsing
                                this.cuurrentindex = this.Text.Length;
                                break;
                            }

                            if (this.state != ParseState.AttributeName)
                            {
                                continue;
                            }

                            this.state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this.cuurrentindex);
                            continue;
                        }

                        break;

                    case ParseState.AttributeBeforeEquals:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        if (IsWhiteSpace(this.currentchar))
                        {
                            continue;
                        }

                        if (this.currentchar == '>')
                        {
                            if (!this.PushNodeEnd(this.cuurrentindex, false))
                            {
                                // stop parsing
                                this.cuurrentindex = this.Text.Length;
                                break;
                            }

                            if (this.state != ParseState.AttributeBeforeEquals)
                            {
                                continue;
                            }

                            this.state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this.cuurrentindex);
                            continue;
                        }

                        if (this.currentchar == '=')
                        {
                            this.state = ParseState.AttributeAfterEquals;
                            continue;
                        }

                        // no equals, no whitespace, it's a new attrribute starting
                        this.state = ParseState.BetweenAttributes;
                        this.DecrementPosition();
                        break;

                    case ParseState.AttributeAfterEquals:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        if (IsWhiteSpace(this.currentchar))
                        {
                            continue;
                        }

                        if ((this.currentchar == '\'') || (this.currentchar == '"'))
                        {
                            this.state = ParseState.QuotedAttributeValue;
                            this.PushAttributeValueStart(this.cuurrentindex, this.currentchar);
                            lastquote = this.currentchar;
                            continue;
                        }

                        if (this.currentchar == '>')
                        {
                            if (!this.PushNodeEnd(this.cuurrentindex, false))
                            {
                                // stop parsing
                                this.cuurrentindex = this.Text.Length;
                                break;
                            }

                            if (this.state != ParseState.AttributeAfterEquals)
                            {
                                continue;
                            }

                            this.state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this.cuurrentindex);
                            continue;
                        }

                        this.PushAttributeValueStart(this.cuurrentindex - 1);
                        this.state = ParseState.AttributeValue;
                        break;

                    case ParseState.AttributeValue:
                        if (this.NewCheck())
                        {
                            continue;
                        }

                        if (IsWhiteSpace(this.currentchar))
                        {
                            this.PushAttributeValueEnd(this.cuurrentindex - 1);
                            this.state = ParseState.BetweenAttributes;
                            continue;
                        }

                        if (this.currentchar == '>')
                        {
                            this.PushAttributeValueEnd(this.cuurrentindex - 1);
                            if (!this.PushNodeEnd(this.cuurrentindex, false))
                            {
                                // stop parsing
                                this.cuurrentindex = this.Text.Length;
                                break;
                            }

                            if (this.state != ParseState.AttributeValue)
                            {
                                continue;
                            }

                            this.state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this.cuurrentindex);
                            continue;
                        }

                        break;

                    case ParseState.QuotedAttributeValue:
                        if (this.currentchar == lastquote)
                        {
                            this.PushAttributeValueEnd(this.cuurrentindex - 1);
                            this.state = ParseState.BetweenAttributes;
                            continue;
                        }

                        if (this.currentchar == '<')
                        {
                            if (this.cuurrentindex < this.Text.Length)
                            {
                                if (this.Text[this.cuurrentindex] == '%')
                                {
                                    this.oldstate = this.state;
                                    this.state = ParseState.ServerSideCode;
                                    continue;
                                }
                            }
                        }

                        break;

                    case ParseState.Comment:
                        if (this.currentchar == '>')
                        {
                            if (this.fullcomment)
                            {
                                if ((this.Text[this.cuurrentindex - 2] != '-') || (this.Text[this.cuurrentindex - 3] != '-'))
                                {
                                    continue;
                                }
                            }

                            if (!this.PushNodeEnd(this.cuurrentindex, false))
                            {
                                // stop parsing
                                this.cuurrentindex = this.Text.Length;
                                break;
                            }

                            this.state = ParseState.Text;
                            this.PushNodeStart(HtmlNodeType.Text, this.cuurrentindex);
                            continue;
                        }

                        break;

                    case ParseState.ServerSideCode:
                        if (this.currentchar == '%')
                        {
                            if (this.cuurrentindex < this.Text.Length)
                            {
                                if (this.Text[this.cuurrentindex] == '>')
                                {
                                    switch (this.oldstate)
                                    {
                                        case ParseState.AttributeAfterEquals:
                                            this.state = ParseState.AttributeValue;
                                            break;

                                        case ParseState.BetweenAttributes:
                                            this.PushAttributeNameEnd(this.cuurrentindex + 1);
                                            this.state = ParseState.BetweenAttributes;
                                            break;

                                        default:
                                            this.state = this.oldstate;
                                            break;
                                    }

                                    this.IncrementPosition();
                                }
                            }
                        }

                        break;

                    case ParseState.PcData:

                        // look for </tag + 1 char

                        // check buffer end
                        if ((this.currentnode.NameLength + 3) <= (this.Text.Length - (this.cuurrentindex - 1)))
                        {
                            if (string.Compare(
                                this.Text.Substring(this.cuurrentindex - 1, this.currentnode.NameLength + 2),
                                "</" + this.currentnode.NodeName,
                                StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                int c = this.Text[this.cuurrentindex - 1 + 2 + this.currentnode.NodeName.Length];
                                if ((c == '>') || IsWhiteSpace(c))
                                {
                                    // add the script as a text node
                                    HtmlNode script = this.CreateNode(
                                        HtmlNodeType.Text,
                                        this.currentnode.OuterStartIndex + this.currentnode.OuterLength);
                                    script.OuterLength = this.cuurrentindex - 1 - script.OuterStartIndex;
                                    this.currentnode.AppendChild(script);

                                    this.PushNodeStart(HtmlNodeType.Element, this.cuurrentindex - 1);
                                    this.PushNodeNameStart(false, this.cuurrentindex - 1 + 2);
                                    this.state = ParseState.Tag;
                                    this.IncrementPosition();
                                }
                            }
                        }

                        break;
                }
            }

            // finish the current work
            if (this.currentnode.NameStartIndex > 0)
            {
                this.PushNodeNameEnd(this.cuurrentindex);
            }

            this.PushNodeEnd(this.cuurrentindex, false);

            // we don't need this anymore
            this.LastNodes.Clear();
        }

        /// <summary>
        /// The push attribute name end.
        /// </summary>
        /// <param name="index">The index.</param>
        private void PushAttributeNameEnd(int index)
        {
            this.currentattribute.NameLength = index - this.currentattribute.NameStartIndex;
            this.currentnode.Attributes.Append(this.currentattribute);
        }

        /// <summary>
        /// The push attribute name start.
        /// </summary>
        /// <param name="index">The index.</param>
        private void PushAttributeNameStart(int index)
        {
            this.currentattribute = this.CreateAttribute();
            this.currentattribute.NameStartIndex = index;
            this.currentattribute.Line = this.line;
            this.currentattribute.LinePosition = this.lineposition;
            this.currentattribute.StreamPosition = index;
        }

        /// <summary>
        /// The push attribute value end.
        /// </summary>
        /// <param name="index">The index.</param>
        private void PushAttributeValueEnd(int index)
        {
            this.currentattribute.ValueLength = index - this.currentattribute.ValueStartIndex;
        }

        /// <summary>
        /// The push attribute value start.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="quote">The quote.</param>
        private void PushAttributeValueStart(int index, int quote = 0)
        {
            this.currentattribute.ValueStartIndex = index;
            if (quote == '\'')
            {
                this.currentattribute.QuoteType = AttributeValueQuote.SingleQuote;
            }
        }

        /// <summary>
        /// The push node end.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="close">The close.</param>
        /// <returns>
        /// The node.
        /// </returns>
        private bool PushNodeEnd(int index, bool close)
        {
            this.currentnode.OuterLength = index - this.currentnode.OuterStartIndex;

            if ((this.currentnode.NodeType == HtmlNodeType.Text)
                || (this.currentnode.NodeType == HtmlNodeType.Comment))
            {
                // forget about void nodes
                if (this.currentnode.OuterLength > 0)
                {
                    this.currentnode.InnerLength = this.currentnode.OuterLength;
                    this.currentnode.InnerStartIndex = this.currentnode.OuterStartIndex;
                    if (this.lastparentnode != null)
                    {
                        this.lastparentnode.AppendChild(this.currentnode);
                    }
                }
            }
            else
            {
                if (this.currentnode.StartTag && (this.lastparentnode != this.currentnode))
                {
                    // add to parent node
                    if (this.lastparentnode != null)
                    {
                        this.lastparentnode.AppendChild(this.currentnode);
                    }

                    this.ReadDocumentEncoding(this.currentnode);

                    // remember last node of this kind
                    HtmlNode prev = this.LastNodes.GetDictionaryValueOrNull(this.currentnode.NodeName);

                    this.currentnode.PreviousWithSameName = prev;
                    this.LastNodes[this.currentnode.NodeName] = this.currentnode;

                    // change parent?
                    if ((this.currentnode.NodeType == HtmlNodeType.Document)
                        || (this.currentnode.NodeType == HtmlNodeType.Element))
                    {
                        this.lastparentnode = this.currentnode;
                    }

                    if (HtmlNode.IsCDataElement(this.CurrentNodeName()))
                    {
                        this.state = ParseState.PcData;
                        return true;
                    }

                    if (HtmlNode.IsClosedElement(this.currentnode.NodeName) || HtmlNode.IsEmptyElement(this.currentnode.NodeName))
                    {
                        close = true;
                    }
                }
            }

            if (close || (!this.currentnode.StartTag))
            {
                if ((this.OptionStopperNodeName != null) && (this.Remainder == null)
                    &&
                    (string.Compare(this.currentnode.NodeName, this.OptionStopperNodeName, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    this.RemainderOffset = index;
                    this.Remainder = this.Text.Substring(this.RemainderOffset);
                    this.CloseCurrentNode();
                    return false; // stop parsing
                }

                this.CloseCurrentNode();
            }

            return true;
        }

        /// <summary>
        /// The push node name end.
        /// </summary>
        /// <param name="index">The index.</param>
        private void PushNodeNameEnd(int index)
        {
            this.currentnode.NameLength = index - this.currentnode.NameStartIndex;
            if (this.OptionFixNestedTags)
            {
                this.FixNestedTags();
            }
        }

        /// <summary>
        /// The push node name start.
        /// </summary>
        /// <param name="starttag">The starttag.</param>
        /// <param name="index">The index.</param>
        private void PushNodeNameStart(bool starttag, int index)
        {
            this.currentnode.StartTag = starttag;
            this.currentnode.NameStartIndex = index;
        }

        /// <summary>
        /// The push node start.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="index">The index.</param>
        private void PushNodeStart(HtmlNodeType type, int index)
        {
            this.currentnode = this.CreateNode(type, index);
            this.currentnode.Line = this.line;
            this.currentnode.LinePosition = this.lineposition;
            if (type == HtmlNodeType.Element)
            {
                this.currentnode.LinePosition--;
            }

            this.currentnode.StreamPosition = index;
        }

        /// <summary>
        /// The read document encoding.
        /// </summary>
        /// <param name="node">The node.</param>
        private void ReadDocumentEncoding(HtmlNode node)
        {
            if (!this.OptionReadEncoding)
            {
                return;
            }

            // format is 
            // <meta http-equiv="content-type" content="text/html;charset=iso-8859-1" />

            // when we append a child, we are in node end, so attributes are already populated
            if (node.NameLength != 4)
            {
                // quick check, avoids string alloc
                return;
            }

            if (node.NodeName != "meta")
            {
                // all nodes names are lowercase
                return;
            }

            HtmlAttribute att = node.Attributes["http-equiv"];
            if (att == null)
            {
                return;
            }

            if (string.Compare(att.Value, "content-type", StringComparison.OrdinalIgnoreCase) != 0)
            {
                return;
            }

            HtmlAttribute content = node.Attributes["content"];
            if (content != null)
            {
                string charset = NameValuePairList.GetNameValuePairsValue(content.Value, "charset");
                if (!string.IsNullOrEmpty(charset))
                {
                    // The following check fixes the the bug described at: http://htmlagilitypack.codeplex.com/WorkItem/View.aspx?WorkItemId=25273
                    if (string.Equals(charset, "utf8", StringComparison.OrdinalIgnoreCase))
                    {
                        charset = "utf-8";
                    }

                    try
                    {
                        this.DeclaredEncoding = Encoding.GetEncoding(charset);
                    }
                    catch (ArgumentException)
                    {
                        this.DeclaredEncoding = null;
                    }

                    if (this.onlyDetectEncoding)
                    {
                        throw new EncodingFoundException(this.DeclaredEncoding);
                    }

                    if (this.StreamEncoding != null)
                    {
                        if (this.DeclaredEncoding != null)
                        {
                            if (this.DeclaredEncoding.WindowsCodePage != this.StreamEncoding.WindowsCodePage)
                            {
                                this.AddError(
                                    HtmlParseErrorCode.CharsetMismatch,
                                    this.line,
                                    this.lineposition,
                                    this.cuurrentindex,
                                    node.OuterHtml,
                                    "Encoding mismatch between StreamEncoding: " + this.StreamEncoding.WebName + " and DeclaredEncoding: " + this.DeclaredEncoding.WebName);
                            }
                        }
                    }
                }
            }
        }
    }
}