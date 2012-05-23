//-----------------------------------------------------------------------------
// <copyright file="ValidateStateAbbreviationIsValidAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/25/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("E55317F1-7342-45BF-8727-E4FA45C5B0BD", "The state field is invalid!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("E55317F1-7342-45BF-8727-E4FA45C5B0BD")]
    public sealed class ValidateStateAbbreviationIsValidAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateStateAbbreviationIsValidAttribute"/> class.
        /// </summary>
        public ValidateStateAbbreviationIsValidAttribute()
            : this(typeof(ValidateStateAbbreviationIsValidAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateStateAbbreviationIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateStateAbbreviationIsValidAttribute(string itemid)
            : base(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateStateAbbreviationIsValidAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateStateAbbreviationIsValidAttribute(Guid itemid)
            : base(itemid)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether empty field is valid or not.
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
            if (this.Optional && string.IsNullOrEmpty(input))
            {
                /* Info: Empty field is valid */
                return true;
            }

            return input.IsValidUsStateAbbreviation();
        }
    }
}
