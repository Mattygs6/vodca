//-----------------------------------------------------------------------------
// <copyright file="ValidateCompareEmailsAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("EA47C04C-A0B0-4C28-BE2F-ED303E038A4F", "The emails don't match!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateCompareEmailsAttribute.cs" title="ValidateCompareEmailsAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("EA47C04C-A0B0-4C28-BE2F-ED303E038A4F")]
    public sealed class ValidateCompareEmailsAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCompareEmailsAttribute"/> class.
        /// </summary>
        /// <param name="comparepropertyname">The compare property name.</param>
        public ValidateCompareEmailsAttribute(string comparepropertyname)
            : this(comparepropertyname, typeof(ValidateCompareEmailsAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCompareEmailsAttribute"/> class.
        /// </summary>
        /// <param name="comparepropertyname">The compare property name.</param>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateCompareEmailsAttribute(string comparepropertyname, Guid itemid)
            : base(itemid)
        {
            Ensure.IsNotNullOrEmpty(comparepropertyname, "compare property name");

            this.ComparePropertyName = comparepropertyname;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCompareEmailsAttribute"/> class.
        /// </summary>
        /// <param name="comparepropertyname">The compare property name.</param>
        /// <param name="itemid">The item id.</param>
        public ValidateCompareEmailsAttribute(string comparepropertyname, string itemid)
            : this(comparepropertyname, Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Gets the name of the compare property.
        /// </summary>
        /// <value>
        /// The name of the compare property.
        /// </value>
        public string ComparePropertyName { get; private set; }

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
            return this.CompareValueAsStrings(input, this.ComparePropertyName, args, caseinsensitive: true);
        }
    }
}
