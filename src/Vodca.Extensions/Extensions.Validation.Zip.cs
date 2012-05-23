//-----------------------------------------------------------------------------
// <copyright file="Extensions.Validation.Zip.cs" company="genuine">
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
        ///     Rule determines whether the specified string is a valid US Zip Code, using the 5 digit format only.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// <pre>
        /// Valid entries:
        ///     02780
        ///     02766
        /// </pre>
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// An extension method attached to any typeof(string) object instance 
        ///     bool isvalid = "02766".IsValidZipCodeFive();
        /// OR
        ///     if(this.Page.TextBoxZip.Text.IsValidZipCodeFive())
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsValidZipCodeFive("02766");
        /// </code>
        /// </example>
        public static bool IsValidZipCodeFive(this string input)
        {
            if (!string.IsNullOrEmpty(input) && input.Length == 5)
            {
                char[] inputchars = input.ToCharArray();
                for (int i = 0; i < 5; i++)
                {
                    if (!char.IsDigit(inputchars[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Rule determines whether the specified string is a valid US Zip Code, using the 5 digit format only OR Empty OR Null strings.
        /// </summary>
        /// <param name="input">String containing the data to validate.</param>
        /// <returns>
        ///     <c>true</c> if valid; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// <pre>
        /// Valid entries:
        ///     "" --Empty string
        ///     02780
        ///     02766
        /// </pre>
        /// </remarks>
        /// <example>View code: <br />
        /// <code title="C# File" lang="C#">
        /// /* An extension method attached to any typeof(string) object instance */
        ///     bool isvalid = "02766".IsValidZipCodeFiveOptional();
        /// OR
        ///     if(this.Page.TextBoxZip.Text.IsValidZipCodeFiveOptional())
        ///     {
        ///         // Do something
        ///     }
        ///  OR
        ///     bool isvalid = Extensions.IsValidZipCodeFiveOptional("02766");
        /// </code>   
        /// </example> 
        public static bool IsValidZipCodeFiveOptional(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.IsValidZipCodeFive();
            }

            return true;
        }
    }
}