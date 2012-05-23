//-----------------------------------------------------------------------------
// <copyright file="ValidateZipCodeIsValidAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       08/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("3DD40ACE-CE55-418C-BA83-5EC92126E322", "The zip code is not valid!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateZipCodeIsValidAttribute.cs" title="ValidateZipCodeIsValidAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("3DD40ACE-CE55-418C-BA83-5EC92126E322")]
    public sealed class ValidateZipCodeIsValidAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateZipCodeIsValidAttribute"/> class.
        /// </summary>
        public ValidateZipCodeIsValidAttribute()
            : this(typeof(ValidateZipCodeIsValidAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateZipCodeIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateZipCodeIsValidAttribute(string itemid)
            : base(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateZipCodeIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateZipCodeIsValidAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether image <see cref="ValidateZipCodeIsValidAttribute"/> is optional.
        /// </summary>
        /// <value>
        ///     <c>true</c> if optional; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// If flag sets for the file uploaded logic only. In another words if image is not uploaded and  Optional = false, the validation result is true;
        /// </remarks>
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
                /* Info: Empty field is valid */
                return input.IsValidZipCodeFiveOptional();
            }

            return input.IsValidZipCodeFive();
        }
    }
}
