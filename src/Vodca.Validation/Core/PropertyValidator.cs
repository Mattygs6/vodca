//-----------------------------------------------------------------------------
// <copyright file="PropertyValidator.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///     Validation supplementary class
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\System.Web.Validators\PropertyValidator.cs" title="PropertyValidator.cs" lang="C#" />
    /// </example>
    internal class PropertyValidator
    {
        /// <summary>
        ///     The property info
        /// </summary>
        private readonly PropertyInfo property;

        /// <summary>
        ///     The list of validation attributes attached to the property
        /// </summary>
        private readonly HashSet<ValidateAttribute> validateAttributes = new HashSet<ValidateAttribute>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValidator"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        public PropertyValidator(PropertyInfo property)
        {
            Ensure.IsNotNull(property, "PropertyValidator-ctor()");

            this.property = property;
            this.FormValidationOrder = 255;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <value>The property.</value>
        public PropertyInfo Property
        {
            get { return this.property; }
        }

        /// <summary>
        /// Gets the form validation order.
        /// </summary>
        public byte FormValidationOrder { get; private set; }

        /// <summary>
        /// Gets the property validate attribute.
        /// </summary>
        /// <param name="ruleset">The rule set.</param>
        /// <returns>The list of the Validation attributes</returns>
        /// <value>The property validate attribute.</value>
        public IEnumerable<ValidateAttribute> PropertyValidateAttribute(string ruleset)
        {
            return from attribute in this.validateAttributes where string.Equals(attribute.RuleSet, ruleset, StringComparison.OrdinalIgnoreCase) orderby attribute.PropertyValidationOrder select attribute;
        }

        /// <summary>
        /// Adds the validate attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public void AddValidateAttribute(ValidateAttribute attribute)
        {
            Ensure.IsNotNull(attribute, "PropertyValidator.AddValidateAttribute-attribute");
            if (attribute.FormValidationOrder < this.FormValidationOrder)
            {
                this.FormValidationOrder = attribute.FormValidationOrder;
            }

            this.validateAttributes.Add(attribute);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Property == null ? 0 : this.Property.MetadataToken;
        }
    }
}
