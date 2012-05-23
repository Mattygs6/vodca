//-----------------------------------------------------------------------------
// <copyright file="HtmlNameTable.cs" company="genuine">
//     Copyright (c) Simon Mourier. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:         HtmlAgilityPack V1.0 - Simon Mourier
//  Modifications:  J.Baltikauskas
//   Date:          04/20/2012
//-----------------------------------------------------------------------------
namespace Vodca.HtmlAgilityPack
{
    using System.Xml;

    /// <summary>
    /// The html name table.
    /// </summary>
    internal partial class HtmlNameTable : XmlNameTable
    {
        /// <summary>
        /// The name table.
        /// </summary>
        private readonly NameTable nametable = new NameTable();

        /// <summary>
        /// When overridden in a derived class, atomizes the specified string and adds it to the XmlNameTable.
        /// </summary>
        /// <param name="array">The name to add.</param>
        /// <returns>
        /// The new atomized string or the existing one if it already exists.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array"/> is null. </exception>
        public override string Add(string array)
        {
            return this.nametable.Add(array);
        }

        /// <summary>
        /// When overridden in a derived class, atomizes the specified string and adds it to the XmlNameTable.
        /// </summary>
        /// <param name="array">The character array containing the name to add.</param>
        /// <param name="offset">Zero-based index into the array specifying the first character of the name.</param>
        /// <param name="length">The number of characters in the name.</param>
        /// <returns>
        /// The new atomized string or the existing one if it already exists. If length is zero, String.Empty is returned.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">0 &gt; <paramref name="offset"/>-or- <paramref name="offset"/> &gt;= <paramref name="array"/>.Length -or- <paramref name="length"/> &gt; <paramref name="array"/>.Length The above conditions do not cause an exception to be thrown if <paramref name="length"/> =0. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="length"/> &lt; 0. </exception>
        public override string Add(char[] array, int offset, int length)
        {
            return this.nametable.Add(array, offset, length);
        }

        /// <summary>
        /// When overridden in a derived class, gets the atomized string containing the same value as the specified string.
        /// </summary>
        /// <param name="array">The name to look up.</param>
        /// <returns>
        /// The atomized string or null if the string has not already been atomized.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array"/> is null. </exception>
        public override string Get(string array)
        {
            return this.nametable.Get(array);
        }

        /// <summary>
        /// When overridden in a derived class, gets the atomized string containing the same characters as the specified range of characters in the given array.
        /// </summary>
        /// <param name="array">The character array containing the name to look up.</param>
        /// <param name="offset">The zero-based index into the array specifying the first character of the name.</param>
        /// <param name="length">The number of characters in the name.</param>
        /// <returns>
        /// The atomized string or null if the string has not already been atomized. If <paramref name="length"/> is zero, String.Empty is returned.
        /// </returns>
        public override string Get(char[] array, int offset, int length)
        {
            return this.nametable.Get(array, offset, length);
        }

        /// <summary>
        /// Gets the or add the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The atomized string </returns>
        internal string GetOrAdd(string array)
        {
            string s = this.Get(array);
            if (s == null)
            {
                return this.Add(array);
            }

            return s;
        }
    }
}