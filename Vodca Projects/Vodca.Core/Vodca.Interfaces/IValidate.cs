//-----------------------------------------------------------------------------
// <copyright file="IValidate.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       1/14/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    ///     The object validation contract
    /// </summary>
    public interface IValidate
    {
        /// <summary>
        ///     Gets a flag indicating whether object is valid or not
        /// </summary>
        /// <returns>True if valid otherwise false</returns>
        bool Validate();
    }
}
