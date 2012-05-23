//-----------------------------------------------------------------------------
// <copyright file="ValidateAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
namespace Vodca.VForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Vodca.SDK.NLog;

    /// <summary>
    ///     Abstract class for all validation attributes.
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Validation\Core\Validate.Attribute.cs" title="Validate.Attribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    [Serializable]
    public abstract partial class ValidateAttribute : Attribute
    {
        /// <summary>
        /// The Logger Instance
        /// </summary>
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Holds the name of Rule Set. Default is empty.
        /// </summary>
        private string ruleSet;

        /// <summary>
        ///     Initializes a new instance of the ValidateAttribute class.
        /// </summary>
        /// <param name="messageid">The error message ID for the user</param>
        /// <remarks>
        ///     In the case of an application with a user interface this property would often be displayed to the user.
        /// So it should contain proper grammar and punctuation.
        /// </remarks>
        protected ValidateAttribute(Guid messageid)
        {
            this.MessageId = messageid;
            this.PropertyValidationOrder = 255;
            this.FormValidationOrder = 255;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateAttribute"/> class.
        /// </summary>
        /// <param name="itemid">The item id.</param>
        protected ValidateAttribute(string itemid)
            : this(Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ValidateAttribute"/> class from being created.
        /// </summary>
        private ValidateAttribute()
        {
        }

        /// <summary>
        ///     Gets or sets a string used to group ValidateAttributes. 
        ///  Use a null to indicate no grouping.
        /// </summary>
        /// <remarks>
        ///     Will be a null to indicate no rule set.<br/>
        /// Case insensitive so this will always return a uppercase string no matter what is passed into the constructor.
        /// </remarks>
        public string RuleSet
        {
            get
            {
                return this.ruleSet;
            }

            set
            {
                Ensure.IsNotNullOrEmpty(value, "ValidateAttribute.RuleSet");
                this.ruleSet = value.ToUpperInvariant();
            }
        }

        /// <summary>
        /// Gets or sets the message id.
        /// </summary>
        /// <value>
        /// The message id.
        /// </value>
        /// <remarks>
        /// In the case of an application with a user interface this property would often be displayed to the user.
        /// So it should contain proper grammar and punctuation.
        /// </remarks>
        public Guid? MessageId { get; set; }

        /// <summary>
        /// Gets or sets the JavaScript ID (Optional).
        /// </summary>
        /// <value>The JavaScript ID.</value>
        public string ClientSideId { get; set; }

        /// <summary>
        /// Gets or sets the ordinal for sorting (Optional).
        /// </summary>
        /// <value>The ordinal.</value>
        public byte DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the validation order.
        /// </summary>
        /// <value>
        /// The validation order.
        /// </value>
        public byte PropertyValidationOrder { get; set; }

        /// <summary>
        /// Gets or sets the form validation order.
        /// </summary>
        /// <value>
        /// The form validation order.
        /// </value>
        public byte FormValidationOrder { get; set; }

        /// <summary>
        /// Gets or sets the message params.
        /// </summary>
        /// <value>
        /// The message params.
        /// </value>
        protected IEnumerable<object> MessageParams { get; set; }

        /// <summary>
        /// Validate input
        /// </summary>
        /// <param name="args">The validation args.</param>
        /// <returns>
        /// true if input passed the validation; otherwise false.
        /// </returns>
        public abstract bool Validate(ValidationArgs args);

        /// <summary>
        /// Sets the default error message.
        /// </summary>
        /// <param name="args">The event args.</param>
        /// <returns>
        /// The validation error message
        /// </returns>
        public virtual string GetValidationErrorMessage(ValidationArgs args)
        {
#if DEBUG
            Ensure.IsTrue(this.MessageId.HasValue, "The item id '{0}' is missing!");
#endif

            if (this.MessageId.HasValue)
            {
                string message = VFormMessageCacheManager.TryResolveErrorMessageById(this, args);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    if (this.MessageParams != null)
                    {
                        return string.Format(message, this.MessageParams.ToArray());
                    }

                    return message;
                }
            }

            return args.PropertyName;
        }
    }
}