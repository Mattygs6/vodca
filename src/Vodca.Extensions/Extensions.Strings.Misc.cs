//-----------------------------------------------------------------------------
// <copyright file="Extensions.Strings.Misc.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Determines whether the comparison value string is contained within the input value string
        /// </summary>
        /// <param name="inputValue">The input value.</param>
        /// <param name="comparisonValue">The comparison value.</param>
        /// <param name="comparisonType">Type of the comparison to allow case sensitive or insensitive comparison.</param>
        /// <returns>
        ///     <c>true</c> if input value contains the specified value, otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string inputValue, string comparisonValue, StringComparison comparisonType)
        {
            if (!string.IsNullOrEmpty(inputValue) && !string.IsNullOrEmpty(comparisonValue))
            {
                return inputValue.IndexOf(comparisonValue, comparisonType) != -1;
            }

            return false;
        }

        /// <summary>
        /// Trims the text to a provided maximum length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <param name="lastindexofany">The last index of any character.</param>
        /// <returns>The string with length equal or less max length</returns>
        public static string Truncate(this string value, int maxLength, params char[] lastindexofany)
        {
            /* Accounted for the case: the case then last char is equal one of lastindexofany and max length are equal too */
            if (!string.IsNullOrEmpty(value) && value.Length >= maxLength - 1)
            {
                if (lastindexofany == null || lastindexofany.Length == 0)
                {
                    return value.Substring(0, maxLength);
                }

                string truncated = value.Substring(0, maxLength);
                int index = truncated.LastIndexOfAny(lastindexofany);
                if (index > 1)
                {
                    return truncated.Substring(0, index - 1);
                }

                return truncated;
            }

            return value;
        }

        /// <summary>
        /// Ensures that a string starts with a given prefix.
        /// </summary>
        /// <param name="value">The string value to check.</param>
        /// <param name="prefix">The prefix value to check for.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>
        /// The string value including the prefix
        /// </returns>
        /// <example>View code: <br/>
        ///   <code title="C# File" lang="C#">
        /// var extension = "txt";
        /// var fileName = string.Concat(file.Name, extension.EnsureStartsWith("."));
        ///   </code>
        /// </example>
        public static string EnsureStartsWith(this string value, string prefix, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.StartsWith(prefix, comparison))
                {
                    return value;
                }
            }

            return string.Concat(prefix, value);
        }

        /// <summary>
        /// Ensures that a string ends with a given suffix.
        /// </summary>
        /// <param name="value">The string value to check.</param>
        /// <param name="suffix">The suffix value to check for.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>
        /// The string value including the suffix
        /// </returns>
        /// <example>View code: <br/>
        ///   <code title="C# File" lang="C#">
        /// var dir = Environment.CurrentDirectory;
        /// dir = dir.EnsureEndsWith("/"));
        ///   </code>
        /// </example>
        public static string EnsureEndsWith(this string value, string suffix, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if (value.EndsWith(suffix, comparison))
            {
                return value;
            }

            return string.Concat(value, suffix);
        }

        /// <summary>
        /// Uses regular expressions to split a string into parts.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The regular expression options.</param>
        /// <returns>The splitted string array</returns>
        public static string[] Split(this string value, string regexPattern, RegexOptions options = RegexOptions.None)
        {
            if (!string.IsNullOrWhiteSpace(value) || !string.IsNullOrWhiteSpace(regexPattern))
            {
                // ReSharper disable AssignNullToNotNullAttribute
                return Regex.Split(value, regexPattern, options);
                // ReSharper restore AssignNullToNotNullAttribute
            }

            return null;
        }

        /// <summary>
        /// Splits the given string into words and returns a string array.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>The splitted string array</returns>
        public static string[] GetWords(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Split(@"\W");
            }

            return null;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>The converted bytes to string</returns>
        public static string ConvertToString(this byte[] bytes)
        {
            if (bytes != null)
            {
                return Encoding.UTF8.GetString(bytes);
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns the string's value, or string.Empty if it is null
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns>The string result of the operation</returns>
        public static string GetValueOrEmpty(this string str)
        {
            if (str != null)
            {
                return str;
            }

            return string.Empty;
        }
    }
}