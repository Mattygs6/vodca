//-----------------------------------------------------------------------------
// <copyright file="ValidateFieldGuidIsRequiredAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/16/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;

    /// <summary>
    ///     The form field validator
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class ValidateFieldGuidIsRequiredAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateFieldGuidIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateFieldGuidIsRequiredAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateFieldGuidIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateFieldGuidIsRequiredAttribute(string itemid)
            : base(Guid.Parse(itemid))
        {
        }

        /// <summary>
        ///     Validate input
        /// </summary>
        /// <param name="args">The validation args.</param>
        /// <returns>
        ///     true if input passed the validation; otherwise false.
        /// </returns>
        public override bool Validate(ValidationArgs args)
        {
            var input = args.PropertyValue as Guid?;

            return input.HasValue;
        }
    }
}
