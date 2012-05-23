//-----------------------------------------------------------------------------
// <copyright file="VEmail.Response.cs" company="GenuineInteractive">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       11/02/2010
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Net.Mail;

    /// <summary>
    ///     The Email Send Response Message
    /// </summary>
    [Serializable]
    public partial class VEmailResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VEmailResponse"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the SMTP status code.
        /// </summary>
        /// <value>The SMTP status code.</value>
        public SmtpStatusCode? SmtpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Success: {0}, SmtpStatusCode: {1}, Response: {2}", this.Success, this.SmtpStatusCode, this.Response);
        }
    }
}
