//-----------------------------------------------------------------------------
// <copyright file="ValidateComparePasswordsAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("32DEB426-7704-4AB2-8E96-E927A9AFF592", "The passwords don't match!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateComparePasswordsAttribute.cs" title="ValidateComparePasswordsAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("32DEB426-7704-4AB2-8E96-E927A9AFF592")]
    public sealed class ValidateComparePasswordsAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateComparePasswordsAttribute"/> class.
        /// </summary>
        /// <param name="comparepropertyname">The compare property name.</param>
        public ValidateComparePasswordsAttribute(string comparepropertyname)
            : this(comparepropertyname, typeof(ValidateComparePasswordsAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateComparePasswordsAttribute"/> class.
        /// </summary>
        /// <param name="comparepropertyname">The compare property name.</param>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateComparePasswordsAttribute(string comparepropertyname, Guid itemid)
            : base(itemid)
        {
            Ensure.IsNotNullOrEmpty(comparepropertyname, "comparepropertyname");

            this.ComparePropertyName = comparepropertyname;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateComparePasswordsAttribute"/> class.
        /// </summary>
        /// <param name="comparepropertyname">The compare property name.</param>
        /// <param name="itemid">The item id.</param>
        public ValidateComparePasswordsAttribute(string comparepropertyname, string itemid)
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

        // ReSharper disable UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Gets or sets a value indicating whether [case insensitive].
        /// </summary>
        /// <value>
        /// <c>true</c> if [case insensitive]; otherwise, <c>false</c>.
        /// </value>
        public bool CaseInsensitive { get; set; }

        // ReSharper restore UnusedAutoPropertyAccessor.Global

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
            return this.CompareValueAsStrings(input, this.ComparePropertyName, args, caseinsensitive: this.CaseInsensitive);
        }
    }
}
