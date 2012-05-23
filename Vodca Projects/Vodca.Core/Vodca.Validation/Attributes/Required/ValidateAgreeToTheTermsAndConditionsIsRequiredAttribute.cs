//-----------------------------------------------------------------------------
// <copyright file="ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("9DF6C04D-9DC7-4647-9479-A220D49A5B60", "Check the agree to the Terms And Conditions checkbox, please!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute.cs" title="ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("9DF6C04D-9DC7-4647-9479-A220D49A5B60")]
    public sealed class ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute"/> class.
        /// </summary>
        public ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute()
            : this(typeof(ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateAgreeToTheTermsAndConditionsIsRequiredAttribute(string itemid)
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
            string input = string.Concat(args.PropertyValue);

            return input.ConvertToBoolean().GetValueOrDefault();
        }
    }
}
