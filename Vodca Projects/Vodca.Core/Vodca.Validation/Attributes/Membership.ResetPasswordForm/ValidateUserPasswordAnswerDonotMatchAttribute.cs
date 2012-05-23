//-----------------------------------------------------------------------------
// <copyright file="ValidateUserPasswordAnswerDonotMatchAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       06/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("F47833B2-E8EC-4D73-8963-379C23700D8A", "The password answer don't match !")]

namespace Vodca.VForms
{
    using System;

    using System.Runtime.InteropServices;
    using System.Web.Security;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateUserPasswordAnswerDonotMatchAttribute.cs" title="ValidateUserPasswordAnswerDonotMatchAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [GuidAttribute("F47833B2-E8EC-4D73-8963-379C23700D8A")]
    public sealed class ValidateUserPasswordAnswerDonotMatchAttribute : ValidateAttribute, IMembershipProviderName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserPasswordAnswerDonotMatchAttribute"/> class.
        /// </summary>
        public ValidateUserPasswordAnswerDonotMatchAttribute()
            : this(typeof(ValidateUserPasswordAnswerDonotMatchAttribute).GUID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserPasswordAnswerDonotMatchAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        public ValidateUserPasswordAnswerDonotMatchAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateUserPasswordAnswerDonotMatchAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateUserPasswordAnswerDonotMatchAttribute(Guid itemid)
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
                    MembershipUser user = provider.GetUser(username, false);
                    if (user != null)
                    {
                        // TODO : not implemented
                        return true;
                    }

                    LogFailedCondition("The user name can't be resolved!");
                    return false;
                }

                LogFailedCondition("The MembershipProvider not found and validation can't be executed!");
                return false;
            }

            LogFailedCondition("The user name or PasswordAnswer can't be empty!");

            return false;
        }
    }
}

////using (var hasher = HashAlgorithm.Create(Membership.HashAlgorithmType))
////                        {
////                            if (hasher is KeyedHashAlgorithm)
////                            {
////                                using (var keyedHasher = (KeyedHashAlgorithm)hasher)
////                                {
////                                    string connStr = provider.GetConnectionString();
////                                    string sql = "SELECT PasswordAnswer,PasswordSalt FROM aspnet_Membership WHERE UserId=@UserId";

////                                    var result = SqlQuery.IList<PasswordSaltAndAnswer>(connStr,
////                                                CommandType.Text,
////                                                sql,
////                                                new SqlParameter("UserId", user.ProviderUserKey))
////                                                .FirstOrDefault();

////                                    var passwordanswer = args.Collection["passwordanswer"].ToLowerInvariant();

////                                    byte[] src = Convert.FromBase64String(result.PasswordSalt);
////                                    byte[] passBytes = Encoding.Unicode.GetBytes(passwordanswer);

////                                    if (keyedHasher.Key.Length == src.Length)
////                                    {
////                                        keyedHasher.Key = src;
////                                    }
////                                    else if (keyedHasher.Key.Length < src.Length)
////                                    {
////                                        byte[] dst = new byte[keyedHasher.Key.Length];
////                                        Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
////                                        keyedHasher.Key = dst;
////                                    }
////                                    else
////                                    {
////                                        int num2;
////                                        byte[] buffer5 = new byte[keyedHasher.Key.Length];
////                                        for (int i = 0; i < buffer5.Length; i += num2)
////                                        {
////                                            num2 = Math.Min(src.Length, buffer5.Length - i);
////                                            Buffer.BlockCopy(src, 0, buffer5, i, num2);
////                                        }
////                                        keyedHasher.Key = buffer5;
////                                    }

////                                    byte[] paHashBytes = keyedHasher.ComputeHash(passBytes);
////                                    string paHashText = Convert.ToBase64String(paHashBytes);

////                                    return result.PasswordAnswer == paHashText;
////                                }
////                            }
////                        }