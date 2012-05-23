//-----------------------------------------------------------------------------
// <copyright file="VForm.Validation.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/21/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public abstract partial class VForm
    {
        /// <summary>
        /// Runs the validators.
        /// </summary>
        /// <param name="validationGroup">The validation group.</param>
        /// <returns>True if valid otherwise false</returns>
        private bool RunValidators(string validationGroup)
        {
            /* Reset errors */
            this.errorMessages = new HashSet<IValidationError>();

            /* Run some events before validation */
            if (this.BeforeValidationStart != null)
            {
                this.BeforeValidationStart(this, EventArgs.Empty);
            }

            Type type = this.GetType();

            var properties = VFormTypeCacheManager.GetOrderedProperties(type);

            foreach (PropertyValidator validator in properties)
            {
                foreach (var attribute in validator.PropertyValidateAttribute(validationGroup))
                {
                    object input = validator.Property.GetValue(this, null);

                    /* Init validation args  */
                    var args = new ValidationArgs(validator.Property.Name, input, this, this.Collection);

                    if (this.AfterValidationArgInit != null)
                    {
                        this.AfterValidationArgInit(this, args);
                    }

                    if (!attribute.Validate(args))
                    {
                        var error = this.GetValidationErrorInstance();

                        error.Message = attribute.GetValidationErrorMessage(args);
                        error.Property = validator.Property.Name;
                        error.JsId = attribute.ClientSideId ?? validator.Property.Name.ToLowerInvariant();
                        error.Ordinal = attribute.DisplayOrder;

                        this.AddErrorMessage(error);

                        /* Abort Validation Pipeline */
                        if (args.AbortValidationPipeline)
                        {
                            return false;
                        }

                        /* Add first property error and jump to next property */
                        break;
                    }
                }
            }

            this.isvalid = this.ErrorMessages.Count == 0;

            /* Run some events before validation */
            if (this.AfterValidationEnd != null)
            {
                this.AfterValidationEnd(this, EventArgs.Empty);
            }

            // ReSharper disable PossibleInvalidOperationException
            return this.isvalid.Value;
            // ReSharper restore PossibleInvalidOperationException
        }
    }
}