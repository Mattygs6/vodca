//-----------------------------------------------------------------------------
// <copyright file="IVComparable.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       5/03/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// Allows for comparison of objects used for VComparableHashSet&lt;TComparable&gt;
    /// </summary>
    /// <example>View code: <br />
    /// <code lang="C#" title="Example">
    /// <![CDATA[
    /// public string GetUniqueJsonString()
    /// {
    ///     return new { PropertyName1 = this.UniqueProperty1, PropertyName2 = this.UniqueProperty2 }.SerializeToJson();
    /// }
    /// 
    /// public int GetComparableHashCode()
    /// {
    ///     return string.Concat("#", this.UniqueProperty1, "#", this.UniqueProperty2, "#").GetHashCode();
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public interface IVComparable
    {
        /// <summary>
        /// Gets the unique json string.
        /// </summary>
        /// <returns>The unique json string</returns>
        string GetUniqueJsonString();

        /// <summary>
        /// Gets the comparable hash code.
        /// </summary>
        /// <returns>The hash code</returns>
        int GetComparableHashCode();
    }
}
