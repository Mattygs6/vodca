//-----------------------------------------------------------------------------
// <copyright file="VRegisterValidationMessageAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       03/01/2012
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Web;

    /// <summary>
    ///     The validation default message.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [Serializable]
    public sealed partial class VRegisterValidationMessageAttribute : VRegisterAttribute, IValidateDisplayMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterValidationMessageAttribute"/> class.
        /// </summary>
        /// <param name="messageid">The message id.</param>
        /// <param name="message">The message.</param>
        public VRegisterValidationMessageAttribute(Guid messageid, string message)
        {
            this.MessageId = messageid;
            Ensure.IsNotNullOrEmpty(message, "message");
            this.Message = message;

            this.Order = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VRegisterValidationMessageAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        /// <param name="message">The message.</param>
        public VRegisterValidationMessageAttribute(string itemid, string message)
        {
            Guid id;
            if (Guid.TryParse(itemid, out id))
            {
                this.MessageId = id;
            }
            else
            {
                throw new HttpException("Couldn't parse GUID:" + itemid);
            }

            Ensure.IsNotNullOrEmpty(message, "message");
            this.Message = message;

            this.Order = 0;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="VRegisterValidationMessageAttribute"/> class from being created.
        /// </summary>
        private VRegisterValidationMessageAttribute()
        {
        }

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
        public Guid MessageId { get; private set; }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string MessageLanguage { get; set; }
    }
}