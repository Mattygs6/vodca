//-----------------------------------------------------------------------------
// <copyright file="ValidateUserNameNotTakenNameAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("D2FAEF13-76C9-4736-8F6A-A4E4F95641D7", "User name is already taken!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateUserNameNotTakenNameAttribute.cs" title="ValidateUserNameNotTakenNameAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("D2FAEF13-76C9-4736-8F6A-A4E4F95641D7")]
    public sealed class ValidateUserNameNotTakenNameAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNameNotTakenNameAttribute"/> class.
        /// </summary>
        public ValidateUserNameNotTakenNameAttribute()
            : this(typeof(ValidateUserNameNotTakenNameAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNameNotTakenNameAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateUserNameNotTakenNameAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNameNotTakenNameAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateUserNameNotTakenNameAttribute(Guid itemid)
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

                if (provider != null)
                {
                    var users = provider.GetUser(input, false);
                    return users == null;
                }

                LogFailedCondition("The MembershipProvider not found and validation can't be executed!");
                return false;
            }

            LogFailedCondition("The user name can't be empty. Pipeline aborted!");
            return false;
        }
    }
}
