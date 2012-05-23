//-----------------------------------------------------------------------------
// <copyright file="Ensure.IPAddress.cs" company="genuine">
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
        ///     Ensure/guarantee that a string is valid IP Address.
        /// </summary>
        /// <param name="ipaddress">The string to check/validate</param>
        /// <param name="paramName">The parameter name</param>
        /// <param name="statuscode">The status code.</param>
        /// <example>View code: <br />
        /// <code source="..\Vodca.Core\Vodca.Ensure\Ensure.IPAddress.cs" title="Ensure.IPAddress.cs" lang="C#" />
        /// </example>
        [DebuggerHidden]
        public static void IsValidIpAddress([NotNull]string ipaddress, string paramName, int statuscode = VHttpStatusCodeExtension.ArgumentException)
        {
            if (!ipaddress.IsValidIPAddress())
            {
                throw new HttpException(statuscode, "String isn't a valid IP address!: " + paramName);
            }
        }
    }
}
