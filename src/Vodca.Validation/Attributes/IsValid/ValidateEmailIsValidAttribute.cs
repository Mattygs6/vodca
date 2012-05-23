//-----------------------------------------------------------------------------
// <copyright file="ValidateEmailIsValidAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("239B3DB4-71FC-4F6C-A0A7-505508742F3A", "The email is not valid!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("239B3DB4-71FC-4F6C-A0A7-505508742F3A")]
    public sealed class ValidateEmailIsValidAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateEmailIsValidAttribute"/> class.
        /// </summary>
        public ValidateEmailIsValidAttribute()
            : this(typeof(ValidateEmailIsValidAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateEmailIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateEmailIsValidAttribute(string itemid)
            : base(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateEmailIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateEmailIsValidAttribute(Guid itemid)
            : base(itemid)
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

            return input.IsValidEmail();
        }
    }
}
