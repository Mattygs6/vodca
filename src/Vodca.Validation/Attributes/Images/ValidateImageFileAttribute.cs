//-----------------------------------------------------------------------------
// <copyright file="ValidateImageFileAttribute.cs" company="genuine">
//     Copyright (c) J.Baltikauskas. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
//  Author:     J.Baltikauskas
//  Date:       05/01/2011
//-----------------------------------------------------------------------------
[assembly: Vodca.VForms.VRegisterValidationMessage("66E654DD-1FA4-481F-A36C-3217CAD5629A", "Image width is above max allowed")]
[assembly: Vodca.VForms.VRegisterValidationMessage("A64FFE7B-597E-4427-958D-5C42C669C17E", "Image height is above max allowed")]
[assembly: Vodca.VForms.VRegisterValidationMessage("A2D207F3-386A-49C2-8C28-BE5B7168CE11", "Image size exceeded max allowed")]
[assembly: Vodca.VForms.VRegisterValidationMessage("d2a06fa8-c5ea-494d-8088-cd77694e55bd", "Image min width did not met requirements")]
[assembly: Vodca.VForms.VRegisterValidationMessage("FE7150BE-B134-4606-BDD9-2842525705EC", "Image min height did not met requirements")]

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
    /// <code source="..\Vodca.Core\Vodca.Validation\ValidateImageFileAttribute.cs" title="ValidateImageFileAttribute.cs" lang="C#" />
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true), GuidAttribute("9779ED5C-323B-4287-9983-BB5E8E56328D")]
    public sealed class ValidateImageFileAttribute : ValidateAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateImageFileAttribute"/> class.
        /// </summary>
        /// <param name="clientsideid">The input type of the file client side id.</param>
        /// <param name="itemid">The error message ID for the user</param>
        /// <param name="maxwidth">The max width in pixels.</param>
        /// <param name="maxheight">The max height in pixels.</param>
        /// <param name="maxsize">The max size bytes.</param>
        /// <param name="minwidth">The min width in pixels.</param>
        /// <param name="minheight">The min height in pixels.</param>
        public ValidateImageFileAttribute(string clientsideid, Guid itemid, int maxwidth = 0, int maxheight = 0, int maxsize = 0, int minwidth = 0, int minheight = 0)
            : base(itemid)
        {
            Ensure.IsNotNullOrEmpty(clientsideid, "clientsideid");
            this.InputClientSideId = clientsideid;

            this.MaxWidth = maxwidth;
            this.MaxHeight = maxheight;
            this.MaxSize = maxsize;
            this.MinWidth = minwidth;
            this.MinHeight = minheight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateImageFileAttribute"/> class.
        /// </summary>
        /// <param name="clientsideid">The client side id.</param>
        /// <param name="itemid">The item id.</param>
        /// <param name="maxwidth">The max width in pixels.</param>
        /// <param name="maxheight">The max height in pixels.</param>
        /// <param name="maxsize">The max size bytes.</param>
        /// <param name="minwidth">The min width in pixels.</param>
        /// <param name="minheight">The min height in pixels.</param>
        public ValidateImageFileAttribute(string clientsideid, string itemid, int maxwidth = 0, int maxheight = 0, int maxsize = 0, int minwidth = 0, int minheight = 0)
            : this(clientsideid, Guid.Parse(itemid), maxwidth, maxheight, maxsize, minwidth, minheight)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateImageFileAttribute"/> class.
        /// </summary>
        /// <param name="clientsideid">The client side id.</param>
        /// <param name="maxwidth">The max width in pixels.</param>
        /// <param name="maxheight">The max height in pixels.</param>
        /// <param name="maxsize">The max size bytes.</param>
        /// <param name="minwidth">The min width in pixels.</param>
        /// <param name="minheight">The min height in pixels.</param>
        public ValidateImageFileAttribute(string clientsideid, int maxwidth = 0, int maxheight = 0, int maxsize = 0, int minwidth = 0, int minheight = 0)
            : this(clientsideid, typeof(ValidateEmailIsRequiredAttribute).GUID, maxwidth, maxheight, maxsize, minwidth, minheight)
        {
        }

        /// <summary>
        /// Gets the client side id.
        /// </summary>
        public string InputClientSideId { get; private set; }

        // ReSharper disable UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Gets or sets a value indicating whether image <see cref="ValidateImageFileAttribute"/> is optional.
        /// </summary>
        /// <value>
        ///     <c>true</c> if optional; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// If flag sets for the file uploaded logic only. In another words if image is not uploaded and  Optional = false, the validation result is true;
        /// </remarks>
        public bool Optional { get; set; }

        /// <summary>
        /// Gets or sets the message image max size GUID.
        /// </summary>
        /// <value>
        /// The message image max size GUID.
        /// </value>
        public string MessageImageMaxSizeGuid { get; set; }

        /// <summary>
        /// Gets or sets the message image max height GUID.
        /// </summary>
        /// <value>
        /// The message image max height GUID.
        /// </value>
        public string MessageImageMaxHeightGuid { get; set; }

        /// <summary>
        /// Gets or sets the message image max width GUID.
        /// </summary>
        /// <value>
        /// The message image max width GUID.
        /// </value>
        public string MessageImageMaxWidthGuid { get; set; }

        /// <summary>
        /// Gets or sets the message image min height GUID.
        /// </summary>
        /// <value>
        /// The message image min height GUID.
        /// </value>
        public string MessageImageMinHeightGuid { get; set; }

        /// <summary>
        /// Gets or sets the message image min width GUID.
        /// </summary>
        /// <value>
        /// The message image min width GUID.
        /// </value>
        public string MessageImageMinWidthGuid { get; set; }

        /// <summary>
        /// Gets or sets the width of the min.
        /// </summary>
        /// <value>
        /// The width of the min.
        /// </value>
        private int MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the min.
        /// </summary>
        /// <value>
        /// The height of the min.
        /// </value>
        private int MinHeight { get; set; }

        /// <summary>
        /// Gets or sets the width of the max.
        /// </summary>
        /// <value>
        /// The width of the max.
        /// </value>
        private int MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the max.
        /// </summary>
        /// <value>
        /// The height of the max.
        /// </value>
        private int MaxHeight { get; set; }

        /// <summary>
        /// Gets or sets the size of the max.
        /// </summary>
        /// <value>
        /// The size of the max.
        /// </value>
        private int MaxSize { get; set; }

        // ReSharper restore UnusedAutoPropertyAccessor.Global

        /// <summary>
        ///     Validate input
        /// </summary>
        /// <param name="args">The validation args.</param>
        /// <returns>
        ///     true if input passed the validation; otherwise false.
        /// </returns>
        public override bool Validate(ValidationArgs args)
        {
            HttpPostedFile postedfile = HttpContext.Current.Request.Files[this.InputClientSideId];

            if (this.Optional && postedfile == null)
            {
                return true;
            }

            Ensure.IsNotNull(postedfile, "The file not found and validation can't be executed!");

            if (postedfile.ContentType == VContentTypes.Jpeg || postedfile.ContentType == VContentTypes.Jpg || postedfile.ContentType == VContentTypes.Gif || postedfile.ContentType == VContentTypes.Png)
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(postedfile.InputStream))
                {
                    if (this.MaxWidth > 0 && this.MaxWidth < image.Width)
                    {
                        this.MessageId = Guid.Parse(!string.IsNullOrWhiteSpace(this.MessageImageMaxWidthGuid) ? this.MessageImageMaxWidthGuid : "66E654DD-1FA4-481F-A36C-3217CAD5629A");

                        return false;
                    }

                    if (this.MaxHeight > 0 && this.MaxHeight < image.Height)
                    {
                        this.MessageId = Guid.Parse(!string.IsNullOrWhiteSpace(this.MessageImageMaxHeightGuid) ? this.MessageImageMaxHeightGuid : "A64FFE7B-597E-4427-958D-5C42C669C17E");

                        return false;
                    }

                    if (this.MaxSize > 0 && this.MaxSize < postedfile.ContentLength)
                    {
                        this.MessageId = Guid.Parse(!string.IsNullOrWhiteSpace(this.MessageImageMaxSizeGuid) ? this.MessageImageMaxSizeGuid : "A2D207F3-386A-49C2-8C28-BE5B7168CE11");

                        return false;
                    }

                    if (this.MinWidth > 0 && this.MinWidth > image.Width)
                    {
                        this.MessageId = Guid.Parse(!string.IsNullOrWhiteSpace(this.MessageImageMinWidthGuid) ? this.MessageImageMinWidthGuid : "A06FA8-C5EA-494D-8088-CD77694E55BD");

                        return false;
                    }

                    if (this.MinHeight > 0 && this.MinHeight > image.Height)
                    {
                        this.MessageId = Guid.Parse(!string.IsNullOrWhiteSpace(this.MessageImageMinHeightGuid) ? this.MessageImageMinHeightGuid : "FE7150BE-B134-4606-BDD9-2842525705EC");

                        return false;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
