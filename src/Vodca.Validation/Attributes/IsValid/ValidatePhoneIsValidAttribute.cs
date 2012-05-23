//-----------------------------------------------------------------------------
// <copyright file="ValidatePhoneIsValidAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("956A3C19-65E2-4D2A-B616-B5CDE3096DCF", "The phone number isn't valid!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidatePhoneIsValidAttribute.cs" title="ValidatePhoneIsValidAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("956A3C19-65E2-4D2A-B616-B5CDE3096DCF")]
    public sealed class ValidatePhoneIsValidAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePhoneIsValidAttribute"/> class.
        /// </summary>
        public ValidatePhoneIsValidAttribute()
            : this(typeof(ValidatePhoneIsValidAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePhoneIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidatePhoneIsValidAttribute(string itemid)
            : base(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePhoneIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidatePhoneIsValidAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether empty input is valid or not.
        /// </summary>
        /// <value>
        ///     <c>true</c> if optional; otherwise, <c>false</c>.
        /// </value>
        public bool Optional { get; set; }

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
            if (this.Optional)
            {
                /* INFO: Empty field is valid. If value non empty validate */
                return input.IsValidPhoneOptional();
            }

            return input.IsValidPhone();
        }
    }
}
