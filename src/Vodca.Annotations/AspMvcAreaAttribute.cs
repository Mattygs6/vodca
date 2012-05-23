//-----------------------------------------------------------------------------
// <copyright file="AspMvcAreaAttribute.cs" company="JetBrains">
//     Copyright (c) JetBrains. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.Annotations
{
    /// <summary>
    /// Re-sharper Annotation
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Parameter)]
    public sealed class AspMvcAreaAttribute : PathReferenceAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspMvcAreaAttribute"/> class.
        /// </summary>
        public AspMvcAreaAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AspMvcAreaAttribute"/> class.
        /// </summary>
        /// <param name="anonymousProperty">The anonymous property.</param>
        public AspMvcAreaAttribute(string anonymousProperty)
        {
            this.AnonymousProperty = anonymousProperty;
        }

        /// <summary>
        /// Gets the anonymous property.
        /// </summary>
        public string AnonymousProperty { get; private set; }
    }
}