//-----------------------------------------------------------------------------
// <copyright file="XmlSettings.cs" company="genuine">
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
    using System.Xml.Serialization;

    /// <summary>
    /// Setting serialization/deserilazion class
    /// </summary>
    [Serializable]
    [XmlRoot("yuicompressor")]
    public sealed partial class XmlSettings
    {
        /// <summary>
        ///  A flags indicating whether debug version
        /// </summary>
        private bool debugVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSettings"/> class.
        /// </summary>
        public XmlSettings()
        {
            this.FileGroups = new HashSet<XmlFileGroup>();
        }

        /// <summary>
        /// Gets or sets the file groups.
        /// </summary>
        /// <value>
        /// The file groups.
        /// </value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The Xml serialization rules imposed the restriction")]
        [XmlArray("filegroups")]
        [XmlArrayItem("filegroup")]
        public HashSet<XmlFileGroup> FileGroups { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether debug version.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug version]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute("debug")]
        public bool DebugVersion
        {
            get
            {
                return this.debugVersion;
            }

            set
            {
#if DEBUG
                this.debugVersion = value;
#else
                this.debugVersion = false;
#endif
            }
        }
    }
}
