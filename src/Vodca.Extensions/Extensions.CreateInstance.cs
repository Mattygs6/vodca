//-----------------------------------------------------------------------------
// <copyright file="Extensions.CreateInstance.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:    J.Baltikauskas
//  Date:       04/24/2012
//-----------------------------------------------------------------------------

namespace Vodca
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Returns an instance of the <paramref name="type"/> on which the method is invoked.
        /// </summary>
        /// <param name="type">The type on which the method was invoked.</param>
        /// <returns>An instance of the <paramref name="type"/>.</returns>
        public static object CreateInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}