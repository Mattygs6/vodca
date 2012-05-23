//-----------------------------------------------------------------------------
// <copyright company="genuine" file="ILoginForm.cs">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    /// <summary>
    /// The login form mandatory properties
    /// </summary>
    public interface ILoginForm
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is persistent.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is persistent; otherwise, <c>false</c>.
        /// </value>
        bool IsPersistent { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The site domain.
        /// </value>
        string Domain { get; set; }
    }
}