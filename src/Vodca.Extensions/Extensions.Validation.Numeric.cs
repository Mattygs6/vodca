//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Numeric.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------

namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>    
        ///  A description of the Double regular expression: 
        ///  ^-?\d+
        ///      Beginning of line or string
        ///      -, zero or one repetitions
        ///      Any digit, one or more repetitions
        ///  [1]: A numbered capture group. [\.\d+], zero or one repetitions
        ///      \.\d+
        ///          Literal .
        ///          Any digit, one or more repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternDouble = @"^-{0,1}\d{1,19}(\.\d{1,})?$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexDouble;

        /// <summary>
        ///    Gets as Double Regular Expression  
        /// </summary>
        private static Regex RegexDouble
        {
            get
            {
                return Extensions.regexDouble ?? (Extensions.regexDouble = new Regex(Extensions.RegexPatternDouble, RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string contains positive integer (greater or equal then zero)
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the string is positive integer; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// An extension method attached to any typeof(string) object instance 
        ///     bool isvalid = "2766".IsValidDigits();
        /// OR
        ///     if(this.Page.TextBoxNumeric.Text.IsValidDigits())
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsValidDigits("8789");
        /// </code>
        /// </example>
        public static bool IsValidDigits(this string input)
        {
            // long.MaxValue.ToString() = "9223372036854775807"
            // "9223372036854775807".Length = 19
            return !string.IsNullOrEmpty(input) && input.Length <= 19 && Extensions.IsDigits(input);
        }

        /// <summary>
        ///     Rule determines whether the specified string contains integer only
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the string is integer; otherwise, <c>false</c>.
        /// </returns>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// An extension method attached to any typeof(string) object instance 
        ///     bool isvalid = "2766".IsValidInteger();
        /// OR
        ///     if(this.Page.TextBoxNumeric.Text.IsValidInteger())
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsValidInteger("8789");
        /// </code>
        /// </example>
        public static bool IsValidInteger(this string input)
        {
            // long.MaxValue.ToString() = "9223372036854775807"
            // "9223372036854775807".Length = 19
            if (!string.IsNullOrEmpty(input) && input.Length <= 19)
            {
                if (input.Length == 1)
                {
                    return char.IsDigit(input[0]);
                }

                return (char.IsDigit(input[0]) || input[0] == '+' || input[0] == '-')
                       && Extensions.IsDigits(input.Substring(1));
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string contains double target only
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the string is double; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// <pre>
        /// Valid entries:
        ///         562
        ///         562.1
        ///         562.36
        ///         562.09876303
        ///         -562
        ///         -562.1
        ///         -562.36
        ///         -562.09876303
        /// </pre>
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// /* An extension method attached to any typeof(string) object instance  */
        ///     bool isvalid = "2766".IsValidNumber();
        /// OR
        ///     if(this.Page.TextBoxNumeric.Text.IsValidNumber())
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsValidNumber("2766");
        /// </code>    
        /// </example>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1631:DocumentationMustMeetCharacterPercentage", Justification = "Reviewed. Suppression is OK here.")]
        public static bool IsValidNumber(this string input)
        {
            // long.MaxValue.ToString() = "9223372036854775807"
            // "9223372036854775807".Length = 19
            return !string.IsNullOrEmpty(input) && Extensions.RegexDouble.IsMatch(input);
        }

        /// <summary>
        ///     This is supplementary function
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the string is digits only; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsDigits(string input)
        {
            // Assume that input not null or empty!
            return input.All(char.IsDigit);
        }
    }
}