//-----------------------------------------------------------------------------
// <copyright file="AspMvcActionAttribute.cs" company="JetBrains">
//     Copyright (c) JetBrains. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.Annotations
{
    /// <summary>
    /// Re-sharper Annotation
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Parameter | System.AttributeTargets.Method)]
    public sealed class AspMvcActionAttribute : System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AspMvcActionAttribute"/> class.
        /// </summary>
        public AspMvcActionAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AspMvcActionAttribute"/> class.
        /// </summary>
        /// <param name="anonymousProperty">The anonymous property.</param>
        public AspMvcActionAttribute(string anonymousProperty)
        {
            this.AnonymousProperty = anonymousProperty;
        }

        /// <summary>
        /// Gets the anonymous property.
        /// </summary>
        public string AnonymousProperty { get; private set; }
    }
}