//-----------------------------------------------------------------------------
// <copyright file="IVApiValidationAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       04/01/2012
//-----------------------------------------------------------------------------
namespace Vodca.WebApi
{
    /// <summary>
    ///  The base properties of Vodca Web API validation attributes
    /// </summary>
    public interface IVApiValidationAttribute
    {
        /// <summary>
        /// Gets or sets the validation order.
        /// </summary>
        /// <value>
        /// The validation order.
        /// </value>
        byte ValidationOrder { get; set; }

        /// <summary>
        /// Validates the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>The true to continue and false otherwise</returns>
        bool Validate(VApiArgs args);

        /// <summary>
        /// Called when validation fails.
        /// </summary>
        /// <param name="args">The args.</param>
        void OnValidationFailure(VApiArgs args);
    }
}