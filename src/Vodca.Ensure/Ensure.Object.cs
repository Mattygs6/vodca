//-----------------------------------------------------------------------------
// <copyright file="Ensure.Object.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/10/2009
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
        ///     Ensure/Guarantee that a specified object is not null.
        /// </summary>
        /// <param name="target">The object to check/validate</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.Object.cs" title="Ensure.Object.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsNotNull([CanBeNull]object target, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentNullException)
        {
            if (target == null)
            {
                throw new HttpException(statuscode, string.Format("The param '{0}' is not valid! Object reference is Null!", paramName));
            }
        }
    }
}
