//-----------------------------------------------------------------------------
// <copyright file="Ensure.Strings.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/01/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Diagnostics;
    using System.Web;
    using Vodca.Annotations;

    /// <content>
    ///     Contains Design by Contract ensure/guarantee methods
    /// </content>
    public static partial class Ensure
    {
        /// <summary>
        ///     Ensure/Guarantee that a specified string is not null or empty.
        /// </summary>
        /// <param name="target">The string to check/validate</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Strings.cs" title="Ensure.Strings.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsNotNullOrEmpty([NotNull]string target, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentNullException)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new HttpException(statuscode, string.Format("The parameter '{0}' can't be null or empty", paramName));
            }
        }

        /// <summary>
        ///     Ensure/Guarantee that the specified string Is Null Or Empty AND doesn't exceed a specified length.
        /// </summary>
        /// <param name="target">String containing the data to check/validate.</param>
        /// <param name="maxLength">The Max Length of string</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Strings.cs" title="Ensure.Strings.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsNotNullOrEmptyOrStringMaxLength([NotNull]string target, int maxLength, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentNullException)
        {
            if (string.IsNullOrEmpty(target) || target.Length > maxLength)
            {
                throw new HttpException(statuscode, string.Format("The parameter {0} can't be null or empty or string max length must not exceed {1} char", paramName, maxLength));
            }
        }

        /// <summary>
        ///     Ensure/Guarantee that the specified string has specified length.
        /// </summary>
        /// <param name="target">String containing the data to check/validate.</param>
        /// <param name="length">The Length of string</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Strings.cs" title="Ensure.Strings.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void MaxStringLength([NotNull]string target, int length, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException)
        {
            if (!string.IsNullOrEmpty(target) && target.Length > length)
            {
                throw new HttpException(statuscode, string.Format("The string '{1} Length must be equal {0}.", length, paramName));
            }
        }

        /// <summary>
        ///     Ensure/Guarantee that the specified string is Not NullOrEmpty and doesn't exceed a specified length.
        /// </summary>
        /// <param name="target">String containing the data to validate.</param>
        /// <param name="maxLength">The Max Length of string</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Strings.cs" title="Ensure.Strings.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsNullOrEmptyAndStringMaxLength([NotNull]string target, int maxLength, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException)
        {
            if (string.IsNullOrEmpty(target) || target.Length > maxLength)
            {
                throw new HttpException(statuscode, string.Format("The string '{1}' parameter can be null or empty or string max length must not exceed {0} char", maxLength, paramName));
            }
        }

        /// <summary>
        ///     Ensure/Guarantee that a specified string length is more or equal min Length.
        /// </summary>
        /// <param name="target">The string to check/validate</param>
        /// <param name="minLength">The min length of the string</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Strings.cs" title="Ensure.Strings.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsMinLength([NotNull]string target, int minLength, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException)
        {
            if (!string.IsNullOrEmpty(target) && target.Length >= minLength)
            {
                throw new HttpException(statuscode, string.Format("Min length of the '{1}' must be equal or greater than {0}.", minLength, paramName));
            }
        }
    }
}
