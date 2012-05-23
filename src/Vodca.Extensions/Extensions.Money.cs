//-----------------------------------------------------------------------------
// <copyright file="Extensions.Money.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       08/24/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Extension methods partial class.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Format value as the money.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The float formatted as money</returns>
        [DebuggerHidden]
        public static string ToMoney(this float? value)
        {
            return string.Format("{0:C}", value);
        }

        /// <summary>
        /// Format value as the money.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The float formatted as money</returns>
        [DebuggerHidden]
        public static string ToMoney(this float value)
        {
            return string.Format("{0:C}", value);
        }

        /// <summary>
        /// Format value as the money.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The double formatted as money</returns>
        [DebuggerHidden]
        public static string ToMoney(this double? value)
        {
            return string.Format("{0:C}", value);
        }

        /// <summary>
        /// Format value as the money.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The double formatted as money</returns>
        [DebuggerHidden]
        public static string ToMoney(this double value)
        {
            return string.Format("{0:C}", value);
        }

        /// <summary>
        /// Format value as the money.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The decimal formatted as money</returns>
        [DebuggerHidden]
        public static string ToMoney(this decimal? value)
        {
            return string.Format("{0:C}", value);
        }

        /// <summary>
        /// Format value as the money.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The decimal formatted as money</returns>
        [DebuggerHidden]
        public static string ToMoney(this decimal value)
        {
            return string.Format("{0:C}", value);
        }
    }
}
