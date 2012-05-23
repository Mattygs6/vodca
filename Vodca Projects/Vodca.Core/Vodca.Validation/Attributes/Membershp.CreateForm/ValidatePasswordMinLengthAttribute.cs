//-----------------------------------------------------------------------------
// <copyright file="ValidatePasswordMinLengthAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("36c7dbe2-7685-4d26-b7eb-dc5d54e24913", "The minimum required password length is {0}!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidatePasswordMinLengthAttribute.cs" title="ValidatePasswordMinLengthAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("36C7DBE2-7685-4D26-B7EB-DC5D54E24913")]
    public sealed class ValidatePasswordMinLengthAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordMinLengthAttribute"/> class.
        /// </summary>
        public ValidatePasswordMinLengthAttribute()
            : this(typeof(ValidatePasswordMinLengthAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordMinLengthAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidatePasswordMinLengthAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordMinLengthAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidatePasswordMinLengthAttribute(Guid itemid)
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
                MembershipProvider provider = Membership.Providers[this.MembershipProviderName];

                if (provider != null)
                {
                    int min = provider.MinRequiredPasswordLength;

                    this.MessageParams = new object[] { min };

                    return input.Length >= min;
                }
            }

            LogFailedCondition("The users password can't be empty!");
            return false;
        }
    }
}
