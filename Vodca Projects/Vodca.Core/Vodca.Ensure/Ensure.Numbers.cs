//-----------------------------------------------------------------------------
// <copyright file="Ensure.Numbers.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/10/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Diagnostics;
    using System.Web;
    using Vodca.Annotations;

    /// <content>
    ///     Contains Design by Contract ensure/guarantee methods
    /// </content>
    public static partial class Ensure
    {
        /// <summary>
        ///     Ensure/guarantee that a number is within a particular range.
        /// </summary>
        /// <typeparam name="TObject">The generic object implemented by IComparable interface</typeparam>
        /// <param name="target">The object to check/validate</param>
        /// <param name="min">The min value of the range</param>
        /// <param name="max">The max value of the range</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Numbers.cs" title="Ensure.Numbers.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsBetween<TObject>([NotNull]TObject target, TObject min, TObject max, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException) where TObject : IComparable
        {
            if (target.CompareTo(min) < 0 || target.CompareTo(max) > 0)
            {
                throw new HttpException(statuscode, string.Format("{0} was set to {1}. Must be between {2} and {3}!", paramName, target, min, max));
            }
        }

        /// <summary>
        ///     Ensure/guarantee that a number is within a particular range.
        /// </summary>
        /// <typeparam name="TObject">The generic object implemented by IComparable interface</typeparam>
        /// <param name="target">The object to check/validate</param>
        /// <param name="range">The maximum value allowed.</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Numbers.cs" title="Ensure.Numbers.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsLessThan<TObject>([NotNull]TObject target, TObject range, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException) where TObject : IComparable
        {
            if (target.CompareTo(range) >= 0)
            {
                throw new HttpException(statuscode, string.Format("{0} was set to {1}. Must be less than {2}!", paramName, target, range));
            }
        }

        /// <summary>
        ///     Ensure/guarantee that a number is within a particular range.
        /// </summary>
        /// <typeparam name="TObject">The generic object implemented by IComparable interface</typeparam>
        /// <param name="target">The object to check/validate</param>
        /// <param name="range">The min value allowed.</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Numbers.cs" title="Ensure.Numbers.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsGreaterThanOrEqual<TObject>([NotNull]TObject target, TObject range, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException) where TObject : IComparable
        {
            if (target.CompareTo(range) < 0)
            {
                throw new HttpException(statuscode, string.Format("The parameter '{0}' was set to {1}. Must be more than {2}!", paramName, target, range));
            }
        }

        /// <summary>
        ///     Ensure/guarantee that a number is within a particular range.
        /// </summary>
        /// <typeparam name="TObject">The generic object implemented by IComparable interface</typeparam>
        /// <param name="target">The object to check/validate</param>
        /// <param name="range">The maximum value allowed.</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Numbers.cs" title="Ensure.Numbers.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsLessThanOrEqual<TObject>([NotNull]TObject target, TObject range, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException) where TObject : IComparable
        {
            if (target.CompareTo(range) > 0)
            {
                throw new HttpException(statuscode, string.Format("The parameter '{0}' was set to {1}. Must be more than {2}!", paramName, target, range));
            }
        }
    }
}
