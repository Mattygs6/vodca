//-----------------------------------------------------------------------------
// <copyright file="VLogServerSideError.Operator.Methods.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       07/30/2008
//-----------------------------------------------------------------------------
namespace Vodca.Logging
{
    /// <summary>
    ///     Represents a logical application error (as opposed to the actual 
    /// exception it may be representing).
    /// </summary>
    public sealed partial class VLogServerSideError
    {
        #region Static operators

        /// <summary>
        ///     The equality operator (==) returns true if the values of its operands are equal, false otherwise.
        /// </summary>
        /// <param name="one">Firsts Object typeof VLogServerSideError</param>
        /// <param name="two">Second Object typeof VLogServerSideError</param>
        /// <returns>True if the values of its operands are equal</returns>
        public static bool operator ==(VLogServerSideError one, VLogServerSideError two)
        {
#pragma warning disable 183
            if (two is VLogServerSideError && one is VLogServerSideError)
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
        /// <param name="one">First Object typeof VLogServerSideError</param>
        /// <param name="two">Object typeof VLogServerSideError</param>
        /// <returns>True if the values of its operands are equal</returns>
        public static bool operator !=(VLogServerSideError one, VLogServerSideError two)
        {
#pragma warning disable 183
            if (two is VLogServerSideError && one is VLogServerSideError)
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
        ///     Determines whether the specified Object typeof VLogServerSideError is equal to the current Object.
        /// </summary>
        /// <param name="obj">The Object to compare with the current Object.</param>
        /// <returns>true if the specified Object is equal to the current Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var webserversideerror = obj as VLogServerSideError;
            return this == webserversideerror;
        }

        /// <summary>
        ///     Serves as a hash function for a particular command, suitable for use
        /// in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>A hash code for the current Object(VLogServerSideError).</returns>
        public override int GetHashCode()
        {
            return this.Id.ToHashCode();
        }

        #endregion
    }
}
