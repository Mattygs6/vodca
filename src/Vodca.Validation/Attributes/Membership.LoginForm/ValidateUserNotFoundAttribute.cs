//-----------------------------------------------------------------------------
// <copyright file="ValidateUserNotFoundAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("ee68ab4e-98ce-4646-9f46-dc6a4bb926c8", "There is no user in the database with this username.")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateUserNotFoundAttribute.cs" title="ValidateUserNotFoundAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("EE68AB4E-98CE-4646-9F46-DC6A4BB926C8")]
    public sealed class ValidateUserNotFoundAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNotFoundAttribute"/> class.
        /// </summary>
        public ValidateUserNotFoundAttribute()
            : this(typeof(ValidateUserNotFoundAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNotFoundAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateUserNotFoundAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserNotFoundAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateUserNotFoundAttribute(Guid itemid)
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
                    var user = provider.GetUser(input, false);
                    bool valid = user != null;

                    args.AbortValidationPipeline = !valid;

                    return valid;
                }

                LogFailedCondition("The MembershipProvider not found and validation can't be executed!");
                return false;
            }

            LogFailedCondition("The user name can't be empty!");
            return false;
        }
    }
}
