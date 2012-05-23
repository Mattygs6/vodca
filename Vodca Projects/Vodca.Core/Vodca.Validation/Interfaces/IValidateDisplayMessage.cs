//-----------------------------------------------------------------------------
// <copyright file="IValidateDisplayMessage.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/24/2012
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;

    /// <summary>
    /// The validation mandatory properties
    /// </summary>
    public interface IValidateDisplayMessage
    {
        /// <summary>
        /// Gets the message id.
        /// </summary>
        /// <value>
        /// The message id.
        /// </value>
        /// <remarks>
        /// In the case of an application with a user interface this property would often be displayed to the user.
        /// So it should contain proper grammar and punctuation.
        /// </remarks>
        Guid MessageId { get; }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        string MessageLanguage { get; set; }
    }
}