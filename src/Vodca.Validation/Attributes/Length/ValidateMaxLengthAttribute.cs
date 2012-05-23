//-----------------------------------------------------------------------------
// <copyright file="ValidateMaxLengthAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/24/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("3DBE700D-D94F-4387-9E57-FED338785135", "The field max length exceeded!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\Attributes\Length\ValidateMaxLengthAttribute.cs" title="ValidateMaxLengthAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true), GuidAttribute("3DBE700D-D94F-4387-9E57-FED338785135")]
    public sealed class ValidateMaxLengthAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMaxLengthAttribute"/> class.
        /// </summary>
        /// <param name="maxlength">The max length.</param>
        public ValidateMaxLengthAttribute(int maxlength)
            : this(maxlength, typeof(ValidateMaxLengthAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMaxLengthAttribute"/> class.
        /// </summary>
        /// <param name="maxlength">The max length.</param>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateMaxLengthAttribute(int maxlength, Guid itemid)
            : base(itemid)
        {
            this.MaxLength = maxlength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMaxLengthAttribute"/> class.
        /// </summary>
        /// <param name="maxlength">The max length.</param>
        /// <param name="itemid">The item id.</param>
        public ValidateMaxLengthAttribute(int maxlength, string itemid)
            : this(maxlength, Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Gets the length of the min.
        /// </summary>
        /// <value>
        /// The length of the min.
        /// </value>
        public int MaxLength { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ValidateMaxLengthAttribute"/> is optional.
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

            return !isempty && input.Length <= this.MaxLength;
        }
    }
}
