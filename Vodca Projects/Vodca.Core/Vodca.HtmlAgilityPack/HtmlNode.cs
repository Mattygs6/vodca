//-----------------------------------------------------------------------------
// <copyright file="HtmlNode.cs" company="genuine">
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
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Represents an HTML node.
    /// </summary>
    [DebuggerDisplay("Name: {OriginalName}}")]
    public partial class HtmlNode
    {
        /// <summary>
        ///   Gets the name of a comment node. It is actually defined as '#comment'.
        /// </summary>
        public const string HtmlNodeTypeNameComment = "#comment";

        /// <summary>
        ///   Gets the name of the document node. It is actually defined as '#document'.
        /// </summary>
        public const string HtmlNodeTypeNameDocument = "#document";

        /// <summary>
        ///   Gets the name of a text node. It is actually defined as '#text'.
        /// </summary>
        public const string HtmlNodeTypeNameText = "#text";

        /// <summary>
        ///  The attribute collection
        /// </summary>
        private HtmlAttributeCollection attributes;

        /// <summary>
        ///  The child nodes
        /// </summary>
        private HtmlNodeCollection childnodes;

        /// <summary>
        ///  The Inner Html
        /// </summary>
        private string innerhtml;

        /// <summary>
        ///  The outer html
        /// </summary>
        private string outerhtml;

        /// <summary>
        /// Initializes static members of the <see cref="HtmlNode"/> class. 
        ///   Initialize HtmlNode. Builds a list of all tags that have special allowances
        /// </summary>
        static HtmlNode()
        {
            // tags whose content may be anything
            ElementsFlags = new Dictionary<string, HtmlElementFlag>
                {
                    { "script", HtmlElementFlag.CData },
                    { "style", HtmlElementFlag.CData },
                    { "noxhtml", HtmlElementFlag.CData },
                    { "base", HtmlElementFlag.Empty },
                    { "link", HtmlElementFlag.Empty },
                    { "meta", HtmlElementFlag.Empty },
                    { "isindex", HtmlElementFlag.Empty },
                    { "hr", HtmlElementFlag.Empty },
                    { "col", HtmlElementFlag.Empty },
                    { "img", HtmlElementFlag.Empty },
                    { "param", HtmlElementFlag.Empty },
                    { "embed", HtmlElementFlag.Empty },
                    { "frame", HtmlElementFlag.Empty },
                    { "wbr", HtmlElementFlag.Empty },
                    { "bgsound", HtmlElementFlag.Empty },
                    { "spacer", HtmlElementFlag.Empty },
                    { "keygen", HtmlElementFlag.Empty },
                    { "area", HtmlElementFlag.Empty },
                    { "input", HtmlElementFlag.Empty },
                    { "basefont", HtmlElementFlag.Empty },
                    { "form", HtmlElementFlag.CanOverlap | HtmlElementFlag.Empty },
                    { "option", HtmlElementFlag.Empty },
                    { "br", HtmlElementFlag.Empty | HtmlElementFlag.Closed },
                    { "p", HtmlElementFlag.Empty | HtmlElementFlag.Closed }
                };

            // tags that can not contain other tags

            // they sometimes contain, and sometimes they don 't...

            // tag whose closing tag is equivalent to open tag:
            // <p>bla</p>bla will be transformed into <p>bla</p>bla
            // <p>bla<p>bla will be transformed into <p>bla<p>bla and not <p>bla></p><p>bla</p> or <p>bla<p>bla</p></p>
            // <br> see above
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlNode"/> class.
        /// Initializes HtmlNode, providing type, owner and where it exists in a collection
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="ownerdocument">The owner document.</param>
        /// <param name="index">The index.</param>
        public HtmlNode(HtmlNodeType type, HtmlDocument ownerdocument, int index)
        {
            this.NodeType = type;
            this.OwnerDocument = ownerdocument;
            this.OuterStartIndex = index;

            switch (type)
            {
                case HtmlNodeType.Comment:
                    this.NodeName = HtmlNodeTypeNameComment;
                    this.EndNode = this;
                    break;

                case HtmlNodeType.Document:
                    this.NodeName = HtmlNodeTypeNameDocument;
                    this.EndNode = this;
                    break;

                case HtmlNodeType.Text:
                    this.NodeName = HtmlNodeTypeNameText;
                    this.EndNode = this;
                    break;
            }

            if (this.OwnerDocument.Openednodes != null)
            {
                if (!this.Closed)
                {
                    // we use the index as the key

                    // -1 means the node comes from public
                    if (-1 != index)
                    {
                        this.OwnerDocument.Openednodes.Add(index, this);
                    }
                }
            }

            if ((-1 != index) || (type == HtmlNodeType.Comment) || (type == HtmlNodeType.Text))
            {
                return;
            }

            // innerhtml and outerhtml must be calculated
            this.OuterChanged = true;
            this.InnerChanged = true;
        }

        /// <summary>
        ///   Gets a collection of flags that define specific behaviors for specific element nodes. The table contains a DictionaryEntry list with the lowercase tag name as the Key, and a combination of HtmlElementFlags as the Value.
        /// </summary>
        public static IDictionary<string, HtmlElementFlag> ElementsFlags { get; private set; }

        /// <summary>
        ///   Gets the collection of HTML attributes for this node. May not be null.
        /// </summary>
        public HtmlAttributeCollection Attributes
        {
            get
            {
                if (!this.HasAttributes)
                {
                    this.attributes = new HtmlAttributeCollection(this);
                }

                return this.attributes;
            }

            internal set
            {
                this.attributes = value;
            }
        }

        /// <summary>
        ///   Gets all the children of the node.
        /// </summary>
        public HtmlNodeCollection ChildNodes
        {
            get
            {
                return this.childnodes ?? (this.childnodes = new HtmlNodeCollection(this));
            }

            internal set
            {
                this.childnodes = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="HtmlNode"/> is closed or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if closed; otherwise, <c>false</c>.
        /// </value>
        public bool Closed
        {
            get
            {
                return this.EndNode != null;
            }
        }

        /// <summary>
        ///   Gets the collection of HTML attributes for the closing tag. May not be null.
        /// </summary>
        public HtmlAttributeCollection ClosingAttributes
        {
            get
            {
                return !this.HasClosingAttributes ? new HtmlAttributeCollection(this) : this.EndNode.Attributes;
            }
        }

        /// <summary>
        ///   Gets the first child of the node.
        /// </summary>
        public HtmlNode FirstChild
        {
            get
            {
                return !this.HasChildNodes ? null : this.childnodes[0];
            }
        }

        /// <summary>
        ///   Gets a value indicating whether the current node has any attributes.
        /// </summary>
        public bool HasAttributes
        {
            get
            {
                if (this.attributes == null)
                {
                    return false;
                }

                if (this.attributes.Count <= 0)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether this node has any child nodes.
        /// </summary>
        public bool HasChildNodes
        {
            get
            {
                if (this.childnodes == null)
                {
                    return false;
                }

                if (this.childnodes.Count <= 0)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether the current node has any attributes on the closing tag.
        /// </summary>
        public bool HasClosingAttributes
        {
            get
            {
                if ((this.EndNode == null) || (this.EndNode == this))
                {
                    return false;
                }

                if (this.EndNode.attributes == null)
                {
                    return false;
                }

                if (this.EndNode.attributes.Count <= 0)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        ///   Gets or sets the value of the 'id' HTML attribute. The document must have been parsed using the OptionUseIdAttribute set to true.
        /// </summary>
        public string Id
        {
            get
            {
                if (this.OwnerDocument.Nodesid == null)
                {
                    throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
                }

                return this.GetId();
            }

            set
            {
                if (this.OwnerDocument.Nodesid == null)
                {
                    throw new Exception(HtmlDocument.HtmlExceptionUseIdAttributeFalse);
                }

                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.SetId(value);
            }
        }

        /// <summary>
        /// Gets or sets the HTML between the start and end tags of the object.
        /// </summary>
        /// <value>
        /// The inner HTML.
        /// </value>
        public virtual string InnerHtml
        {
            get
            {
                if (this.InnerChanged)
                {
                    this.innerhtml = this.WriteContentTo();
                    this.InnerChanged = false;
                    return this.innerhtml;
                }

                if (this.innerhtml != null)
                {
                    return this.innerhtml;
                }

                if (this.InnerStartIndex < 0)
                {
                    return string.Empty;
                }

                return this.OwnerDocument.Text.Substring(this.InnerStartIndex, this.InnerLength);
            }

            set
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(value);

                this.RemoveAllChildren();
                this.AppendChildren(doc.DocumentNode.ChildNodes);
            }
        }

        /// <summary>
        ///   Gets or Sets the text between the start and end tags of the object.
        /// </summary>
        public virtual string InnerText
        {
            get
            {
                if (this.NodeType == HtmlNodeType.Text)
                {
                    return ((HtmlTextNode)this).Text;
                }

                if (this.NodeType == HtmlNodeType.Comment)
                {
                    return ((HtmlCommentNode)this).Comment;
                }

                // note: right now, this method is *slow*, because we recompute everything.
                // it could be optimized like innerhtml
                if (!this.HasChildNodes)
                {
                    return string.Empty;
                }

                return this.ChildNodes.Aggregate<HtmlNode, string>(null, (current, node) => current + node.InnerText);
            }
        }

        /// <summary>
        ///   Gets the last child of the node.
        /// </summary>
        public HtmlNode LastChild
        {
            get
            {
                return !this.HasChildNodes ? null : this.childnodes[this.childnodes.Count - 1];
            }
        }

        /// <summary>
        ///   Gets the line number of this node in the document.
        /// </summary>
        public int Line { get; internal set; }

        /// <summary>
        ///   Gets the column number of this node in the document.
        /// </summary>
        public int LinePosition { get; internal set; }

        /// <summary>
        ///   Gets or sets this node's name.
        /// </summary>
        public string NodeName
        {
            get
            {
                if (this.HtmlNodeName == null)
                {
                    this.NodeName = this.OwnerDocument.Text.Substring(this.NameStartIndex, this.NameLength);
                }

                return this.HtmlNodeName != null ? this.HtmlNodeName.ToLower() : string.Empty;
            }

            set
            {
                this.HtmlNodeName = value;
            }
        }

        /// <summary>
        ///   Gets the HTML node immediately following this element.
        /// </summary>
        public HtmlNode NextSibling { get; internal set; }

        /// <summary>
        ///   Gets the type of this node.
        /// </summary>
        public HtmlNodeType NodeType { get; internal set; }

        /// <summary>
        /// Gets the name of the original/unaltered name of the tag
        /// </summary>
        /// <value>
        /// The name of the original.
        /// </value>
        public string OriginalName
        {
            get
            {
                return this.HtmlNodeName;
            }
        }

        /// <summary>
        ///   Gets or Sets the object and its content in HTML.
        /// </summary>
        public virtual string OuterHtml
        {
            get
            {
                if (this.OuterChanged)
                {
                    this.outerhtml = this.WriteTo();
                    this.OuterChanged = false;
                    return this.outerhtml;
                }

                if (this.outerhtml != null)
                {
                    return this.outerhtml;
                }

                if (this.OuterStartIndex < 0)
                {
                    return string.Empty;
                }

                return this.OwnerDocument.Text.Substring(this.OuterStartIndex, this.OuterLength);
            }
        }

        /// <summary>
        ///   Gets the <see cref="HtmlDocument" /> to which this node belongs.
        /// </summary>
        public HtmlDocument OwnerDocument { get; internal set; }

        /// <summary>
        ///   Gets the parent of this node (for nodes that can have parents).
        /// </summary>
        public HtmlNode ParentNode { get; internal set; }

        /// <summary>
        ///   Gets the node immediately preceding this node.
        /// </summary>
        public HtmlNode PreviousSibling { get; internal set; }

        /// <summary>
        ///   Gets the stream position of this node in the document, relative to the start of the document.
        /// </summary>
        public int StreamPosition { get; internal set; }

        /// <summary>
        ///   Gets a valid XPath string that points to this node
        /// </summary>
        public string XPath
        {
            get
            {
                string basePath = (this.ParentNode == null || this.ParentNode.NodeType == HtmlNodeType.Document)
                                      ? "/"
                                      : this.ParentNode.XPath + "/";
                return basePath + this.GetRelativeXpath();
            }
        }

        /// <summary>
        /// Determines if an element node can be kept overlapped.
        /// </summary>
        /// <param name="name">
        /// The name of the element node to check. May not be <c>null</c> . 
        /// </param>
        /// <returns>
        /// true if the name is the name of an element node that can be kept overlapped, <c>false</c> otherwise. 
        /// </returns>
        public static bool CanOverlapElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (!ElementsFlags.ContainsKey(name.ToLower()))
            {
                return false;
            }

            HtmlElementFlag flag = ElementsFlags[name.ToLower()];
            return (flag & HtmlElementFlag.CanOverlap) != 0;
        }

        /// <summary>
        /// Creates an HTML node from a string representing literal HTML.
        /// </summary>
        /// <param name="html">
        /// The HTML text. 
        /// </param>
        /// <returns>
        /// The newly created node instance. 
        /// </returns>
        public static HtmlNode CreateNode(string html)
        {
            // REVIEW: this is *not* optimum...
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.FirstChild;
        }

        /// <summary>
        /// Determines if an element node is a CDATA element node.
        /// </summary>
        /// <param name="name">
        /// The name of the element node to check. May not be null. 
        /// </param>
        /// <returns>
        /// true if the name is the name of a CDATA element node, false otherwise. 
        /// </returns>
        public static bool IsCDataElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (!ElementsFlags.ContainsKey(name.ToLower()))
            {
                return false;
            }

            HtmlElementFlag flag = ElementsFlags[name.ToLower()];
            return (flag & HtmlElementFlag.CData) != 0;
        }

        /// <summary>
        /// Determines if an element node is closed.
        /// </summary>
        /// <param name="name">
        /// The name of the element node to check. May not be null. 
        /// </param>
        /// <returns>
        /// true if the name is the name of a closed element node, false otherwise. 
        /// </returns>
        public static bool IsClosedElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (!ElementsFlags.ContainsKey(name.ToLower()))
            {
                return false;
            }

            HtmlElementFlag flag = ElementsFlags[name.ToLower()];
            return (flag & HtmlElementFlag.Closed) != 0;
        }

        /// <summary>
        /// Determines if an element node is defined as empty.
        /// </summary>
        /// <param name="name">
        /// The name of the element node to check. May not be null. 
        /// </param>
        /// <returns>
        /// true if the name is the name of an empty element node, false otherwise. 
        /// </returns>
        public static bool IsEmptyElement(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (name.Length == 0)
            {
                return true;
            }

            // <!DOCTYPE ...
            if ('!' == name[0])
            {
                return true;
            }

            // <?xml ...
            if ('?' == name[0])
            {
                return true;
            }

            if (!ElementsFlags.ContainsKey(name.ToLower()))
            {
                return false;
            }

            HtmlElementFlag flag = ElementsFlags[name.ToLower()];
            return (flag & HtmlElementFlag.Empty) != 0;
        }

        /// <summary>
        /// Determines if a text corresponds to the closing tag of an node that can be kept overlapped.
        /// </summary>
        /// <param name="text">
        /// The text to check. May not be null. 
        /// </param>
        /// <returns>
        /// true or false. 
        /// </returns>
        public static bool IsOverlappedClosingElement(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            // min is </x>: 4
            if (text.Length <= 4)
            {
                return false;
            }

            if ((text[0] != '<') || (text[text.Length - 1] != '>') || (text[1] != '/'))
            {
                return false;
            }

            string name = text.Substring(2, text.Length - 3);
            return CanOverlapElement(name);
        }

        /// <summary>
        /// Returns a collection of all ancestor nodes of this element.
        /// </summary>
        /// <returns>The ancestors nodes</returns>
        public IEnumerable<HtmlNode> Ancestors()
        {
            HtmlNode node = this.ParentNode;
            while (node.ParentNode != null)
            {
                yield return node.ParentNode;
                node = node.ParentNode;
            }
        }

        /// <summary>
        /// Get Ancestors with matching name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The ancestors nodes</returns>
        public IEnumerable<HtmlNode> Ancestors(string name)
        {
            for (HtmlNode n = this.ParentNode; n != null; n = n.ParentNode)
            {
                if (n.NodeName == name)
                {
                    yield return n;
                }
            }
        }

        /// <summary>
        /// Returns a collection of all ancestor nodes of this element.
        /// </summary>
        /// <returns>The ancestors node or it self</returns>
        public IEnumerable<HtmlNode> AncestorsAndSelf()
        {
            for (HtmlNode n = this; n != null; n = n.ParentNode)
            {
                yield return n;
            }
        }

        /// <summary>
        /// Gets all ancestor nodes and the current node
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The ancestors node or it self</returns>
        public IEnumerable<HtmlNode> AncestorsAndSelf(string name)
        {
            for (HtmlNode n = this; n != null; n = n.ParentNode)
            {
                if (n.NodeName == name)
                {
                    yield return n;
                }
            }
        }

        /// <summary>
        /// Adds the specified node to the end of the list of children of this node.
        /// </summary>
        /// <param name="newChild">
        /// The node to add. May not be null. 
        /// </param>
        /// <returns>
        /// The node added. 
        /// </returns>
        public HtmlNode AppendChild(HtmlNode newChild)
        {
            if (newChild == null)
            {
                throw new ArgumentNullException("newChild");
            }

            this.ChildNodes.Append(newChild);
            this.OwnerDocument.SetIdForNode(newChild, newChild.GetId());
            this.OuterChanged = true;
            this.InnerChanged = true;
            return newChild;
        }

        /// <summary>
        /// Adds the specified node to the end of the list of children of this node.
        /// </summary>
        /// <param name="newChildren">
        /// The node list to add. May not be null. 
        /// </param>
        public void AppendChildren(IEnumerable<HtmlNode> newChildren)
        {
            if (newChildren == null)
            {
                throw new ArgumentNullException("newChildren");
            }

            foreach (HtmlNode newChild in newChildren)
            {
                this.AppendChild(newChild);
            }
        }

        /// <summary>
        /// Gets all Attributes with name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The child attributes</returns>
        public IEnumerable<HtmlAttribute> ChildAttributes(string name)
        {
            return this.Attributes.AttributesWithName(name);
        }

        /// <summary>
        /// Creates a duplicate of the node
        /// </summary>
        /// <returns>The clone</returns>
        public HtmlNode Clone()
        {
            return this.CloneNode(true);
        }

        /// <summary>
        /// Creates a duplicate of the node and changes its name at the same time.
        /// </summary>
        /// <param name="newName">
        /// The new name of the cloned node. May not be null. 
        /// </param>
        /// <param name="deep">
        /// true to recursively clone the subtree under the specified node; false to clone only the node itself. 
        /// </param>
        /// <returns>
        /// The cloned node. 
        /// </returns>
        public HtmlNode CloneNode(string newName, bool deep = true)
        {
            if (newName == null)
            {
                throw new ArgumentNullException("newName");
            }

            HtmlNode node = this.CloneNode(deep);
            node.NodeName = newName;
            return node;
        }

        /// <summary>
        /// Creates a duplicate of the node.
        /// </summary>
        /// <param name="deep">
        /// true to recursively clone the subtree under the specified node; false to clone only the node itself. 
        /// </param>
        /// <returns>
        /// The cloned node. 
        /// </returns>
        public HtmlNode CloneNode(bool deep)
        {
            HtmlNode node = this.OwnerDocument.CreateNode(this.NodeType);
            node.NodeName = this.NodeName;

            switch (this.NodeType)
            {
                case HtmlNodeType.Comment:
                    ((HtmlCommentNode)node).Comment = ((HtmlCommentNode)this).Comment;
                    return node;

                case HtmlNodeType.Text:
                    ((HtmlTextNode)node).Text = ((HtmlTextNode)this).Text;
                    return node;
            }

            // attributes
            if (this.HasAttributes)
            {
                foreach (HtmlAttribute att in this.attributes)
                {
                    HtmlAttribute newatt = att.Clone();
                    node.Attributes.Append(newatt);
                }
            }

            // closing attributes
            if (this.HasClosingAttributes)
            {
                node.EndNode = this.EndNode.CloneNode(false);
                foreach (HtmlAttribute att in this.EndNode.attributes)
                {
                    HtmlAttribute newatt = att.Clone();
                    node.EndNode.attributes.Append(newatt);
                }
            }

            if (!deep)
            {
                return node;
            }

            if (!this.HasChildNodes)
            {
                return node;
            }

            // child nodes
            foreach (HtmlNode child in this.childnodes)
            {
                HtmlNode newchild = child.Clone();
                node.AppendChild(newchild);
            }

            return node;
        }

        /// <summary>
        /// Creates a duplicate of the node.
        /// </summary>
        /// <param name="node">
        /// The node to duplicate. May not be <c>null</c> . 
        /// </param>
        /// <param name="deep">
        /// true to recursively clone the subtree under the specified node, false to clone only the node itself. 
        /// </param>
        public void CopyFrom(HtmlNode node, bool deep = true)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            this.Attributes.RemoveAll();
            if (node.HasAttributes)
            {
                foreach (HtmlAttribute att in node.Attributes)
                {
                    this.SetAttributeValue(att.Name, att.Value);
                }
            }

            if (!deep)
            {
                this.RemoveAllChildren();
                if (node.HasChildNodes)
                {
                    foreach (HtmlNode child in node.ChildNodes)
                    {
                        this.AppendChild(child.CloneNode(true));
                    }
                }
            }
        }

        /// <summary>
        /// Gets all Descendant nodes for this node and each of child nodes
        /// </summary>
        /// <returns>The descendants</returns>
        public IEnumerable<HtmlNode> DescendantNodes()
        {
            foreach (HtmlNode node in this.ChildNodes)
            {
                yield return node;
                foreach (HtmlNode descendant in node.DescendantNodes())
                {
                    yield return descendant;
                }
            }
        }

        /// <summary>
        /// Returns a collection of all descendant nodes of this element, in document order
        /// </summary>
        /// <returns>The descendants and self</returns>
        public IEnumerable<HtmlNode> DescendantNodesAndSelf()
        {
            return this.DescendantsAndSelf();
        }

        /// <summary>
        /// Gets all Descendant nodes in enumerated list
        /// </summary>
        /// <returns>The descendants</returns>
        public IEnumerable<HtmlNode> Descendants()
        {
            return this.DescendantNodes();
        }

        /// <summary>
        /// Get all descendant nodes with matching name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The descendants</returns>
        public IEnumerable<HtmlNode> Descendants(string name)
        {
            return this.Descendants().Where(node => node.NodeName == name);
        }

        /// <summary>
        /// Returns a collection of all descendant nodes of this element, in document order
        /// </summary>
        /// <returns>The descendants and self</returns>
        public IEnumerable<HtmlNode> DescendantsAndSelf()
        {
            yield return this;
            foreach (HtmlNode n in this.DescendantNodes())
            {
                HtmlNode el = n;
                if (el != null)
                {
                    yield return el;
                }
            }
        }

        /// <summary>
        /// Gets all descendant nodes including this node
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The descendants and self</returns>
        public IEnumerable<HtmlNode> DescendantsAndSelf(string name)
        {
            yield return this;
            foreach (HtmlNode node in this.Descendants())
            {
                if (node.NodeName == name)
                {
                    yield return node;
                }
            }
        }

        /// <summary>
        /// Gets first generation child node matching name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The first child by name</returns>
        public HtmlNode Element(string name)
        {
            return this.ChildNodes.FirstOrDefault(node => node.NodeName == name);
        }

        /// <summary>
        /// Gets matching first generation child nodes matching name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The child nodes by name</returns>
        public IEnumerable<HtmlNode> Elements(string name)
        {
            return this.ChildNodes.Where(node => node.NodeName == name);
        }

        /// <summary>
        /// Helper method to get the value of an attribute of this node. If the attribute is not found, the default value will be returned.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute to get. May not be <c>null</c> . 
        /// </param>
        /// <param name="def">
        /// The default value to return if not found. 
        /// </param>
        /// <returns>
        /// The value of the attribute if found, the default value if not found. 
        /// </returns>
        public string GetAttributeValue(string name, string def)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (!this.HasAttributes)
            {
                return def;
            }

            HtmlAttribute att = this.Attributes[name];
            if (att == null)
            {
                return def;
            }

            return att.Value;
        }

        /// <summary>
        /// Helper method to get the value of an attribute of this node. If the attribute is not found, the default value will be returned.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute to get. May not be <c>null</c> . 
        /// </param>
        /// <param name="def">
        /// The default value to return if not found. 
        /// </param>
        /// <returns>
        /// The value of the attribute if found, the default value if not found. 
        /// </returns>
        public int GetAttributeValue(string name, int def)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (!this.HasAttributes)
            {
                return def;
            }

            HtmlAttribute att = this.Attributes[name];
            if (att == null)
            {
                return def;
            }

            try
            {
                return Convert.ToInt32(att.Value);
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// Helper method to get the value of an attribute of this node. If the attribute is not found, the default value will be returned.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute to get. May not be <c>null</c> . 
        /// </param>
        /// <param name="def">
        /// The default value to return if not found. 
        /// </param>
        /// <returns>
        /// The value of the attribute if found, the default value if not found. 
        /// </returns>
        public bool GetAttributeValue(string name, bool def)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (!this.HasAttributes)
            {
                return def;
            }

            HtmlAttribute att = this.Attributes[name];
            if (att == null)
            {
                return def;
            }

            try
            {
                return Convert.ToBoolean(att.Value);
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// Inserts the specified node immediately after the specified reference node.
        /// </summary>
        /// <param name="newChild">
        /// The node to insert. May not be <c>null</c> . 
        /// </param>
        /// <param name="refChild">
        /// The node that is the reference node. The newNode is placed after the refNode. 
        /// </param>
        /// <returns>
        /// The node being inserted. 
        /// </returns>
        public HtmlNode InsertAfter(HtmlNode newChild, HtmlNode refChild)
        {
            if (newChild == null)
            {
                throw new ArgumentNullException("newChild");
            }

            if (refChild == null)
            {
                return this.PrependChild(newChild);
            }

            if (newChild == refChild)
            {
                return newChild;
            }

            int index = -1;

            if (this.childnodes != null)
            {
                index = this.childnodes[refChild];
            }

            if (index == -1)
            {
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            }

            if (this.childnodes != null)
            {
                this.childnodes.Insert(index + 1, newChild);
            }

            this.OwnerDocument.SetIdForNode(newChild, newChild.GetId());
            this.OuterChanged = true;
            this.InnerChanged = true;
            return newChild;
        }

        /// <summary>
        /// Inserts the specified node immediately before the specified reference node.
        /// </summary>
        /// <param name="newChild">
        /// The node to insert. May not be <c>null</c> . 
        /// </param>
        /// <param name="refChild">
        /// The node that is the reference node. The newChild is placed before this node. 
        /// </param>
        /// <returns>
        /// The node being inserted. 
        /// </returns>
        public HtmlNode InsertBefore(HtmlNode newChild, HtmlNode refChild)
        {
            if (newChild == null)
            {
                throw new ArgumentNullException("newChild");
            }

            if (refChild == null)
            {
                return this.AppendChild(newChild);
            }

            if (newChild == refChild)
            {
                return newChild;
            }

            int index = -1;

            if (this.childnodes != null)
            {
                index = this.childnodes[refChild];
            }

            if (index == -1)
            {
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            }

            if (this.childnodes != null)
            {
                this.childnodes.Insert(index, newChild);
            }

            this.OwnerDocument.SetIdForNode(newChild, newChild.GetId());
            this.OuterChanged = true;
            this.InnerChanged = true;
            return newChild;
        }

        /// <summary>
        /// Adds the specified node to the beginning of the list of children of this node.
        /// </summary>
        /// <param name="newChild">
        /// The node to add. May not be <c>null</c> . 
        /// </param>
        /// <returns>
        /// The node added. 
        /// </returns>
        public HtmlNode PrependChild(HtmlNode newChild)
        {
            if (newChild == null)
            {
                throw new ArgumentNullException("newChild");
            }

            this.ChildNodes.Prepend(newChild);
            this.OwnerDocument.SetIdForNode(newChild, newChild.GetId());
            this.OuterChanged = true;
            this.InnerChanged = true;
            return newChild;
        }

        /// <summary>
        /// Adds the specified node list to the beginning of the list of children of this node.
        /// </summary>
        /// <param name="newChildren">
        /// The node list to add. May not be <c>null</c> . 
        /// </param>
        public void PrependChildren(IEnumerable<HtmlNode> newChildren)
        {
            if (newChildren == null)
            {
                throw new ArgumentNullException("newChildren");
            }

            foreach (HtmlNode newChild in newChildren)
            {
                this.PrependChild(newChild);
            }
        }

        /// <summary>
        /// Removes node from parent collection
        /// </summary>
        public void Remove()
        {
            if (this.ParentNode != null)
            {
                this.ParentNode.ChildNodes.Remove(this);
            }
        }

        /// <summary>
        /// Removes all the children and/or attributes of the current node.
        /// </summary>
        public void RemoveAll()
        {
            this.RemoveAllChildren();

            if (this.HasAttributes)
            {
                this.attributes.Clear();
            }

            if ((this.EndNode != null) && (this.EndNode != this))
            {
                if (this.EndNode.attributes != null)
                {
                    this.EndNode.attributes.Clear();
                }
            }

            this.OuterChanged = true;
            this.InnerChanged = true;
        }

        /// <summary>
        /// Removes all the children of the current node.
        /// </summary>
        public void RemoveAllChildren()
        {
            if (!this.HasChildNodes)
            {
                return;
            }

            if (this.OwnerDocument.OptionUseIdAttribute)
            {
                // remove nodes from id list
                foreach (HtmlNode node in this.childnodes)
                {
                    this.OwnerDocument.SetIdForNode(null, node.GetId());
                }
            }

            this.childnodes.Clear();
            this.OuterChanged = true;
            this.InnerChanged = true;
        }

        /// <summary>
        /// Removes the specified child node.
        /// </summary>
        /// <param name="oldChild">
        /// The node being removed. May not be <c>null</c> . 
        /// </param>
        /// <returns>
        /// The node removed. 
        /// </returns>
        public HtmlNode RemoveChild(HtmlNode oldChild)
        {
            if (oldChild == null)
            {
                throw new ArgumentNullException("oldChild");
            }

            int index = -1;

            if (this.childnodes != null)
            {
                index = this.childnodes[oldChild];
            }

            if (index == -1)
            {
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            }

            if (this.childnodes != null)
            {
                this.childnodes.Remove(index);
            }

            this.OwnerDocument.SetIdForNode(null, oldChild.GetId());
            this.OuterChanged = true;
            this.InnerChanged = true;
            return oldChild;
        }

        /// <summary>
        /// Removes the specified child node.
        /// </summary>
        /// <param name="oldChild">
        /// The node being removed. May not be <c>null</c> . 
        /// </param>
        /// <param name="keepGrandChildren">
        /// true to keep grand children of the node, false otherwise. 
        /// </param>
        /// <returns>
        /// The node removed. 
        /// </returns>
        public HtmlNode RemoveChild(HtmlNode oldChild, bool keepGrandChildren)
        {
            if (oldChild == null)
            {
                throw new ArgumentNullException("oldChild");
            }

            if ((oldChild.childnodes != null) && keepGrandChildren)
            {
                // get prev sibling
                HtmlNode prev = oldChild.PreviousSibling;

                // reroute grand children to ourselves
                foreach (HtmlNode grandchild in oldChild.childnodes)
                {
                    this.InsertAfter(grandchild, prev);
                }
            }

            this.RemoveChild(oldChild);
            this.OuterChanged = true;
            this.InnerChanged = true;
            return oldChild;
        }

        /// <summary>
        /// Replaces the child node oldChild with newChild node.
        /// </summary>
        /// <param name="newChild">
        /// The new node to put in the child list. 
        /// </param>
        /// <param name="oldChild">
        /// The node being replaced in the list. 
        /// </param>
        /// <returns>
        /// The node replaced. 
        /// </returns>
        public HtmlNode ReplaceChild(HtmlNode newChild, HtmlNode oldChild)
        {
            if (newChild == null)
            {
                return this.RemoveChild(oldChild);
            }

            if (oldChild == null)
            {
                return this.AppendChild(newChild);
            }

            int index = -1;

            if (this.childnodes != null)
            {
                index = this.childnodes[oldChild];
            }

            if (index == -1)
            {
                throw new ArgumentException(HtmlDocument.HtmlExceptionRefNotChild);
            }

            if (this.childnodes != null)
            {
                this.childnodes.Replace(index, newChild);
            }

            this.OwnerDocument.SetIdForNode(null, oldChild.GetId());
            this.OwnerDocument.SetIdForNode(newChild, newChild.GetId());
            this.OuterChanged = true;
            this.InnerChanged = true;
            return newChild;
        }

        /// <summary>
        /// Helper method to set the value of an attribute of this node. If the attribute is not found, it will be created automatically.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute to set. May not be null. 
        /// </param>
        /// <param name="value">
        /// The value for the attribute. 
        /// </param>
        /// <returns>
        /// The corresponding attribute instance. 
        /// </returns>
        public HtmlAttribute SetAttributeValue(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            HtmlAttribute att = this.Attributes[name];
            if (att == null)
            {
                return this.Attributes.Append(this.OwnerDocument.CreateAttribute(name, value));
            }

            att.Value = value;
            return att;
        }

        /// <summary>
        /// Saves all the children of the node to the specified TextWriter.
        /// </summary>
        /// <param name="outText">
        /// The TextWriter to which you want to save. 
        /// </param>
        public void WriteContentTo(TextWriter outText)
        {
            if (this.childnodes == null)
            {
                return;
            }

            foreach (HtmlNode node in this.childnodes)
            {
                node.WriteTo(outText);
            }
        }

        /// <summary>
        /// Saves all the children of the node to a string.
        /// </summary>
        /// <returns>
        /// The saved string. 
        /// </returns>
        public string WriteContentTo()
        {
            var sw = new StringWriter();
            this.WriteContentTo(sw);
            sw.Flush();
            return sw.ToString();
        }

        /// <summary>
        /// Saves the current node to the specified TextWriter.
        /// </summary>
        /// <param name="outText">
        /// The TextWriter to which you want to save. 
        /// </param>
        public void WriteTo(TextWriter outText)
        {
            string html;
            switch (this.NodeType)
            {
                case HtmlNodeType.Comment:
                    html = ((HtmlCommentNode)this).Comment;
                    if (this.OwnerDocument.OptionOutputAsXml)
                    {
                        outText.Write("<!--" + GetXmlComment((HtmlCommentNode)this) + " -->");
                    }
                    else
                    {
                        outText.Write(html);
                    }

                    break;

                case HtmlNodeType.Document:
                    if (this.OwnerDocument.OptionOutputAsXml)
                    {
                        outText.Write("<?xml version=\"1.0\" encoding=\"" + this.OwnerDocument.GetOutEncoding().BodyName + "\"?>");

                        // check there is a root element
                        if (this.OwnerDocument.DocumentNode.HasChildNodes)
                        {
                            int rootnodes = this.OwnerDocument.DocumentNode.childnodes.Count;
                            if (rootnodes > 0)
                            {
                                HtmlNode xml = this.OwnerDocument.GetXmlDeclaration();
                                if (xml != null)
                                {
                                    rootnodes--;
                                }

                                if (rootnodes > 1)
                                {
                                    if (this.OwnerDocument.OptionOutputUpperCase)
                                    {
                                        outText.Write("<SPAN>");
                                        this.WriteContentTo(outText);
                                        outText.Write("</SPAN>");
                                    }
                                    else
                                    {
                                        outText.Write("<span>");
                                        this.WriteContentTo(outText);
                                        outText.Write("</span>");
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    this.WriteContentTo(outText);
                    break;

                case HtmlNodeType.Text:
                    html = ((HtmlTextNode)this).Text;
                    outText.Write(this.OwnerDocument.OptionOutputAsXml ? HtmlDocument.HtmlEncode(html) : html);
                    break;

                case HtmlNodeType.Element:
                    string name = this.OwnerDocument.OptionOutputUpperCase ? this.NodeName.ToUpper() : this.NodeName;

                    if (this.OwnerDocument.OptionOutputOriginalCase)
                    {
                        name = this.OriginalName;
                    }

                    if (this.OwnerDocument.OptionOutputAsXml)
                    {
                        if (name.Length > 0)
                        {
                            if (name[0] == '?')
                            {
                                // forget this one, it's been done at the document level
                                break;
                            }

                            if (name.Trim().Length == 0)
                            {
                                break;
                            }

                            name = HtmlDocument.GetXmlName(name);
                        }
                        else
                        {
                            break;
                        }
                    }

                    outText.Write("<" + name);
                    this.WriteAttributes(outText, false);

                    if (this.HasChildNodes)
                    {
                        outText.Write(">");
                        bool cdata = false;
                        if (this.OwnerDocument.OptionOutputAsXml && IsCDataElement(this.NodeName))
                        {
                            // this code and the following tries to output things as nicely as possible for old browsers.
                            cdata = true;
                            outText.Write("\r\n//<![CDATA[\r\n");
                        }

                        if (cdata)
                        {
                            if (this.HasChildNodes)
                            {
                                // child must be a text
                                this.ChildNodes[0].WriteTo(outText);
                            }

                            outText.Write("\r\n//]]>//\r\n");
                        }
                        else
                        {
                            this.WriteContentTo(outText);
                        }

                        outText.Write("</" + name);
                        if (!this.OwnerDocument.OptionOutputAsXml)
                        {
                            this.WriteAttributes(outText, true);
                        }

                        outText.Write(">");
                    }
                    else
                    {
                        if (IsEmptyElement(this.NodeName))
                        {
                            if (this.OwnerDocument.OptionWriteEmptyNodes || (this.OwnerDocument.OptionOutputAsXml))
                            {
                                outText.Write(" />");
                            }
                            else
                            {
                                if (this.NodeName.Length > 0 && this.NodeName[0] == '?')
                                {
                                    outText.Write("?");
                                }

                                outText.Write(">");
                            }
                        }
                        else
                        {
                            outText.Write("></" + name + ">");
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Saves the current node to the specified XmlWriter.
        /// </summary>
        /// <param name="writer">
        /// The XmlWriter to which you want to save. 
        /// </param>
        public void WriteTo(XmlWriter writer)
        {
            switch (this.NodeType)
            {
                case HtmlNodeType.Comment:
                    writer.WriteComment(GetXmlComment((HtmlCommentNode)this));
                    break;

                case HtmlNodeType.Document:
                    writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"" + this.OwnerDocument.GetOutEncoding().BodyName + "\"");

                    if (this.HasChildNodes)
                    {
                        foreach (HtmlNode subnode in this.ChildNodes)
                        {
                            subnode.WriteTo(writer);
                        }
                    }

                    break;

                case HtmlNodeType.Text:
                    string html = ((HtmlTextNode)this).Text;
                    writer.WriteString(html);
                    break;

                case HtmlNodeType.Element:
                    string name = this.OwnerDocument.OptionOutputUpperCase ? this.NodeName.ToUpper() : this.NodeName;

                    if (this.OwnerDocument.OptionOutputOriginalCase)
                    {
                        name = this.OriginalName;
                    }

                    writer.WriteStartElement(name);
                    WriteAttributes(writer, this);

                    if (this.HasChildNodes)
                    {
                        foreach (HtmlNode subnode in this.ChildNodes)
                        {
                            subnode.WriteTo(writer);
                        }
                    }

                    writer.WriteEndElement();
                    break;
            }
        }

        /// <summary>
        /// Saves the current node to a string.
        /// </summary>
        /// <returns>
        /// The saved string. 
        /// </returns>
        public string WriteTo()
        {
            using (var sw = new StringWriter())
            {
                this.WriteTo(sw);
                sw.Flush();
                return sw.ToString();
            }
        }

        /// <summary>
        /// The get xml comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// The xml comment.
        /// </returns>
        internal static string GetXmlComment(HtmlCommentNode comment)
        {
            string s = comment.Comment;
            return s.Substring(4, s.Length - 7).Replace("--", " - -");
        }

        /// <summary>
        /// The write attributes.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        internal static void WriteAttributes(XmlWriter writer, HtmlNode node)
        {
            if (!node.HasAttributes)
            {
                return;
            }

            // we use Hashitems to make sure attributes are written only once
            foreach (HtmlAttribute att in node.Attributes.Hashitems.Values)
            {
                writer.WriteAttributeString(att.XmlName, att.Value);
            }
        }

        /// <summary>
        /// The close node.
        /// </summary>
        /// <param name="htmlendnode">The end node.</param>
        internal void CloseNode(HtmlNode htmlendnode)
        {
            if (!this.OwnerDocument.OptionAutoCloseOnEnd)
            {
                // close all children
                if (this.childnodes != null)
                {
                    foreach (HtmlNode child in this.childnodes)
                    {
                        if (child.Closed)
                        {
                            continue;
                        }

                        // create a fake closer node
                        var close = new HtmlNode(this.NodeType, this.OwnerDocument, -1);
                        close.EndNode = close;
                        child.CloseNode(close);
                    }
                }
            }

            if (!this.Closed)
            {
                this.EndNode = htmlendnode;

                if (this.OwnerDocument.Openednodes != null)
                {
                    this.OwnerDocument.Openednodes.Remove(this.OuterStartIndex);
                }

                HtmlNode self = this.OwnerDocument.LastNodes.GetDictionaryValueOrNull(this.NodeName);
                if (self == this)
                {
                    this.OwnerDocument.LastNodes.Remove(this.NodeName);
                    this.OwnerDocument.UpdateLastParentNode();
                }

                if (htmlendnode == this)
                {
                    return;
                }

                // create an inner section
                this.InnerStartIndex = this.OuterStartIndex + this.OuterLength;
                this.InnerLength = htmlendnode.OuterStartIndex - this.InnerStartIndex;

                // update full length
                this.OuterLength = (htmlendnode.OuterStartIndex + htmlendnode.OuterLength) - this.OuterStartIndex;
            }
        }

        /// <summary>
        /// The get id.
        /// </summary>
        /// <returns>
        /// The id value.
        /// </returns>
        internal string GetId()
        {
            HtmlAttribute att = this.Attributes["id"];
            return att == null ? string.Empty : att.Value;
        }

        /// <summary>
        /// The set id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        internal void SetId(string id)
        {
            HtmlAttribute att = this.Attributes["id"] ?? this.OwnerDocument.CreateAttribute("id");
            att.Value = id;
            this.OwnerDocument.SetIdForNode(this, att.Value);
            this.OuterChanged = true;
        }

        /// <summary>
        /// The write attribute.
        /// </summary>
        /// <param name="outText">
        /// The out text.
        /// </param>
        /// <param name="att">
        /// The att.
        /// </param>
        internal void WriteAttribute(TextWriter outText, HtmlAttribute att)
        {
            string name;
            string quote = att.QuoteType == AttributeValueQuote.DoubleQuote ? "\"" : "'";
            if (this.OwnerDocument.OptionOutputAsXml)
            {
                name = this.OwnerDocument.OptionOutputUpperCase ? att.XmlName.ToUpper() : att.XmlName;
                if (this.OwnerDocument.OptionOutputOriginalCase)
                {
                    name = att.OriginalName;
                }

                outText.Write(" " + name + "=" + quote + HtmlDocument.HtmlEncode(att.XmlValue) + quote);
            }
            else
            {
                name = this.OwnerDocument.OptionOutputUpperCase ? att.Name.ToUpper() : att.Name;

                if (att.Name.Length >= 4)
                {
                    if ((att.Name[0] == '<') && (att.Name[1] == '%') && (att.Name[att.Name.Length - 1] == '>')
                        && (att.Name[att.Name.Length - 2] == '%'))
                    {
                        outText.Write(" " + name);
                        return;
                    }
                }

                if (this.OwnerDocument.OptionOutputOptimizeAttributeValues)
                {
                    if (att.Value.IndexOfAny(new[] { (char)10, (char)13, (char)9, ' ' }) < 0)
                    {
                        outText.Write(" " + name + "=" + att.Value);
                    }
                    else
                    {
                        outText.Write(" " + name + "=" + quote + att.Value + quote);
                    }
                }
                else
                {
                    outText.Write(" " + name + "=" + quote + att.Value + quote);
                }
            }
        }

        /// <summary>
        /// The write attributes.
        /// </summary>
        /// <param name="outText">
        /// The out text.
        /// </param>
        /// <param name="closing">
        /// The closing.
        /// </param>
        internal void WriteAttributes(TextWriter outText, bool closing)
        {
            if (this.OwnerDocument.OptionOutputAsXml)
            {
                if (this.attributes == null)
                {
                    return;
                }

                // we use Hashitems to make sure attributes are written only once
                foreach (HtmlAttribute att in this.attributes.Hashitems.Values)
                {
                    this.WriteAttribute(outText, att);
                }

                return;
            }

            if (!closing)
            {
                if (this.attributes != null)
                {
                    foreach (HtmlAttribute att in this.attributes)
                    {
                        this.WriteAttribute(outText, att);
                    }
                }

                if (!this.OwnerDocument.OptionAddDebuggingAttributes)
                {
                    return;
                }

                this.WriteAttribute(outText, this.OwnerDocument.CreateAttribute("_closed", this.Closed.ToString(CultureInfo.InvariantCulture)));
                this.WriteAttribute(outText, this.OwnerDocument.CreateAttribute("_children", this.ChildNodes.Count.ToString(CultureInfo.InvariantCulture)));

                int i = 0;
                foreach (HtmlNode n in this.ChildNodes)
                {
                    this.WriteAttribute(outText, this.OwnerDocument.CreateAttribute("_child_" + i, n.NodeName));
                    i++;
                }
            }
            else
            {
                if (this.EndNode == null || this.EndNode.attributes == null || this.EndNode == this)
                {
                    return;
                }

                foreach (HtmlAttribute att in this.EndNode.attributes)
                {
                    this.WriteAttribute(outText, att);
                }

                if (!this.OwnerDocument.OptionAddDebuggingAttributes)
                {
                    return;
                }

                this.WriteAttribute(outText, this.OwnerDocument.CreateAttribute("_closed", this.Closed.ToString(CultureInfo.InvariantCulture)));
                this.WriteAttribute(outText, this.OwnerDocument.CreateAttribute("_children", this.ChildNodes.Count.ToString(CultureInfo.InvariantCulture)));
            }
        }

        /// <summary>
        /// The get relative XPATH.
        /// </summary>
        /// <returns>
        /// The relative XPATH.
        /// </returns>
        private string GetRelativeXpath()
        {
            if (this.ParentNode == null)
            {
                return this.NodeName;
            }

            if (this.NodeType == HtmlNodeType.Document)
            {
                return string.Empty;
            }

            int i = 1 + this.ParentNode.ChildNodes.Where(node => node.NodeName == this.NodeName).TakeWhile(node => node != this).Count();

            return this.NodeName + "[" + i + "]";
        }
    }
}