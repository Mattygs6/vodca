//-----------------------------------------------------------------------------
// <copyright file="DebuggerProxy.cs" company="GenuineInteractive">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/05/2010
//-----------------------------------------------------------------------------
namespace Vodca.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Visual Studio Debugger utility
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.VisualStudio\DebuggerProxy.cs" title="DebuggerProxy.cs" lang="C#" />
    /// </example>
    [GuidAttribute("3C5E80BA-CAD2-4E58-92BF-81D6CEFD8E1E")]
    internal sealed partial class CollectionDebuggerProxy : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionDebuggerProxy"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Visual Studio Debugger Visualizer")]
        public CollectionDebuggerProxy(VNameValueCollection collection)
        {
            var data = new DataTable("VNameValueCollection");
            data.Columns.Add(new DataColumn("Key", typeof(string)));
            data.Columns.Add(new DataColumn("Value", typeof(string)));

            if (collection != null)
            {
                foreach (var item in collection.ToList())
                {
                    data.Rows.Add(item.Key, item.Value);
                }
            }

            this.DebuggerData = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionDebuggerProxy"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Visual Studio Debugger Visualizer")]
        public CollectionDebuggerProxy(VStringDictionary collection)
        {
            var data = new DataTable("VStringDictionary");
            data.Columns.Add(new DataColumn("Key", typeof(string)));
            data.Columns.Add(new DataColumn("Value", typeof(string)));

            if (collection != null)
            {
                foreach (var item in collection.ToArray())
                {
                    data.Rows.Add(item.Key, item.Value);
                }
            }

            this.DebuggerData = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionDebuggerProxy"/> class.
        /// </summary>
        /// <param name="collection">The primitive collection.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Visual Studio Debugger Visualizer")]
        public CollectionDebuggerProxy(IEnumerable<Guid> collection)
        {
            var data = new DataTable("IEnumerable<Guid>");

            data.Columns.Add(new DataColumn("Value", typeof(string)));

            if (collection != null)
            {
                foreach (var item in collection)
                {
                    data.Rows.Add(item);
                }
            }

            this.DebuggerData = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The Debugger data.</value>
        public DataTable DebuggerData { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.DebuggerData != null)
            {
                this.DebuggerData.Dispose();
            }
        }
    }
}
