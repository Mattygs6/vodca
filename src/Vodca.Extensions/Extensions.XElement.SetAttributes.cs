//-----------------------------------------------------------------------------
// <copyright file="Extensions.XElement.SetAttributes.cs" company="genuine">
//      Copyright (c) Genuine Interactive. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
// Author:      M.Gramolini
//   Date:      11/22/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the attribute values.
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="splitter">The splitter.</param>
        /// <param name="attr">The attr.</param>
        /// <returns>The XElement after setting the values</returns>
        public static XElement SetAttributeValues(this XElement xelement, char splitter = '=', params string[] attr)
        {
            return xelement.SetAttributeValues(attr.Select(s => s.Split(splitter))
                                            .ToDictionary(a => a[0].Trim(), a => a[1].Trim()));
        }

        /// <summary>
        /// Sets the attribute values.
        /// </summary>
        /// <param name="xelement">The XElement instance.</param>
        /// <param name="dict">The dict.</param>
        /// <returns>The XElement after setting the values</returns>
        public static XElement SetAttributeValues(this XElement xelement, IEnumerable<KeyValuePair<string, string>> dict)
        {
            if (dict != null)
            {
                foreach (var kvp in dict)
                {
                    xelement.SetAttributeValue(kvp.Key, kvp.Value);
                }
            }

            return xelement;
        }
    }
}
