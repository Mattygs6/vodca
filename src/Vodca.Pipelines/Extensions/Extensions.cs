//-----------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       02/10/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The pipeline extensions
    /// </summary>
    internal static partial class InternalExtensions
    {
        /// <summary>
        /// Gets the VTask pipeline attributes from an assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The list of VTaskPipelines</returns>
        public static IEnumerable<VRegisterTaskPipelineAttribute> GetRegisterVTaskPipelineAttributes(this Assembly assembly)
        {
            return assembly.GetCustomAttributes(typeof(VRegisterTaskPipelineAttribute), inherit: false).OfType<VRegisterTaskPipelineAttribute>();
        }
    }
}
