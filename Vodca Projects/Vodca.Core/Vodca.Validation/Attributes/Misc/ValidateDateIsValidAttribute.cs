//-----------------------------------------------------------------------------
// <copyright file="ValidateDateIsValidAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/10/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("C9A52B5F-1AA9-480B-9A7C-A6E13F4D14CF", "Please enter a valid date.")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\Attributes\Misc\ValidateDateIsValidAttribute.cs" title="ValidateDateIsValidAttribute.cs" lang="C#" />
    /// </example>
    /// <remarks>Designed for US dates only</remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("C9A52B5F-1AA9-480B-9A7C-A6E13F4D14CF")]
    public sealed class ValidateDateIsValidAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDateIsValidAttribute"/> class.
        /// </summary>
        public ValidateDateIsValidAttribute()
            : this(typeof(ValidateDateIsValidAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDateIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateDateIsValidAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDateIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateDateIsValidAttribute(string itemid)
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

            return input.IsValidDate();
        }
    }
}
