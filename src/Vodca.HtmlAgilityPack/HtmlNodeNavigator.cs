//-----------------------------------------------------------------------------
// <copyright file="HtmlNodeNavigator.cs" company="genuine">
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
    using System.Xml;
    using System.Xml.XPath;

    /// <summary>
    /// Represents an HTML navigator on an HTML document seen as a data store.
    /// </summary>
    public class HtmlNodeNavigator : XPathNavigator
    {
        /// <summary>
        /// The _doc.
        /// </summary>
        private readonly HtmlDocument htmlDocument = new HtmlDocument();

        /// <summary>
        /// The _nametable.
        /// </summary>
        private readonly HtmlNameTable htmlNameTable = new HtmlNameTable();

        /// <summary>
        /// The _attindex.
        /// </summary>
        private int attindex;

        /// <summary>
        /// The _currentnode.
        /// </summary>
        private HtmlNode currentnode;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        public HtmlNodeNavigator(Stream stream)
        {
            this.htmlDocument.Load(stream);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the stream. 
        /// </param>
        public HtmlNodeNavigator(Stream stream, bool detectEncodingFromByteOrderMarks)
        {
            this.htmlDocument.Load(stream, detectEncodingFromByteOrderMarks);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a stream.
        /// </summary>
        /// <param name="stream">
        /// The input stream. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        public HtmlNodeNavigator(Stream stream, Encoding encoding)
        {
            this.htmlDocument.Load(stream, encoding);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a stream.
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
        public HtmlNodeNavigator(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
        {
            this.htmlDocument.Load(stream, encoding, detectEncodingFromByteOrderMarks);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a stream.
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
        public HtmlNodeNavigator(
            Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
        {
            this.htmlDocument.Load(stream, encoding, detectEncodingFromByteOrderMarks, buffersize);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a TextReader.
        /// </summary>
        /// <param name="reader">
        /// The TextReader used to feed the HTML data into the document. 
        /// </param>
        public HtmlNodeNavigator(TextReader reader)
        {
            this.htmlDocument.Load(reader);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. 
        /// </param>
        public HtmlNodeNavigator(string path)
        {
            this.htmlDocument.Load(path);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. 
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        /// Indicates whether to look for byte order marks at the beginning of the file. 
        /// </param>
        public HtmlNodeNavigator(string path, bool detectEncodingFromByteOrderMarks)
        {
            this.htmlDocument.Load(path, detectEncodingFromByteOrderMarks);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a file.
        /// </summary>
        /// <param name="path">
        /// The complete file path to be read. 
        /// </param>
        /// <param name="encoding">
        /// The character encoding to use. 
        /// </param>
        public HtmlNodeNavigator(string path, Encoding encoding)
        {
            this.htmlDocument.Load(path, encoding);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a file.
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
        public HtmlNodeNavigator(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
        {
            this.htmlDocument.Load(path, encoding, detectEncodingFromByteOrderMarks);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class. 
        /// Initializes a new instance of the HtmlNavigator and loads an HTML document from a file.
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
        public HtmlNodeNavigator(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int buffersize)
        {
            this.htmlDocument.Load(path, encoding, detectEncodingFromByteOrderMarks, buffersize);
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class.
        /// </summary>
        internal HtmlNodeNavigator()
        {
            this.Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="currentNode">The current node.</param>
        internal HtmlNodeNavigator(HtmlDocument doc, HtmlNode currentNode)
        {
            if (currentNode == null)
            {
                throw new ArgumentNullException("currentNode");
            }

            if (currentNode.OwnerDocument != doc)
            {
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            }

            this.htmlDocument = doc;
            this.Reset();
            this.currentnode = currentNode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNodeNavigator"/> class.
        /// </summary>
        /// <param name="nav">
        /// The nav.
        /// </param>
        private HtmlNodeNavigator(HtmlNodeNavigator nav)
        {
            if (nav == null)
            {
                throw new ArgumentNullException("nav");
            }

            this.htmlDocument = nav.htmlDocument;
            this.currentnode = nav.currentnode;
            this.attindex = nav.attindex;
            this.htmlNameTable = nav.htmlNameTable; // REVIEW: should we do this?
        }

        /// <summary>
        ///   Gets the base URI for the current node. Always returns string.Empty in the case of HtmlNavigator implementation.
        /// </summary>
        public override string BaseURI
        {
            get
            {
                return this.htmlNameTable.GetOrAdd(string.Empty);
            }
        }

        /// <summary>
        ///   Gets the current HTML document.
        /// </summary>
        public HtmlDocument CurrentDocument
        {
            get
            {
                return this.htmlDocument;
            }
        }

        /// <summary>
        ///   Gets the current HTML node.
        /// </summary>
        public HtmlNode CurrentNode
        {
            get
            {
                return this.currentnode;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether the current node has child nodes.
        /// </summary>
        public override bool HasAttributes
        {
            get
            {
                return this.currentnode.Attributes.Count > 0;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether the current node has child nodes.
        /// </summary>
        public override bool HasChildren
        {
            get
            {
                return this.currentnode.ChildNodes.Count > 0;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether the current node is an empty element.
        /// </summary>
        public override bool IsEmptyElement
        {
            get
            {
                // REVIEW: is this ok?
                return !this.HasChildren;
            }
        }

        /// <summary>
        ///   Gets the name of the current HTML node without the namespace prefix.
        /// </summary>
        public override string LocalName
        {
            get
            {
                if (this.attindex != -1)
                {
                    return this.htmlNameTable.GetOrAdd(this.currentnode.Attributes[this.attindex].Name);
                }

                return this.htmlNameTable.GetOrAdd(this.currentnode.NodeName);
            }
        }

        /// <summary>
        ///   Gets the qualified name of the current node.
        /// </summary>
        public override string Name
        {
            get
            {
                return this.htmlNameTable.GetOrAdd(this.currentnode.NodeName);
            }
        }

        /// <summary>
        ///   Gets the <see cref="XmlNameTable" /> associated with this implementation.
        /// </summary>
        public override XmlNameTable NameTable
        {
            get
            {
                return this.htmlNameTable;
            }
        }

        /// <summary>
        ///   Gets the namespace URI (as defined in the W3C Namespace Specification) of the current node. Always returns string.Empty in the case of HtmlNavigator implementation.
        /// </summary>
        public override string NamespaceURI
        {
            get
            {
                return this.htmlNameTable.GetOrAdd(string.Empty);
            }
        }

        /// <summary>
        ///   Gets the type of the current node.
        /// </summary>
        public override XPathNodeType NodeType
        {
            get
            {
                switch (this.currentnode.NodeType)
                {
                    case HtmlNodeType.Comment:
                        return XPathNodeType.Comment;

                    case HtmlNodeType.Document:
                        return XPathNodeType.Root;

                    case HtmlNodeType.Text:
                        return XPathNodeType.Text;

                    case HtmlNodeType.Element:
                        {
                            if (this.attindex != -1)
                            {
                                return XPathNodeType.Attribute;
                            }

                            return XPathNodeType.Element;
                        }

                    default:
                        throw new NotImplementedException("Internal error: Unhandled HtmlNodeType: " + this.currentnode.NodeType);
                }
            }
        }

        /// <summary>
        ///   Gets the prefix associated with the current node. Always returns string.Empty in the case of HtmlNavigator implementation.
        /// </summary>
        public override string Prefix
        {
            get
            {
                return this.htmlNameTable.GetOrAdd(string.Empty);
            }
        }

        /// <summary>
        ///   Gets the text value of the current node.
        /// </summary>
        public override string Value
        {
            get
            {
                switch (this.currentnode.NodeType)
                {
                    case HtmlNodeType.Comment:
                        return ((HtmlCommentNode)this.currentnode).Comment;

                    case HtmlNodeType.Document:
                        return string.Empty;

                    case HtmlNodeType.Text:
                        return ((HtmlTextNode)this.currentnode).Text;

                    case HtmlNodeType.Element:
                        {
                            if (this.attindex != -1)
                            {
                                return this.currentnode.Attributes[this.attindex].Value;
                            }

                            return this.currentnode.InnerText;
                        }

                    default:
                        throw new NotImplementedException("Internal error: Unhandled HtmlNodeType: " + this.currentnode.NodeType);
                }
            }
        }

        /// <summary>
        ///   Gets the xml:lang scope for the current node. Always returns string.Empty in the case of HtmlNavigator implementation.
        /// </summary>
        public override string XmlLang
        {
            get
            {
                return this.htmlNameTable.GetOrAdd(string.Empty);
            }
        }

        /// <summary>
        /// Creates a new HtmlNavigator positioned at the same node as this HtmlNavigator.
        /// </summary>
        /// <returns>
        /// A new HtmlNavigator object positioned at the same node as the original HtmlNavigator. 
        /// </returns>
        public override XPathNavigator Clone()
        {
            return new HtmlNodeNavigator(this);
        }

        /// <summary>
        /// Gets the value of the HTML attribute with the specified LocalName and NamespaceURI.
        /// </summary>
        /// <param name="localName">
        /// The local name of the HTML attribute. 
        /// </param>
        /// <param name="namespaceUri">
        /// The namespace URI of the attribute. Unsupported with the HtmlNavigator implementation. 
        /// </param>
        /// <returns>
        /// The value of the specified HTML attribute. String.Empty or null if a matching attribute is not found or if the navigator is not positioned on an element node. 
        /// </returns>
        public override string GetAttribute(string localName, string namespaceUri)
        {
            HtmlAttribute att = this.currentnode.Attributes[localName];
            if (att == null)
            {
                return string.Empty;
            }

            return att.Value;
        }

        /// <summary>
        /// Returns the value of the namespace node corresponding to the specified local name. Always returns string.Empty for the HtmlNavigator implementation.
        /// </summary>
        /// <param name="name">
        /// The local name of the namespace node. 
        /// </param>
        /// <returns>
        /// Always returns string.Empty for the HtmlNavigator implementation. 
        /// </returns>
        public override string GetNamespace(string name)
        {
            return string.Empty;
        }

        /// <summary>
        /// Determines whether the current HtmlNavigator is at the same position as the specified HtmlNavigator.
        /// </summary>
        /// <param name="other">
        /// The HtmlNavigator that you want to compare against. 
        /// </param>
        /// <returns>
        /// true if the two navigators have the same position, otherwise, false. 
        /// </returns>
        public override bool IsSamePosition(XPathNavigator other)
        {
            var nav = other as HtmlNodeNavigator;
            if (nav == null)
            {
                return false;
            }

            return nav.currentnode == this.currentnode;
        }

        /// <summary>
        /// Moves to the same position as the specified HtmlNavigator.
        /// </summary>
        /// <param name="other">
        /// The HtmlNavigator positioned on the node that you want to move to. 
        /// </param>
        /// <returns>
        /// true if successful, otherwise false. If false, the position of the navigator is unchanged. 
        /// </returns>
        public override bool MoveTo(XPathNavigator other)
        {
            var nav = other as HtmlNodeNavigator;
            if (nav == null)
            {
                return false;
            }

            if (nav.htmlDocument == this.htmlDocument)
            {
                this.currentnode = nav.currentnode;
                this.attindex = nav.attindex;

                return true;
            }

            // we don't know how to handle that
            return false;
        }

        /// <summary>
        /// Moves to the HTML attribute with matching LocalName and NamespaceURI.
        /// </summary>
        /// <param name="localName">
        /// The local name of the HTML attribute. 
        /// </param>
        /// <param name="namespaceUri">
        /// The namespace URI of the attribute. Unsupported with the HtmlNavigator implementation. 
        /// </param>
        /// <returns>
        /// true if the HTML attribute is found, otherwise, false. If false, the position of the navigator does not change. 
        /// </returns>
        public override bool MoveToAttribute(string localName, string namespaceUri)
        {
            int index = this.currentnode.Attributes.GetAttributeIndex(localName);
            if (index == -1)
            {
                return false;
            }

            this.attindex = index;

            return true;
        }

        /// <summary>
        /// Moves to the first sibling of the current node.
        /// </summary>
        /// <returns>
        /// true if the navigator is successful moving to the first sibling node, false if there is no first sibling or if the navigator is currently positioned on an attribute node. 
        /// </returns>
        public override bool MoveToFirst()
        {
            if (this.currentnode.ParentNode == null)
            {
                return false;
            }

            if (this.currentnode.ParentNode.FirstChild == null)
            {
                return false;
            }

            this.currentnode = this.currentnode.ParentNode.FirstChild;

            return true;
        }

        /// <summary>
        /// Moves to the first HTML attribute.
        /// </summary>
        /// <returns>
        /// true if the navigator is successful moving to the first HTML attribute, otherwise, false. 
        /// </returns>
        public override bool MoveToFirstAttribute()
        {
            if (!this.HasAttributes)
            {
                return false;
            }

            this.attindex = 0;

            return true;
        }

        /// <summary>
        /// Moves to the first child of the current node.
        /// </summary>
        /// <returns>
        /// true if there is a first child node, otherwise false. 
        /// </returns>
        public override bool MoveToFirstChild()
        {
            if (!this.currentnode.HasChildNodes)
            {
                return false;
            }

            this.currentnode = this.currentnode.ChildNodes[0];

            return true;
        }

        /// <summary>
        /// Moves the XPathNavigator to the first namespace node of the current element. Always returns false for the HtmlNavigator implementation.
        /// </summary>
        /// <param name="scope">
        /// An XPathNamespaceScope value describing the namespace scope. 
        /// </param>
        /// <returns>
        /// Always returns false for the HtmlNavigator implementation. 
        /// </returns>
        public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
        {
            return false;
        }

        /// <summary>
        /// Moves to the node that has an attribute of type ID whose value matches the specified string.
        /// </summary>
        /// <param name="id">
        /// A string representing the ID value of the node to which you want to move. This argument does not need to be atomized. 
        /// </param>
        /// <returns>
        /// true if the move was successful, otherwise false. If false, the position of the navigator is unchanged. 
        /// </returns>
        public override bool MoveToId(string id)
        {
            HtmlNode node = this.htmlDocument.GetElementbyId(id);
            if (node == null)
            {
                return false;
            }

            this.currentnode = node;

            return true;
        }

        /// <summary>
        /// Moves the XPathNavigator to the namespace node with the specified local name. Always returns false for the HtmlNavigator implementation.
        /// </summary>
        /// <param name="name">
        /// The local name of the namespace node. 
        /// </param>
        /// <returns>
        /// Always returns false for the HtmlNavigator implementation. 
        /// </returns>
        public override bool MoveToNamespace(string name)
        {
            return false;
        }

        /// <summary>
        /// Moves to the next sibling of the current node.
        /// </summary>
        /// <returns>
        /// true if the navigator is successful moving to the next sibling node, false if there are no more siblings or if the navigator is currently positioned on an attribute node. If false, the position of the navigator is unchanged. 
        /// </returns>
        public override bool MoveToNext()
        {
            if (this.currentnode.NextSibling == null)
            {
                return false;
            }

            this.currentnode = this.currentnode.NextSibling;
            return true;
        }

        /// <summary>
        /// Moves to the next HTML attribute.
        /// </summary>
        /// <returns>
        /// The move to next attribute.
        /// </returns>
        public override bool MoveToNextAttribute()
        {
            if (this.attindex >= (this.currentnode.Attributes.Count - 1))
            {
                return false;
            }

            this.attindex++;

            return true;
        }

        /// <summary>
        /// Moves the XPathNavigator to the next namespace node. Always returns falsefor the HtmlNavigator implementation.
        /// </summary>
        /// <param name="scope">
        /// An XPathNamespaceScope value describing the namespace scope. 
        /// </param>
        /// <returns>
        /// Always returns false for the HtmlNavigator implementation. 
        /// </returns>
        public override bool MoveToNextNamespace(XPathNamespaceScope scope)
        {
            return false;
        }

        /// <summary>
        /// Moves to the parent of the current node.
        /// </summary>
        /// <returns>
        /// true if there is a parent node, otherwise false. 
        /// </returns>
        public override bool MoveToParent()
        {
            if (this.currentnode.ParentNode == null)
            {
                return false;
            }

            this.currentnode = this.currentnode.ParentNode;
            return true;
        }

        /// <summary>
        /// Moves to the previous sibling of the current node.
        /// </summary>
        /// <returns>
        /// true if the navigator is successful moving to the previous sibling node, false if there is no previous sibling or if the navigator is currently positioned on an attribute node. 
        /// </returns>
        public override bool MoveToPrevious()
        {
            if (this.currentnode.PreviousSibling == null)
            {
                return false;
            }

            this.currentnode = this.currentnode.PreviousSibling;
            return true;
        }

        /// <summary>
        /// Moves to the root node to which the current node belongs.
        /// </summary>
        public override void MoveToRoot()
        {
            this.currentnode = this.htmlDocument.DocumentNode;
        }

        /// <summary>
        /// The reset.
        /// </summary>
        private void Reset()
        {
            this.currentnode = this.htmlDocument.DocumentNode;
            this.attindex = -1;
        }
    }
}