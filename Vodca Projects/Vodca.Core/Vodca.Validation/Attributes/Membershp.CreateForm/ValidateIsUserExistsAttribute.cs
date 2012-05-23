//-----------------------------------------------------------------------------
// <copyright file="ValidateIsUserExistsAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("970f3fa6-bb09-484c-ad7b-e4a7bbc3abb1", "User exists in the database")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateIsUserExistsAttribute.cs" title="ValidateIsUserExistsAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("970F3FA6-BB09-484C-AD7B-E4A7BBC3ABB1")]
    public sealed class ValidateIsUserExistsAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateIsUserExistsAttribute"/> class.
        /// </summary>
        public ValidateIsUserExistsAttribute()
            : this(typeof(ValidateIsUserExistsAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateIsUserExistsAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateIsUserExistsAttribute(string itemid)
            : base(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateIsUserExistsAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateIsUserExistsAttribute(Guid itemid)
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
            /* ReSharper disable AssignNullToNotNullAttribute */
            Ensure.IsNotNullOrEmpty(input, "value");
            /* ReSharper restore AssignNullToNotNullAttribute */

            MembershipProvider provider = Membership.Providers[this.MembershipProviderName] ?? Membership.Provider;

            if (provider != null)
            {
                var user = provider.GetUser(input, false);
                return user != null;
            }

            return false;
        }
    }
}
