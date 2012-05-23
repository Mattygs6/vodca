//-----------------------------------------------------------------------------
// <copyright file="Ensure.Zip.cs" company="genuine">
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

    /// <content>
    ///     Contains Design by Contract ensure/guarantee methods
    /// </content>
    public static partial class Ensure
    {
        /// <summary>
        ///     Exception Message
        /// </summary>
        private const string MsgStringMustBeValidZip = "The parameter '{0}' must be valid zip code!";

        /// <summary>
        ///     Ensure method
        /// </summary>
        /// <param name="target">The target to validate</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Zip.cs" title="Ensure.Zip.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsZipCodeFive(string target, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException)
        {
            if (!target.IsValidZipCodeFive())
            {
                throw new HttpException(statuscode, string.Format(MsgStringMustBeValidZip, paramName));
            }
        }

        /// <summary>
        ///     Ensure method
        /// </summary>
        /// <param name="target">The target to validate</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Zip.cs" title="Ensure.Zip.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsZipCodeFiveOptional(string target, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException)
        {
            if (!target.IsValidZipCodeFiveOptional())
            {
                throw new HttpException(statuscode, string.Format(MsgStringMustBeValidZip, paramName));
            }
        }
    }
}
