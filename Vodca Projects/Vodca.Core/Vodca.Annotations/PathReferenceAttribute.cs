//-----------------------------------------------------------------------------
// <copyright file="PathReferenceAttribute.cs" company="JetBrains">
//     Copyright (c) JetBrains. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.Annotations
{
    /// <summary>
    /// Re-sharper Annotation
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Parameter)]
    public class PathReferenceAttribute : System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PathReferenceAttribute"/> class.
        /// </summary>
        public PathReferenceAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathReferenceAttribute"/> class.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        public PathReferenceAttribute([PathReference] string basePath)
        {
            this.BasePath = basePath;
        }

        /// <summary>
        /// Gets the base path.
        /// </summary>
        public string BasePath { get; private set; }
    }
}