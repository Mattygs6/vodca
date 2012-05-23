//-----------------------------------------------------------------------------
// <copyright file="ValidateDuplicateEmailAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("9a206152-fb1c-4c16-9d3c-2f09cec9326a", "The user with this email exists already!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateDuplicateEmailAttribute.cs" title="ValidateDuplicateEmailAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("9A206152-FB1C-4C16-9D3C-2F09CEC9326A")]
    public sealed class ValidateDuplicateEmailAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDuplicateEmailAttribute"/> class.
        /// </summary>
        public ValidateDuplicateEmailAttribute()
            : this(typeof(ValidateDuplicateEmailAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDuplicateEmailAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateDuplicateEmailAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDuplicateEmailAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateDuplicateEmailAttribute(Guid itemid)
            : base(itemid)
        {
            /* Assign to default ASP.NET provider */
            this.MembershipProviderName = "AspNetSqlMembershipProvider";
        }

        /// <summary>
        /// Gets or sets the name of the membership provider.
        /// </summary>
        /// <value>
        /// The name of the membership provider.
        /// </value>
        public string MembershipProviderName { get; set; }

        /// <summary>
        /// Validate input
        /// </summary>
        /// <param name="args">The validation args.</param>
        /// <returns>
        /// true if input passed the validation; otherwise false.
        /// </returns>
        public override bool Validate(ValidationArgs args)
        {
            var input = args.PropertyValue as string;

            if (!string.IsNullOrEmpty(input))
            {
                var provider = Membership.Providers[this.MembershipProviderName] ?? Membership.Provider;

                if (provider != null)
                {
                    var result = !provider.IsUserEmailExist(input).GetValueOrDefault();

                    return result;
                }
            }

            LogFailedCondition("The user email can't be empty!");
            return false;
        }
    }
}
