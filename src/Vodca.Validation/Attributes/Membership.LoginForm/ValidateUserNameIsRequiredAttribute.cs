//-----------------------------------------------------------------------------
// <copyright file="ValidateUserNameIsRequiredAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("15a6fa5a-0a11-433c-984f-ab7682889589", "Enter user name, please!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateUserNameIsRequiredAttribute.cs" title="ValidateUserNameIsRequiredAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("15A6FA5A-0A11-433C-984F-AB7682889589")]
    public sealed class ValidateUserNameIsRequiredAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNameIsRequiredAttribute"/> class.
        /// </summary>
        public ValidateUserNameIsRequiredAttribute()
            : this(typeof(ValidateUserNameIsRequiredAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNameIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateUserNameIsRequiredAttribute(string itemid)
            : base(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNameIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateUserNameIsRequiredAttribute(Guid itemid)
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

            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
