//-----------------------------------------------------------------------------
// <copyright file="IChangePasswordForm.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       12/01/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    /// <summary>
    /// The Change password form properties
    /// </summary>
    public interface IChangePasswordForm
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        string NewPassword { get; set; }
    }
}