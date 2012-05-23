//-----------------------------------------------------------------------------
// <copyright file="XmlFileGroup.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/05/2012
//-----------------------------------------------------------------------------
namespace Vodca.YuiCompressor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Setting serialization/desiarilazion class
    /// </summary>
    [Serializable]
    [XmlRoot("filegroup")]
    public sealed partial class XmlFileGroup : IValidate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileGroup"/> class.
        /// </summary>
        public XmlFileGroup()
        {
            this.Files = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            this.HtmlAttributes = new HashSet<XmlSettingsAttribute>();
        }

        /// <summary>
        /// Gets or sets the name of the minified file.
        /// </summary>
        /// <value>
        /// The name of the minified file.
        /// </value>
        [XmlAttribute("minfilename")]
        public string MinifiedFileName { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The Xml serialization rules imposed the restriction")]
        [XmlArray("files")]
        [XmlArrayItem("file")]
        public HashSet<string> Files { get; set; }

        /// <summary>
        /// Gets or sets the HTML attributes.
        /// </summary>
        /// <value>
        /// The HTML attributes.
        /// </value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The Xml serialization rules imposed the restriction")]
        [XmlArray("html")]
        [XmlArrayItem("attribute")]
        public HashSet<XmlSettingsAttribute> HtmlAttributes { get; set; }

        /// <summary>
        /// Gets a flag indicating whether object is valid or not
        /// </summary>
        /// <returns>
        /// True if valid otherwise false
        /// </returns>
        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(this.MinifiedFileName);
        }

        /// <summary>
        /// Gets the compressor action.
        /// </summary>
        /// <returns>The compression action</returns>
        public CompressorAction GetCompressorAction()
        {
            return string.Equals(Path.GetExtension(this.MinifiedFileName), ".css", StringComparison.InvariantCultureIgnoreCase) ? CompressorAction.CssCompression : CompressorAction.JsCompression;
        }
    }
}
