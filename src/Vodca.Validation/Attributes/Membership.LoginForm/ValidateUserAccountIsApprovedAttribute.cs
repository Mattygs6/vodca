//-----------------------------------------------------------------------------
// <copyright file="ValidateUserAccountIsApprovedAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("7bea0e67-5716-4511-a4f7-43f0842565fd", "User Account is not approved|Your account has not yet been approved by the site's administrators. Please try again later ... ")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateUserAccountIsApprovedAttribute.cs" title="ValidateUserAccountIsApprovedAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("7BEA0E67-5716-4511-A4F7-43F0842565FD")]
    public sealed class ValidateUserAccountIsApprovedAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserAccountIsApprovedAttribute"/> class.
        /// </summary>
        public ValidateUserAccountIsApprovedAttribute()
            : this(typeof(ValidateUserAccountIsApprovedAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserAccountIsApprovedAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateUserAccountIsApprovedAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserAccountIsApprovedAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateUserAccountIsApprovedAttribute(Guid itemid)
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
        ///     Validate input
        /// </summary>
        /// <param name="args">The validation args.</param>
        /// <returns>
        ///     true if input passed the validation; otherwise false.
        /// </returns>
        public override bool Validate(ValidationArgs args)
        {
            var input = args.PropertyValue as string;

            if (!string.IsNullOrEmpty(input))
            {
                MembershipProvider provider = Membership.Providers[this.MembershipProviderName] ?? Membership.Provider;
                if (provider != null)
                {
                    MembershipUser user = provider.GetUser(input, false);
                    if (user != null)
                    {
                        bool valid = user.IsApproved;

                        args.AbortValidationPipeline = !valid;

                        return valid;
                    }

                    LogFailedCondition("The user name not found and validation can't be executed!");
                    return false;
                }

                LogFailedCondition("The MembershipProvider not found and validation can't be executed!");
                return false;
            }

            LogFailedCondition("The user name can't be empty!");
            return false;
        }
    }
}
