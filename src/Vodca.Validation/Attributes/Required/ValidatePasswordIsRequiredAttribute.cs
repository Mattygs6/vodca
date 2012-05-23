//-----------------------------------------------------------------------------
// <copyright file="ValidatePasswordIsRequiredAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("54B7AB47-6060-433D-9F77-8BC41878989E", "Enter password, please!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidatePasswordIsRequiredAttribute.cs" title="ValidatePasswordIsRequiredAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("54B7AB47-6060-433D-9F77-8BC41878989E")]
    public sealed class ValidatePasswordIsRequiredAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordIsRequiredAttribute"/> class.
        /// </summary>
        public ValidatePasswordIsRequiredAttribute()
            : this(typeof(ValidatePasswordIsRequiredAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidatePasswordIsRequiredAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidatePasswordIsRequiredAttribute(string itemid)
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
