//-----------------------------------------------------------------------------
// <copyright file="Extensions.ToXElement.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/22/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Converts Object to LINQ XElemnent
        /// </summary>
        /// <param name="target">The target Object</param>
        /// <param name="rootname">The root name.</param>
        /// <param name="elemname">The element name.</param>
        /// <returns>
        /// Represents an XML element of the object
        /// </returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.ToXElement.cs" title="C# Source File" lang="C#"/>
        // ReSharper disable ParameterTypeCanBeEnumerable.Global
        public static XElement ToXElement(this ICollection<IToXElement> target, string rootname = "Collection", string elemname = null)
        // ReSharper restore ParameterTypeCanBeEnumerable.Global
        {
            if (target != null)
            {
                var root = new XElement(rootname);

                foreach (var xe in target)
                {
                    root.Add(string.IsNullOrWhiteSpace(elemname) ? xe.ToXElement() : xe.ToXElement(elemname));
                }

                return root;
            }

            return null;
        }

        /// <summary>
        /// Converts collection to the XElement.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="rootname">The rootname.</param>
        /// <param name="elementname">The element name.</param>
        /// <returns>The XElement from string collection</returns>
        public static XElement ToXElement(this IEnumerable<string> target, string rootname, string elementname)
        {
            if (target != null)
            {
                if (string.IsNullOrWhiteSpace(rootname))
                {
                    rootname = "Collection";
                }

                if (string.IsNullOrWhiteSpace(elementname))
                {
                    elementname = "Item";
                }

                var root = new XElement(rootname);
                foreach (var value in target)
                {
                    root.Add(new XElement(elementname, value));
                }

                return root;
            }

            return null;
        }

        /// <summary>
        /// Converts collection to the XElement.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="rootname">The root name.</param>
        /// <param name="elementname">The element name.</param>
        /// <returns>
        /// The XElement from string collection
        /// </returns>
        public static XElement ToXElement<TObject>(this IEnumerable<TObject> target, string rootname, string elementname) where TObject : struct
        {
            if (target != null)
            {
                if (string.IsNullOrWhiteSpace(rootname))
                {
                    rootname = "Collection";
                }

                if (string.IsNullOrWhiteSpace(elementname))
                {
                    elementname = "Item";
                }

                var root = new XElement(rootname);
                foreach (var value in target)
                {
                    root.Add(new XElement(elementname, value));
                }

                return root;
            }

            return null;
        }

        /// <summary>
        /// Converts Object to LINQ XElemnent
        /// </summary>
        /// <param name="target">The target Object</param>
        /// <param name="rootname">The root name.</param>
        /// <param name="elemname">The element name.</param>
        /// <returns>
        /// Represents an XML element of the object
        /// </returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.ToXElement.cs" title="C# Source File" lang="C#"/>
        public static XElement ToXElement(this IEnumerable<IToXElement> target, string rootname = "Collection", string elemname = null)
        {
            if (target != null)
            {
                var root = new XElement(rootname);

                foreach (var xe in target)
                {
                    root.Add(string.IsNullOrWhiteSpace(elemname) ? xe.ToXElement() : xe.ToXElement(elemname));
                }

                return root;
            }

            return null;
        }

        /// <summary>
        /// Converts Object to LINQ XElemnent
        /// </summary>
        /// <param name="target">The target Object</param>
        /// <param name="rootname">The root name.</param>
        /// <returns>
        /// Represents an XML element of the object
        /// </returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.ToXElement.cs" title="C# Source File" lang="C#"/>
        public static XElement ToXElement(this NameValueCollection target, string rootname = "Collection")
        {
            if (target != null)
            {
                var xelement = new XElement(rootname);

                for (int i = 0; i < target.Count; i++)
                {
                    xelement.Add(new XElement(
                        "Pair",
                        new XElement("Key", target.GetKey(i)),
                        new XElement("Value", target[i])));
                }

                return xelement;
            }

            return null;
        }

        /// <summary>
        /// Converts Object to LINQ XElemnent
        /// </summary>
        /// <param name="target">The target Object</param>
        /// <param name="rootname">The root name. Default is 'DataView'</param>
        /// <param name="rowname">The row name. Default is 'DataRowView'</param>
        /// <returns>
        /// Represents an XML element of the object
        /// </returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.ToXElement.cs" title="C# Source File" lang="C#"/>
        public static XElement ToXElement(this DataView target, string rootname = "DataView", string rowname = "DataRowView")
        {
            if (target != null)
            {
                var xarrayelement = new XElement(rootname);

                if (target.Count == 0)
                {
                    return xarrayelement;
                }

                DataColumnCollection columncollection = target.Table.Columns;

                int rowcount = target.Count;
                for (int i = 0; i < rowcount; i++)
                {
                    var row = new XElement(rowname);
                    for (int j = 0; j < columncollection.Count; j++)
                    {
                        row.Add(new XElement(columncollection[j].Caption, target[i][j]));
                    }

                    xarrayelement.Add(row);
                }

                return xarrayelement;
            }

            return null;
        }

        /// <summary>
        /// Converts a DataTable to an XElement.
        /// </summary>
        /// <param name="table">The Data table.</param>
        /// <param name="rootname">The root name.</param>
        /// <param name="rowname">The row name.</param>
        /// <returns>
        /// The equivalent XElement.
        /// </returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.ToXElement.cs" title="C# Source File" lang="C#"/>
        public static XElement ToXElement(this DataTable table, string rootname = "DataTable", string rowname = "DataRow")
        {
            if (table != null)
            {
                var root = new XElement(rootname);

                if (table.Rows.Count > 0)
                {
                    DataColumnCollection columnCollection = table.Columns;

                    var columnnames = new List<string>(columnCollection.Count);
                    columnnames.AddRange(from DataColumn datacolumn in columnCollection select datacolumn.Caption.RemoveNonLetterOrDigitChars());

                    foreach (DataRow tablerow in table.Rows)
                    {
                        var row = new XElement(rowname);
                        object[] rowitems = tablerow.ItemArray;

                        for (int i = 0; i < columnnames.Count; i++)
                        {
                            row.Add(new XElement(columnnames[i], rowitems[i]));
                        }

                        root.Add(row);
                    }
                }

                return root;
            }

            return null;
        }

        /// <summary>
        ///     Converts the <see cref="T:System.Xml.XmlNode"/> to an instance of <see cref="T:System.Xml.Linq.XElement"/>.
        /// </summary>
        /// <param name="instance">The instance of the object.</param>
        /// <returns>The XElement equivalent of the XmlNode</returns>
        /// <code source="..\Vodca.Core\Vodca.Extensions\Extensions.ToXElement.cs" title="C# Source File" lang="C#" />
        public static XElement ToXElement(this XmlNode instance)
        {
            var xmldocument = new XDocument();

            using (XmlWriter xmlWriter = xmldocument.CreateWriter())
            {
                instance.WriteTo(xmlWriter);
            }

            return xmldocument.Root;
        }
    }
}
