//-----------------------------------------------------------------------------
// <copyright file="HtmlAttribute.cs" company="genuine">
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
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents an HTML attribute.
    /// </summary>
    [DebuggerDisplay("Name: {OriginalName}, Value: {Value}")]
    public class HtmlAttribute : IComparable
    {
        /// <summary>
        ///  The value.
        /// </summary>
        private string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttribute"/> class.
        /// </summary>
        /// <param name="ownerdocument">The owner document.</param>
        internal HtmlAttribute(HtmlDocument ownerdocument)
        {
            this.QuoteType = AttributeValueQuote.DoubleQuote;
            this.OwnerDocument = ownerdocument;
        }

        /// <summary>
        ///   Gets a valid XPath string that points to this Attribute
        /// </summary>
        public string XPath
        {
            get
            {
                string basePath = (this.OwnerNode == null) ? "/" : this.OwnerNode.XPath + "/";
                return basePath + this.GetRelativeXpath();
            }
        }

        /// <summary>
        /// Gets or sets the qualified name of the attribute.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                if (this.OriginalName == null)
                {
                    this.OriginalName = this.OwnerDocument.Text.Substring(this.NameStartIndex, this.NameLength);
                }

                return this.OriginalName.ToLower();
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.OriginalName = value;
                if (this.OwnerNode != null)
                {
                    this.OwnerNode.InnerChanged = true;
                    this.OwnerNode.OuterChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of quote the data should be wrapped in
        /// </summary>
        /// <value>
        /// The type of the quote.
        /// </value>
        public AttributeValueQuote QuoteType { get; set; }

        /// <summary>
        ///   Gets the line number of this attribute in the document.
        /// </summary>
        public int Line { get; internal set; }

        /// <summary>
        ///   Gets the column number of this attribute in the document.
        /// </summary>
        public int LinePosition { get; internal set; }

        /// <summary>
        /// Gets the name of attribute with original case
        /// </summary>
        /// <value>
        /// The name of the original.
        /// </value>
        public string OriginalName { get; internal set; }

        /// <summary>
        ///   Gets the HTML document to which this attribute belongs.
        /// </summary>
        public HtmlDocument OwnerDocument { get; internal set; }

        /// <summary>
        ///   Gets the HTML node to which this attribute belongs.
        /// </summary>
        public HtmlNode OwnerNode { get; internal set; }

        /// <summary>
        ///   Gets the stream position of this attribute in the document, relative to the start of the document.
        /// </summary>
        public int StreamPosition { get; internal set; }

        /// <summary>
        ///   Gets or sets the value of the attribute.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value ?? (this.value = this.OwnerDocument.Text.Substring(this.ValueStartIndex, this.ValueLength));
            }

            set
            {
                this.value = value;
                if (this.OwnerNode != null)
                {
                    this.OwnerNode.InnerChanged = true;
                    this.OwnerNode.OuterChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets XmlName.
        /// </summary>
        internal string XmlName
        {
            get
            {
                return HtmlDocument.GetXmlName(this.Name);
            }
        }

        /// <summary>
        /// Gets XmlValue.
        /// </summary>
        internal string XmlValue
        {
            get
            {
                return this.Value;
            }
        }

        /// <summary>
        /// Gets or sets the length of the name.
        /// </summary>
        /// <value>
        /// The length of the name.
        /// </value>
        internal int NameLength { get; set; }

        /// <summary>
        /// Gets or sets the start index of the name.
        /// </summary>
        /// <value>
        /// The start index of the name.
        /// </value>
        internal int NameStartIndex { get; set; }

        /// <summary>
        /// Gets or sets the value length.
        /// </summary>
        /// <value>
        /// The value length.
        /// </value>
        internal int ValueLength { get; set; }

        /// <summary>
        /// Gets or sets the value start index.
        /// </summary>
        /// <value>
        /// The value start index.
        /// </value>
        internal int ValueStartIndex { get; set; }

        /// <summary>
        /// Creates a duplicate of this attribute.
        /// </summary>
        /// <returns>
        /// The cloned attribute. 
        /// </returns>
        public HtmlAttribute Clone()
        {
            return new HtmlAttribute(this.OwnerDocument) { Name = this.Name, Value = this.Value };
        }

        /// <summary>
        /// Compares the current instance with another attribute. Comparison is based on attributes' name.
        /// </summary>
        /// <param name="obj">
        /// An attribute to compare with this instance. 
        /// </param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the names comparison. 
        /// </returns>
        public int CompareTo(object obj)
        {
            var att = obj as HtmlAttribute;
            if (att == null)
            {
                throw new ArgumentException("obj");
            }

            return string.CompareOrdinal(this.Name, att.Name);
        }

        /// <summary>
        /// Removes this attribute from it's parents collection
        /// </summary>
        public void Remove()
        {
            this.OwnerNode.Attributes.Remove(this);
        }

        /// <summary>
        /// Gets the relative XPATH.
        /// </summary>
        /// <returns>
        /// The relative XPATH.
        /// </returns>
        private string GetRelativeXpath()
        {
            if (this.OwnerNode == null)
            {
                return this.Name;
            }

            int i = 1 + this.OwnerNode.Attributes.Where(node => node.Name == this.Name).TakeWhile(node => node != this).Count();

            return "@" + this.Name + "[" + i + "]";
        }
    }
}