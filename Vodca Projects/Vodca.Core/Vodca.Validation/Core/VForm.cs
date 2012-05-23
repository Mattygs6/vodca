//-----------------------------------------------------------------------------
// <copyright file="VForm.cs" company="genuine">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Web.Script.Serialization;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Vodca.SDK.Newtonsoft.Json;

    /// <summary>
    /// The abstract class for every Data Entity class who need validation
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\System.Web.Validators\VForm.cs" title="VForm.cs" lang="C#" />
    /// </example>
    [DataContract]
    [Serializable, GuidAttribute("06D84278-406F-47CF-BC08-686747CCD7DB")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1401:FieldsMustBePrivate", Justification = "Form design")]
    public abstract partial class VForm : IValidate
    {
        /// <summary>
        /// The collection holder
        /// </summary>
        [NonSerialized]
        private readonly VNameValueCollection collection;

        /// <summary>
        ///     The validation flag
        /// </summary>
        [NonSerialized]
        [XmlIgnore]
        private bool? isvalid;

        /// <summary>
        ///     Holds an error message list
        /// </summary>
        [NonSerialized]
        [XmlIgnore]
        private HashSet<IValidationError> errorMessages;

        /// <summary>
        /// Initializes static members of the <see cref="VForm"/> class.
        /// </summary>
        static VForm()
        {
            /* 
             * http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.expect100continue.aspx 
             */
            System.Net.ServicePointManager.Expect100Continue = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VForm"/> class.
        /// </summary>
        protected VForm()
        {
            this.collection = new VNameValueCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VForm"/> class.
        /// </summary>
        /// <param name="json">The JSON string.</param>
        protected VForm(string json)
            : this()
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                var keynamevalues = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                foreach (var pair in keynamevalues)
                {
                    this.collection.Add(pair.Key, pair.Value);
                }
            }
        }

        /* ReSharper disable MemberCanBeProtected.Global */

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <value>
        /// The collection.
        /// </value>
        [ScriptIgnore]
        [XmlIgnore]
        [JsonIgnore]
        public VNameValueCollection Collection
        {
            get { return this.collection; }
        }

        /// <summary>
        /// Gets or sets the context language.
        /// </summary>
        /// <value>
        /// The context language.
        /// </value>
        [ScriptIgnore]
        [XmlIgnore]
        [JsonIgnore]
        public string ContextLanguage { get; set; }

        /* ReSharper restore MemberCanBeProtected.Global */

        /// <summary>
        /// Gets the error messages.
        /// </summary>
        /// <value>The error messages.</value>
        [XmlIgnore]
        [ScriptIgnore]
        [JsonIgnore]
        protected HashSet<IValidationError> ErrorMessages
        {
            get { return this.errorMessages ?? (this.errorMessages = new HashSet<IValidationError>()); }
        }

        /// <summary>
        /// Gets the specified JSON.
        /// </summary>
        /// <typeparam name="TXForm">The type of the XForm.</typeparam>
        /// <param name="json">The JSON string.</param>
        /// <returns>
        /// The instance of specific profile
        /// </returns>
        public static TXForm Parse<TXForm>(string json) where TXForm : VForm, new()
        {
            var form = new TXForm();
            var keynamevalues = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (var pair in keynamevalues)
            {
                form.collection.Add(pair.Key, pair.Value);
            }

            return form;
        }

        /// <summary>
        /// Gets an error message list
        /// </summary>
        /// <returns>An error message list</returns>
        public IEnumerable<IValidationError> GetErrorList()
        {
            return this.ErrorMessages;
        }

        /// <summary>
        /// The error list as JSON.
        /// </summary>
        /// <returns>The JSON string.</returns>
        public string GetErrorListAsJson()
        {
            return this.ErrorMessages.SerializeToJson();
        }

        /// <summary>
        /// Errors list as XElements.
        /// </summary>
        /// <returns>The XElement structure</returns>
        public XElement GetErrorAsXElements()
        {
            var root = new XElement(this.GetType().Name);
            foreach (IValidationError error in this.ErrorMessages)
            {
                root.Add(error.ToXElement());
            }

            return root;
        }

        /// <summary>
        ///     Instructs the validation controls in the specified validation group to validate
        /// their assigned information.    
        /// </summary>
        /// <returns>True if valid otherwise false</returns>
        public virtual bool Validate()
        {
            return this.RunValidators(null);
        }

        // ReSharper disable MemberCanBeProtected.Global

        /// <summary>
        ///     Instructs the validation controls in the specified validation group to validate
        /// their assigned information.    
        /// </summary>
        /// <param name="validationgroup">The validation group name of the controls to validate.</param>
        /// <returns>True if valid otherwise false</returns>
        public virtual bool Validate(string validationgroup)
        {
            return this.RunValidators(validationgroup);
        }

        // ReSharper restore MemberCanBeProtected.Global

        /// <summary>
        ///     Add the error message to the list
        /// </summary>
        /// <param name="message">The error message</param>
        public void AddErrorMessage(IValidationError message)
        {
            Ensure.IsNotNull(message, "XForm.AddErrorMessage-message");

            this.ErrorMessages.Add(message);
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <returns>The XForm key value collection</returns>
        public IEnumerable<VKeyValue> GetFormData()
        {
            if (this.collection != null)
            {
                return this.collection.ToList();
            }

            return new VKeyValue[] { };
        }

        /// <summary>
        /// Adds to collection.
        /// </summary>
        /// <param name="key">The collection key.</param>
        /// <param name="value">The collection value.</param>
        public void AddToCollection(string key, object value)
        {
            this.collection[key] = string.Concat(value);
        }

        /// <summary>
        /// Gets the validation error instance.
        /// </summary>
        /// <returns>The validation error instance</returns>
        /// <remarks>This method could be overridden in child class. 
        /// This is inbuilt functionality for the extreme case to allow use custom logic and object instead of ValidationError</remarks>
        protected virtual IValidationError GetValidationErrorInstance()
        {
            return new ValidationError();
        }
    }
}
