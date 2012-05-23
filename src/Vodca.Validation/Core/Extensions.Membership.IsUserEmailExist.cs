//-----------------------------------------------------------------------------
// <copyright file="Extensions.Membership.IsUserEmailExist.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/01/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Security;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static partial class Extensions
    {
        /// <summary>
        /// Determines whether is user email exist with specified name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="email">The email.</param>
        /// <returns>True if user exist otherwise false</returns>
        public static bool? IsUserEmailExist(this MembershipProvider provider, string email)
        {
            return IsUserEmailExist(provider as SqlMembershipProvider, email);
        }

        /// <summary>
        /// Determines whether is user email exist with specified name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="email">The email.</param>
        /// <returns>True if user exist otherwise false</returns>
        public static bool? IsUserEmailExist(this SqlMembershipProvider provider, string email)
        {
            // ReSharper disable InvocationIsSkipped
            System.Diagnostics.Debug.Assert(provider != null, "SqlMembershipProvider");
            // ReSharper restore InvocationIsSkipped

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (provider != null && !string.IsNullOrWhiteSpace(email))
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                var connectionstring = FieldInfoSqlConnectionString.GetValue(provider) as string;

                return SqlQuery.ExecuteScalar<bool>(
                                    connectionstring,
                                    CommandType.Text,
                                    "SELECT TOP 1  CAST( 1 AS BIT) FROM [dbo].[aspnet_Membership] WHERE @Email = [Email]",
                                    new SqlParameter("@email", email));
            }

            return null;
        }
    }
}