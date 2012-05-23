//-----------------------------------------------------------------------------
// <copyright file="ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("d2ea4af2-362f-4b45-acc1-41f388ceb4a0", "The minimum required non alphanumeric characters are {0}!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute.cs" title="ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("D2EA4AF2-362F-4B45-ACC1-41F388CEB4A0")]
    public sealed class ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute"/> class.
        /// </summary>
        public ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute()
            : this(typeof(ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidatePasswordMinRequiredNonAlphanumericCharactersAttribute(Guid itemid)
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
                    int min = provider.MinRequiredNonAlphanumericCharacters;
                    if (min > 0)
                    {
                        byte counter = 0;

                        foreach (char character in input)
                        {
                            if (!char.IsLetterOrDigit(character))
                            {
                                counter++;
                                if (counter >= min)
                                {
                                    return true;
                                }
                            }
                        }

                        this.MessageParams = new object[] { min };

                        return false;
                    }

                    return true;
                }
            }

            LogFailedCondition("The users password can't be empty!");
            return false;
        }
    }
}
