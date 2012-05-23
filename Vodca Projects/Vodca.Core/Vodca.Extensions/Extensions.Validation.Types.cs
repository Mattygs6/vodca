//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Types.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///  A description of the regular expression:
        ///  Beginning of line or string
        ///  [1]: A numbered capture group. [\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1}]
        ///      \{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1}
        ///          Literal {, between 0 and 1 repetitions
        ///          [2]: A numbered capture group. [[0-9a-fA-F]], exactly 8 repetitions
        ///              Any character in this class: [0-9a-fA-F]
        ///          -
        ///          [3]: A numbered capture group. [[0-9a-fA-F]], exactly 4 repetitions
        ///              Any character in this class: [0-9a-fA-F]
        ///          -
        ///          [4]: A numbered capture group. [[0-9a-fA-F]], exactly 4 repetitions
        ///              Any character in this class: [0-9a-fA-F]
        ///          -
        ///          [5]: A numbered capture group. [[0-9a-fA-F]], exactly 4 repetitions
        ///              Any character in this class: [0-9a-fA-F]
        ///          -
        ///          [6]: A numbered capture group. [[0-9a-fA-F]], exactly 12 repetitions
        ///              Any character in this class: [0-9a-fA-F]
        ///          Literal }, between 0 and 1 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternGuid = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexGuid;

        /// <summary>
        ///    Gets a Guid Regular Expression  
        /// </summary>
        private static Regex RegexGuid
        {
            get
            {
                return Extensions.regexGuid ?? (Extensions.regexGuid = new Regex(Extensions.RegexPatternGuid, RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string is valid Guid
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the string is Guid; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// /* An extension method attached to any typeof(string) object instance */
        ///     bool isvalid = "A2CC11F4-10D5-4dd2-B746-24FD4B7C4503".IsValidZipCodeFive();
        /// OR
        ///     if(this.Page.HiddenPrimaryID.Value.IsValidGuid())
        ///     {
        ///         // Do something
        ///     }
        /// OR
        ///     bool isvalid = Extensions.IsValidGuid("A2CC11F4-10D5-4dd2-B746-24FD4B7C4503");
        /// </code>    
        /// </example>
        public static bool IsValidGuid(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexGuid.IsMatch(input);
        }

        /// <summary>
        ///     Rule determines whether the specified string is Boolean representation as string 
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns><c>true</c> if it validates; otherwise, <c>false</c>.</returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        ///  /* An extension method attached to any typeof(string) object instance */ 
        ///     bool isvalid = "True".IsValidBoolean();
        ///  OR
        ///     bool isvalid = Extensions.IsValidBoolean("True");
        /// </code>    
        /// </example>
        public static bool IsValidBoolean(this string input)
        {
            return !string.IsNullOrEmpty(input) && (string.Equals(input, bool.TrueString, StringComparison.OrdinalIgnoreCase) || string.Equals(input, bool.FalseString, StringComparison.OrdinalIgnoreCase));
        }
    }
}