//-----------------------------------------------------------------------------
// <copyright file="UsPhoneNumberFormatter.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       04/27/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Us Phone Number Formatter
    /// </summary>
    public sealed partial class UsPhoneNumberFormatter : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns>
        /// An instance of the object specified by <paramref name="formatType"/>, if the <see cref="T:System.IFormatProvider"/> implementation can supply that type of object; otherwise, null.
        /// </returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>
        /// The string representation of the value of <paramref name="arg"/>, formatted as specified by <paramref name="format"/> and <paramref name="formatProvider"/>.
        /// </returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            /* d: 555.555.5555 x0000
             * D: 555-555-5555 x0000
             * P: (555) 555-5555 x0000
             * */

            string result = string.Concat(arg).RemoveNonDigitsChars();
            if (!string.IsNullOrWhiteSpace(result) && result.Length >= 10 && !result.StartsWith("1"))
            {
                var sb = new StringBuilder(21);

                switch (format)
                {
                    case "d":
                        sb.Append(result, 0, 3)
                            .Append('.')
                            .Append(result, 3, 3)
                            .Append('.')
                            .Append(result, 6, 4);
                        break;
                    case "D":
                        sb.Append(result, 0, 3)
                            .Append('-')
                            .Append(result, 3, 3)
                            .Append('-')
                            .Append(result, 6, 4);
                        break;
                    case "P":
                        sb.Append('(')
                            .Append(result, 0, 3)
                            .Append(") ")
                            .Append(result, 3, 3)
                            .Append('-')
                            .Append(result, 6, 4);
                        break;
                    default:
                        return this.HandleOtherFormats(format, arg);
                }

                if (result.Length > 10)
                {
                    sb.Append(" x")
                        .Append(result.Substring(10));
                }

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Handles the other formats.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg">The arg.</param>
        /// <returns>The result string</returns>
        private string HandleOtherFormats(string format, object arg)
        {
            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            if (arg is IFormattable)
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull
            {
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            }

            if (arg != null)
            {
                return arg.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// US Phone Number Formats
        /// </summary>
        public static partial class Formats
        {
            /// <summary>
            /// Dotted Format 555.555.5555 x0000
            /// </summary>
            public const string Dotted = "{0:d}";

            /// <summary>
            /// Dashed Format 555-555-5555 x0000
            /// </summary>
            public const string Dashed = "{0:D}";

            /// <summary>
            /// Parenthesis and Dashed Format (555) 555-5555 x0000
            /// </summary>
            public const string ParenthesisDash = "{0:P}";
        }
    }
}
