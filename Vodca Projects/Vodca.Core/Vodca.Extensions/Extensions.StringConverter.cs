//-----------------------------------------------------------------------------
// <copyright file="Extensions.StringConverter.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     String conversion to typeof Nullable(Byte) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable Byte</returns>
        /// <example>View code: <br />
        ///     byte? id = "245".ConvertToByte();<br />
        ///     byte? id = Extensions.ConvertToByte("245");<br />
        /// </example>
        public static byte? ConvertToByte(this string input)
        {
            if (input.IsValidInteger())
            {
                long temp = Convert.ToInt64(input, CultureInfo.InvariantCulture);
                if (temp <= byte.MaxValue && temp >= byte.MinValue)
                {
                    return (byte)temp;
                }
            }

            return null;
        }

        /// <summary>
        /// Converts a string to a byte value or gets the default if the result is null
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The converted value, or if the Nullable.HasValue property is false, returns defaultValue
        /// </returns>
        public static byte ConvertToByte(this string input, byte defaultValue)
        {
            return input.ConvertToByte().GetValueOrDefault(defaultValue);
        }

        /// <summary>
        ///     String conversion to typeof Nullable(int) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable int</returns>
        /// <example>View code: <br />
        ///     int? id = "24536".ConvertToInt();<br />
        ///     int? id = Extensions.ConvertToInt("24536");<br />
        /// </example>
        public static int? ConvertToInt(this string input)
        {
            if (input.IsValidInteger())
            {
                long temp = Convert.ToInt64(input, CultureInfo.InvariantCulture);
                if (temp <= int.MaxValue && temp >= int.MinValue)
                {
                    return (int)temp;
                }
            }

            return null;
        }

        /// <summary>
        /// Converts a string to an int value or gets the default if the result is null
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The converted value, or if the Nullable.HasValue property is false, returns defaultValue
        /// </returns>
        public static int ConvertToInt(this string input, int defaultValue)
        {
            return input.ConvertToInt().GetValueOrDefault(defaultValue);
        }

        /// <summary>
        ///     String conversion to typeof Nullable(long) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable long</returns>
        /// <example>View code: <br />
        ///     long? id = "24536".ConvertToLong();<br />
        ///     long? id = Extensions.ConvertToLong("24536");<br />
        /// </example>
        public static long? ConvertToLong(this string input)
        {
            if (input.IsValidInteger())
            {
                return Convert.ToInt64(input, CultureInfo.InvariantCulture);
            }

            return null;
        }

        /// <summary>
        /// Converts a string to a long value or gets the default if the result is null
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The converted value, or if the Nullable.HasValue property is false, returns defaultValue
        /// </returns>
        public static long ConvertToLong(this string input, long defaultValue)
        {
            return input.ConvertToLong().GetValueOrDefault(defaultValue);
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Double) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable Double</returns>
        /// <example>View code: <br />
        ///     Double? id = "24536.365".ConvertToDouble();<br />
        ///     Double? id = Extensions.ConvertToDouble("24536.365");<br />
        /// </example>
        public static double? ConvertToDouble(this string input)
        {
            if (input.IsValidNumber())
            {
                return Convert.ToDouble(input, CultureInfo.InvariantCulture);
            }

            return null;
        }

        /// <summary>
        /// Converts a string to a double value or gets the default if the result is null
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The converted value, or if the Nullable.HasValue property is false, returns defaultValue
        /// </returns>
        public static double ConvertToDouble(this string input, double defaultValue)
        {
            return input.ConvertToDouble().GetValueOrDefault(defaultValue);
        }

        /// <summary>
        ///     String conversion to typeof Nullable(float) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable float</returns>
        /// <example>View code: <br />
        ///     float? id = "24536.365".ConvertToFloat();<br />
        ///     float? id = Extensions.ConvertToFloat("24536.365");<br />
        /// </example>
        public static float? ConvertToFloat(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string[] digits = input.Trim().Split(new[] { '+', '-', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (digits.Any(item => !Extensions.IsDigits(item)))
                {
                    return null;
                }

                double temp = Convert.ToDouble(input, CultureInfo.InvariantCulture);
                if (temp <= float.MaxValue && temp >= float.MinValue)
                {
                    return (float)temp;
                }
            }

            return null;
        }

        /// <summary>
        /// Converts a string to a float value or gets the default if the result is null
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The converted value, or if the Nullable.HasValue property is false, returns defaultValue
        /// </returns>
        public static float ConvertToFloat(this string input, float defaultValue)
        {
            return input.ConvertToFloat().GetValueOrDefault(defaultValue);
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Decimal) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable Decimal</returns>
        /// <example>View code: <br />
        ///     decimal? id = "24536.365".ConvertToDecimal();<br />
        ///     decimal? id = Extensions.ConvertToDecimal("24536.365");<br />
        /// </example>
        public static decimal? ConvertToDecimal(this string input)
        {
            if (input.IsValidNumber())
            {
                return decimal.Parse(input, CultureInfo.InvariantCulture);
            }

            return null;
        }

        /// <summary>
        /// Converts a string to a decimal value or gets the default if the result is null
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The converted value, or if the Nullable.HasValue property is false, returns defaultValue
        /// </returns>
        public static decimal ConvertToDecimal(this string input, decimal defaultValue)
        {
            return input.ConvertToDecimal().GetValueOrDefault(defaultValue);
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Boolean) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable Boolean</returns>
        /// <example>View code: <br />
        ///     bool? id = "true".ConvertToBoolean();<br />
        ///     bool? id = Extensions.ConvertToBoolean("true");<br />
        /// </example>
        public static bool? ConvertToBoolean(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return string.Equals(input, bool.TrueString, StringComparison.OrdinalIgnoreCase) || string.Equals(input, "1", StringComparison.OrdinalIgnoreCase);
            }

            return null;
        }

        /// <summary>
        /// Converts a string to a Boolean value or gets the default if the result is null
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>
        /// The converted value, or if the Nullable.HasValue property is false, returns defaultValue
        /// </returns>
        public static bool ConvertToBoolean(this string input, bool defaultValue)
        {
            return input.ConvertToBoolean().GetValueOrDefault(defaultValue);
        }

        /// <summary>
        ///     String conversion to typeof Nullable(DateTime) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable Decimal</returns>
        /// <example>View code: <br />
        ///     DateTime? id = Extensions.ConvertToDateTime("07/30/1972");<br />
        ///     DateTime? id = "07/30/1972".ConvertToDateTime();<br />
        /// </example>
        public static DateTime? ConvertToDateTime(this string input)
        {
            if (input.IsValidDateTime())
            {
                return Convert.ToDateTime(input, CultureInfo.InvariantCulture);
            }

            return null;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(DateTime) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <param name="format">Parse format like  'yyyyMMddTHHmmss'</param>
        /// <returns>string param as Nullable Decimal</returns>
        /// <example>View code: <br />
        ///     DateTime? id = Extensions.ConvertToDateTime("07/30/1972");<br />
        ///     DateTime? id = "07/30/1972".ConvertToDateTime();<br />
        /// </example>
        public static DateTime? ConvertToDateTime(this string input, string format)
        {
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    return DateTime.ParseExact(input, format, DateTimeFormatInfo.InvariantInfo);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        ///     String conversion to typeof Nullable(Guid) utility
        /// </summary>
        /// <param name="input">String version of the object</param>
        /// <returns>string param as Nullable Guid</returns>
        /// <example>View code: <br />
        ///     Guid? id = Extensions.ConvertToGuid("CE9693C5-3E6A-40ac-8248-9824547E7229");<br />
        ///     Guid? id = "CE9693C5-3E6A-40ac-8248-9824547E7229".ConvertToGuid();<br />
        /// </example>
        public static Guid? ConvertToGuid(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                string guid = input.RemoveNonLetterOrDigitChars();

                if (!string.IsNullOrEmpty(guid) && guid.Length == 32 && guid.IsValidLettersOrDigits())
                {
                    return Guid.Parse(input);
                }
            }

            return null;
        }

        /// <summary>
        /// Converts the Boolean to bit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The bit value</returns>
        public static string ConvertBooleanToBit(this string value)
        {
            if (string.Equals(value, "yes", StringComparison.InvariantCultureIgnoreCase) || string.Equals(value, "true", StringComparison.InvariantCultureIgnoreCase) || string.Equals(value, "1", StringComparison.InvariantCultureIgnoreCase) || string.Equals(value, "y", StringComparison.InvariantCultureIgnoreCase))
            {
                return "1";
            }

            return "0";
        }

        /// <summary>
        ///     Converts the string representation of the name or numeric value of one or
        /// more enumerated constants to an equivalent enumerated object. A parameter
        /// specifies whether the operation is case-sensitive.
        /// </summary>
        /// <typeparam name="TEnum">The System.Type of the enumeration</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        /// <returns>An object of type enumType whose value is represented by value.</returns>
        public static TEnum ConvertAsEnum<TEnum>(this string value, bool ignoreCase) where TEnum : IComparable, IFormattable
        {
            // From Standard Extensions Library idea
            Ensure.IsNotNullOrEmpty(value, "value");

            Type enumType = typeof(TEnum);

            // If specified type is not enum then throws exception
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("The object isn't an Enum type!");
            }

            return (TEnum)Enum.Parse(enumType, value, ignoreCase);
        }

        /// <summary>
        ///     Converts the string representation of the name or numeric value of one or
        /// more enumerated constants to an equivalent enumerated object. A parameter
        /// specifies whether the operation is case-sensitive.
        /// </summary>
        /// <typeparam name="TEnum">The System.Type of the enumeration</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <returns>An object of type enumType whose value is represented by value.</returns>
        public static TEnum ConvertAsEnum<TEnum>(this string value) where TEnum : IComparable, IFormattable
        {
            return value.ConvertAsEnum<TEnum>(true);
        }

        /// <summary>
        /// Converts to type of TValue
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>The converted value</returns>
        public static TValue ConvertTo<TValue>(this string input)
        {
            return input.ConvertTo(default(TValue));
        }

        /// <summary>
        /// Converts to type of TValue
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="input">The input.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted value</returns>
        public static TValue ConvertTo<TValue>(this string input, TValue defaultValue)
        {
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    return (TValue)converter.ConvertFrom(input);
                }

                converter = TypeDescriptor.GetConverter(typeof(string));
                if (converter.CanConvertTo(typeof(TValue)))
                {
                    return (TValue)converter.ConvertTo(input, typeof(TValue));
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
            }

            return defaultValue;
        }
    }
}
