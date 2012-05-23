//-----------------------------------------------------------------------------
// <copyright file="VApiResponse.cs" company="genuine">
//     Copyright (c) M.Gramolini. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     M.Gramolini
//   Date:      04/30/2012
//-----------------------------------------------------------------------------
namespace Vodca
{
    /// <summary>
    /// VApi Response Helper
    /// </summary>
    public partial class VApiResponse : VListDictionary
    {
        /// <summary>
        /// Errors the message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The Response</returns>
        public static VApiResponse ErrorMessage(string errorMessage)
        {
            return new VApiResponse { { "ErrorMessage", errorMessage } };
        }

        /// <summary>
        /// Data the specified data object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <returns>The Response</returns>
        public static VApiResponse Data(object dataObject)
        {
            return new VApiResponse { { "Data", dataObject } };
        }

        /// <summary>
        /// the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// The Response
        /// </returns>
        public static VApiResponse Message(string message)
        {
            return new VApiResponse { { "Message", message } };
        }
    }
}
