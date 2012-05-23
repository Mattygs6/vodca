//-----------------------------------------------------------------------------
// <copyright file="ValidatePasswordAnswerIsRequiredAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("041A39D6-01B6-4076-AAF6-EED9E5EFBC65", "Enter Password answer field is required!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidatePasswordAnswerIsRequiredAttribute.cs" title="ValidatePasswordAnswerIsRequiredAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("041A39D6-01B6-4076-AAF6-EED9E5EFBC65")]
    public sealed class ValidatePasswordAnswerIsRequiredAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordAnswerIsRequiredAttribute"/> class.
        /// </summary>
        public ValidatePasswordAnswerIsRequiredAttribute()
            : this(typeof(ValidatePasswordAnswerIsRequiredAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordAnswerIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidatePasswordAnswerIsRequiredAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordAnswerIsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidatePasswordAnswerIsRequiredAttribute(string itemid)
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
