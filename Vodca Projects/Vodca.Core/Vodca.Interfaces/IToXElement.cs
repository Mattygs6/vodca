//-----------------------------------------------------------------------------
// <copyright file="IToXElement.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/20/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Xml.Linq;

    /// <summary>
    ///     Convert object to XElement    
    /// </summary>
    /// <example>View code: <br />
    /// <code lang="C#" title="Example">
    /// <![CDATA[
    /// public XElement ToXElement(string root = "Item")
    /// {
    ///     XElement root = new XElement(root);
    /// 
    ///     root.Add(new XAttribute("ID", this.ID));
    ///     root.Add(new XAttribute("ParentID", this.ParentID));
    ///     root.Add(new XAttribute("ContentPath", this.ContentPath));
    ///     root.Add(new XElement("DisplayName", this.DisplayName));
    ///     root.Add(new XElement("Collation", this.Collation));
    /// 
    ///     return root;
    /// }
    /// ]]>
    /// </code>
    /// <code source="..\Vodca.Core\Vodca.Web\Design\IToXElement.cs" title="IToXElement.cs" lang="C#" />
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Code sample")]
    public interface IToXElement
    {
        /// <summary>
        /// Converts current object data to XElement for further Xml modifications
        /// </summary>
        /// <param name="rootname">The root element name.</param>
        /// <returns>
        /// The object instance data as XElement
        /// </returns>
        XElement ToXElement(string rootname = "Item");
    }
}
