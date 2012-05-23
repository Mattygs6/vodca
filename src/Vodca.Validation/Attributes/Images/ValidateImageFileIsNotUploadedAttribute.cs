//-----------------------------------------------------------------------------
// <copyright file="ValidateImageFileIsNotUploadedAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("AAA688D9-F8FB-4AC8-9398-DEC5E6561756", "The image file isn't uploaded!")]

namespace Vodca.VForms
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web;

    /// <summary>
    ///     The form field validator
    /// </summary>
    /// <example>View code: <br />
    /// <code lang="xml" title="Example">
    /// <![CDATA[
    /// /* clientsideid is equal 'photo' */
    /// <input type="file" title="Select photo, please" class="required error" name="photo" id="photo">
    /// ]]>
    /// </code>
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateImageFileIsNotUploadedAttribute.cs" title="ValidateImageFileIsNotUploadedAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true), GuidAttribute("AAA688D9-F8FB-4AC8-9398-DEC5E6561756")]
    public sealed class ValidateImageFileIsNotUploadedAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateImageFileIsNotUploadedAttribute"/> class.
        /// </summary>
        /// <param name="clientsideid">The input type of the file client side id.</param>
        /// <param name="itemid">The error message ID for the user</param>
        public ValidateImageFileIsNotUploadedAttribute(string clientsideid, Guid itemid)
            : base(itemid)
        {
            Ensure.IsNotNullOrEmpty(clientsideid, "clientsideid");
            this.InputClientSideId = clientsideid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateImageFileIsNotUploadedAttribute"/> class.
        /// </summary>
        /// <param name="clientsideid">The client side id.</param>
        /// <param name="itemid">The item id.</param>
        public ValidateImageFileIsNotUploadedAttribute(string clientsideid, string itemid)
            : this(clientsideid, Guid.Parse(itemid))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateImageFileIsNotUploadedAttribute"/> class.
        /// </summary>
        /// <param name="clientsideid">The client side id.</param>
        public ValidateImageFileIsNotUploadedAttribute(string clientsideid)
            : this(clientsideid, typeof(ValidateEmailIsRequiredAttribute).GUID)
        {
        }

        /// <summary>
        /// Gets the client side id.
        /// </summary>
        public string InputClientSideId { get; private set; }

        /// <summary>
        ///     Validate input
        /// </summary>
        /// <param name="args">The validation args.</param>
        /// <returns>
        ///     true if input passed the validation; otherwise false.
        /// </returns>
        public override bool Validate(ValidationArgs args)
        {
            HttpPostedFile photoImage = HttpContext.Current.Request.Files[this.InputClientSideId];

            return photoImage != null;
        }
    }
}
