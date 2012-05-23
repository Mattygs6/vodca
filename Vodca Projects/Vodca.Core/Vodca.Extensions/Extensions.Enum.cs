//-----------------------------------------------------------------------------
// <copyright file="Extensions.Enum.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        ///     Converts int, byte, long and etc to enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The default or converted instance of the enum</returns>
        public static TEnum ConvertToEnum<TEnum>(this string value)
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value);
            }

            return default(TEnum);
        }

        /// <summary>
        /// Converts to enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The default enum or converted instance of the enum</returns>
        public static TEnum ConvertToEnum<TEnum>(this string value, TEnum defaultValue)
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value);
            }

            return defaultValue;
        }
    }
}
