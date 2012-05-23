//-----------------------------------------------------------------------------
// <copyright file="Ensure.Email.cs" company="genuine">
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
        ///     Ensure method
        /// </summary>
        /// <param name="target">The target to validate</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Email.cs" title="Ensure.Email.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsValidEmail(string target, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentNullException)
        {
            if (!target.IsValidEmail())
            {
                throw new HttpException(statuscode, "The parameter '" + paramName + "' must be valid email");
            }
        }

        /// <summary>
        ///     Ensure method
        /// </summary>
        /// <param name="target">The target to validate</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Email.cs" title="Ensure.Email.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsValidEmailOptional(string target, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentNullException)
        {
            if (!target.IsValidEmailOptional())
            {
                throw new HttpException(statuscode, "The parameter '" + paramName + "' must be valid email");
            }
        }
    }
}
