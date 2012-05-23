//-----------------------------------------------------------------------------
// <copyright file="VJsonResponse.cs" company="GenuineInteractive">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/10/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Runtime.InteropServices;
    using System.Web.Script.Serialization;
    using System.Xml.Serialization;
    using Vodca.SDK.Newtonsoft.Json;
    using Vodca.VForms;

    /// <summary>
    ///     The Ajax web service call JSON
    /// </summary>
    /// <example>View code: <br />
    /// <code source="..\Vodca.Core\Vodca.Ajax\VJsonResponse.cs" title="VJsonResponse.cs" lang="C#" />
    /// </example>
    [Serializable, XmlRoot("VJsonResponse"), GuidAttribute("4642C689-A5B6-4A50-88BE-9C17DD57222F")]
    public sealed partial class VJsonResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VJsonResponse"/> class.
        /// </summary>
        /// <param name="taskcompleted">if set to <c>true</c> task completed.</param>
        /// <param name="taskaborted">if set to <c>true</c> task aborted.</param>
        /// <param name="dataisvalid">if set to <c>true</c>  data is valid .</param>
        public VJsonResponse(bool taskcompleted = false, bool taskaborted = false, bool dataisvalid = false)
        {
            this.Properties = new ExpandoObject();
            this.Data = new ExpandoObject();
            this.AddTaskCompletedProperty(taskcompleted);
            this.AddTaskAbortedProperty(taskaborted);
            this.AddTaskDataIsValidProperty(dataisvalid);
            this.AddJavaScriptProperty(string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VJsonResponse"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="taskaborted">if set to <c>true</c> task aborted.</param>
        public VJsonResponse(Exception ex, bool taskaborted = true)
            : this(taskaborted: taskaborted)
        {
            // this.AddTaskAbortedProperty(taskaborted);
            if (ex != null)
            {
                this.AddTaskValidationErrorListProperty(ex.Message);
                // ReSharper disable InvocationIsSkipped
                this.AddTaskExceptionProperty(ex);
                // ReSharper restore InvocationIsSkipped
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VJsonResponse"/> class.
        /// </summary>
        /// <param name="validationerror">The validation error.</param>
        public VJsonResponse(IEnumerable<IValidationError> validationerror)
            : this(taskcompleted: true)
        {
            this.AddTaskValidationErrorListProperty(validationerror);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="VJsonResponse"/> class from being created.
        /// </summary>
        private VJsonResponse()
        {
        }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public dynamic Properties { get; set; }

        /// <summary>
        /// Gets or sets the JSON.
        /// </summary>
        /// <value>The JSON object.</value>
        public dynamic Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether task aborted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [task aborted]; otherwise, <c>false</c>.
        /// </value>
        [ScriptIgnore, JsonIgnore]
        public bool TaskAborted
        {
            get
            {
                return this.Properties.TaskAborted;
            }

            set
            {
                this.AddTaskAbortedProperty(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [task completed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [task completed]; otherwise, <c>false</c>.
        /// </value>
        [ScriptIgnore, JsonIgnore]
        public bool TaskCompleted
        {
            get
            {
                return this.Properties.TaskCompleted;
            }

            set
            {
                this.AddTaskCompletedProperty(value);
            }
        }

        /// <summary>
        /// Gets or sets the java script.
        /// </summary>
        /// <value>
        /// The java script.
        /// </value>
        [ScriptIgnore, JsonIgnore]
        public string JavaScript
        {
            get
            {
                return this.Properties.JavaScript;
            }

            set
            {
                this.AddJavaScriptProperty(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether task data is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [task data is valid]; otherwise, <c>false</c>.
        /// </value>
        [ScriptIgnore, JsonIgnore]
        public bool TaskDataIsValid
        {
            get
            {
                return this.Properties.TaskDataIsValid;
            }

            set
            {
                this.AddTaskDataIsValidProperty(value);
            }
        }

        /// <summary>
        /// Performs an implicit conversion from VJsonResponse to <see cref="System.String"/>.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(VJsonResponse response)
        {
            if (response != null)
            {
                return JsonConvert.SerializeObject(response);
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Completed the specified result.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [result].</param>
        /// <returns>The instance of the object</returns>
        public VJsonResponse AddTaskCompletedProperty(bool value)
        {
            this.Properties.TaskCompleted = value;
            return this;
        }

        /// <summary>
        /// Adds the task data is valid property.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>
        /// The instance of the object
        /// </returns>
        public VJsonResponse AddTaskDataIsValidProperty(bool value)
        {
            this.Properties.TaskDataIsValid = value;
            return this;
        }

        /// <summary>
        /// Exceptions the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        [Conditional("DEBUG")]
        public void AddTaskExceptionProperty(Exception exception)
        {
            this.Properties.TaskException = string.Concat(exception);
        }

        /// <summary>
        /// Adds the internal error.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>The instance of the object</returns>
        public VJsonResponse AddTaskAbortedProperty(bool value)
        {
            this.Properties.TaskAborted = value;
            return this;
        }

        /// <summary>
        /// Adds the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The instance of the object</returns>
        public VJsonResponse AddTaskValidationErrorListProperty(string message)
        {
            this.Properties.TaskValidationErrorsType = "ValidationError";
            this.Properties.TaskValidationErrors = new[] { new ValidationError { Message = message } };
            return this;
        }

        /// <summary>
        /// Adds the task validation error list property.
        /// </summary>
        /// <param name="validationerrors">The validation errors.</param>
        /// <returns>The instance of the object</returns>
        public VJsonResponse AddTaskValidationErrorListProperty(IEnumerable<IValidationError> validationerrors)
        {
            this.Properties.TaskValidationErrorsType = "ValidationError";
            this.Properties.TaskValidationErrors = validationerrors;
            return this;
        }

        /// <summary>
        /// Adds the task validation error list property.
        /// </summary>
        /// <param name="validationerrors">The validation errors.</param>
        /// <param name="errortype">The error type.</param>
        /// <returns>
        /// The instance of the object
        /// </returns>
        /// <remarks>
        /// Designed for third party custom error only
        /// </remarks>
        public VJsonResponse AddTaskValidationErrorListProperty(IEnumerable<object> validationerrors, string errortype)
        {
            this.Properties.TaskValidationErrorsType = errortype;
            this.Properties.TaskValidationErrors = validationerrors;
            return this;
        }

        /// <summary>
        /// Adds the java script.
        /// </summary>
        /// <param name="javascript">The java script.</param>
        /// <returns>
        /// The instance of the object
        /// </returns>
        public VJsonResponse AddJavaScriptProperty(string javascript)
        {
            this.Properties.JavaScript = javascript;

            return this;
        }
    }
}