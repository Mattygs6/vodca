//-----------------------------------------------------------------------------
// <copyright file="VTag.Operators.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       02/16/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Xml.Linq;

    /// <summary>
    /// VTag for inline quick HTML writing
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Tag\VTag.cs" title="VTag.cs" lang="C#" />
    /// </example>
    public partial class VTag
    {
        /// <summary>
        /// Performs an implicit conversion from <see cref="Vodca.VTag"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(VTag tag)
        {
            if (tag != null)
            {
                return tag.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Vodca.VTag"/>.
        /// </summary>
        /// <param name="xml">The xml.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator VTag(string xml)
        {
            if (!string.IsNullOrWhiteSpace(xml) && xml.Length > 3 && char.Equals(xml[0], '<'))
            {
                return (VTag)XElement.Parse(xml);
            }

            return null;
        }
    }
}