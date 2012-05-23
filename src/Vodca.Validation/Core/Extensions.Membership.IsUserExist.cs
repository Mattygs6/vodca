//-----------------------------------------------------------------------------
// <copyright file="Extensions.Membership.IsUserExist.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/01/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Configuration.Provider;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web.Security;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public static partial class Extensions
    {
        /// <summary>
        /// The Sql connection field from SqlMembershipProvider
        /// </summary>
        private static readonly FieldInfo FieldInfoSqlConnectionString = typeof(SqlMembershipProvider).GetField("_sqlConnectionString", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);

        /// <summary>
        /// Determines whether is user exist with specified name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="username">The username.</param>
        /// <returns>True if user exist otherwise false</returns>
        public static bool? IsUserExist(this MembershipProvider provider, string username)
        {
            return IsUserExist(provider as SqlMembershipProvider, username);
        }

        /// <summary>
        /// Determines whether is user exist with specified name.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="username">The username.</param>
        /// <returns>True if user exist otherwise false</returns>
        public static bool? IsUserExist(this SqlMembershipProvider provider, string username)
        {
            // ReSharper disable InvocationIsSkipped
            System.Diagnostics.Debug.Assert(provider != null, "SqlMembershipProvider");
            // ReSharper restore InvocationIsSkipped

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (provider != null && string.IsNullOrWhiteSpace(username)) // ReSharper restore ConditionIsAlwaysTrueOrFalse
            {
                var connectionstring = FieldInfoSqlConnectionString.GetValue(provider) as string;

                return SqlQuery.ExecuteScalar<bool>(
                                    connectionstring,
                                    CommandType.Text,
                                    "SELECT TOP 1  1 FROM [dbo].[aspnet_Users] WHERE @UserName = [UserName]",
                                    new SqlParameter("@UserName", username));
            }

            return null;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>The connection string</returns>
        public static string GetConnectionString(this ProviderBase provider)
        {
            return FieldInfoSqlConnectionString.GetValue(provider) as string;
        }
    }
}