//-----------------------------------------------------------------------------
// <copyright file="VHttpStatusCodeExtension.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       01/01/2009
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    ///     The extended standard Http codes 
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Ensure\HttpExceptions\VHttpStatusCodeExtension.cs" title="VHttpStatusCodeExtension.cs" lang="C#" />
    /// </example>
    /// <remarks>
    /// Originally it was enumerator type. However, it create problem with Microsoft Style cop for the default method parameters.
    /// </remarks>
    public struct VHttpStatusCodeExtension
    {
        /// <summary>
        ///     The Server Error 5xx
        /// </summary>
        public const int ArgumentNullException = 550;

        /// <summary>
        ///     The Server Error 5xx
        /// </summary>
        public const int ArgumentException = 551;

        /// <summary>
        ///     The Server Error 5xx
        /// </summary>
        public const int NotSupportedException = 552;
    }
}
