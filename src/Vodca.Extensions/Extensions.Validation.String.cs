//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.String.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Rule determines whether the specified string Is NOT Null Or Empty AND doesn't exceed a specified length.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <param name="maxLength">The Max Length of string</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// An extension method attached to any typeof(string) object instance 
        ///     bool isvalid = "some text".IsNotNullOrEmptyAndMaxLength(50);
        /// OR
        ///     if(this.Page.TextNotes.Text.IsNotNullOrEmptyAndMaxLength(50))
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsNotNullOrEmptyAndMaxLength("some text", 50 );
        /// ]]>
        /// </code>
        /// </example>
        public static bool IsNotNullOrEmptyAndMaxLength(this string input, int maxLength)
        {
            return !string.IsNullOrEmpty(input) && input.Length <= maxLength;
        }

        /// <summary>
        ///     Rule determines whether the specified string is IsNullOrEmpty and doesn't exceed a specified length.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <param name="maxLength">The Max Length of string</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// <![CDATA[
        /// /* An extension method attached to any typeof(string) object instance */ 
        ///     bool isvalid = "some text".IsNullOrEmptyAndMaxLength(50);
        /// OR
        ///     if(this.Page.TextNotes.Text.IsNullOrEmptyAndMaxLength(50))
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsNullOrEmptyAndMaxLength("some text", 50 );
        /// ]]>
        /// </code>
        /// </example>
        public static bool IsNullOrEmptyAndMaxLength(this string input, int maxLength)
        {
            return string.IsNullOrEmpty(input) || (!string.IsNullOrEmpty(input) && input.Length <= maxLength);
        }
    }
}