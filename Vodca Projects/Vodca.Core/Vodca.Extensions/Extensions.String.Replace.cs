//-----------------------------------------------------------------------------
// <copyright file="Extensions.String.Replace.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/03/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Replace All non-digit chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="exceptchars">Leave (except) chars in the string.</param>
        /// <returns>Input string without additional chars</returns>
        public static string ReplaceNonDigitsChars(this string input, string replacement, params char[] exceptchars)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptchars.Length == 0)
                {
                    foreach (char current in input)
                    {
                        if (char.IsDigit(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }
                else
                {
                    // Case then has additionalchars
                    var list = new List<char>(exceptchars.Length);
                    list.AddRange(exceptchars);

                    foreach (char current in input)
                    {
                        if (char.IsDigit(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Replace All non-digit chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="exceptchars">Leave (except) chars in the string.</param>
        /// <returns>User input with digits only left</returns>
        public static string ReplaceNonNumberChars(this string input, string replacement, params char[] exceptchars)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int firstindex = input.IndexOf('.');
                if (firstindex > -1)
                {
                    int lastindex = input.LastIndexOf('.');
                    if (firstindex != lastindex)
                    {
                        throw new VHttpArgumentException("The string can't contain more then one '.' char!");
                    }
                }

                var builder = new StringBuilder(input.Length);
                if (exceptchars.Length == 0)
                {
                    for (int i = 0; i < input.Length; i++)
                    {
                        char current = input[i];
                        if (i == 0 && '.'.Equals(current))
                        {
                            /* Add 0 prefix for cases like '.05' */
                            builder.Append(0);
                            builder.Append(current);
                            continue;
                        }

                        if (char.IsDigit(current) || '.'.Equals(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }
                else
                {
                    // Case then has additionalchars
                    var list = new List<char>(exceptchars.Length);
                    list.AddRange(exceptchars);

                    for (int i = 0; i < input.Length; i++)
                    {
                        char current = input[i];
                        if (i == 0 && '.'.Equals(current))
                        {
                            /* Add 0 prefix for cases like '.05' */
                            builder.Append(0);
                            builder.Append(current);
                            continue;
                        }

                        if (char.IsDigit(current) || '.'.Equals(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Replace All non-letter chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="exceptchars">Leave (except) chars in the string</param>
        /// <returns>User input with letters only left</returns>
        public static string ReplaceNonLettersChars(this string input, string replacement, params char[] exceptchars)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptchars.Length == 0)
                {
                    foreach (char current in input)
                    {
                        if (char.IsLetter(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }
                else
                {
                    // Case then has additionalchars
                    var list = new List<char>(exceptchars.Length);
                    list.AddRange(exceptchars);

                    foreach (char current in input)
                    {
                        if (char.IsLetter(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Replace All non-alphanumeric chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="exceptchars">Leave (except) chars in the string.</param>
        /// <returns>User input with Alphanumeric only left</returns>
        public static string ReplaceNonLetterOrDigitChars(this string input, string replacement, params char[] exceptchars)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptchars.Length == 0)
                {
                    foreach (char current in input)
                    {
                        if (char.IsLetterOrDigit(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }
                else
                {
                    // Case then has additionalchars
                    var list = new List<char>(exceptchars.Length);
                    list.AddRange(exceptchars);

                    foreach (char current in input)
                    {
                        if (char.IsLetterOrDigit(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            builder.Append(replacement);
                        }
                    }
                }

                return builder.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Deletes/removes a specified string from the current string
        /// </summary>
        /// <param name="value">Value to modify</param>
        /// <param name="stringToReplace">Substring, which is deleted from value</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns>Modified string</returns>
        public static string Replace(this string value, string stringToReplace, string replacement)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Replace(stringToReplace, replacement);
            }

            return value;
        }
    }
}
