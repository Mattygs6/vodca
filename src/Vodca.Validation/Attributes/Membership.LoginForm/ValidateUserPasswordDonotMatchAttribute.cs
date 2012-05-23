//-----------------------------------------------------------------------------
// <copyright file="ValidateUserPasswordDonotMatchAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("3F5D68B6-9978-48DC-BD30-E9CCED79ED6A", "Entered password don't match!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateUserPasswordDonotMatchAttribute.cs" title="ValidateUserPasswordDonotMatchAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("3F5D68B6-9978-48DC-BD30-E9CCED79ED6A")]
    public sealed class ValidateUserPasswordDonotMatchAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserPasswordDonotMatchAttribute"/> class.
        /// </summary>
        public ValidateUserPasswordDonotMatchAttribute()
            : this(typeof(ValidateUserPasswordDonotMatchAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserPasswordDonotMatchAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateUserPasswordDonotMatchAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserPasswordDonotMatchAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateUserPasswordDonotMatchAttribute(Guid itemid)
            : base(itemid)
        {
            /* Default */
            this.UserNameFieldNameKey = "username";

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
        /// Gets or sets the user name field name key.
        /// </summary>
        /// <value>
        /// The user name field name key.
        /// </value>
        public string UserNameFieldNameKey { get; set; }

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
            var username = args.Collection[this.UserNameFieldNameKey];
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(username))
            {
                MembershipProvider provider = Membership.Providers[this.MembershipProviderName] ?? Membership.Provider;

                if (provider != null)
                {
                    // is this the call we want to make?
                    bool valid = provider.ValidateUser(username, input);
                    return valid;
                }

                LogFailedCondition("The MembershipProvider not found and validation can't be executed!");
                return false;
            }

            LogFailedCondition("The user name or password can't be empty!");
            return false;
        }
    }
}
