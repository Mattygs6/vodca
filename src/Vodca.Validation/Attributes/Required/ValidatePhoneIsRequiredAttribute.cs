//-----------------------------------------------------------------------------
// <copyright file="ValidatePhoneIsRequiredAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("5C745DE1-106A-4DB7-82BF-C8535BE9A07C", "Enter phone number, please!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidatePhoneIsRequiredAttribute.cs" title="ValidatePhoneIsRequiredAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("5C745DE1-106A-4DB7-82BF-C8535BE9A07C")]
    public sealed class ValidatePhoneIsRequiredAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePhoneIsRequiredAttribute"/> class.
        /// </summary>
        public ValidatePhoneIsRequiredAttribute()
            : this(typeof(ValidatePhoneIsRequiredAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePhoneIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidatePhoneIsRequiredAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePhoneIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidatePhoneIsRequiredAttribute(string itemid)
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
