//-----------------------------------------------------------------------------
// <copyright file="VLogClientSideError.Methods.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    /// <summary>
    ///     Represents a logical application error  on client side
    /// </summary>
    public sealed partial class VLogClientSideError
    {
        #region Static operators

        /// <summary>
        ///     The equality operator (==) returns true if the values of its operands are equal, false otherwise.
        /// </summary>
        /// <param name="one">Firsts Object typeof VLogClientSideError</param>
        /// <param name="two">Second Object typeof VLogClientSideError</param>
        /// <returns>True if the values of its operands are equal</returns>
        public static bool operator ==(VLogClientSideError one, VLogClientSideError two)
        {
#pragma warning disable 183
            if (two is VLogClientSideError && one is VLogClientSideError)
#pragma warning restore 183
            {
                return one.Id == two.Id;
            }

            // Use .NET comparison then one side is NULL
            return object.Equals(one, two);
        }

        /// <summary>
        ///     The equality operator (!=) returns true if the values of its operands are NOT equal, false otherwise.
        /// </summary>
        /// <param name="one">First Object typeof VLogClientSideError</param>
        /// <param name="two">Object Object typeof VLogClientSideError</param>
        /// <returns>True if the values of its operands are equal</returns>
        public static bool operator !=(VLogClientSideError one, VLogClientSideError two)
        {
#pragma warning disable 183
            if (two is VLogClientSideError && one is VLogClientSideError)
#pragma warning restore 183
            {
                return one.Id != two.Id;
            }

            // Use .NET comparison then one side is NULL
            return !object.Equals(one, two);
        }
        #endregion

        #region Equals, GetHashCode, operator ==, operator !=

        /// <summary>
        ///     Determines whether the specified Object typeof VLogClientSideError is equal to the current Object.
        /// </summary>
        /// <param name="obj">The Object to compare with the current Object.</param>
        /// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var webclientsideerror = obj as VLogClientSideError;
            return this == webclientsideerror;
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current Object(VLogClientSideError).</returns>
        public override int GetHashCode()
        {
            return this.Id.ToHashCode();
        }

        #endregion
    }
}
