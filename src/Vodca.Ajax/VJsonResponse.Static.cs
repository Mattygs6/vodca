//-----------------------------------------------------------------------------
// <copyright file="VJsonResponse.Static.cs" company="GenuineInteractive">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       10/10/2011
//-----------------------------------------------------------------------------
namespace Vodca
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Vodca.VForms;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public partial class VJsonResponse
    {
        /// <summary>
        /// Successes the specified data.
        /// </summary>
        /// <returns>
        /// The new VJsonResponse Success instance
        /// </returns>
        public static VJsonResponse Success()
        {
            return new VJsonResponse(taskcompleted: true, taskaborted: false, dataisvalid: true);
        }

        /// <summary>
        /// Successes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>The new VJsonResponse Success instance</returns>
        public static VJsonResponse Success(object data)
        {
            return new VJsonResponse(taskcompleted: true, taskaborted: false, dataisvalid: true) { Data = data };
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>The new VJson Response Failed instance</returns>
        public static VJsonResponse Fail(System.Exception ex)
        {
            ex = ex ?? new System.Exception("The task has failed and was aborted. No Exception was thrown");
            return new VJsonResponse(ex);
        }

        /// <summary>
        /// Fails the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The new VJson Response Failed instance</returns>
        public static VJsonResponse Fail(string message = "The task has failed and was aborted. No Exception was thrown!")
        {
            var response = new VJsonResponse(taskaborted: true).AddTaskValidationErrorListProperty(message);

            return response;
        }

        /// <summary>
        /// Datas the is invalid.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <returns>
        /// The new VJson Response Failed instance
        /// </returns>
        public static VJsonResponse DataIsInvalid(IEnumerable<IValidationError> errors)
        {
            var js = new VJsonResponse(taskcompleted: true, taskaborted: false, dataisvalid: false);
            js.AddTaskValidationErrorListProperty(errors);

            return js;
        }
    }
}