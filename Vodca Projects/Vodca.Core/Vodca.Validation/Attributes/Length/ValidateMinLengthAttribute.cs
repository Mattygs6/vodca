//-----------------------------------------------------------------------------
// <copyright file="ValidateMinLengthAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/24/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("BB50D589-AF9F-420E-98A4-31247374707C", "The field min length requirement not met!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\Attributes\Length\ValidateMinLengthAttribute.cs" title="ValidateMinLengthAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true), GuidAttribute("BB50D589-AF9F-420E-98A4-31247374707C")]
    public sealed class ValidateMinLengthAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMinLengthAttribute"/> class.
        /// </summary>
        /// <param name="minlength">The min length.</param>
        public ValidateMinLengthAttribute(int minlength)
            : this(minlength, typeof(ValidateMinLengthAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMinLengthAttribute"/> class.
        /// </summary>
        /// <param name="minlength">The min length.</param>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateMinLengthAttribute(int minlength, Guid itemid)
            : base(itemid)
        {
            this.MinLength = minlength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMinLengthAttribute"/> class.
        /// </summary>
        /// <param name="minlength">The min length.</param>
        /// <param name="itemid">The item id.</param>
        public ValidateMinLengthAttribute(int minlength, string itemid)
            : this(minlength, Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Gets the length of the min.
        /// </summary>
        /// <value>
        /// The length of the min.
        /// </value>
        public int MinLength { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ValidateMinLengthAttribute"/> is optional.
        /// </summary>
        /// <value>
        ///   <c>true</c> if optional; otherwise, <c>false</c>.
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
            bool isempty = string.IsNullOrWhiteSpace(input);

            if (this.Optional && isempty)
            {
                return true;
            }

            return !isempty && input.Length >= this.MinLength;
        }
    }
}
