//-----------------------------------------------------------------------------
// <copyright file="ValidateDuplicateUserNameAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("fa5e6d38-3c18-41a8-80be-94464b99dcfa", "User name already exists. Please enter a different user name.")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateDuplicateUserNameAttribute.cs" title="ValidateDuplicateUserNameAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("fa5e6d38-3c18-41a8-80be-94464b99dcfa")]
    public sealed class ValidateDuplicateUserNameAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDuplicateUserNameAttribute"/> class.
        /// </summary>
        public ValidateDuplicateUserNameAttribute()
            : this(typeof(ValidateDuplicateUserNameAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDuplicateUserNameAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateDuplicateUserNameAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateDuplicateUserNameAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateDuplicateUserNameAttribute(Guid itemid)
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
                MembershipProvider provider = Membership.Providers[this.MembershipProviderName] ?? Membership.Provider;

                var result = !provider.IsUserExist(input).GetValueOrDefault();
                return result;
            }

            LogFailedCondition("The user name can't be empty and duplicate email pipeline aborted!");
            return false;
        }
    }
}
