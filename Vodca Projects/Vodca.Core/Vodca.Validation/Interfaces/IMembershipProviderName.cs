//-----------------------------------------------------------------------------
// <copyright company="genuine" file="IMembershipProviderName.cs">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    /// <summary>
    /// The Membership validation attributes
    /// </summary>
    public interface IMembershipProviderName
    {
        /// <summary>
        /// Gets or sets the name of the membership provider.
        /// </summary>
        /// <value>
        /// The name of the membership provider.
        /// </value>
        string MembershipProviderName { get; set; }
    }
}