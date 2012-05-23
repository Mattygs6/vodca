//-----------------------------------------------------------------------------
// <copyright file="Ensure.Boolean.cs" company="genuine">
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
        /// Ensure method
        /// </summary>
        /// <param name="target">The target to validate</param>
        /// <param name="message">The Exception message</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        ///   <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Boolean.cs" title="Ensure.Boolean.cs" lang="C#"/>
        /// </example>
        [DebuggerHidden]
        public static void IsTrue(bool target, string message, int statuscode = VHttpStatusCodeExtension.ArgumentException)
        {
            if (!target)
            {
                throw new HttpException(statuscode, message);
            }
        }
    }
}
