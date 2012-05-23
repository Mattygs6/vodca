//-----------------------------------------------------------------------------
// <copyright file="PublicApiAttribute.cs" company="JetBrains">
//     Copyright (c) JetBrains. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.Annotations
{
    using System;

    /// <summary>
    /// This attribute is intended to mark publicly available API which should not be removed and so is treated as used.
    /// </summary>
    [MeansImplicitUse]
    public sealed class PublicApiAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicApiAttribute"/> class.
        /// </summary>
        public PublicApiAttribute()
        {
        }

        // ReSharper disable UnusedParameter.Local

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicApiAttribute"/> class.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public PublicApiAttribute(string comment)
        {
        }

        // ReSharper restore UnusedParameter.Local
    }
}