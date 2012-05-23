//-----------------------------------------------------------------------------
// <copyright file="ValidateMinValueAttribute.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       04/02/2012
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("A06DFB0D-C6AF-4EFC-B82E-7DCB138C32BA", "The min value requirement not met!")]

namespace Vodca.VForms
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\Attributes\Length\ValidateMinValueAttribute.cs" title="ValidateMinValueAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true), GuidAttribute("A06DFB0D-C6AF-4EFC-B82E-7DCB138C32BA")]
    public sealed class ValidateMinValueAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMinValueAttribute"/> class.
        /// </summary>
        /// <param name="minvalue">The min length.</param>
        public ValidateMinValueAttribute(double minvalue)
            : this(minvalue, typeof(ValidateMinValueAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMinValueAttribute"/> class.
        /// </summary>
        /// <param name="minvalue">The min length.</param>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateMinValueAttribute(double minvalue, Guid itemid)
            : base(itemid)
        {
            this.MinValue = minvalue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateMinValueAttribute"/> class.
        /// </summary>
        /// <param name="minvalue">The min length.</param>
        /// <param name="itemid">The item id.</param>
        public ValidateMinValueAttribute(double minvalue, string itemid)
            : this(minvalue, Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Gets the min value.
        /// </summary>
        public double MinValue { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ValidateMinValueAttribute"/> is optional.
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
            double input;
            bool isvalidnumber = double.TryParse(Convert.ToString(args.PropertyValue, CultureInfo.InvariantCulture), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out input);

            if (this.Optional && !isvalidnumber)
            {
                return true;
            }

            return isvalidnumber && input >= this.MinValue;
        }
    }
}
