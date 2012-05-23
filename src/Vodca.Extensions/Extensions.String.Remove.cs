//-----------------------------------------------------------------------------
// <copyright file="Extensions.String.Remove.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/03/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Remove All non-digit chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="appendwhitespaceinstead">if set to <c>true</c> append white space instead non-valid character.</param>
        /// <param name="exceptchars">Leave (except) chars in the string.</param>
        /// <returns>
        /// Input string without additional chars
        /// </returns>
        public static string RemoveNonDigitsChars(this string input, bool appendwhitespaceinstead = false, params char[] exceptchars)
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
                            if (appendwhitespaceinstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
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
                            if (appendwhitespaceinstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        /// <summary>
        /// Remove All non-digit chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="appendwhitespaceinstead">if set to <c>true</c> append white space instead non-valid character.</param>
        /// <param name="exceptchars">Leave (except) chars in the string.</param>
        /// <returns>User input with digits only left</returns>
        public static string RemoveNonNumberChars(this string input, bool appendwhitespaceinstead = false, params char[] exceptchars)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);

                if (exceptchars.Length == 0)
                {
                    foreach (char current in input)
                    {
                        if (char.IsDigit(current) || '.'.Equals(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendwhitespaceinstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
                    var list = new List<char>(exceptchars.Length);
                    list.AddRange(exceptchars);

                    foreach (char current in input)
                    {
                        if (char.IsDigit(current) || '.'.Equals(current) || list.Contains(current))
                        {
                            builder.Append(current);
                        }
                        else
                        {
                            if (appendwhitespaceinstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        /// <summary>
        /// Remove All non-letter chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="appendwhitespaceinstead">if set to <c>true</c> append white space instead non-valid character.</param>
        /// <param name="exceptchars">Leave (except) chars in the string</param>
        /// <returns>
        /// User input with letters only left
        /// </returns>
        public static string RemoveNonLettersChars(this string input, bool appendwhitespaceinstead = false, params char[] exceptchars)
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
                            if (appendwhitespaceinstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
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
                            if (appendwhitespaceinstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        /// <summary>
        /// Remove All non-alphanumeric chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="appendwhitespaceinstead">if set to <c>true</c> append white space instead non-valid character.</param>
        /// <param name="exceptchars">Leave (except) chars in the string.</param>
        /// <returns>User input with Alphanumeric only left</returns>
        public static string RemoveNonLetterOrDigitChars(this string input, bool appendwhitespaceinstead = false, params char[] exceptchars)
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
                            if (appendwhitespaceinstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }
                else
                {
                    // Case then has additional chars
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
                            if (appendwhitespaceinstead)
                            {
                                builder.Append(' ');
                            }
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Remove All illegal non file name chars from string
        /// </summary>
        /// <param name="input">User input</param>
        /// <param name="appendwhitespaceinstead">if set to <c>true</c> append white space instead non-valid character.</param>
        /// <returns>User input with correct chars only</returns>
        public static string RemoveNonFileNameChars(this string input, bool appendwhitespaceinstead = false)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var builder = new StringBuilder(input.Length);
                foreach (char current in input)
                {
                    if (char.IsLetterOrDigit(current) || char.IsWhiteSpace(current) || '-'.Equals(current) || '.'.Equals(current) || '_'.Equals(current))
                    {
                        builder.Append(current);
                    }
                    else
                    {
                        if (appendwhitespaceinstead)
                        {
                            builder.Append(' ');
                        }
                    }
                }

                return builder.ToString().Trim();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Deletes/removes a specified string from the current string
        /// </summary>
        /// <param name="value">Value to modify</param>
        /// <param name="stringToRemove">Substring, which is deleted from value</param>
        /// <returns>Modified string</returns>
        public static string Remove(this string value, string stringToRemove)
        {
            // From Standard Extensions Library idea
            if (!string.IsNullOrEmpty(value))
            {
                return value.Replace(stringToRemove, string.Empty);
            }

            return value;
        }

        /// <summary>
        /// Splits the and trim string input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="chars">The chars to split.</param>
        /// <returns>The collection of strings</returns>
        public static IEnumerable<string> SplitAndTrim(this string input, params char[] chars)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var result = input.Trim().Split(chars, StringSplitOptions.RemoveEmptyEntries);
                return result.Select(x => x.TrimEmptySpaces());
            }

            return new string[] { };
        }

        /// <summary>
        /// HTMLs the decode.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The collection with decoded inputs</returns>
        public static IEnumerable<string> HtmlDecode(this IEnumerable<string> collection)
        {
            if (collection != null)
            {
                foreach (var input in collection)
                {
                    yield return input.HtmlDecode();
                }
            }
        }
    }
}
