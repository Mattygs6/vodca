//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Currency.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>  
        ///  A description of the Balance regular expression: 
        ///  Beginning of line or string
        ///  [1]: A numbered capture group. [-], zero or one repetitions
        ///      -
        ///  Any digit, one or more repetitions
        ///  [2]: A numbered capture group. [\.\d{1,2}], zero or one repetitions
        ///      \.\d{1,2}
        ///          Literal .
        ///          Any digit, between 1 and 2 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternBalance = @"^\d+(\.\d{1,2})?$";

        /// <summary>   
        ///  A description of the Currency regular expression:  
        ///  ^\$?
        ///      Beginning of line or string
        ///      Literal $, zero or one repetitions
        ///  [1]: A numbered capture group. [[1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?]
        ///      Select from 4 alternatives
        ///          [1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?
        ///              Any character in this class: [1-9], exactly 1 repetitions
        ///              Any character in this class: [0-9], between 0 and 2 repetitions
        ///              [2]: A numbered capture group. [\,[0-9]{3}], any number of repetitions
        ///                  \,[0-9]{3}
        ///                      Literal ,
        ///                      Any character in this class: [0-9], exactly 3 repetitions
        ///              [3]: A numbered capture group. [\.[0-9]{0,2}], zero or one repetitions
        ///                  \.[0-9]{0,2}
        ///                      Literal .
        ///                      Any character in this class: [0-9], between 0 and 2 repetitions
        ///          [1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?
        ///              Any character in this class: [1-9], exactly 1 repetitions
        ///              Any character in this class: [0-9], at least 0 repetitions
        ///              [4]: A numbered capture group. [\.[0-9]{0,2}], zero or one repetitions
        ///                  \.[0-9]{0,2}
        ///                      Literal .
        ///                      Any character in this class: [0-9], between 0 and 2 repetitions
        ///          0(\.[0-9]{0,2})?
        ///              0
        ///              [5]: A numbered capture group. [\.[0-9]{0,2}], zero or one repetitions
        ///                  \.[0-9]{0,2}
        ///                      Literal .
        ///                      Any character in this class: [0-9], between 0 and 2 repetitions
        ///          [6]: A numbered capture group. [\.[0-9]{1,2}], zero or one repetitions
        ///              \.[0-9]{1,2}
        ///                  Literal .
        ///                  Any character in this class: [0-9], between 1 and 2 repetitions
        ///  End of line or string
        /// </summary>
        private const string RegexPatternCurrency = @"^\$?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$";

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexCurrency;

        /// <summary>
        ///     Regular Expression holder
        /// </summary>
        private static Regex regexBalance;

        /// <summary>
        ///    Gets a Currency Regular Expression  
        /// </summary>
        private static Regex RegexCurrency
        {
            get
            {
                return Extensions.regexCurrency ?? (Extensions.regexCurrency = new Regex(Extensions.RegexPatternCurrency, RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///    Gets a Balance Regular Expression  
        /// </summary>
        private static Regex RegexBalance
        {
            get
            {
                return Extensions.regexBalance ?? (Extensions.regexBalance = new Regex(Extensions.RegexPatternBalance, RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled));
            }
        }

        /// <summary>
        ///     Rule determines whether the specified string contains currency only
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the string is integer; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// <pre>
        /// Valid entries:
        ///         562
        ///         562.1
        ///         562.36
        /// </pre>
        /// </remarks> 
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// An extension method attached to any typeof(string) object instance 
        ///     bool isvalid = "546.90".IsValidCurrency();
        /// OR
        ///     if(this.Page.TextBox.Text.IsValidCurrency())
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsValidCurrency("568.90");
        /// </code>
        /// </example>
        public static bool IsValidCurrency(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexCurrency.IsMatch(input);
        }

        /// <summary>
        ///     Rule determines whether the specified string contains balance only ( a positive or negative currency amount).
        /// </summary>
        /// <param name="input">string containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if the string is integer; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///     Valid inputs:
        ///         562
        ///         562.1
        ///         562.36
        ///         -562
        ///         -562.1
        ///         -562.36     
        /// </remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1631:DocumentationMustMeetCharacterPercentage", Justification = "Reviewed. Suppression is OK here.")]
        public static bool IsValidBalance(this string input)
        {
            return !string.IsNullOrEmpty(input) && Extensions.RegexBalance.IsMatch(input);
        }
    }
}