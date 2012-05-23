//-----------------------------------------------------------------------------
// <copyright file="ValidateConfirmNewPasswordIsRequiredAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("8CEC5A43-6685-45F3-B7FD-8E824B1B0F36", "Enter confirm new password is required field, please!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\Attributes\Membership.ChangePassword\ValidateConfirmNewPasswordIsRequiredAttribute.cs" title="ValidateConfirmNewPasswordIsRequiredAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("8CEC5A43-6685-45F3-B7FD-8E824B1B0F36")]
    public sealed class ValidateConfirmNewPasswordIsRequiredAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateConfirmNewPasswordIsRequiredAttribute"/> class.
        /// </summary>
        public ValidateConfirmNewPasswordIsRequiredAttribute()
            : this(typeof(ValidateConfirmNewPasswordIsRequiredAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateConfirmNewPasswordIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateConfirmNewPasswordIsRequiredAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateConfirmNewPasswordIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateConfirmNewPasswordIsRequiredAttribute(string itemid)
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
            var input = args.PropertyValue as string;

            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
