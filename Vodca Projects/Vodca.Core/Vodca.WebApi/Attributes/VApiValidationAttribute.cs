//-----------------------------------------------------------------------------
// <copyright file="VApiValidationAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/01/2012
//-----------------------------------------------------------------------------
namespace Vodca.WebApi
{
    using System;

    /// <summary>
    ///  The base class of Vodca Web API validation attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [Serializable]
    public abstract class VApiValidationAttribute : Attribute, IVApiValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VApiValidationAttribute"/> class.
        /// </summary>
        protected VApiValidationAttribute()
        {
            this.ValidationOrder = byte.MaxValue;
        }

        /// <summary>
        /// Gets or sets the validation order.
        /// </summary>
        /// <value>
        /// The validation order.
        /// </value>
        public byte ValidationOrder { get; set; }

        /// <summary>
        /// Validates the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>The true to continue and false otherwise</returns>
        public abstract bool Validate(VApiArgs args);

        /// <summary>
        /// Called when validation fails.
        /// </summary>
        /// <param name="args">The args.</param>
        public abstract void OnValidationFailure(VApiArgs args);
    }
}
