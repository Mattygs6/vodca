//-----------------------------------------------------------------------------
// <copyright company="genuine" file="IResetPasswordForm.cs">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    /// <summary>
    /// The Membership reset form
    /// </summary>
    public interface IResetPasswordForm
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password answer.
        /// </summary>
        /// <value>
        /// The password answer.
        /// </value>
        string PasswordAnswer { get; set; }
    }
}