//-----------------------------------------------------------------------------
// <copyright file="IVCookieName.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//  Date:       03/09/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// Ensures the VCookie class will get a name to use while providing lots of flexibility
    /// </summary>
    public interface IVCookieName
    {
        /// <summary>
        /// Gets the name of the cookie. If not implemented, will use the Descendant class name.
        /// </summary>
        /// <returns>The cookie name to use</returns>
        string GetCookieName();

        /// <summary>
        /// Uses the encryption.
        /// </summary>
        /// <returns>True or false to use encryption</returns>
        bool UseEncryption();
    }
}
